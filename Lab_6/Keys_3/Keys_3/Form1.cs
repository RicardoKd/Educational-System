using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Keys_3 {

    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
        }

        private string text1 = "Operation Grapple was a series of British nuclear weapons tests carried out in 1957 and 1958 at Malden Island and Christmas Island in what is now Kiribati. Britain had successfully tested an atomic bomb in October 1952, and in July 1954, decided to develop a hydrogen bomb.";
        private string text2 = "The treaty provided for the sale to the UK of one complete nuclear submarine propulsion plant, as well as ten years' supply of enriched uranium to fuel it. Other nuclear material was also acquired from the US under the treaty. Some 5.4 tonnes of UK-produced plutonium was sent to the US in return for 6.7 kilograms (15 lb) of tritium and 7.5 tonnes of highly enriched uranium (HEU) between 1960 and 1979, but much of the HEU was used not for weapons but as fuel for the growing fleet of British nuclear submarines.";
        private string text3 = "During the early part of the Second World War, Britain had a nuclear weapons project, codenamed Tube Alloys.[2] At the Quadrant Conference in August 1943, the Prime Minister of the United Kingdom, Winston Churchill, and the President of the United States, Franklin Roosevelt, signed the Quebec Agreement, which merged Tube Alloys with the American Manhattan Project to create a combined British, American and Canadian project.[3] The Quebec Agreement established the Combined Policy Committee and the Combined Development Trust to co-ordinate their efforts.[4] Many of Britain's top scientists participated in the Manhattan Project.";

        private DateTime start;

        private void button1_Click(object sender, EventArgs e) {
            TimeSpan timeSpent = DateTime.Now - start;
            string rtb1Text = Convert.ToString(richTextBox1.Text);
            string rtb2Text = Convert.ToString(richTextBox2.Text);
            int symbolsWritten = rtb2Text.Length;
            int symbolsToWrite = rtb1Text.Length;

            int spm = Convert.ToInt32(symbolsWritten / timeSpent.TotalMinutes);

            int incorrectSymbols = 0;
            for (int i = 0; i < symbolsToWrite; i++) {
                if (i < symbolsWritten) {
                    if (rtb1Text[i] != rtb2Text[i])
                        incorrectSymbols++;
                } else {
                    incorrectSymbols += symbolsToWrite - symbolsWritten;
                    break;
                }
            }

            label3.Text = "Result:\nIncorrect symbols: " + incorrectSymbols + "\nYou wrote " + symbolsWritten + " symbols in " + timeSpent.TotalSeconds + " seconds\nSpeed: " + spm + " symbols per minute";
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e) {
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            richTextBox1.Text = text1;
            start = DateTime.Now;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {
            richTextBox1.Text = text2;
            start = DateTime.Now;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) {
            richTextBox1.Text = text3;
            start = DateTime.Now;
        }
    }
}