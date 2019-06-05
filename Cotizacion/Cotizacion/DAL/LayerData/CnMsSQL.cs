#region Usings
using CORE.IGP.Commons;
using Cotizacion.DAL.Command;
using Cotizacion.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace IGP.LayerData
{
    /// <summary>
    /// CnMsSQL Class
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public class CnMsSQL<T> where T : class
    {
        #region Properties
        private IOptions<SQLConnectionStrings> Settings { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CnMsSQL(IOptions<SQLConnectionStrings> settings)
        {
            Settings = settings;
        }
        #endregion

        #region Methods
        /// <summary>
        /// CreateCommand 
        /// </summary>
        private SqlCommand CreateCommand(Command command, SqlConnection msSqlConnection)
        {
            SqlCommand cmdSql = new SqlCommand
            {
                Connection = msSqlConnection,
                CommandType = command.Type,
                CommandText = command.Text
            };
            if (command.Parameters != null)
            {
                foreach (KeyValuePair<string, object> value in command.Parameters)
                {
                    cmdSql.Parameters.AddWithValue(value.Key, value.Value);
                }
            }
            return cmdSql;
        }

        private SqlCommand CreateCommand(Command command, SqlConnection msSqlConnection, SqlTransaction sqlTransaction)
        {
            SqlCommand cmdSql = new SqlCommand
            {
                Connection = msSqlConnection,
                CommandType = command.Type,
                CommandText = command.Text,
                Transaction = sqlTransaction
            };
            if (command.Parameters != null)
            {
                foreach (KeyValuePair<string, object> value in command.Parameters)
                {
                    cmdSql.Parameters.AddWithValue(value.Key, value.Value);
                }
            }
            return cmdSql;
        }

        /// <summary>
        /// ExecuteNonQuery
        /// Ejecuta una instruccion de Transact-SQL en la conexion y devuelve el numero de filas afectadas.
        /// Ejecutar para comandos DELETE, UPDATE e INSERT.
        /// Ejecutar para SELECT cuando la cantidad de registros a obtener es igual a 1.
        /// </summary>
        /// <param name="command">Command command</param>
        /// <returns>int</returns> 
        public int ExecuteNonQuery(Command command)
        {
            int result = 0;
            using (SqlConnection msSqlConnection = new SqlConnection(command.Databases))
            {
                try
                {
                    if (msSqlConnection.State == ConnectionState.Closed)
                        msSqlConnection.Open();

                    SqlCommand cmdSql = CreateCommand(command, msSqlConnection);
                    return cmdSql.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    return result;
                }
                finally
                {
                    if (msSqlConnection != null && msSqlConnection.State == ConnectionState.Open)
                        msSqlConnection.Close();
                }
            }
        }

        /// <summary>
        /// ExecuteScalar
        /// Retorna el valor contenido en la primera fila de la primera columna.
        /// Utilizar cuando el tipo de dato a obtener no es soportado como parametro de salida, como las imagenes. 
        /// En este caso, son transformadas a arreglos de bytes antes de ser obtenidas.
        /// </summary>
        /// <param name="command">Command command</param>       
        /// <returns>object</returns>
        public object ExecuteScalar(Command command)
        {
            object result = 0;
            using (SqlConnection msSqlConnection = new SqlConnection(command.Databases))
            {
                try
                {
                    if (msSqlConnection.State == ConnectionState.Closed)
                        msSqlConnection.Open();

                    SqlCommand cmdSql = CreateCommand(command, msSqlConnection);
                    return cmdSql.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    return null;
                }
                finally
                {
                    if (msSqlConnection != null && msSqlConnection.State == ConnectionState.Open)
                        msSqlConnection.Close();
                }
            }
        }

        /// <summary>
        /// ExecuteReader
        /// Envia la propiedad CommandText a Connection y crea un objeto SqlDataReader.
        /// Obtiene un conjunto de registros.
        /// </summary>
        /// <param name="command">Command command</param>
        /// <returns>IEnumerable<T></returns>
        public IEnumerable<T> ExecuteReader(Command command)
        {
            List<T> objects = new List<T>();
            using (SqlConnection msSqlConnection = new SqlConnection(command.Databases))
            {
                try
                {
                    if (msSqlConnection.State == ConnectionState.Closed)
                        msSqlConnection.Open();
                }
                catch (SqlException ex)
                {
                    yield break;
                }

                SqlCommand cmdSql = CreateCommand(command, msSqlConnection);
                var properties = typeof(T).GetProperties();
                using (SqlDataReader sqlDataReader = cmdSql.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (sqlDataReader.Read())
                    {
                        var element = Activator.CreateInstance<T>();
                        foreach (var column in properties)
                        {
                            try
                            {
                                object[] attrs = column.GetCustomAttributes(true);

                                bool skipRead = false;
                                foreach (object attr in attrs)
                                {
                                    if (attr is EvictAttribute evictAttr)
                                    {
                                        skipRead = true;
                                        break;
                                    }
                                }

                                if (!skipRead)
                                {
                                    var data = sqlDataReader[column.Name];
                                    if (data.GetType() != typeof(DBNull)) column.SetValue(element, data, null);
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        yield return element;
                    }
                }
            }
        }

        /// <summary>
        /// SqlCommand.DataSet
        /// Envia la propiedad CommandText a Connection y crea un objeto DataSet.
        /// </summary>
        /// <param name="command">Command command</param>
        /// <returns>SqlCommand.DataSet</returns>
        public DataSet GetDataSet(Command command)
        {
            DataSet dataset = new DataSet();

            using (SqlConnection msSqlConnection = new SqlConnection(command.Databases))
            {
                try
                {
                    if (msSqlConnection.State == ConnectionState.Closed)
                        msSqlConnection.Open();

                    SqlCommand cmdSql = CreateCommand(command, msSqlConnection);
                    SqlDataAdapter adapter = new SqlDataAdapter
                    {
                        SelectCommand = cmdSql
                    };
                    dataset = new DataSet();
                    adapter.Fill(dataset, "table");
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (msSqlConnection != null && msSqlConnection.State == ConnectionState.Open)
                        msSqlConnection.Close();
                }
                return dataset;
            }
        }


        /// <summary>
        /// ExecuteNonQueryTransactions
        /// Ejecuta una instruccion de Transact-SQL en la conexion y devuelve el numero de filas afectadas.
        /// Ejecutar para comandos DELETE, UPDATE e INSERT.
        /// Ejecutar para SELECT cuando la cantidad de registros a obtener es igual a 1.
        /// </summary>
        /// <param name="command">Command command</param>
        /// <returns>int</returns> 
        public int ExecuteNonQueryTransactions(List<Command> commands)
        {
            int result = 0;
            int commandsCount = commands.Count;

            if (commandsCount == 0) return result;

            string connectionSQL = commands.FirstOrDefault().Databases;
            SqlTransaction transaction = null;

            using (SqlConnection msSqlConnection = new SqlConnection(connectionSQL))
            {
                try
                {
                    if (msSqlConnection.State == ConnectionState.Closed) msSqlConnection.Open();

                    transaction = msSqlConnection.BeginTransaction();

                    SqlCommand[] sqlCommands = new SqlCommand[commandsCount];

                    int ixcommand = 0;
                    foreach (Command command in commands)
                    {
                        sqlCommands[ixcommand] = CreateCommand(command, msSqlConnection, transaction);
                        result = sqlCommands[ixcommand].ExecuteNonQuery();

                        if (result == 0) throw new Exception("ExecuteTransactionsNonQuery sin resultados " + sqlCommands[ixcommand].CommandText);

                        ixcommand++;
                    }
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    result = 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    result = 0;
                }
                finally
                {
                    if (msSqlConnection != null && msSqlConnection.State == ConnectionState.Open) msSqlConnection.Close();
                }
            }
            return result;
        }

        #endregion
    }
}
