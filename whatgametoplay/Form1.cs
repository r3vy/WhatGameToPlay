using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace whatgametoplay
{
    //This has committed.
    public partial class Form1 : Form
    {
        List<string> gameNames = new List<string>();
        string filePath = Application.StartupPath + "\\gameNames.txt";

        public Form1()
        {
            InitializeComponent();
            UpdateTextPosition();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            
            using (StreamReader r = new StreamReader(filePath))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    gameNames.Add(line);
                    comboBox1.Items.Add(line);
                }
            }
        }

        //This is the Random Button.
        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            //Tells user to add a game if empty list.
            if (gameNames.Count == 0)
                label1.Text = "Please add a Game first!";
            else if (gameNames.Count == 1)
                label1.Text = gameNames[0];
            else
                label1.Text = gameNames[rnd.Next(gameNames.Count)]; //Randomly pick a game between 0 and the number of items in the list.
        }

        //This Button will add a game to the List as a string,(what ever is in the textbox).
        private void button2_Click(object sender, EventArgs e)
        {
            string userInput;

            userInput = textBox1.Text; //Users Input is taken from the textbox
            if (textBox1.Text == "") //If it is blank, we send error message to user.
                MessageBox.Show("Please enter a game name to add to the list");
            else //Add the game name to the list, and to the combobox.
            {
                gameNames.Add(userInput);
                comboBox1.Items.Add(userInput);
            }
            //Save the List to text file to ensure these games our added next time program is ran.
            StreamWriter file = new StreamWriter(filePath);
            gameNames.ForEach(file.WriteLine);
            file.Close();
            textBox1.Clear(); //Clear text for neatness. :)

        }


        //This is the Delete Button, it will delete selected items from combobox.
        private void button3_Click(object sender, EventArgs e)
        {
            //Error Message to user if nothing is selected.
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a game to remove from the list.");
            }
            else
            {
                gameNames.Remove(comboBox1.SelectedItem.ToString()); //Changes selected Item/Object to a string, then Removes that string from the <List>String.
                comboBox1.Items.Remove(comboBox1.SelectedItem); //Removes selected Item from combobox listing.
            }

            //Save the List to text file to ensure these games our added next time program is ran.
            StreamWriter file = new StreamWriter(filePath);
            gameNames.ForEach(file.WriteLine);
            file.Close();
        }


        //Center the title bar text. Thanks Overstack.
        private void UpdateTextPosition()
        {
            Graphics g = this.CreateGraphics();
            Double startingPoint = (this.Width / 2) - (g.MeasureString(this.Text.Trim(), this.Font).Width / 2);
            Double widthOfASpace = g.MeasureString(" ", this.Font).Width;
            String tmp = " ";
            Double tmpWidth = 0;

            while ((tmpWidth + widthOfASpace) < startingPoint)
            {
                tmp += " ";
                tmpWidth += widthOfASpace;
            }

            this.Text = tmp + this.Text.Trim();
        }
    }
}