using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace UP_Design_Maket
{
    public partial class OperProgress : Fparent
    {
        public OperProgress()
        {
            InitializeComponent();
        }
        public void InvokeUI(Action a)
        {
            this.BeginInvoke(new MethodInvoker(a));
        }
    }
}
