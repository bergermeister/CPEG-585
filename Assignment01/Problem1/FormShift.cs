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
    public partial class FormShift : Form
    {
        public FormShift()
        {
            InitializeComponent();
        }

        public int ShiftAmount
        {
            get
            {
                return (Convert.ToInt32(txtShiftAmount.Text, 10));
            }
            set { txtShiftAmount.Text = value.ToString(); }
        }
    }
}
