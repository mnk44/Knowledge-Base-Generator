using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Npgsql 4.0;

namespace TesisAnaReglas.DbConnect
{
    class ConnectionHandler
    {

        private static ConnectionHandler connectionHandler = null;
        private bool blnFound = false;

        public string UserName { get; set; }
        public string PassWord { get; set; }
        private ConnectionHandler()
        {

        }

        public static ConnectionHandler getConnectionHandler()
        {
            return connectionHandler ?? (connectionHandler = new ConnectionHandler());
        }

        private NpgsqlConnection Conex = new NpgsqlConnection();
        private NpgsqlDataReader dr = null;
        

        public bool isLoging()
        {
            return blnFound;
        }

        public  void Conectar()
        {
            Conex.ConnectionString = "server=localhost; Port=5432; Database=UserRules; User Id=postgres; Password=postgres;";
            Conex.Open();
        }

        public  void Desconectar()
        {
            //Cerramos la conexión.
            Conex.Close();
            dr.Close();
        }

        public void Log(string campos, string tabla, string condition, string password,string username)
        {
            //Declaramos una variable para almacenar la consulta.
            string Consulta = "select " + campos + " from " + "\"" + tabla + "\"" + " where  " + condition + ";";
            NpgsqlCommand cmd = new NpgsqlCommand(Consulta, Conex);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                blnFound = true;
                PassWord = password;
                UserName = username;
            }
            ConnectionHandler.getConnectionHandler().Desconectar();
          
        }
        public void AddUser( string name, string password)
        {
         
            string Consulta = "insert into \"user\" (username , password) values " + "('" + name + "'" + "," + "'"+ MD5Encrypt.getMD5Encrypt().MD5(password) + "'"+")" + ";";
            NpgsqlCommand cmd = new NpgsqlCommand(Consulta, Conex);
            dr = cmd.ExecuteReader();
           
        }
        public void UpdateUser(string name, string password)
        {

            string Consulta = "update \"user\" set  password = " + "'" + MD5Encrypt.getMD5Encrypt().MD5(password) + "'" + " where username = " + "'" + name + "';";
            NpgsqlCommand cmd = new NpgsqlCommand(Consulta, Conex);
            dr = cmd.ExecuteReader();

        }

        public bool ExistUser(string name)
        {
            
            string Consulta = "select * from \"user\" where username = " + "'" + name + "';";
            NpgsqlCommand cmd = new NpgsqlCommand(Consulta, Conex);
            dr = cmd.ExecuteReader();
            return (dr.HasRows) ? true : false;
        }
      
    }
}
