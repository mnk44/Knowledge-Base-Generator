using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesisAnaReglas.BussinesLogic
{
    public class Cause
    {
        private String staticCauseR;
        private String idCausa;
        public String staticCause
        {
            get { return staticCauseR; }
            set { staticCauseR = value; }
        }

        public string IdCausa
        {
            get
            {
                return idCausa;
            }

            set
            {
                idCausa = value;
            }
        }

        public Cause(String staticCause)
        {
            this.staticCause = staticCause;
        }
        public Cause()
        {
         
        }
       
    }
}
