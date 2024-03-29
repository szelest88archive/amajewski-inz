﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms; 
//zmiana
namespace mapka
{
    public partial class Form1 : Form
    {
        //test repo bo coś nawala
        List<Point> moja_lista_punktow;
        List<Point> moja_lista_linii;
        Graphics test;

        Pen pioro;
        float skala = 1.0f;///?
        public Form1()
        {
            InitializeComponent();
            moja_lista_punktow = new List<Point>();
            moja_lista_linii = new List<Point>();
            test = pictureBox1.CreateGraphics();
            pioro = new Pen(Brushes.Red);
        }
        bool poczatek = true;
        
        private void odswiez()
        {
            
            float szer = 4 / skala;
            pioro.Width = szer;
            foreach (Point punkt in moja_lista_punktow)
            {
                
                test.DrawLine(
                pioro,
                new Point(punkt.X - (int)szer, punkt.Y - (int)szer),
                new Point(punkt.X + (int)szer, punkt.Y + (int)szer)
                );
                test.DrawLine(
                    pioro,
                    new Point(punkt.X + (int)szer, punkt.Y - (int)szer),
                    new Point(punkt.X - (int)szer, punkt.Y + (int)szer)
                    );
                
           }
        //linie
            for(int i=0;i<moja_lista_linii.Count;i++)
            {
                if (i % 2 == 1)
                    test.DrawLine(pioro,moja_lista_linii[i], moja_lista_linii[i - 1]);

            }
            pictureBox1.Update();
            
           


        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
          
        }
        bool parzyste = true;
        //do wstawiania tekstu
        //kosmos, do zmiany rozmiaru czcionki
        static public Font ChangeFontSize(Font font, float fontSize)
        {
            if (font != null)
            {
                float currentSize = font.Size;
                if (currentSize != fontSize)
                {
                    font = new Font(font.Name, fontSize
                        );
                }
            }
            return font;
        }
        //...
        TextBox textBox;
        Label labelka;
        int x, y;
        
        
        private void enterWText(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                ((TextBox)sender).Visible = false;
                labelka = new Label();
                labelka.ForeColor = Color.Red;
                labelka.Font = ChangeFontSize(labelka.Font, 12.0F);
                labelka.BackColor = Color.Transparent;
                labelka.SendToBack();
               
                labelka.Text = ((TextBox)sender).Text;
                labelka.Left = ((TextBox)sender).Location.X+5;
                labelka.Top = ((TextBox)sender).Location.Y;
                
                pictureBox1.Controls.Add(labelka);
                
                //radioButton3.Text = ((TextBox)sender).Text;
            }
        }
        private void mouseDown(object sender, MouseEventArgs e)
        {  
            pioro.Width = 4/skala;//?
            if (radioButtonPunkty.Checked== true)
            {
                textBox = new TextBox();
                textBox.Visible = true;
                textBox.BringToFront();
                textBox.Left = e.Location.X;
                textBox.Top = e.Location.Y;
                textBox.KeyDown += enterWText;
                pictureBox1.Controls.Add(textBox);
               // panel1.Controls.Add(textBox);

                moja_lista_punktow.Add(new Point((int)(e.Location.X / skala), (int)(e.Location.Y / skala)));
            }
            else if(radioButtonLinie.Checked ==true)
            {
                parzyste = !parzyste;
                moja_lista_linii.Add(new Point((int)(e.Location.X / skala), (int)(e.Location.Y / skala))); 
            }
            //test
            else if (radioButton3.Checked == true) //to, oczywiście, bardzo robocze
            {
                TextBox temp = new TextBox();


                temp.BringToFront();
                temp.Left = e.X;
                temp.Top = e.Y;
                temp.BringToFront();
              //  pictureBox1.Visible = false;
               
                pictureBox1.Controls.Add(temp);
                temp.Show();
           
            }

            
            odswiez();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            odswiez();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            panel1.Width = Form1.ActiveForm.Width - 30;
            panel1.Height = Form1.ActiveForm.Height - 75;
            odswiez();
        }

        private void przySkrolnieciu(object sender, ScrollEventArgs e)
        {
            odswiez();
        }

        private void rysowanie(object sender, PaintEventArgs e)
        {
            odswiez();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (skala < 4)
            {
                /*poniższym, z odpowiednim argumentem, powinno się
                 * dać zescrollować do odpowiedniego obszaru,
                 * naczy centrum obrazka
                 * A tak w ogóle, to przerobić to i poniższe na
                 * jakąś funkcję
                 */
                //    panel1.SetAutoScrollMargin(); 
                pictureBox1.Scale(new System.Drawing.SizeF(2, 2));
                test.ScaleTransform(2, 2);//?
                skala *= 2.0f;///?
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                pictureBox1.Refresh();
                odswiez();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*poniższym, z odpowiednim argumentem, powinno się
             * dać zescrollować do odpowiedniego obszaru,
             * naczy centrum obrazka
             * A tak w ogóle, to przerobić to i powyższe na jakąś 
             * funkcję
             */
            //    panel1.SetAutoScrollMargin(); 
            if (skala > 0.0625)
            {
                panel1.ScrollControlIntoView(pictureBox1);
                pictureBox1.Scale(new System.Drawing.SizeF(0.5f, 0.5f));
                test.ScaleTransform(0.5f, 0.5f);//?
                skala *= 0.5f;///?
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Refresh();
                odswiez();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            odswiez();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            odswiez();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            odswiez();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        GraphicsState stan;
        private void mm(object sender, MouseEventArgs e)
        {
            //tu jest prawie ok, ale coś miga
            pictureBox1.Refresh();
            odswiez();
            if (!parzyste)
            {
                test.DrawLine(pioro,//trza poprawic skale
                    moja_lista_linii[moja_lista_linii.Count-1],
                    new Point(
                        (int)(e.Location.X/skala),
                    (int)(e.Location.Y/skala))
                    
                );
            }
           
            
            
            
        }

        private void radioButtonLinie_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonPunkty_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
