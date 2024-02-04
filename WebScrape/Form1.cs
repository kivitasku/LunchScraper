using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ravintolat;

namespace WebScrape
{
    public partial class Form1 : Form
    {
        Searcher etsija;

        public Form1()
        {
            InitializeComponent();
            etsija = new Searcher();
        }


        private void Empty_Click(object sender, System.EventArgs e)
        {

            resultTextBox.Clear();
        }

        private void Search_Click(object sender, System.EventArgs e)
        {

           if (todayALLRB.Checked)
            {

                etsija.raflatDay.Clear();
                
                etsija.gatherDayMeals();


                foreach (RavintolaDay rafla in etsija.raflatDay)
                {
                    resultTextBox.Text += rafla.getNimi() + "\n";
                    foreach (string ruoka in rafla.getRuoat())
                    {
                        resultTextBox.Text += ruoka + "\n";
                    }
                    resultTextBox.Text += "-----------------------------------" + "\n";

                }
                resultTextBox.Text += "Hakutuloksia: " + etsija.raflatDay.Count + "\n";

            }

           else if (todayRB.Checked)
            {
                etsija.raflatDay.Clear();
                resultTextBox.Text += "Etsitään sanalla: " + searchTextBox.Text + "\n";
                etsija.gatherDayWord(searchTextBox.Text);


                foreach (RavintolaDay rafla in etsija.raflatDay)
                {
                    resultTextBox.Text += rafla.getNimi() + "\n";
                    foreach (string ruoka in rafla.getRuoat())
                    {
                        resultTextBox.Text += ruoka + "\n";
                    }
                    resultTextBox.Text += "-----------------------------------" + "\n";

                }
                resultTextBox.Text += "Hakutuloksia: " + etsija.raflatDay.Count + "\n";
            }

           else if (weekRB.Checked)
            {


                etsija.raflatWeek.Clear();
                etsija.gatherWeekMeals(searchTextBox.Text);
                foreach (RavintolaWeek rafla in etsija.raflatWeek)
                {
                    resultTextBox.Text += rafla.getNimi() + "\n";
                    resultTextBox.Text += "--------------------------------------" + "\n";
                    foreach (FoodWeek paivanruoka in rafla.getRuoat())
                    {
                        resultTextBox.Text += "-----" + paivanruoka.paiva + "\n";
                        foreach (string ruoka in paivanruoka.getRuoat())
                        {
                            resultTextBox.Text += ruoka + "\n";
                        }
                    }
                    resultTextBox.Text += "\n";
                    resultTextBox.Text += "\n";
                }
                

                

                
            }


        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void info_Click(object sender, EventArgs e)
        {
            const string message = "Päivän hakutoiminto: Sovellus etsii sen hetkisen päivän lounaslistat kokonaisuudessaan tai rajattuna \nViikon hakutoiminto: Sovellus etsii koko sen hetkisen viikon lounaslistoista hakusanaa vastaavaa annosta, kuitenkin vain niistä ravintoloista jotka tarjoavat hakuhetkellä lounasta (suodatetaan ravintoloita jotka eivät näy avoimen rajapinnan kautta) \nOiva Kupila - 2023 - 1.0.0";
            var result = MessageBox.Show(message, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Question);
            
        }
    }





}
