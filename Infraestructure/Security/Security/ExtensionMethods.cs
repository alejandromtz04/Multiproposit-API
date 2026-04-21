using ApiLogin.Models.DB;
using System.Data;
using System.Reflection;
using System.Text;

namespace ApiLogin.Infraestructure.Security.Security2
{
    public static class ExtensionMethods
    {
        public static List<T> ToList<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value)
                    {
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }

        public static string Sys(int id)
        {
            StringBuilder resultado = new StringBuilder();
            if (id == 2015)
            {
                DateTime dateTime = DateTime.Now;
                DateTime date = dateTime.Date;
                DateTime inicio = new DateTime(date.Year, 1, 1).Date;
                TimeSpan span = date - inicio;
                string lcBase36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                double msegundos = dateTime.TimeOfDay.TotalSeconds * 1000;
                int dias = span.Days + 1 + date.Year % 100 * 367;
                for (int lcounter = 1; lcounter <= 6; lcounter++)
                {
                    double pos = msegundos % 36;
                    resultado.Insert(0, lcBase36.Substring((int)pos, 1));
                    msegundos /= 36;
                }
                for (int lcounter = 1; lcounter <= 3; lcounter++)
                {
                    int pos = dias % 36;
                    resultado.Insert(0, lcBase36.Substring(pos, 1));
                    dias /= 36;
                }
                resultado.Insert(0, "_");
            }
            return resultado.ToString();
        }
    }

    public class AClase
    {
        public static T Datarow<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value)
                    {
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }

        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value)
                    {
                        if (pro.Name == "Foto" && dr[column.ColumnName] != DBNull.Value)
                        {
                            pro.SetValue(obj, (byte[])dr[column.ColumnName], null);
                        }
                        else
                        {
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        }

                    }
                    else
                        continue;
                }
            }
            return obj;
        }
    }

    public static class DeClase<T>
    {
        public static List<Parametros> AParametros(T clase)
        {
            List<Parametros> obj = new List<Parametros>();
            foreach (var prop in clase.GetType().GetProperties())
            {
                Parametros param = new Parametros();
                param.nombre = prop.Name;
                if (prop.GetValue(clase, null).ToString() == null)
                {
                    param.valor = "";
                }
                else
                {
                    if (prop.PropertyType.ToString() == "System.Nullable`1[System.DateTime]")
                    {
                        param.valor = prop.GetValue(clase, null).ToString();
                        DateTime date;
                        string fecloc;
                        if (DateTime.TryParse(param.valor, out date))
                        {
                            fecloc = date.ToString("dd/MM/yyyy HH:mm");
                            param.valor = fecloc;
                        }
                    }
                    else
                    {
                        param.valor = prop.GetValue(clase, null).ToString();
                    }
                }
                obj.Add(param);
            }
            return obj;
        }
    }

}
