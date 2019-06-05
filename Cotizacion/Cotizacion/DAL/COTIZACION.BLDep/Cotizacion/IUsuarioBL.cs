#region Using
using Cotizacion.DAL.Command;
using Cotizacion.Models;
using System.Collections.Generic;
using System.Data;
#endregion


namespace COTIZACION.Core.BLDep.Cotizacion
{
    public interface IUsuarioBL
	{
		object Create(Usuario usuario);
		Usuario Read(int id);
		List<Usuario> Read();
		int Update(Usuario usuario);
		int Delete(int id);
		List<Usuario> PkUsuario(int id);
		object Save(Usuario usuario);
		int TransactionSave(Usuario usuario, IDbConnection Connection, IDbTransaction Transaction);
		int TransactionCreate(Usuario usuario, IDbConnection Connection, IDbTransaction Transaction);
		int TransactionUpdate(Usuario usuario, IDbConnection Connection, IDbTransaction Transaction);
		Command TransactionDelete(int id);
	}
}
