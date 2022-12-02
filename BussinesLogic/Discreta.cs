using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesisAnaReglas.BussinesLogic
{
    
    public class Discreta : VariableType
    {
        private StateD _stateD;

        public StateD estat
        {
            get { return _stateD; }
            set { _stateD = value; }
        }

        public Discreta(StateD _stateD, string type) : base()
        {
            this._stateD = _stateD;
            this.type = type;

        }

    }
}
