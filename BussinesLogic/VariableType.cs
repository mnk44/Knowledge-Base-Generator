using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesisAnaReglas.BussinesLogic
{
    public class VariableType
    {
        protected String typeR;
        public String estado;

        public String type
        {
            get { return typeR; }
            set { typeR = value; }
        }
        public VariableType()
        {
        }
    }
}

