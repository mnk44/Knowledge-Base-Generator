using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TesisAnaReglas.BussinesLogic
{
    public class VariableCause : Rule
    {
        public LinkedList<Variable> variableList { get; set; }
        public LinkedList<Cause> causeList { get; set; }

        public VariableCause() : base()
        {
            variableList = new LinkedList<Variable>();
            causeList = new LinkedList<Cause>();
        }
        public VariableCause(LinkedList<Variable> variableList, LinkedList<Cause> causeList, String rule, string nameRule) : base()
        {
            this.ruleR = rule;
            this.ruleN =nameRule;
            this.variableList = variableList;
            this.causeList = causeList;
            
        }
    }
}
