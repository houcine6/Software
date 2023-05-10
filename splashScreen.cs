using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MATDESQ2
{
    public partial class splashScreen : Form
    {
        public splashScreen()
        {
            InitializeComponent();
        }

        private void splashScreen_Load(object sender, EventArgs e)
        {
            // Set the form's shape to a rounded rectangle
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int arcSize = 50; // Set the arc size to your desired value
            path.AddArc(0, 0, arcSize, arcSize, 180, 90); // Top left corner
            path.AddArc(this.Width - arcSize, 0, arcSize, arcSize, 270, 90); // Top right corner
            path.AddArc(this.Width - arcSize, this.Height - arcSize, arcSize, arcSize, 0, 90); // Bottom right corner
            path.AddArc(0, this.Height - arcSize, arcSize, arcSize, 90, 90); // Bottom left corner
            this.Region = new System.Drawing.Region(path);
        }

        int iBDD = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            iBDD++;

            if (iBDD == 1)
            {
                try
                {
                    // TODO: This line of code loads data into the 'databaseApplicationDataSet.ClientV1' table. You can move, or remove it, as needed.
                    this.clientV1TableAdapter.Fill(this.databaseApplicationDataSet.ClientV1);
                    materialProgressBar1.Increment(20);
                    Console.WriteLine("Bdd1 charge");
                }
                catch (Exception eBDD)
                {
                    timer1.Enabled = false;
                    if (MessageBox.Show("La Base de Données n'a pas été Chargée Correctement. Veuillez Redémarrer le logiciel.", "Erreur", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                }
            }
            if (iBDD == 2)
            {
                try
                {
                    // TODO: This line of code loads data into the 'databaseApplicationDataSet.FactureV1' table. You can move, or remove it, as needed.
                    this.factureV1TableAdapter.Fill(this.databaseApplicationDataSet.FactureV1);
                    materialProgressBar1.Increment(20);
                    Console.WriteLine("Bdd2 charge");
                }
                catch (Exception eBDD)
                {
                    timer1.Enabled = false;
                    if (MessageBox.Show("La Base de Données n'a pas été Chargée Correctement. Veuillez Redémarrer le logiciel.", "Erreur", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                }
            }
            if (iBDD == 3)
            {
                try
                {
                    // TODO: This line of code loads data into the 'databaseApplicationDataSet.NumFactV1' table. You can move, or remove it, as needed.
                    this.numFactV1TableAdapter.Fill(this.databaseApplicationDataSet.NumFactV1);
                    materialProgressBar1.Increment(20);
                    Console.WriteLine("Bdd3 charge");
                }
                catch (Exception eBDD)
                {
                    timer1.Enabled = false;
                    if (MessageBox.Show("La Base de Données n'a pas été Chargée Correctement. Veuillez Redémarrer le logiciel.", "Erreur", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                }
            }
            if (iBDD == 4)
            {
                try
                {
                    
                    // TODO: This line of code loads data into the 'databaseApplicationDataSet.stockV4' table. You can move, or remove it, as needed.
                    this.stockV4TableAdapter.Fill(this.databaseApplicationDataSet.stockV4);
                    
                    materialProgressBar1.Increment(20);
                    Console.WriteLine("Bdd4 charge");
                }
                catch (Exception eBDD)
                {
                    timer1.Enabled = false;
                    if (MessageBox.Show("La Base de Données n'a pas été Chargée Correctement. Veuillez Redémarrer le logiciel.", "Erreur", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                }
            }
            if (iBDD == 5)
            {
                try
                {
                    // TODO: This line of code loads data into the 'databaseApplicationDataSet.HistoriqueV2' table. You can move, or remove it, as needed.
                    this.historiqueV2TableAdapter.Fill(this.databaseApplicationDataSet.HistoriqueV2);
                    materialProgressBar1.Increment(20);
                    Console.WriteLine("Bdd5 charge");
                }
                catch (Exception eBDD)
                {
                    timer1.Enabled = false;
                    if (MessageBox.Show("La Base de Données n'a pas été Chargée Correctement. Veuillez Redémarrer le logiciel.", "Erreur", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                }
            }

            if (materialProgressBar1.Value == 100)
            {
                label6.Text = "Chargement complet. Démarrage en cours.";
                if (iBDD == 8)
                {
                    timer1.Enabled = false;
                    MaterialForm1 form = new MaterialForm1();
                    form.Show();
                    this.Hide();
                }
            }
        }
    }
}
