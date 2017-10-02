using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    class CatalogoProducto
    {

        public List<Producto> lstProducto = new List<Producto>();


        public void agregarProducto(Producto p) {

            lstProducto.Add(p);

        }
        public void cargar()
        {

            XmlDocument documento = new XmlDocument();

            documento.Load("producto.xml");
            XmlNode unProducto;

            XmlNodeList auxLstProductos = documento.SelectNodes("tipos/tipo");
            for (int i = 0; i < auxLstProductos.Count; i++)
            {
                unProducto = auxLstProductos.Item(i);
                Producto auxProducto = new Producto(unProducto.SelectSingleNode("idProducto").InnerText, unProducto.SelectSingleNode("producto").InnerText, int.Parse(unProducto.SelectSingleNode("cantidad").InnerText), double.Parse(unProducto.SelectSingleNode("precioCosto").InnerText), double.Parse(unProducto.SelectSingleNode("precioVenta").InnerText));
                
                lstProducto.Add(auxProducto);
            }
        }

        public Producto traerProducto(int idProd)
        {
            Producto p = null;
            cargar();
            if (lstProducto == null)
            {
                MessageBox.Show("no esta cargada la lista");
            } else
            {
                foreach (Producto z in lstProducto)
                {
                    if (int.Parse(z.getIdProducto()) == idProd)
                    {

                        p = z;
                    }
                }
            }
            return p;
        }

        public void guardarArchivo(List<Producto> z)
        {

            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode tipoNode = doc.CreateElement("tipos");
            doc.AppendChild(tipoNode);



            foreach (Producto p in z) {

                XmlNode tipoNode2 = doc.CreateElement("tipo");
                tipoNode.AppendChild(tipoNode2);

                XmlNode idProductoNode = doc.CreateElement("idProducto");
                idProductoNode.AppendChild(doc.CreateTextNode(p.getIdProducto()));
                tipoNode2.AppendChild(idProductoNode);
               
                XmlNode productoNode = doc.CreateElement("producto");
                productoNode.AppendChild(doc.CreateTextNode(p.getProducto()));
                tipoNode2.AppendChild(productoNode);

                XmlNode cantidadNode = doc.CreateElement("cantidad");
                cantidadNode.AppendChild(doc.CreateTextNode(p.cantidad.ToString()));
                tipoNode2.AppendChild(cantidadNode);

                XmlNode precioCosto = doc.CreateElement("precioCosto");
                precioCosto.AppendChild(doc.CreateTextNode(p.getPrecioCosto().ToString()));
                tipoNode2.AppendChild(precioCosto);

                XmlNode precioVentaNode = doc.CreateElement("precioVenta");
                precioVentaNode.AppendChild(doc.CreateTextNode(p.getPrecioVenta().ToString()));
                tipoNode2.AppendChild(precioVentaNode);

               
 
            }
           

            String dir = System.IO.Directory.GetCurrentDirectory();

            System.IO.File.Delete(dir + "\\producto.xml");
            doc.Save(dir+ "\\producto.xml");

        }

       public Producto treaerXID(int id) {
            cargar();
            Producto p = null;
            foreach (Producto lst in lstProducto) {
               if(int.Parse(lst.getIdProducto()) == id)
                {

                    p = lst;
                }


            }
            return p;

        }

       

    }


    }