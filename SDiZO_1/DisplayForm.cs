using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDiZO_1
{
    public partial class DisplayForm : Form
    {
        public DisplayForm(string filename)
        {
            InitializeComponent();
            textBox1.Text = File.ReadAllText("./"+filename+".txt");
        }
    }
}
