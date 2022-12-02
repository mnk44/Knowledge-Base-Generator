using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesisAnaReglas.BussinesLogic
{
    public class Variable
    {
        private String nameR;
        private String idVar;
        public VariableType typeR;
        private String dayanaR;
        private float valorMinR;
        private float valorMaxR;
        public String dayana
        {
            get { return dayanaR; }
            set { dayanaR = value; }
        }

        public float valorMin
        {
            get { return valorMinR; }
            set { valorMinR = value; }
        }

        public float valorMax
        {
            get { return valorMaxR; }
            set { valorMaxR = value; }
        }
        public String name
        {
            get { return nameR; }
            set { nameR = value; }
        }

      
        public string typeS
        {
         get { return typeR.type; }
         set { typeR.type = value; }
        }
        public VariableType type
        {
            get { return typeR; }
            set { typeR = value; }
        }  

        public string IdVar
        {
            get
            {
                return idVar;
            }

            set
            {
                idVar = value;
            }
        }

        public Variable(String name, VariableType type/*, StateC c*/)
        {
            this.name = name;
            this.type = type;
            //this.c = c;

        }
        public Variable()
        {
             
        }
      }
}
