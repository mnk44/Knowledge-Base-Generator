using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesisAnaReglas.BussinesLogic
{
    public class User
    {
        private String nameR;
        private String contraseñaR;
       
        public String name
        {
            get { return nameR; }
            set { nameR = value; }
        }
        public String contraseña
        {
            get { return contraseñaR; }
            set { contraseñaR = value; }
        }
     
        public User(String name, String contraseña)
        {
            this.name = name;
            this.contraseña = contraseña;
           
        }
        public User()
        {

        }

    }
}
