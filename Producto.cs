using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class Producto
    {
        public string idProducto{get; set;}
        public string producto { get; set; }
        public int cantidad { get; set; }
        public double precioCosto { get; set; }
        public double precioVenta { get; set; }

        public Producto(string idProducto, string producto, int cantidad, double precioCosto, double precioVenta) {
            this.idProducto=idProducto;
            this.producto=producto;
            this.cantidad=cantidad;
            this.precioCosto=precioCosto;
            this.precioVenta=precioVenta;

        }


        public Producto(string idProducto, string producto, double precioCosto, double precioVenta)
        {
            this.idProducto = idProducto;
            this.producto = producto;
            this.precioCosto = precioCosto;
            this.precioVenta = precioVenta;

        }


        public Producto() {
        }
        public string getProducto()
        {
            return this.producto;
        }

        public string getIdProducto() {
            return this.idProducto;
        }
        public int getCantidad()
        {
            return this.cantidad;
        }
        public double getPrecioCosto()
        {
            return this.precioCosto;
        }
        public double getPrecioVenta()
        {
            return this.precioVenta;
        }

        public Boolean compararProducto(Producto p2)
        {
            bool verdadero = false;
            if (this.getIdProducto().CompareTo(p2.getIdProducto()) == 0)
            {
                verdadero = true;
            }
            return verdadero;
        }
    }

}
