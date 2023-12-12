using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Паспорт_форм
{
    public partial class AddHistoryFK : Form
    {
        public AddHistoryFK()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dgwHistory = ((Form1)this.Owner).AddHistory;
            dgwHistory.Rows.Add(dateTimePicker1.Value.ToString(), richTextBox1.Text);
            Close();
        }
    }
}
