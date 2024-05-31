using ApiLogin.Models;
using System.Data;
using ApiLogin.Repository;

namespace ApiLogin.DAccess
{
    public class DatosSQL<T>
    {
        public static string Actualizar(Procedure pproceso)
        {
            var result = "NO";
            try
            {
                using (var _cone = ComunDB.GetSQL(pproceso.NBase))
                {
                    var proce = ComunDB.SQLCrearProcedimiento(_cone, pproceso.Procedimiento);
                    if (pproceso.Parametros != null)
                    {
                        foreach (var item in pproceso.Parametros)
                        {
                            ComunDB.SQLCrearParametro(ref proce, item.nombre, item.valor);
                        }
                    }
                    int respuesta = ComunDB.ExecuteCommand(proce);
                    if (respuesta <= 0)
                    {
                        result = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                result = "SQL Error: " + ex.ToString();
            }
            return result;
        }

        public static List<T> LstComando(Procedure pproceso, ref string msgError)
        {
            List<T> _lista = new List<T>();
            DataTable tabla = new DataTable();
            try
            {
                using (var _cone = ComunDB.GetSQL(pproceso.NBase))
                {
                    var _proce = ComunDB.SQLCrearConsulta(_cone, pproceso.Procedimiento);
                    using (var _reader = ComunDB.SQLExecuteReader(_proce))
                    {
                        tabla.Load(_reader);
                        if (tabla.Rows.Count > 0)
                        {
                            _lista = tabla.ToList<T>();
                        }
                        else
                        {
                            msgError = "Ningun resultado";
                            _lista = null;
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

        public static List<T> LstProcedimiento(Procedure pproceso, ref string msgError)
        {
            List<T> _lista = new List<T>();
            DataTable tabla = new DataTable();
            try
            {
                using (var _cone = ComunDB.GetSQL(pproceso.NBase))
                {
                    var _proce = ComunDB.SQLCrearProcedimiento(_cone, pproceso.Procedimiento);
                    if (pproceso.Parametros != null)
                    {
                        foreach (var item in pproceso.Parametros)
                        {
                            ComunDB.SQLCrearParametro(ref _proce, item.nombre, item.valor);
                        }
                    }
                    using (var _reader = ComunDB.SQLExecuteReader(_proce))
                    {
                        tabla.Load(_reader);
                        if (tabla.Rows.Count > 0)
                        {
                            _lista = tabla.ToList<T>();
                        }
                        else
                        {
                            msgError = "Ningun resultado";
                            _lista = null;
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

        public static T UnProcedimiento(Procedure pproceso, ref string msgError)
        {
            Type temp = typeof(T);
            T _lista = Activator.CreateInstance<T>();
            DataTable tabla = new DataTable();
            try
            {
                using (var _cone = ComunDB.GetSQL(pproceso.NBase))
                {
                    var _proce = ComunDB.SQLCrearProcedimiento(_cone, pproceso.Procedimiento);
                    if (pproceso.Parametros != null)
                    {
                        foreach (var item in pproceso.Parametros)
                        {
                            string lvalor = item.valor == "DateTime.Now" ? DateTime.Now.ToString("dd-MM-yyyy") : item.valor;
                            ComunDB.SQLCrearParametro(ref _proce, item.nombre, item.valor);
                        }
                    }
                    using (var _reader = ComunDB.SQLExecuteReader(_proce))
                    {
                        tabla.Load(_reader);
                        if (tabla.Rows.Count > 0)
                        {
                            _lista = AClase.GetItem<T>(tabla.Rows[0]);
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

        public static dynamic Convertir(string tipo, string svalor)
        {
            //Type t;
            if (svalor == "DateTime.Now")
            {
                return DateTime.Now.ToString("dd-MM-yyyy");
            }
            dynamic obe;
            string tipoDato = tipo.ToString().ToLower();
            if (tipoDato.Contains("int16"))
            {
                obe = Activator.CreateInstance(typeof(short));
                obe.GetType().GetProperty(tipoDato).SetValue(obe, Convert.ToInt16(svalor));
            }
            else if (tipoDato.Contains("int32"))
            {
                obe = Activator.CreateInstance(typeof(int));
                obe.GetType().GetProperty(tipoDato).SetValue(obe, Convert.ToInt32(svalor));
            }
            else if (tipoDato.Contains("decimal"))
            {
                obe = Activator.CreateInstance(typeof(decimal));
                obe.GetType().GetProperty(tipoDato).SetValue(obe, Convert.ToDecimal(svalor));
            }
            else if (tipoDato.Contains("datetime"))
            {
                obe = Activator.CreateInstance(typeof(DateTime));
                obe.GetType().GetProperty(tipo).SetValue(obe, Convert.ToDateTime(svalor), null);
            }
            else if (tipoDato.Contains("bool") || tipoDato.Contains("boolean"))
            {
                obe = Activator.CreateInstance(typeof(bool));
                obe.GetType().GetProperty(tipoDato).SetValue(obe, Convert.ToBoolean(svalor));
            }
            else if (tipoDato.Contains("double"))
            {
                obe = Activator.CreateInstance(typeof(double));
                obe.GetType().GetProperty(tipoDato).SetValue(obe, Convert.ToDouble(svalor));
            }
            else if (tipoDato.Contains("char"))
            {
                obe = Activator.CreateInstance(typeof(char));
                obe.GetType().GetProperty(tipoDato).SetValue(obe, Convert.ToChar(svalor));
            }
            else if (tipoDato.Contains("byte"))
            {
                obe = Activator.CreateInstance(typeof(byte));
                obe.GetType().GetProperty(tipoDato).SetValue(obe, Convert.ToByte(svalor));
            }

            else if (tipoDato.Contains("string"))
            {
                obe = Type.GetType("string");
                obe.GetType().GetProperty(tipoDato).SetValue(obe, svalor);
            }
            else
            {
                obe = Activator.CreateInstance(typeof(Type));
                obe.GetType().GetProperty(tipoDato).SetValue(obe, svalor);
            }
            return obe;
        }

        //Otra forma de consumirlo
        //private static T GetItem(DataRow dr)
        //{
        //    Type temp = typeof(T);
        //    T obj = Activator.CreateInstance<T>();
        //    foreach (DataColumn column in dr.Table.Columns)
        //    {
        //        foreach (PropertyInfo pro in temp.GetProperties())
        //        {
        //            if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value)
        //            {
        //                pro.SetValue(obj, dr[column.ColumnName], null);
        //            }
        //            else
        //                continue;
        //        }
        //    }
        //    return obj;
        //}
    }

}
