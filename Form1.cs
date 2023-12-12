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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var compositionFKName = new string[] 
                { 
                    "Форма чистовая", 
                    "Поддон чистовой формы", 
                    "Форма черновая" , 
                    "Прессовая головка", 
                    "Корпус затвора", 
                    "Вставка затвора", 
                    "Воронка", 
                    "Головка дутьевая", 
                    "Кольцо горловое", 
                    "Пластина финишная", 
                    "Плунжер", 
                    "Втулка плунжера", 
                    "Охладитель", 
                    "Плита охлаждения", 
                    "Захват отставителя", 
                    "Вставка захвата отставителя" 
                };
            compositionFKName.ToList().ForEach(x => compositionFK.Rows.Add(x, "", ""));
        }

        public DataGridView AddHistory
        {
            get {return historyFK; }
        }

        private void numberCheck_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (Enabled)
            {
                base.OnPaint(e);
                return;
            }
            using (Brush aBrush = new SolidBrush(Color.Black))
            {
                e.Graphics.DrawString(Text, Font, aBrush, ClientRectangle);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AddHistoryFK().ShowDialog(this);
        }
    }
}
