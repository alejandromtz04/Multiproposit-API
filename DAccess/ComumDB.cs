using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using ApiLogin.Models;
using ApiLogin.Repository;
//using Sybase.Data.AseClient;
using AdoNetCore.AseClient;

namespace ApiLogin.DAccess
{
    public class ComunDB
    {
        //private static readonly IWebHostEnvironment _env;

        //private static string rutaServer = AppDomain.CurrentDomain.GetData("WebRootPath") as string;
        //public ComunDB(IWebHostEnvironment env)
        //{

        //    string ruta = env.WebRootPath;
        //}

        #region General
        static string m_SQL_base; //= ConfigurationManager.ConnectionStrings["SQL_CON"].ConnectionString.ToString();
        static string m_ASE_base; //= ConfigurationManager.ConnectionStrings["ASE_CON"].ConnectionString.ToString();

        //Ejecuta comandos Insert, Update y Delete, sin importar el origen
        public static int ExecuteCommand(IDbCommand pCommand)
        {
            return pCommand.ExecuteNonQuery();
        }

        //Ejecuta el comando Select independientemente del origen
        public static IDataReader SQLExecuteReader(SqlCommand pComando)
        {
            return pComando.ExecuteReader();
        }

        public static IDataReader ASEExecuteReader(AseCommand pComando)
        {
            return pComando.ExecuteReader();
        }

        public static string obtenerServidor(string lbase)
        {
            string ruta01 = MyServer.MapPath("\\XML\\Bases.xml");
            string servidor = "";
            var doc = from c in XElement.Load(ruta01).Descendants("Base")
                      where c.Element("nombre").Value == lbase.ToUpper()
                      select c;
            foreach (var el in doc)
            {
                servidor = el.Element("servidor").Value;
            }
            return servidor;
        }

        public static servidorCorreo servidor(string cadena, ref string msgError)
        {
            msgError = "OK";
            servidorCorreo lstcorreo = new servidorCorreo();
            try
            {
                var doc = from proceso in XElement.Load(MyServer.MapPath("/XML/SCorreo.xml")).Descendants("Correo")
                          where proceso.Element("Id").Value.StartsWith(cadena)
                          select new servidorCorreo
                          {
                              servidor = proceso.Element("servidor").Value,
                              puerto = int.Parse(proceso.Element("puerto").Value),
                              cuenta = proceso.Element("cuenta").Value,
                              nombre = proceso.Element("nombre").Value,
                              pausa = int.Parse(proceso.Element("pausa").Value),
                              usuario = proceso.Element("usuario").Value,
                              clave = proceso.Element("clave").Value,
                              email = proceso.Element("email").Value,
                              para = proceso.Element("para").Value
                          };
                lstcorreo = doc.FirstOrDefault();
            }
            catch (Exception ex)
            {
                msgError = ex.ToString();
            }
            return lstcorreo;
        }
        #endregion

        #region SQLServer
        public static SqlConnection GetSQL(string nbase)
        {
            SqlConnection _cont = null;
            string mensaje = "";
            m_SQL_base = obtenerServidor("SQL_CON");
            string str_con = m_SQL_base.Replace("BASE_DATOS", nbase);
            _cont = new SqlConnection(str_con);
            try
            {
                _cont.Open();
            }
            catch (Exception exp)
            {
                _cont = null;
                mensaje = exp.ToString();
            }
            return _cont;
        }

        public static void SQLCrearParametro(ref SqlCommand pComando, string pParametro, object pValue)
        {
            SqlParameter _parametro = pComando.CreateParameter();
            _parametro.ParameterName = pParametro;
            _parametro.Value = pValue;
            pComando.Parameters.Add(_parametro);
        }

        public static void SQLCrearParametroTabla(ref SqlCommand pComando, string pParametro, object pValue)
        {
            SqlParameter _parametro = pComando.CreateParameter();
            _parametro.ParameterName = pParametro;
            _parametro.Value = pValue;
            _parametro.SqlDbType = SqlDbType.Structured;
            pComando.Parameters.Add(_parametro);
        }

        public static SqlCommand SQLCrearProcedimiento(SqlConnection pConn, string _procedure)
        {
            SqlCommand _comando = pConn.CreateCommand();
            _comando.CommandText = _procedure;
            _comando.CommandType = CommandType.StoredProcedure;
            return _comando;
        }

        public static SqlCommand SQLCrearConsulta(SqlConnection pConn, string _procedure)
        {
            SqlCommand _comando = pConn.CreateCommand();
            _comando.CommandText = _procedure;
            _comando.CommandType = CommandType.Text;
            return _comando;
        }
        #endregion

        #region ASEServer
        public static AseConnection GetASE()
        {
            //Data Source=myASEserver;Port=5000;Database=myDataBase;Uid=myUsername;Pwd=myPassword;
            m_ASE_base = obtenerServidor("ASE_CON");
            AseConnection conex = new AseConnection(m_ASE_base);
            string mensaje = "";
            try
            {
                conex.Open();
            }
            catch (Exception exp)
            {
                conex = null;
                mensaje = exp.ToString();
            }
            return conex;
        }

        public static void ASECrearParametro(ref AseCommand pComando, string pParametro, object pValue)
        {
            AseParameter _parametro = pComando.CreateParameter();
            _parametro.ParameterName = pParametro;
            _parametro.Value = pValue;
            pComando.Parameters.Add(_parametro);
        }

        public static AseCommand ASECrearProcedimiento(AseConnection pConn, string _procedure)
        {
            AseCommand _comando = pConn.CreateCommand();
            _comando.CommandText = _procedure;
            _comando.CommandType = CommandType.StoredProcedure;
            return _comando;
        }

        public static AseCommand ASECrearConsulta(AseConnection pConn, string _sql)
        {
            AseCommand _comando = pConn.CreateCommand();
            _comando.CommandType = CommandType.Text;
            _comando.CommandText = _sql;
            return _comando;
        }
        #endregion

        #region VFPServer
        public static void CreateParametro(ref IDbCommand pComando, string pParametro, object pValue)
        {
            IDataParameter _parametro = pComando.CreateParameter();
            _parametro.ParameterName = pParametro;
            _parametro.Value = pValue;
            pComando.Parameters.Add(_parametro);
        }
        #endregion     
    }
}
