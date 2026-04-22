using System.Xml.Linq;
using System.Xml;
using ApiLogin.Models.DB;
using ApiLogin.Infraestructure.Security;

namespace ApiLogin.Infraestructure.Security
{
    public class RegistroXML
    {
        private static readonly string archivo = "\\\\10.1.4.44\\container\\EDI\\COARRI\\registro.xml";
        #region Servico XMLDocument
        public static ServicioXml Leer()
        {
            string ruta01 = archivo;
            ServicioXml xmlservicio = new ServicioXml
            {
                lstddeta = new List<iddetas>()
            };
            //var cdn =
            //  from fecha in XElement.Load(ruta01).Descendants("Consulta")
            //  select fecha;
            //foreach (var item in cdn)
            //{
            //    xmlservicio.registro = item.Element("fecha").Value;
            //    xmlservicio.codigo = item.Element("codigo").Value;
            //    //DateTime.Parse(item.Element("fecha").Value);
            //}
            var cdnp =
              from buq in XElement.Load(ruta01).Descendants("pendiente")
              select new iddetas
              {
                  numero = buq.Attribute("numero").Value,
                  tipo = buq.Attribute("tipo").Value
              };
            xmlservicio.lstddeta = cdnp.ToList();
            return xmlservicio;
        }

        public static string Escribir(ServicioXml servicio)
        {
            string resultado;
            try
            {
                string xmlFilePath = archivo;
                XmlDocument docxml = new XmlDocument();
                XmlDeclaration xmldecla = docxml.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = docxml.DocumentElement;
                docxml.InsertBefore(xmldecla, root);
                XmlElement nivel0 = docxml.CreateElement(string.Empty, "Registros", string.Empty);
                docxml.AppendChild(nivel0);
                //XmlElement nivel1 = docxml.CreateElement(string.Empty, "Consulta", string.Empty);
                //nivel0.AppendChild(nivel1);

                //XmlElement nivel1A = docxml.CreateElement(string.Empty, "fecha", string.Empty);
                //XmlText numero = docxml.CreateTextNode(servicio.registro);
                //nivel1A.AppendChild(numero);
                //nivel1.AppendChild(nivel1A);
                //XmlElement nivel1B = docxml.CreateElement(string.Empty, "codigo", string.Empty);
                //XmlText codigo = docxml.CreateTextNode(servicio.codigo);
                //nivel1B.AppendChild(codigo);
                //nivel1.AppendChild(nivel1B);
                XmlElement nivel2 = docxml.CreateElement(string.Empty, "iddetas", string.Empty);
                nivel0.AppendChild(nivel2);
                if (servicio.lstddeta.Count > 0)
                {
                    foreach (var buq in servicio.lstddeta)
                    {
                        XmlElement nivel2A = docxml.CreateElement(string.Empty, "pendiente", string.Empty);
                        //nivel2A.SetAttribute("nul", buq.Pendiente);
                        nivel2A.SetAttribute("tipo", "P");
                        //nivel2A.SetAttribute("numero", buq.Numero.ToString());
                        nivel2A.SetAttribute("numero", buq.numero);
                        nivel2.AppendChild(nivel2A);
                    }
                }
                else
                {
                    XmlElement nivel2A = docxml.CreateElement(string.Empty, "pendiente", string.Empty);
                    //nivel2A.SetAttribute("nul", "4.");
                    nivel2A.SetAttribute("tipo", "X");
                    //nivel2A.SetAttribute("numero", "0");
                    nivel2A.SetAttribute("numero", "0");
                    nivel2.AppendChild(nivel2A);
                }
                docxml.Save(xmlFilePath);
                resultado = "OK";
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }
        #endregion
    }
}
