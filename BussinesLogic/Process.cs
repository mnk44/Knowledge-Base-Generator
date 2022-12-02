using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesisAnaReglas.BussinesLogic
{
    public class Process
    {
        public String processName { get; set; }
        public String dataBaseName { get; set; }
        public string userName { get; set; }
        public string passWord { get; set; }
        public String pass { get; set; }
        public LinkedList<Variable> varList { get; set; }
        public LinkedList<Cause> cauList { get; set; }
        public LinkedList<Recomendation> recList { get; set; }
        public LinkedList<Rule> rulList { get; set; }
        public StateC s { get; set; }

        public Random name=new Random();

        public int getRandomName(){            
            return name.Next();
        }
         
public System.Windows.Controls.Label  LabelName { get; set; }
    public Process(String userName, String passWord ,String processName, String dataBaseName,String pass)
        {
            this.dataBaseName = dataBaseName;
            this.processName = processName;
            this.userName = userName;
            this.passWord = passWord;
            this.pass = pass;
            varList = new LinkedList<Variable>();
            cauList = new LinkedList<Cause>();
            recList = new LinkedList<Recomendation>();
            rulList = new LinkedList<Rule>();
            LabelName = new System.Windows.Controls.Label();
            LabelName.Content = processName;
           

        }
        public Process()
        {
            varList = new LinkedList<Variable>();
            cauList = new LinkedList<Cause>();
            recList = new LinkedList<Recomendation>();
            rulList = new LinkedList<Rule>();
            s = new StateC();

        }
    }
}

