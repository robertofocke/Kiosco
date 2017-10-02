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
            idProducto = this.idProducto;
            producto = this.producto;
            cantidad = this.cantidad;
            precioCosto = this.precioCosto;
            precioVenta = this.precioVenta;

        }

        public string getProducto() {
            return this.idProducto;
        }
    }
}
