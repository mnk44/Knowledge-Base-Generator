using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesisAnaReglas.BussinesLogic
{
    public class Recomendation
    {
        private String staticRecomendationR;
       
        public String staticRecomendation
        {
            get { return staticRecomendationR; }
            set { staticRecomendationR = value; }
        }

        public Recomendation(String staticRecomendation)
        {
            this.staticRecomendation = staticRecomendation;
        }
        public Recomendation()
        {
            
        }

    }
}
