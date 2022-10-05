using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1Model
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            chart1.ChartAreas[0].AxisX.Title = "t [часа]";
            chart1.ChartAreas[0].AxisY.Title = "C [Кмоль/м^3]";
            chart2.ChartAreas[0].AxisX.Title = "t [часа]";
            chart2.ChartAreas[0].AxisY.Title = "C [Кмоль/м^3]";
            chart3.ChartAreas[0].AxisX.Title = "t [часа]";
            chart3.ChartAreas[0].AxisY.Title = "C [Кмоль/м^3]";
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44)
                e.Handled = true;
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void numericUpDown1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void numericUpDown1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool check(){
                bool ok = true;
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                {
                    ok = false;
                    MessageBox.Show("Поля не могут быть пустыми!", "Ошибка!");
                }
                else if (Convert.ToDouble(textBox1.Text) > 5000 || Convert.ToDouble(textBox2.Text) > 5000 || Convert.ToDouble(textBox3.Text) > 5000  )
                {
                    ok = false;
                    MessageBox.Show("Значение не может быть больше 5000!", "Ошибка!");
                }
                else if(Convert.ToDouble(textBox4.Text) > 50)
                {
                    ok = false;
                    MessageBox.Show("M должно быть меньше 50!", "Ошибка!");
                }
                else if (Convert.ToDouble(textBox5.Text) > 50 || Convert.ToDouble(textBox5.Text) < 0.001f)
                {
                    ok = false;
                    MessageBox.Show("Шаг должен быть больше 0.0001 и меньше 50!", "Ошибка!");
                }

                return ok;
            }
            if (check())
            {
                if (numericUpDown1.Value == 1)
                {
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = false;
                    pictureBox3.Visible = false;
                    pictureBox4.Visible = false;
                    pictureBox5.Visible = false;
                }
                if (numericUpDown1.Value == 2)
                {
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = true;
                    pictureBox3.Visible = false;
                    pictureBox4.Visible = true;
                    pictureBox5.Visible = false;
                }
                if (numericUpDown1.Value == 3)
                {
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = true;
                    pictureBox3.Visible = true;
                    pictureBox4.Visible = true;
                    pictureBox5.Visible = true;
                }

                double N = Convert.ToDouble(numericUpDown1.Value);
                double Cin = Convert.ToDouble(textBox1.Text);
                double V = Convert.ToDouble(textBox2.Text);
                double G = Convert.ToDouble(textBox3.Text);
                double M = Convert.ToDouble(textBox4.Text);
                double _step = Convert.ToDouble(textBox5.Text);
                double tau = V / G;


                double Cout = 0;
                double Cout2 = 0;
                double Cout3 = 0;
                chart1.ChartAreas[0].AxisX.Minimum = 0;
              

                if (N == 1)
                {

                    dataGridView1.Rows.Clear();
                    chart1.Series[0].Points.Clear();
                    chart1.Series[1].Points.Clear();
                    chart1.Series[2].Points.Clear();
                    int i = 0;
                    for (double t = 0; t <= M * tau; t += _step)
                    {
                        dataGridView1.Rows.Add();

                        Cout = Cin - Math.Exp(-t / tau) * Cin;
                        dataGridView1.Rows[i].Cells[0].Value = Math.Round(t, 3);
                        dataGridView1.Rows[i].Cells[1].Value = Math.Round(Cout, 3);

                        chart1.Series[0].Points.AddXY(Math.Round(t, 3), Math.Round(Cout, 3));
                        i++;
                    }
                    double Ch = Cout;
                    double t2 = 0;
                    for (double t = M * tau; ; t += _step)
                    {
                        t2 += _step;
                        dataGridView1.Rows.Add();

                        Cout = Math.Exp(-t2 / tau) * Ch;
                        dataGridView1.Rows[i].Cells[0].Value = Math.Round(t, 3);
                        dataGridView1.Rows[i].Cells[1].Value = Math.Round(Cout, 3);

                        chart1.Series[0].Points.AddXY(Math.Round(t, 3), Math.Round(Cout, 3));
                        i++;

                        if (Cout <= 0.001f)
                        {
                            break;
                        }
                    }

                }

                if (N == 2)
                {
         
                    dataGridView1.Rows.Clear();
                    chart1.Series[0].Points.Clear();
                    chart1.Series[1].Points.Clear();
                    chart1.Series[2].Points.Clear();

                    
                    int i = 0;
                    for (double t = 0; t < M * tau; t += _step)
                    {
                        dataGridView1.Rows.Add();

                        Cout = Cin - (1 + (t / tau)) * Math.Exp(-t / tau) * Cin;
                        Cout2 = Cin - Math.Exp(-t / tau) * Cin;
                        dataGridView1.Rows[i].Cells[0].Value = Math.Round(t, 1);
                        dataGridView1.Rows[i].Cells[1].Value = Math.Round(Cout, 3);

                        chart1.Series[0].Points.AddXY(Math.Round(t, 1), Cout);
                        chart1.Series[1].Points.AddXY(Math.Round(t, 1), Cout2);
                        i++;
                    }
                    double Ch = Cout;
                    double t2 = 0;
                    for (double t = M * tau; ; t += _step)
                    {
                        dataGridView1.Rows.Add();
                        t2 += _step;
                        Cout = (1 + (t2 / tau)) * Math.Exp(-t2 / tau) * Ch;
                        Cout2 = Math.Exp(-t2 / tau) * Ch;
                        dataGridView1.Rows[i].Cells[0].Value = Math.Round(t, 3);
                        dataGridView1.Rows[i].Cells[1].Value = Math.Round(Cout, 3);

                        chart1.Series[0].Points.AddXY(Math.Round(t, 3), Cout);
                        chart1.Series[1].Points.AddXY(Math.Round(t, 3), Cout2);
                        i++;
                        if (Cout <= 0.001f)
                        {
                            break;
                        }
                    }

                }

                if (N == 3)
                {
         
                    dataGridView1.Rows.Clear();
                    chart1.Series[0].Points.Clear();
                    chart1.Series[1].Points.Clear();
                    chart1.Series[2].Points.Clear();
                    int i = 0;
                    for (double t = 0; t < M * tau; t += _step)
                    {
                        dataGridView1.Rows.Add();
                        Cout3 = Cin - (1 + (t / tau)) * Math.Exp(-t / tau) * Cin;
                        Cout2 = Cin - Math.Exp(-t / tau) * Cin;
                        Cout = Cin - (1 + (t / tau) + 0.5 * (Math.Pow(t / tau, 2))) * Math.Exp(-t / tau) * Cin;
                        dataGridView1.Rows[i].Cells[0].Value = Math.Round(t, 3);
                        dataGridView1.Rows[i].Cells[1].Value = Math.Round(Cout, 3);

                        chart1.Series[0].Points.AddXY(Math.Round(t, 3), Cout);
                        chart1.Series[2].Points.AddXY(Math.Round(t, 3), Cout3);
                        chart1.Series[1].Points.AddXY(Math.Round(t, 3), Cout2);
                        i++;
                    }
                    double Ch = Cout; double t2 = 0;
                    for (double t = M * tau;; t += _step)
                    {
                        dataGridView1.Rows.Add();
                        t2 += _step;
                        Cout3 = (1 + (t2 / tau)) * Math.Exp(-t2 / tau) * Ch;
                        Cout2 = Math.Exp(-t2 / tau) * Ch;
                        Cout = (1 + (t2 / tau) + 0.5 * (Math.Pow(t2 / tau, 2))) * Math.Exp(-t2 / tau) * Ch;
                        dataGridView1.Rows[i].Cells[0].Value = Math.Round(t, 3);
                        dataGridView1.Rows[i].Cells[1].Value = Math.Round(Cout, 3);

                        chart1.Series[0].Points.AddXY(Math.Round(t, 3), Cout);
                        chart1.Series[2].Points.AddXY(Math.Round(t, 3), Cout3);
                        chart1.Series[1].Points.AddXY(Math.Round(t, 3), Cout2);
                        i++;
                        if (Cout <= 0.001f)
                        {
                            break;
                        }
                    }

                }


            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данную программу разработали студенты 475 группы:\n\t\tОвчинников Роман\n\t\tИброхимова Полина","Разработчики");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool check()
            {
                bool ok = true;
                if (textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox18.Text == "" || textBox14.Text == "" || textBox10.Text == "" || textBox11.Text == "" || textBox12.Text == "" || textBox13.Text == "" || textBox15.Text == "" || textBox16.Text == "" || textBox17.Text == "")
                {
                    ok = false;
                    MessageBox.Show("Поля не могут быть пустыми!", "Ошибка!");
                }
                else if (Convert.ToDouble(textBox6.Text) > 50 || Convert.ToDouble(textBox7.Text) > 50 || Convert.ToDouble(textBox8.Text) > 50 || Convert.ToDouble(textBox14.Text) > 50 || Convert.ToDouble(textBox18.Text) > 50 || Convert.ToDouble(textBox17.Text) > 50 || Convert.ToDouble(textBox16.Text) > 50 || Convert.ToDouble(textBox15.Text) > 50 || Convert.ToDouble(textBox10.Text) > 50 || Convert.ToDouble(textBox11.Text) > 50 || Convert.ToDouble(textBox12.Text) > 50 || Convert.ToDouble(textBox13.Text) > 50)
                {
                    ok = false;
                    MessageBox.Show("Значение не может быть больше 50!", "Ошибка!");
                }
                else if (Convert.ToDouble(textBox10.Text) > 50 || Convert.ToDouble(textBox10.Text) < 0.001f)
                {
                    ok = false;
                    MessageBox.Show("Шаг должен быть больше 0.0001 и меньше 50!", "Ошибка!");
                }

                return ok;
            }

            if (check()) {

                double N = Convert.ToDouble(numericUpDown2.Value);
                double CAin = Convert.ToDouble(textBox6.Text);
                double CBin = Convert.ToDouble(textBox15.Text);
                double CA1 = Convert.ToDouble(textBox11.Text);
                double CB1 = Convert.ToDouble(textBox12.Text);
                double CC1 = Convert.ToDouble(textBox13.Text);
                double CA2 = Convert.ToDouble(textBox16.Text);
                double CB2 = Convert.ToDouble(textBox17.Text);
                double CC2 = Convert.ToDouble(textBox18.Text);
                double t = Convert.ToDouble(textBox9.Text);
                double V = Convert.ToDouble(textBox7.Text);
                double K = Convert.ToDouble(textBox14.Text);
                double G = Convert.ToDouble(textBox8.Text);
                
                double _step = Convert.ToDouble(textBox10.Text);

                double n = t / _step;
                double tau = V / (G * n);


                //double CoutA1 = 0;
                //double CoutB1 = 0;
                //double CoutC1 = 0;

                
                    {

                        dataGridView2.Rows.Clear();
                        chart2.Series[0].Points.Clear();
                        chart2.Series[1].Points.Clear();
                        chart2.Series[2].Points.Clear();
                        chart3.Series[0].Points.Clear();
                        chart3.Series[1].Points.Clear();
                        chart3.Series[2].Points.Clear();
                        chart2.ChartAreas[0].AxisX.Minimum = 0;
                        chart3.ChartAreas[0].AxisX.Minimum = 0;
                }
                int i = 0;
                { // ввод начальных условий в график и таблицу
                  
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[0].Value = 0;
                    dataGridView2.Rows[i].Cells[1].Value = Math.Round(CA1, 3);
                    dataGridView2.Rows[i].Cells[2].Value = Math.Round(CB1, 3);
                    dataGridView2.Rows[i].Cells[3].Value = Math.Round(CC1, 3);
                    dataGridView2.Rows[i].Cells[4].Value = Math.Round(CA2, 3);
                    dataGridView2.Rows[i].Cells[5].Value = Math.Round(CB2, 3);
                    dataGridView2.Rows[i].Cells[6].Value = Math.Round(CC2, 3);

                    chart2.Series[0].Points.AddXY(0, Math.Round(CA1, 3));
                    chart2.Series[1].Points.AddXY(0, Math.Round(CB1, 3));
                    chart2.Series[2].Points.AddXY(0, Math.Round(CC1, 3));

                    chart3.Series[0].Points.AddXY(0, Math.Round(CA2, 3));
                    chart3.Series[1].Points.AddXY(0, Math.Round(CB2, 3));
                    chart3.Series[2].Points.AddXY(0, Math.Round(CC2, 3));
                    i++;
                }
                try
                {

                    for (double tl = _step; tl <= t; tl += _step)
                    {
                        dataGridView2.Rows.Add();



                        //////////////////////////////////////////////////////////////////////////
                        CA1 = Math.Round(   CA1 + _step * ((1 / tau) * (CAin - CA1) - K * CA1 * CB1)   , 3);
                        CB1 = Math.Round(CB1 + _step * ((1 / tau) * (CBin - CB1) - K * CA1 * CB1), 3);
                        CC1 = Math.Round(CC1 + _step * ((1 / tau) * (-CC1) + 2 * K * CA1 * CB1), 3);
                        //////////////////////////////////////////////////////////////////////////

                        {
                            dataGridView2.Rows[i].Cells[0].Value = Math.Round(tl, 3);
                            dataGridView2.Rows[i].Cells[1].Value = Math.Round(CA1, 3);
                            dataGridView2.Rows[i].Cells[2].Value = Math.Round(CB1, 3);
                            dataGridView2.Rows[i].Cells[3].Value = Math.Round(CC1, 3);
                            chart2.Series[0].Points.AddXY(Math.Round(tl, 3), Math.Round(CA1, 3));
                            chart2.Series[1].Points.AddXY(Math.Round(tl, 3), Math.Round(CB1, 3));
                            chart2.Series[2].Points.AddXY(Math.Round(tl, 3), Math.Round(CC1, 3));
                        }

                        //////////////////////////////////////////////////////////////////////////
                        CA2 = Math.Round(CA2 + _step * ((1 / tau) * (CA1 - CA2) - K * CA2 * CB2), 3);
                        CB2 = Math.Round(CB2 + _step * ((1 / tau) * (CB1 - CB2) - K * CA2 * CB2), 3);
                        CC2 = Math.Round(CC2 + _step * ((1 / tau) * (-CC2) + 2 * K * CA2 * CB2), 3);
                        //////////////////////////////////////////////////////////////////////////

                        {
                            dataGridView2.Rows[i].Cells[4].Value = Math.Round(CA2, 3);
                            dataGridView2.Rows[i].Cells[5].Value = Math.Round(CB2, 3);
                            dataGridView2.Rows[i].Cells[6].Value = Math.Round(CC2, 3);
                            chart3.Series[0].Points.AddXY(Math.Round(tl, 3), Math.Round(CA2, 3));
                            chart3.Series[1].Points.AddXY(Math.Round(tl, 3), Math.Round(CB2, 3));
                            chart3.Series[2].Points.AddXY(Math.Round(tl, 3), Math.Round(CC2, 3));
                        }


                        i++;

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Что-то пошло не так. Введите другие значения!", "Ошибка!");
                }
                   
                

            }


        }
    }
}
