


namespace Cotizacion.Models
{
    public class Usuario
	{
		#region Constructor

		public Usuario() { }

		#endregion

		#region Private Fields

		private int _id;
		private string _nombre;
		private string _apellido;
		private string _email;
		private string _pass;

		#endregion

		#region Public Properties

		/// <summary>
		/// Id
		/// </summary>
		public int id
		{
			get { return _id; }
			set { _id = value; }
		}

		/// <summary>
		/// Nombre
		/// </summary>
		public string nombre
		{
			get { return _nombre; }
			set { _nombre = value; }
		}

		/// <summary>
		/// Apellido
		/// </summary>
		public string apellido
		{
			get { return _apellido; }
			set { _apellido = value; }
		}

		/// <summary>
		/// Email
		/// </summary>
		public string email
		{
			get { return _email; }
			set { _email = value; }
		}

		/// <summary>
		/// Pass
		/// </summary>
		public string pass
		{
			get { return _pass; }
			set { _pass = value; }
		}

		#endregion
	}
}
