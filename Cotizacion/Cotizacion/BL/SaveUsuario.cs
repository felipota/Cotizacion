using Cotizacion.DAL.Command;
using Cotizacion.Models;
using COTIZACION.Core.BLDep.Cotizacion;
using System;
using System.Data;

namespace Cotizacion.BL
{
    public class SaveUsuario : ISaveUsuario
    {
        private readonly IUsuarioBL _IUsuarioBL;
        private readonly ITransactionCustom _ITransactionCustom;
        public SaveUsuario(IUsuarioBL usuarioBL, ITransactionCustom transactionCustom)
        {
            _IUsuarioBL = usuarioBL;
            _ITransactionCustom = transactionCustom;
        }

        public int Insert(Usuario usuario)
        {
            int result = _ITransactionCustom.MakeTranssaction<Usuario>(SaveInsert, usuario);
            return result;

        }

        private int SaveInsert(Usuario usuario, IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            int response = 0;
            try
            {
                int res = _IUsuarioBL.TransactionCreate(usuario, dbConnection, dbTransaction);

                if (res != 0)
                {
                    dbTransaction.Commit();
                    response = res;
                }
                else
                {
                    throw new Exception("Error al insertar.");
                }

            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
            }
            finally
            {
                if (dbConnection != null && dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }

                dbConnection.Dispose();
            }
            return response;
        }

        public int Update(Usuario usuario)
        {
            int result = _ITransactionCustom.MakeTranssaction<Usuario>(SaveUpdate, usuario);
            return result;
        }

        private int SaveUpdate(Usuario usuario, IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            int response = 0;
            try
            {

                int AfectedRows = _IUsuarioBL.TransactionUpdate(usuario, dbConnection, dbTransaction);
                if (AfectedRows == 0)
                {
                    throw new Exception("No se afectaron Registros.");
                }

                dbTransaction.Commit();
                response = usuario.id;

            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
            }
            finally
            {

                if (dbConnection != null && dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }

                dbConnection.Dispose();
            }

            return response;
        }
    }
}
