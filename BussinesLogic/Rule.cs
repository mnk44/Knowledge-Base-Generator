using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesisAnaReglas.BussinesLogic
{
    public class Rule
    {
        protected String rule;
        protected String ruleName;

        public String ruleR
        {
            get { return rule; }
            set { rule = value; }
        }
        public String ruleN
        {
            get { return ruleName; }
            set { ruleName = value; }
        }
     
        public Rule()
        { 
        }
    }
}
