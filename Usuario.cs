using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class Usuario
    {
        public string usuario { get; set; }
        public string contra { get; set; }
        public string cuit { get; set; }

        public Usuario(string cuit, string usuario, string contra)
        {
            this.cuit=cuit;
            this.usuario=usuario;
            this.contra=contra;


        }

        public string getUsuario()
        {
            return this.usuario;
        }
        public string getCuit()
        {
            return this.cuit;

        }

        public string getContra()
        {

            return this.contra;
        }



    }
}
