using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Threading;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication12
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
        }

        private String getWord(String text,String type) {
            Translate trans = new Translate(text, type);
            return trans.sendAndGet();
          
        }

       

        private void Form1_Load_1(object sender, EventArgs e)
        {
            System.Drawing.Rectangle rec = Screen.GetWorkingArea(this);

            Left = rec.Width / 2 - this.Width/2;
            Top = rec.Height / 2 - this.Height / 2;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString().Equals("Return"))
            {
                String type = "zh";
                if (Regex.IsMatch(textBox1.Text, "[\u4e00-\u9fa5]"))
                {
                    type = "en";

                }
                if (textBox1.Text.Equals(""))
                {
                    return;
                }
                new Thread(() =>
                {

                    textBox2.Text = (getWord(textBox1.Text, type));
                }).Start();

            }
        }
    }
}
