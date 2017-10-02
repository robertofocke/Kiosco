using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    class ListaUsuario
    {
        public List<Usuario> lstUsuario = cargar();


        static List<Usuario> cargar(){
            List<Usuario> auxlstUsuario = new List<Usuario>();

            XmlDocument documento = new XmlDocument();

            documento.Load("Usuarios.xml");
            XmlNode unUsuario;

            XmlNodeList auxLstUsuarios = documento.SelectNodes("tipo/usuarios");
            for (int i = 0; i < auxLstUsuarios.Count; i++)
            {
                unUsuario = auxLstUsuarios.Item(i);
                Usuario auxUsuario = new Usuario(unUsuario.SelectSingleNode("cuit").InnerText, unUsuario.SelectSingleNode("usuario").InnerText, unUsuario.SelectSingleNode("contrasenia").InnerText);
                auxlstUsuario.Add(auxUsuario);

            }
            return auxlstUsuario;
}




public List<Usuario> getLstUsuario() {

            return lstUsuario;
        }


        public void guardar() {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);


            XmlNode tipoNode = doc.CreateElement("tipo");
            doc.AppendChild(tipoNode);


            foreach (Usuario p in this.getLstUsuario())
            {
                XmlElement usuariosNode = doc.CreateElement("usuarios");

                XmlNode cuitNode = doc.CreateElement("cuit");
                cuitNode.AppendChild(doc.CreateTextNode(p.getCuit()));
                usuariosNode.AppendChild(cuitNode);

                XmlNode usuarioNode = doc.CreateElement("usuario");
                usuarioNode.AppendChild(doc.CreateTextNode(p.getUsuario()));
                usuariosNode.AppendChild(usuarioNode);

                XmlNode contraseniaNode = doc.CreateElement("contrasenia");
                contraseniaNode.AppendChild(doc.CreateTextNode(p.getContra()));
                usuariosNode.AppendChild(contraseniaNode);
                

                tipoNode.AppendChild(usuariosNode);

            }

            String dir = System.IO.Directory.GetCurrentDirectory();

            System.IO.File.Delete(dir + "\\Usuarios.xml");
            doc.Save(dir + "\\Usuarios.xml");


        }


        public Boolean BuscxNombreYcontra(string usuario, string contra) {
            Boolean verita = false;
            

            foreach (Usuario u in getLstUsuario()) {
                if(u.getContra().CompareTo(contra)==0 && u.getUsuario().CompareTo(usuario) == 0)
                {
                    verita = true;
                }


            }



            return verita;
       }

        }
    }

    
