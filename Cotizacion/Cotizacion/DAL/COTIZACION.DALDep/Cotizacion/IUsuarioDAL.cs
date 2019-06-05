#region Using
using Cotizacion.DAL.Command;
using Cotizacion.Models;
using System.Collections.Generic;
using System.Data;
#endregion


namespace COTIZACION.Core.DALDep.Cotizacion
{
    public interface IUsuarioDAL
	{
		object Create(Usuario usuario);
		Usuario Read(int id);
		List<Usuario> Read();
		int Update(Usuario usuario);
		int Delete(int id);
		List<Usuario> PkUsuario(int id);
		int TransactionCreate(Usuario usuario, IDbConnection Connection, IDbTransaction Transaction);
		int TransactionUpdate(Usuario usuario, IDbConnection Connection, IDbTransaction Transaction);
		Command TransactionDelete(int id);
	}
}
