#region Using
using Cotizacion.DAL.Command;
using Cotizacion.Models;
using COTIZACION.Core.BLDep.Cotizacion;
using COTIZACION.Core.DALDep.Cotizacion;
using System.Collections.Generic;
using System.Data;
#endregion

namespace COTIZACION.Core.BL.Cotizacion
{
    public class UsuarioBL : IUsuarioBL
	{

		#region Properties
		private IUsuarioDAL usuarioDAL;
		#endregion

		#region Constructors
		public UsuarioBL(IUsuarioDAL UsuarioDAL)
		{
			usuarioDAL = UsuarioDAL;
		}
		#endregion

		#region CRUD
		public object Create(Usuario usuario)
		{
			return usuarioDAL.Create(usuario);
		}

		public Usuario Read(int id)
		{
			return usuarioDAL.Read(id);
		}

		public List<Usuario> Read()
		{
			return usuarioDAL.Read();
		}

		public int Update(Usuario usuario)
		{
			return usuarioDAL.Update(usuario);
		}

		public int Delete(int id)
		{
			return usuarioDAL.Delete(id);
		}
		#endregion

		#region Indexes
		public List<Usuario> PkUsuario(int id)
		{
			return usuarioDAL.PkUsuario(id);
		}
		#endregion

		public object Save(Usuario usuario)
		{
			return (usuario.id == 0) ? Create(usuario) : Update(usuario);
		}

		#region Transactions

		public int TransactionSave(Usuario usuario, IDbConnection Connection, IDbTransaction Transaction)
		{
			return TransactionUpdate(usuario, Connection, Transaction);
		}

		public int TransactionCreate(Usuario usuario, IDbConnection Connection, IDbTransaction Transaction)
		{
			return usuarioDAL.TransactionCreate(usuario, Connection, Transaction);
		}

		public int TransactionUpdate(Usuario usuario, IDbConnection Connection, IDbTransaction Transaction)
		{
			return usuarioDAL.TransactionUpdate(usuario, Connection, Transaction);
		}

		public Command TransactionDelete(int id)
		{
			return usuarioDAL.TransactionDelete(id);
		}
		#endregion

	}
}
