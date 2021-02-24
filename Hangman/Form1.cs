using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;


namespace Hangman
{
    public partial class Form1 : Form
    {
        ArrayList Guessed = new ArrayList();
        ArrayList WordGuess = new ArrayList();
        ArrayList MyWord = new ArrayList();
        ArrayList TrueWord = new ArrayList();
        string[] words={"UMBRELLA","BOOK","APPLE","CAT","DOG","OUTPUT","ERROR","TABLE",
                           "WRITE","READ","EXIT","IRAN","ANZALI","TREE","HOME","IMAGINE","PUT"};
        string word;
        int NumberGuess = 0,TrueGuess=0;

        private void ShowWord()
        {
            label1.Text = "";
            foreach (string str in WordGuess)
                label1.Text += str;
        }
        private void ShoWTrueWord()
        {
            label1.Text = "";
            foreach (string str in TrueWord)
                label1.Text += str;
        }
        private void Restart()
        {
            Random number = new Random();
            int WordNumber = number.Next(0, words.Length);
            word = words[WordNumber];
            NumberGuess = 0;
            TrueGuess = 0;
            Guessed.Clear();
            WordGuess.Clear();
            MyWord.Clear();
            TrueWord.Clear();

            int count = 0;
            listBox1.Items.Clear();
            textBox1.Text = "";
            textBox1.Focus();
            label1.Text = "";
            label2.ImageIndex = NumberGuess;




            WordGuess.Add(" | ");
            TrueWord.Add(" | ");
            for (int i = 0; i < word.Length; i++)
            {
                TrueWord.Add(word.Substring(i, 1));


                if (count == 2)
                {
                    MyWord.Add("");
                    WordGuess.Add(word.Substring(i, 1));
                    TrueGuess++;
                    count = 0;
                }
                else
                {
                    MyWord.Add(word.Substring(i, 1));
                    WordGuess.Add("_");
                    count++;
                }

                WordGuess.Add(" | ");
                TrueWord.Add(" | ");

            }
            ShowWord();

        }

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Restart(); 
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.ActiveForm.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            string w;
            int aski,Index=0;
            bool Repeat = false, Guess = false;
            if(e.KeyCode==Keys.Return)
            {
                w=textBox1.Text;
                if (w != "")
                {
                    aski = (int)Convert.ToChar(w);
                    if (aski >= 97 && aski <= 122)
                        aski = aski - 32;

                    w = Convert.ToChar(aski).ToString();

                    if (aski >= 65 && aski <= 90)
                    {
                        foreach (string str in Guessed)
                            if (str == w)
                                Repeat = true;
                        if (!Repeat)
                        {
                            Guessed.Add(w);
                            listBox1.Items.Insert(0, w);
                            textBox1.Text = "";

                            foreach (string str in MyWord)
                            {
                                if (str == w)
                                {
                                    Guess = true;
                                    WordGuess.RemoveAt((2 * Index) + 1);
                                    WordGuess.Insert((2 * Index) + 1, w);
                                    TrueGuess++;
                                }
                                Index++;
                            }
                            if (Guess)
                            {
                                ShowWord();
                                Guess = false;
                            }
                            else
                            {
                                NumberGuess++;
                                label2.ImageIndex = NumberGuess;
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("Please enter a new letter");
                            textBox1.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a letter");
                        textBox1.Text = "";
                    }
                    if (NumberGuess == 7)
                    {
                        
                        ShoWTrueWord();
                        MessageBox.Show("GAME OVER", "GAME OVER",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Restart();
                    }
                    if (TrueGuess == word.Length)
                    {
                        MessageBox.Show("YOU WIN", "YOU WIN",
                                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Restart();
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a letter");
                    textBox1.Text = "";
                }
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program is writen by EHSAN RASSEKH\nStudent Number : 9312262100\nEmail : rassekh.ehsan@gmail.com", "About",
                                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}