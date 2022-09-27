using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MathQuiz
{
    public partial class Form1 : Form
    {
        public Form1()
        {         
            InitializeComponent();
            currentDate = DateTime.Now;
            date.Text = $"{currentDate.Day} {currentDate.ToString("MMMM")} {currentDate.Year}";
        }

        public void StartTheQuiz()
        {
            // Fill in the addition problems.
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();
            sum.Value = 0;

            // Fill in the subtraction problems.
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Fill in the multiplication problems.
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Fill in the division problems.
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Start the timer.
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        /// <summary>
        /// Check the answers to see if the user got everything right.
        /// </summary>
        /// <returns>True if the answer's correct, false otherwise.</returns>
        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }



        // Create a Random object to 
        // generate random numbers.
        Random randomizer = new Random();

        // Integers for the addition problems.
        int addend1;
        int addend2;

        // Integers for the subtraction problems.
        int minuend;
        int subtrahend;

        // Integers for the multiplication problems.
        int multiplicand;
        int multiplier;

        // Integers for the division problems.
        int dividend;
        int divisor;

        // Integer to track time left for quiz
        int timeLeft;

        // DateTIme to retrieve today's date.
        DateTime currentDate;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                // If CheckTheAnswer() returns true, then the user 
                // got the answer right. Stop the timer  
                // and show a MessageBox.
                PlaySound();
                timer1.Stop();
                timeLabel.BackColor = Color.White;
                MessageBox.Show("You got all the answers right!",
                                "Congratulations!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                // If there are less than 5 seconds left,
                // turn the timer background color to red.
                if (timeLeft < 7) timeLabel.BackColor = Color.Red;

                // If not all answers are correct, continue the
                // countdown.
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
            }
            else
            {
                // If the user ran out of time, stop the timer, show
                // a MessageBox, and fill in the answers.
                timer1.Stop();
                timeLabel.BackColor = Color.White;
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Select the while answer in the NumericUpDown control
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
;        }

        private void single_Check(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                NumericUpDown answerBox = sender as NumericUpDown;
                switch (answerBox.Name)
                {
                    case "sum":
                        if (addend1 + addend2 == sum.Value) PlaySound();
                        break;
                    case "difference":
                        if (minuend - subtrahend == difference.Value) PlaySound();
                        break;
                    case "product":
                        if (multiplicand * multiplier == product.Value) PlaySound();
                        break;
                    case "quotient":
                        if (dividend / divisor == quotient.Value) PlaySound();
                        break;
                }
            }

        }

        private void PlaySound()
        {
            System.Media.SoundPlayer player =
                new System.Media.SoundPlayer();
            player.SoundLocation = Path.GetFullPath(@"..\..\Assets\Correct.wav");
            player.Load();
            player.Play();
        }
    }
}
