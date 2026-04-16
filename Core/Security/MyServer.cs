namespace ApiLogin.Core.Security
{
    public static class MyServer
    {
        public static string MapPath(string path)
        {
            string ruta = (string)AppDomain.CurrentDomain.GetData("ContentRootPath");
            string local = string.Concat(ruta, path);
            return local;
        }
    }
}
