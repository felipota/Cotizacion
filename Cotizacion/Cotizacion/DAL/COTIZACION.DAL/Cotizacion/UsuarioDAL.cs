#region Using
using Cotizacion.DAL.Command;
using Cotizacion.Models;
using COTIZACION.Core.DALDep.Cotizacion;
using IGP.LayerData;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
#endregion



namespace COTIZACION.Core.DAL.Cotizacion
{
    public class UsuarioDAL : IUsuarioDAL
	{
		string connection;
		private readonly CnMsSQL<Usuario> _CnMsSQL;

		public UsuarioDAL(CnMsSQL<Usuario> CnMsSQL, IOptions<SQLConnectionStrings> options)
		{
			_CnMsSQL = CnMsSQL;
			connection = options.Value.ConnectionStringAdmin;
		}
	
		public object Create(Usuario usuario)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "@_nombre", usuario.nombre },
				{ "@_apellido", usuario.apellido },
				{ "@_email", usuario.email },
				{ "@_pass", usuario.pass }
			};
			Command command = new Command
			{
				Type = CommandType.StoredProcedure,
				Text = "create_usuario",
				Databases = connection,
				Parameters = parameters
			};
			return _CnMsSQL.ExecuteScalar(command);
		}

		public Usuario Read(int id)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "@_id", id }
			};
			Command command = new Command
			{
				Type = CommandType.StoredProcedure,
				Text = "read_usuario",
				Databases = connection,
				Parameters = parameters
			};
			return _CnMsSQL.ExecuteReader(command).FirstOrDefault();
		}

		public List<Usuario> Read()
		{
			Command command = new Command
			{
				Type = CommandType.StoredProcedure,
				Text = "readall_usuario",
				Databases = connection
			};
			return _CnMsSQL.ExecuteReader(command).ToList();
		}

		public int Update(Usuario usuario)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "@_id", usuario.id },
				{ "@_nombre", usuario.nombre },
				{ "@_apellido", usuario.apellido },
				{ "@_email", usuario.email },
				{ "@_pass", usuario.pass }
			};
			Command command = new Command
			{
				Type = CommandType.StoredProcedure,
				Text = "update_usuario",
				Databases = connection,
				Parameters = parameters
			};
			return _CnMsSQL.ExecuteNonQuery(command);
		}

		public int Delete(int id)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "@_id", id }
			};
			Command command = new Command
			{
				Type = CommandType.StoredProcedure,
				Text = "delete_usuario",
				Databases = connection,
				Parameters = parameters
			};
			return _CnMsSQL.ExecuteNonQuery(command);
		}


		public List<Usuario> PkUsuario(int id)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "@_id", id }
			};
			Command command = new Command
			{
				Type = CommandType.StoredProcedure,
				Text = "PkUsuario",
				Databases = connection,
				Parameters = parameters
			};
			return _CnMsSQL.ExecuteReader(command).ToList();
		}



		public int TransactionCreate(Usuario usuario, IDbConnection Connection, IDbTransaction Transaction)
		{
			int response = 0;
			using (IDbCommand Cmd = Connection.CreateCommand())
			{
				SqlParameterCollection Parms = (SqlParameterCollection)Cmd.Parameters;
				Parms.AddWithValue("@_nombre", usuario.nombre);
				Parms.AddWithValue("@_apellido", usuario.apellido);
				Parms.AddWithValue("@_email", usuario.email);
				Parms.AddWithValue("@_pass", usuario.pass);
				
				Cmd.Transaction = Transaction;
				Cmd.CommandType = CommandType.StoredProcedure;
				Cmd.CommandText = "create_usuario";
				response = Convert.ToInt32(Cmd.ExecuteScalar());
			}
			return response;
		}

		public int TransactionUpdate(Usuario usuario, IDbConnection Connection, IDbTransaction Transaction)
		{
			int response = 0;
			using (IDbCommand Cmd = Connection.CreateCommand())
			{
				SqlParameterCollection Parms = (SqlParameterCollection)Cmd.Parameters;
				Parms.AddWithValue("@_id", usuario.id);
				Parms.AddWithValue("@_nombre", usuario.nombre);
				Parms.AddWithValue("@_apellido", usuario.apellido);
				Parms.AddWithValue("@_email", usuario.email);
				Parms.AddWithValue("@_pass", usuario.pass);
				
				Cmd.Transaction = Transaction;
				Cmd.CommandType = CommandType.StoredProcedure;
				Cmd.CommandText = "update_usuario";
				response = Convert.ToInt32(Cmd.ExecuteNonQuery());
			}
			return response;
		}

		public Command TransactionDelete(int id)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>
			{
				{ "@_id", id }
			};
			Command command = new Command
			{
				Type = CommandType.StoredProcedure,
				Text = "delete_usuario",
				Databases = connection,
				Parameters = parameters
			};
			return command;
		}


	}
}
