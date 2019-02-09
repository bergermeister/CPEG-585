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
    public partial class FormRotate : Form
    {
        public FormRotate()
        {
            InitializeComponent();
        }

        public int RotationAngle
        {
            get
            {
                return (Convert.ToInt32(txtRotation.Text, 10));
            }
            set { txtRotation.Text = value.ToString(); }
        }
    }
}
