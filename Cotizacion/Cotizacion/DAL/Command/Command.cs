#region Usings
using System.Collections.Generic;
using System.Data;
#endregion

#region About
// Command
//
// Pedro J Abril
// Systems Engineer
// pedrojabril@gmail.com
// 
// http://pabril.com 
// 2018-12-01
#endregion

namespace Cotizacion.DAL.Command
{
    /// <summary>
    /// Command Class
    /// </summary>
    public class Command
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Command() { }

        /// <summary>
        /// CommandType
        /// </summary>
        public CommandType Type { get; set; }

        /// <summary>
        /// Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Databases
        /// </summary>
        public string Databases { get; set; }

        /// <summary>
        /// Parameters
        /// </summary>
        public Dictionary<string, object> Parameters { get; set; }

        /// <summary>
        /// Command
        /// </summary>
        /// <param name="type"></param>
        /// <param name="text"></param>
        /// <param name="database"></param>
        /// <param name="parameters"></param>
        public Command(CommandType type, string text, string database, Dictionary<string, object> parameters)
        {
            Type = type;
            Text = text;
            Databases = database;
            Parameters = parameters;
        }

        /// <summary>
        /// Command
        /// </summary>
        /// <param name="type"></param>
        /// <param name="text"></param>
        /// <param name="database"></param>
        public Command(CommandType type, string text, string database)
        {
            Type = type;
            Text = text;
            Databases = database;
        }
    }
}
