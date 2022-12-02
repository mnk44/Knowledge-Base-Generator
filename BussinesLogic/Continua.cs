using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesisAnaReglas.BussinesLogic
{

    class Continua:VariableType
    {
        private StateC _stateC;

        public float MAX;

        public float MIN;

        public StateC estat
        {
            get { return _stateC; }
            set { _stateC = value; }
        }
        public float min
        {
            get { return MIN; }
            set { MIN = value; }
        }
        public float max
        {
            get { return MAX; }
            set { MAX = value; }
        }
        public Continua(StateC _stateC, string type ) : base()
        {
            this._stateC = _stateC;
             this.type = type;
           

        }
        public Continua( string type, float min, float max) : base()
        {
            this.type = type;
            this.max = max;
            this.min = min;

        }
    }
}
