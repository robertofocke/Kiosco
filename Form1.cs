using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void exportarAExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Excel._Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            DataSet ds = new DataSet();
            XmlReader xmlFile;
            int i = 0;
            int j = 0;
            int z = 1;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xmlFile = XmlReader.Create("Producto.xml", new XmlReaderSettings());
            ds.ReadXml(xmlFile);
            xlWorkSheet.Cells[1, 1] = "IDENTIFICADOR";
            xlWorkSheet.Cells[1, 2] = "PRODUCTO";
            xlWorkSheet.Cells[1, 3] = "CANTIDAD";
            xlWorkSheet.Cells[1, 4] = "COSTO";
            xlWorkSheet.Cells[1, 5] = "PRECIO";

            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                {

                    xlWorkSheet.Cells[z + 1, j + 1] = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                }
                z++;
            }

            string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);



            xlWorkBook.SaveAs(mdoc +"\\producto.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlApp);
            releaseObject(xlWorkBook);
            releaseObject(xlWorkSheet);

            MessageBox.Show("Guardado en tus documentos .. ");
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }

            finally
            {
                GC.Collect();
            }
        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

            foreach (Control item in panel1.Controls)
            {
                panel1.Controls.Remove(item);

            }
            panel1.Controls.Clear();
            DataGridView myDataGridView = new DataGridView();

            myDataGridView.ColumnCount = 5;
            myDataGridView.Columns[0].Name = "IDENTIFICADOR";
            myDataGridView.Columns[1].Name = "PRODUCTO";
            myDataGridView.Columns[2].Name = "CANTIDAD";
            myDataGridView.Columns[3].Name = "COSTO";
            myDataGridView.Columns[4].Name = "PRECIO";

            CatalogoProducto catalogoProd = new CatalogoProducto();
            catalogoProd.cargar();

            for (int i = 0; i < catalogoProd.lstProducto.Count; i++)
            {
                string idProducto = catalogoProd.lstProducto[i].getIdProducto();
                string producto = catalogoProd.lstProducto[i].getProducto();

                string cantidad = catalogoProd.lstProducto[i].getCantidad().ToString();
                string costo = catalogoProd.lstProducto[i].getPrecioCosto().ToString();
                string precio = catalogoProd.lstProducto[i].getPrecioVenta().ToString();

                string[] row = new string[] { idProducto, producto, cantidad, costo, precio };
                myDataGridView.Rows.Add(row);

            }
            myDataGridView.Width = 300;
            myDataGridView.AutoSize = true;    
            panel1.Controls.Add(myDataGridView);

        }





        public void registrar(Object sender, EventArgs e, TextBox myTextBoxArt, TextBox myTextBoxCant, DataGridView myDataGridViewArt)
        {
            CatalogoProducto catalogoProd = new CatalogoProducto();
            catalogoProd.cargar();

            if (myTextBoxArt.Text == "" || myTextBoxCant.Text == "")
            {

                MessageBox.Show("ups, parece que ese producto no existe o olvidate poner la cantidad");
            }
            else
            {
                Producto p = catalogoProd.traerProducto(int.Parse(myTextBoxArt.Text));
                int cant = int.Parse(myTextBoxCant.Text);
                double precio = Convert.ToDouble(cant) * p.getPrecioCosto();

                myDataGridViewArt.Rows.Add(p.getIdProducto(), cant, precio);
            }

        }


        public void eliminar(Object sender, EventArgs e, DataGridView myDataGridViewArt, TextBox myTextBoxArt, TextBox myTextBoxCant)
        {

            if (myTextBoxArt.Text == "" || myTextBoxCant.Text == "" || myDataGridViewArt.Rows[0].Cells[0].Value==null)
            {

                MessageBox.Show("ups, parece que ese producto no existe o olvidaste poner la cantidad");
            }
            else
            {
                Int32 rowToDelete = myDataGridViewArt.Rows.GetFirstRow(
                 DataGridViewElementStates.Selected);
                if (rowToDelete == -1)
                {
                    MessageBox.Show("seleccione una fila");
                }
                else {
                    myDataGridViewArt.Rows.RemoveAt(rowToDelete);
                }
            }
        }

        public void guardar(Object sender, EventArgs e, DataGridView myDataGridViewArt)
        {
            //tengo que encontrar la forma de no repetir tanto las dos siguientes lineas de codigo
            List<Producto> aux = new List<Producto>();

            CatalogoProducto cata = new CatalogoProducto();
            cata.cargar();


            if (myDataGridViewArt.Rows[0].Cells[0].Value == null)
            {
                MessageBox.Show("No registraste producto");
            }
            else {
                Boolean veritativo = false;
                List<Producto> lista = new List<Producto>();
                //int j=0;
                for (int j = 0; j < cata.lstProducto.Count; j++)
                //foreach (Producto lst in cata.lstProducto)
                {
                    for (int i = 0; i < myDataGridViewArt.RowCount - 1; i++)
                    {
                        veritativo = false;

                        if (string.Compare((myDataGridViewArt.Rows[i].Cells[0].Value.ToString()), (cata.lstProducto[j].getIdProducto())) == 0)
                        {
                            veritativo = true;
                            cata.lstProducto[j].cantidad += (int.Parse(myDataGridViewArt.Rows[i].Cells[1].Value.ToString()));


                        }

                    }
                    if (veritativo)
                    {
                        aux.Add(cata.lstProducto[j]);


                    }
                    else
                    {
                        aux.Add(cata.lstProducto[j]);

                    }

                }

                cata.guardarArchivo(aux);
            }

            myDataGridViewArt.Rows.Clear();
        }



        private void comprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Control item in panel1.Controls)
            {
                panel1.Controls.Remove(item);
            }
            panel1.Controls.Clear();

            Label myLabelArticulos = new Label();
            myLabelArticulos.Text = "ID articulo";
            myLabelArticulos.Top = 40;
            myLabelArticulos.Left = 20;

            TextBox myTextBoxAriticulos = new TextBox();
            myTextBoxAriticulos.Left = 130;
            myTextBoxAriticulos.Top = 40;

            Label myLabelCantidad = new Label();
            myLabelCantidad.Left = 20;
            myLabelCantidad.Top = 70;
            myLabelCantidad.Text = "Cantidad";

            TextBox myTextBoxCantidad = new TextBox();
            myTextBoxCantidad.Top = 70;
            myTextBoxCantidad.Left = 130;

            Button myButtonRegitrar = new Button();
            myButtonRegitrar.Text = "Registrar";
            myButtonRegitrar.Top = 120;
            myButtonRegitrar.Left = 20;


            Button myButtonElim = new Button();
            myButtonElim.Text = "Eliminar";
            myButtonElim.Top = 120;
            myButtonElim.Left = 100;

            Button myButtonGuardad = new Button();
            myButtonGuardad.Text = "Guardar";
            myButtonGuardad.Top = 150;
            myButtonGuardad.Left = 55;
            

            DataGridView myDataGridView = new DataGridView();

            myDataGridView.ColumnCount = 3;
            myDataGridView.Columns[0].Name = "IDENTIFICADOR";
            myDataGridView.Columns[1].Name = "CANTIDAD";
            myDataGridView.Columns[2].Name = "COSTO";
            myDataGridView.Top = 20;
            myDataGridView.Left = 300;
            


            panel1.Controls.Add(myLabelArticulos);
            panel1.Controls.Add(myTextBoxAriticulos);

            panel1.Controls.Add(myLabelCantidad);
            panel1.Controls.Add(myTextBoxCantidad);

            panel1.Controls.Add(myButtonRegitrar);
            panel1.Controls.Add(myButtonElim);
            panel1.Controls.Add(myButtonGuardad);

            panel1.Controls.Add(myDataGridView);

            myButtonRegitrar.Click += delegate (Object senders, EventArgs es)
            {
                registrar(senders, es, myTextBoxAriticulos, myTextBoxCantidad, myDataGridView);
            };
            myButtonGuardad.Click += delegate (Object senders, EventArgs es)
            {
                guardar(senders, es, myDataGridView);
            };

            myButtonElim.Click += delegate (Object senders, EventArgs es)
            {
                eliminar(senders, es, myDataGridView, myTextBoxAriticulos, myTextBoxCantidad);
            };

        }

       

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void sobreKioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 sobre_mi = new Form2();
            sobre_mi.ShowDialog();
        }

        private void click_agregar(Object sender, EventArgs e, Button but_modificar, Button button_eliminar, TextBox text_codigo, TextBox text_nombre, TextBox text_nombre_proveedores, TextBox text_precio_costo, TextBox text_venta)
        {

            CatalogoProducto lstCatalago = new CatalogoProducto();
            
            if (lstCatalago.treaerXID(int.Parse(text_codigo.Text)) != null)
            {

                MessageBox.Show("el id ya existe");

            }
            else {
                but_modificar.Enabled = true;
                text_nombre.Enabled = true;
                text_precio_costo.Enabled = true;
                text_venta.Enabled = true;



            }
            

        }


        private void click_modificar(Object sender, EventArgs e, TextBox text_codigo, TextBox text_nombre, TextBox text_precio_costo, TextBox text_venta)

        {
            //CatalogoProducto lstCatalago = new CatalogoProducto();
            List<Producto> aux = new List<Producto>();

            CatalogoProducto lstCatalagoAux = new CatalogoProducto();
            lstCatalagoAux.cargar();
         
            foreach (Producto p in lstCatalagoAux.lstProducto) 
            {

                aux.Add(p);
            }
            Producto producto = new Producto(text_codigo.Text, text_nombre.Text, Double.Parse(text_precio_costo.Text), Double.Parse(text_venta.Text));

            aux.Add(producto);
            

            lstCatalagoAux.guardarArchivo(aux);
        }



        private void click_buscar(Object sender, EventArgs e, Button but_agregar, Button but_modificar, Button but_eliminar, TextBox text_codigo, TextBox text_nombre, TextBox text_nombre_proveedores, TextBox text_precio_costo, TextBox text_venta) {

            CatalogoProducto lstCatalago = new CatalogoProducto();
            Producto producto = lstCatalago.treaerXID(int.Parse(text_codigo.Text));

            if (producto == null)
            {
                MessageBox.Show("el producto no existe");
            }
            else {
                text_nombre.Enabled = true;
                text_precio_costo.Enabled = true;
                text_venta.Enabled = true;

                text_nombre.Text = producto.getProducto();
                text_precio_costo.Text = producto.getPrecioCosto().ToString();
                text_venta.Text = producto.getPrecioVenta().ToString();
            }
            but_agregar.Enabled = true;
            but_modificar.Enabled = true;
            but_eliminar.Enabled = true;

            
        }

        private void bajasYAltasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Control item in panel1.Controls)
            {
                panel1.Controls.Remove(item);

            }
            panel1.Controls.Clear();

            Label lbl_codigo = new Label();
            lbl_codigo.Text = "Codigo:";
            lbl_codigo.Top = 15;
            lbl_codigo.Left = 20;

            Label lbl_nombre = new Label();
            lbl_nombre.Text = "Nombre";
            lbl_nombre.Top = 40;
            lbl_nombre.Left = 20;

            Label lbl_precio_costo = new Label();
            lbl_precio_costo.Text = "Costo";
            lbl_precio_costo.Top = 70;
            lbl_precio_costo.Left = 20;

            Label lbl_venta = new Label();
            lbl_venta.Text = "Venta";
            lbl_venta.Top = 100;
            lbl_venta.Left = 20;

            Label lbl_nombre_proveedor = new Label();
            lbl_nombre_proveedor.Text = "Proveedor";
            lbl_nombre_proveedor.Top = 130;
            lbl_nombre_proveedor.Left = 20;


            TextBox text_codigo = new TextBox();
            text_codigo.Top = 10;
            text_codigo.Left = 159;


            TextBox text_nombre = new TextBox();
            text_nombre.Top = 40;
            text_nombre.Left = 159;
            text_nombre.Enabled = false;

            TextBox text_precio_costo = new TextBox();
            text_precio_costo.Top = 70;
            text_precio_costo.Left = 159;
            text_precio_costo.Enabled = false;

            TextBox text_venta = new TextBox();
            text_venta.Top = 100;
            text_venta.Left = 159;
            text_venta.Enabled = false;

            TextBox text_nombre_proveedores = new TextBox();
            text_nombre_proveedores.Top = 130;
            text_nombre_proveedores.Left = 159;
            text_nombre_proveedores.Enabled = false;

            Button button_modificar = new Button();
            button_modificar.Text = "Modificar";
            button_modificar.Top = 160;
            button_modificar.Left = 20;
            button_modificar.Enabled = false;
            button_modificar.Click += delegate (Object senders, EventArgs es) { click_modificar(senders, es, text_codigo, text_nombre, text_precio_costo,text_venta); };


            Button button_eliminar = new Button();
            button_eliminar.Text = "Eliminar";
            button_eliminar.Top = 160;
            button_eliminar.Left =100;
            button_eliminar.Enabled = false;

            Button button_agregar = new Button();
            button_agregar.Text = "Agregar";
            button_agregar.Top = 200;
            button_agregar.Left =100;
            button_agregar.Click += delegate (Object senders, EventArgs es) { click_agregar(senders, es, button_modificar, button_eliminar, text_codigo, text_nombre, text_nombre_proveedores, text_precio_costo, text_venta); };

            Button button_buscar = new Button();
            button_buscar.Text = "Buscar";
            button_buscar.Top = 200;
            button_buscar.Left = 20;
            button_buscar.Click += delegate (Object senders, EventArgs es) { click_buscar(senders, es, button_agregar,button_modificar,button_eliminar, text_codigo, text_nombre, text_nombre_proveedores, text_precio_costo, text_venta); };

           

            //text_codigo.Enter += delegate (Object senders, EventArgs es) { click_buscar(senders, es, button_agregar, button_modificar, button_eliminar, text_codigo, text_nombre, text_nombre_proveedores, text_precio_costo, text_venta); };

            panel1.Controls.Add(lbl_codigo);
            panel1.Controls.Add(lbl_nombre);
            panel1.Controls.Add(lbl_precio_costo);
            panel1.Controls.Add(lbl_venta);
            panel1.Controls.Add(lbl_nombre_proveedor);
            panel1.Controls.Add(button_agregar);
            panel1.Controls.Add(button_buscar);
            panel1.Controls.Add(text_codigo);
            panel1.Controls.Add(button_eliminar);
            panel1.Controls.Add(button_modificar);
            panel1.Controls.Add(text_nombre);
            panel1.Controls.Add(text_nombre_proveedores);
            panel1.Controls.Add(text_venta);
            panel1.Controls.Add(text_precio_costo);



            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            ListaUsuario lstUsers = new ListaUsuario();



            if (lstUsers.BuscxNombreYcontra(textBox1.Text, textBox2.Text))
            {
                UsuarioActual.usuario = textBox1.Text;
                UsuarioActual.contra = textBox2.Text;

                textBox1.Enabled = false;
                textBox2.Enabled = false;
                archivoToolStripMenuItem.Enabled = true;
                comprasToolStripMenuItem.Enabled = true;
                ventasToolStripMenuItem.Enabled = true;
                inventarioToolStripMenuItem.Enabled = true;
                bajasYAltasToolStripMenuItem.Enabled = true;
            }
            else { MessageBox.Show("usuario o contraseña incorrectos"); }
          

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 registro = new Form3();
            registro.ShowDialog();

        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}