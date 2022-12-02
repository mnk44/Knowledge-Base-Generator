using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TesisAnaReglas.BussinesLogic
{
    public class CauseRecomendation : Rule
    {
       public Cause cause { get; set; }
        public LinkedList<Recomendation> recomendationList { get; set; }


        public CauseRecomendation( Cause cause, LinkedList<Recomendation> recomendationList,String rule, string rule_name) : base()
        {
            this.ruleR = rule;
            this.ruleN = rule_name;
            this.cause = cause;
            this.recomendationList = recomendationList;

        }
        public CauseRecomendation() : base()
        {
            cause = new Cause();
            recomendationList =  new LinkedList<Recomendation>();
        }
      
    }
}
