using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Problem1
{
    public partial class FormContrast : Form
    {
        public int Contrast
        {
            get
            {
                return int.Parse(txtContrast.Text);
            }
            set { txtContrast.Text = value.ToString(); }
        }
        public FormContrast()
        {
            InitializeComponent();
        }
    }
}
