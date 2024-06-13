using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Remember_
{
    public partial class Form1 : Form
    {
        private Timer timer;
        private List<Color> colors = new List<Color>

    //лист цветов для рандомного закрашивания карточек
    {
        Color.Red,
        Color.Orange,
        Color.Yellow,
        Color.Green,
        Color.Blue,
        Color.Purple
    };
        private Color[,] originalColors; //переменная, запоминающая изначальный цвет карточек
        public Form1()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            button1.Click += button1_Click;
            dataGridView1.CellClick += dataGridView1_CellClick;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
        }
        private void Timer_Tick(object sender, EventArgs e) //работа таймера, запоминание изначальных цветов карточек b по окончанию таймера закрашивает все карточки в белый
        {
            int seconds = (int)numericUpDown2.Value;
            if (seconds > 0)
            {
                seconds--;
                if (seconds >= numericUpDown2.Minimum && seconds <= numericUpDown2.Maximum)
                {
                    label3.Text = "Осталось " + seconds + " секунд";
                }
                else
                {
                    timer.Stop();
                    label3.Text = "Запомнил? Закрашивай!";
                    if (originalColors != null)
                    {
                        for (int i = 0; i < originalColors.GetLength(0); i++)
                        {
                            for (int j = 0; j < originalColors.GetLength(1); j++)
                            {
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                            }
                        }
                    }
                }
            }
            else
            {
                label3.Text = "Запомнил? Закрашивай!";
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                timer.Stop();
            }
        }
        private void button1_Click(object sender, EventArgs e) //начало игрового процесса
        {
            Random rnd = new Random();
            int cellCount;
            cellCount = (int)numericUpDown1.Value;
            dataGridView1.ColumnCount = 5;
            if (cellCount <= 5)
            {
                dataGridView1.RowCount = 1;
            }
            else if (cellCount >= 6 && cellCount <= 10)
            {
                dataGridView1.RowCount = 2;
            }
            else dataGridView1.RowCount = 3;

            originalColors = new Color[dataGridView1.RowCount, dataGridView1.ColumnCount];

            int i, j;
            int count = 0;
            for (i = 0; i < dataGridView1.RowCount; i++)
                for (j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    dataGridView1.Columns[i].Width = 50;
                    dataGridView1.Rows[i].Height = 40;
                    int indx = rnd.Next(colors.Count);
                    originalColors[i, j] = colors[indx];
                    dataGridView1.Rows[i].Cells[j].Style.BackColor = colors[indx];
                    count++;
                    if (cellCount < count)
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }
                }
            timer.Start();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) //закрашивание карточек в выбранный цвет
        {
            if (radioButton1.Checked)
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
            }
            else if (radioButton2.Checked)
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Orange;
            }
            else if (radioButton4.Checked)
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Yellow;
            }
            else if (radioButton3.Checked)
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Green;
            }
            else if (radioButton6.Checked)
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Blue;
            }
            else if (radioButton5.Checked)
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Purple;
            }
            else dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
        }
        private void button2_Click(object sender, EventArgs e) //сброс игры
        {
            timer.Stop();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            label3.Text = "";
        }
        private void button3_Click(object sender, EventArgs e) //проверка правильности закрашивания карточек и вывод результата
        {
            int coolColor = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (originalColors[i, j] == dataGridView1.Rows[i].Cells[j].Style.BackColor)
                    {
                        coolColor++;
                    }
                }
            }
            if (coolColor == (int)numericUpDown1.Value)
            {
                MessageBox.Show("Вы выиграли! \n:)");
            }
            else MessageBox.Show("Вы проиграли! \n:(");
        }
    }
}
