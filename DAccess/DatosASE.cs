using ApiLogin.Models;
using ApiLogin.Repository;
using System.Data;

namespace ApiLogin.DAccess
{
    public class DatosASE<T>
    {
        public static List<T> LstComando(Procedure pproceso, ref string msgError)
        {
            List<T> _lista = new List<T>();
            DataTable tabla = new DataTable();
            try
            {
                using (var _cone = ComunDB.GetASE())
                {
                    var _proce = ComunDB.ASECrearConsulta(_cone, pproceso.Procedimiento);
                    using (var _reader = ComunDB.ASEExecuteReader(_proce))
                    {
                        tabla.Load(_reader);
                        if (tabla.Rows.Count > 0)
                        {
                            _lista = tabla.ToList<T>();
                            msgError = "OK";
                        }
                        else
                        {
                            msgError = "Ningun resultado";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msgError = ex.ToString();
                _lista = null;
            }
            return _lista;
        }

        public static T UnConsulta(Procedure pproceso, ref string msgError)
        {
            Type temp = typeof(T);
            T _lista = Activator.CreateInstance<T>();
            DataTable tabla = new DataTable();
            try
            {
                using (var _cone = ComunDB.GetASE())
                {
                    var _proce = ComunDB.ASECrearConsulta(_cone, pproceso.Procedimiento);
                    using (var _reader = ComunDB.ASEExecuteReader(_proce))
                    {
                        tabla.Load(_reader);
                        if (tabla.Rows.Count > 0)
                        {
                            _lista = AClase.GetItem<T>(tabla.Rows[0]);
                            msgError = "OK";
                        }
                        else
                        {
                            msgError = "Ningun resultado";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msgError = ex.ToString();
            }
            return _lista;
        }
    }

    public class DatosCEPA
    {
        public static DataTable LstComando(Procedure pproceso, ref string msgError)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (var _cone = ComunDB.GetASE())
                {
                    var _proce = ComunDB.ASECrearConsulta(_cone, pproceso.Procedimiento);
                    using (var _reader = ComunDB.ASEExecuteReader(_proce))
                    {
                        tabla.Load(_reader);
                        if (tabla.Rows.Count > 0)
                        {
                            msgError = "OK";
                        }
                        else
                        {
                            tabla.Columns.Add("return_value", typeof(string));
                            tabla.Rows.Add("Ningun resultado");
                            msgError = "Ningun resultado";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msgError = ex.ToString();
                tabla.Columns.Add("return_value", typeof(string));
                tabla.Rows.Add("ERROR:" + ex.ToString());
                //tabla.Columns.Add("return_value", typeof(string));
                //tabla.Rows.Add("ERROR:" + ex.ToString());
            }
            return tabla;
        }
    }

}
