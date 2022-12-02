using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesisAnaReglas.BussinesLogic
{
    public class VarCausRecomen : Rule
    {
              
            public Cause cause { get; set; }
            public LinkedList<Recomendation> recomendationList { get; set; }
            public Variable variable { get; set; }

        public VarCausRecomen(Cause cause, LinkedList<Recomendation> recomendationList,Variable variable, String rule) : base()
            {
                this.rule = rule;
                this.cause = cause;
                this.recomendationList = recomendationList;
                 this.variable = variable;

            }
            public VarCausRecomen() : base()
            {
                cause = new Cause();
                recomendationList = new LinkedList<Recomendation>();
            }

        
    }
}
