using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        string fileName = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(textBox.Text))
            {
                var result = MessageBox.Show("Do you want to save open document?", "Confirm",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    saveFileAs();
                    createNewFile();
                }
                else if(result == DialogResult.No)
                {
                    createNewFile();
                }
                else
                {

                }
             }
        }



        private void saveFileAs()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFile.FilterIndex = 1;
            if(saveFile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter w = new StreamWriter(saveFile.OpenFile());
                w.Write(textBox.Text);
                w.Close();
            }
        }

        private void createNewFile()
        {
            textBox.Text = "";
            fileName = "";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "TXT File (*.txt)|*.txt|All files (*.*)|*.*";
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                fileName = dlg.FileName;
                readFile();
            }
        }

        private void readFile()
        {
            TextReader tr = new StreamReader(fileName);
            textBox.Text = tr.ReadToEnd();
            tr.Close();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileAs();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox != null)
            {
                if (fileName == "")
                    saveFileAs();
                else
                    saveFile();
            }
            else
                MessageBox.Show("There is nothing in the editor!", "Error");
        }

        private void saveFile()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFile.FilterIndex = 1;
            saveFile.FileName = textBox.Text;
            StreamWriter w = new StreamWriter(fileName);
            w.Write(textBox.Text);
            w.Close();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(textBox != null)
            {
                FontDialog fontdlg = new FontDialog();
                fontdlg.Font = textBox.Font;
                fontdlg.ShowDialog();
                textBox.Font = fontdlg.Font;
            }
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog background = new ColorDialog();
            background.Color = textBox.BackColor;
            background.ShowDialog();
            textBox.BackColor = background.Color;
        }

        private void teToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog fore = new ColorDialog();
            fore.Color = textBox.ForeColor;
            fore.ShowDialog();
            textBox.ForeColor = fore.Color;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            MatchCollection numbers = Regex.Matches(textBox.Text, @"[\S]+");
            WordCount.Text = "Word Count = " + numbers.Count;
        }

        private void spellCheckerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String[] dictionary = File.ReadAllLines("dictionary.txt").ToArray();
                string wordToCheck = textBox.Text;
                string[] words = Regex.Split(wordToCheck, @"\W+");
                ArrayList errorWords = new ArrayList();
                foreach(string word in words)
                {
                    Console.WriteLine(word);
                    int index = Array.BinarySearch(dictionary, word);
                    if(index < 0)
                    {
                        errorWords.Add(word);
                    }              
                }
                if (errorWords.Count >= 0)
                {
                    textBox.Clear();
                    foreach (string word in errorWords)
                    {
                        Match match = Regex.Match(wordToCheck, word);
                        Console.WriteLine(match.Value);
                        textBox.SelectionColor = Color.Black;
                        if (match.Index > 0)
                            textBox.SelectedText = wordToCheck.Substring(0, match.Index);
                        textBox.SelectionColor = Color.Red;
                        textBox.SelectedText = wordToCheck.Substring(match.Index, match.Length);
                        wordToCheck = wordToCheck.Substring(match.Index + match.Length);
                    }
                    textBox.SelectionColor = Color.Black;
                    textBox.SelectedText = wordToCheck;
                }
                else
                    textBox.Text = wordToCheck;
            }
            catch (Exception j)
            { }

        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Paste();
        }

    }
}
