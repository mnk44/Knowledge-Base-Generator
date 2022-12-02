using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Converters;

namespace TesisAnaReglas.BussinesLogic
{
    class Valvula:VariableType
    {
        private StateV _stateV;

        public StateV estat
        {
            get { return _stateV; }
            set { _stateV = value; }
        }

        public Valvula(StateV _stateV, string type) : base()
        {
            this._stateV = _stateV;
            this.type = type;

        }
     }
  }
