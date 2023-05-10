using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MATDESQ2
{
    public partial class MaterialForm1 : MaterialForm
    {
        public MaterialForm1()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
            Primary.Grey800,
            Primary.Grey900,
            Primary.Green50,
            Accent.Red700,
            TextShade.WHITE);

            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        public void LoadDB()
        {
            // TODO: This line of code loads data into the 'databaseApplicationDataSet.ClientV1' table. You can move, or remove it, as needed.
            this.clientV1TableAdapter.Fill(this.databaseApplicationDataSet.ClientV1);
            // TODO: This line of code loads data into the 'databaseApplicationDataSet.stockV4' table. You can move, or remove it, as needed.
            this.stockV4TableAdapter.Fill(this.databaseApplicationDataSet.stockV4);
            // TODO: This line of code loads data into the 'databaseApplicationDataSet.HistoriqueV2' table. You can move, or remove it, as needed.
            this.historiqueV2TableAdapter.Fill(this.databaseApplicationDataSet.HistoriqueV2);
            // TODO: This line of code loads data into the 'databaseApplicationDataSet.NumFactV1' table. You can move, or remove it, as needed.
            this.numFactV1TableAdapter.Fill(this.databaseApplicationDataSet.NumFactV1);
            // TODO: This line of code loads data into the 'databaseApplicationDataSet.FactureV1' table. You can move, or remove it, as needed.
            this.factureV1TableAdapter.Fill(this.databaseApplicationDataSet.FactureV1);

            materialButton4.Enabled = false;
            materialButton5.Enabled = false;

            materialButton7.Enabled = false;
            materialButton8.Enabled = false;

            materialButton17.Enabled = false;
            materialButton16.Enabled = false;
        }


        decimal tpcf = 0, TVAf = 0, TTCf = 0;
        public void calculs()
        {
            // initialize variables
            decimal montant = 0;
            decimal tva = 0;
            decimal ttc = 0;

            // loop through rows in dataGridView9
            for (int i = 0; i < dataGridView9.Rows.Count - 1; i++)
            {
                // get value in column 7 (E2)
                string e2Value = dataGridView9.Rows[i].Cells[7].Value.ToString();

                // if E2 is not empty
                if (!string.IsNullOrEmpty(e2Value))
                {
                    // add values to variables
                    montant += decimal.Parse(dataGridView9.Rows[i].Cells[7].Value.ToString());
                    tva += decimal.Parse(dataGridView9.Rows[i].Cells[8].Value.ToString());
                    ttc += decimal.Parse(dataGridView9.Rows[i].Cells[9].Value.ToString());
                }
            }

            // set label texts
            materialLabel47.Text = montant.ToString();
            materialLabel49.Text = tva.ToString();
            materialLabel53.Text = ttc.ToString();

            tpcf = montant;
            TTCf = ttc;
            TVAf = tva;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'databaseApplicationDataSet.NumFactV1' table. You can move, or remove it, as needed.
            this.numFactV1TableAdapter.Fill(this.databaseApplicationDataSet.NumFactV1);
            // TODO: This line of code loads data into the 'databaseApplicationDataSet.FactureV1' table. You can move, or remove it, as needed.
            this.factureV1TableAdapter.Fill(this.databaseApplicationDataSet.FactureV1);
            // TODO: This line of code loads data into the 'databaseApplicationDataSet.HistoriqueV2' table. You can move, or remove it, as needed.
            this.historiqueV2TableAdapter.Fill(this.databaseApplicationDataSet.HistoriqueV2);
            // TODO: This line of code loads data into the 'databaseApplicationDataSet.ClientV1' table. You can move, or remove it, as needed.
            this.clientV1TableAdapter.Fill(this.databaseApplicationDataSet.ClientV1);
            // TODO: This line of code loads data into the 'databaseApplicationDataSet.stockV4' table. You can move, or remove it, as needed.
            this.stockV4TableAdapter.Fill(this.databaseApplicationDataSet.stockV4);

            // Create a new timer object with an interval of 1000 milliseconds (1 second)
            Timer timer = new Timer();
            timer.Interval = 1000;

            // Attach an event handler to the timer's Tick event
            timer.Tick += timer_Tick;

            // Start the timer
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            // Update the label with the current time
            materialLabel19.Text = DateTime.Now.ToString("hh:mm:ss tt");

            // Update the text of the date label every second in the format of "MM/dd/yyyy"
            materialLabel20.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }

        private void materialLabel1_Click(object sender, EventArgs e)
        {

        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            AjoutStock2 ajoutStock2 = new AjoutStock2(this, tourD);
            ajoutStock2.Show();

            materialButton4.Enabled = false;
            dataGridView1.ClearSelection();
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            materialTextBox1.Text = "";
            stockV4BindingSource.Filter = string.Format("Nom LIKE '%" + materialTextBox1.Text + "%'");
        }

        string Id;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the currently selected row
            DataGridViewRow selectedRow = dataGridView1.CurrentRow;

            // Get the value of the second cell (index 1) in the selected row
            object nomObject = selectedRow.Cells[1].Value;
            string nom = nomObject != DBNull.Value ? nomObject.ToString() : null;

            // Check if the value of the second cell is not null
            if (nom != null)
            {
                // Get the value of the first cell in the selected row
                Id = selectedRow.Cells[0].Value.ToString();

                // Set the current row selection to true and enable a button
                selectedRow.Selected = true;
                materialButton4.Enabled = true;
                materialButton5.Enabled = true;
            }
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            // Pass the value to the ModifierStock form and open it
            ModifierStock modifierStock = new ModifierStock(this, Id, tourD);
            modifierStock.Show();

        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            // Prompt the user for confirmation before deleting the row
            DialogResult result = MessageBox.Show("Êtes-vous Sûr de Vouloir Supprimer Cette Ligne ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SqlConnection connection = null;
                try
                {
                    // Define connection string
                    string connectionString = Properties.Settings.Default.DatabaseApplicationConnectionString;

                    // Define SQL query to delete row with specific Id
                    string query = "DELETE FROM [dbo].[stockV4] WHERE Id = @id";

                    // Create connection object
                    using (connection = new SqlConnection(connectionString))
                    {
                        // Create command object with query and connection
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Add parameter for Id value
                            command.Parameters.AddWithValue("@id", Id); // Replace idValue with the actual Id value you want to delete

                            // Open connection
                            connection.Open();

                            // Execute command
                            int rowsAffected = command.ExecuteNonQuery();

                            LoadDB();

                            // Row was deleted successfully
                            MessageBox.Show(string.Format("L'article Sélectionné a été supprimé avec Succès."), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    MessageBox.Show("Une Erreur s'est Produite Lors de la Suppression de la Ligne. Détails de l'erreur : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Close connection
                    if (connection != null)
                    {
                        connection.Close();
                    }
                }
            }

        }

        private void materialTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(materialTextBox1.Text))
                {
                    stockV4BindingSource.Filter = string.Format("Nom LIKE '%" + materialTextBox1.Text + "%'");
                }
                else
                {
                    stockV4BindingSource.Filter = string.Empty;
                }
            }
        }
        
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int numRowsstock;
            decimal totalAchat = 0.00m;
            decimal totalMontant = 0.00m;

            // Get the number of rows in the DataGridView
            numRowsstock = dataGridView1.Rows.Count -1;

            // Loop through each row and add up the values in column 4 and column 6
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                // Check if the first cell in the row is empty
                if (dataGridView1.Rows[i].Cells[0].Value == null || dataGridView1.Rows[i].Cells[0].Value == DBNull.Value)
                {

                }
                else
                {
                    // Add up the values in columns 4 and 6
                    totalMontant += Convert.ToDecimal(dataGridView1.Rows[i].Cells[6].Value);
                    totalAchat += (Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value)) * (Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value));
                }
            }

            // Set the label's Text property to the number of rows
            materialLabel3.Text = "" + numRowsstock.ToString();
            materialLabel4.Text = "" + numRowsstock.ToString();

            string formattedtotalAchat = totalAchat.ToString("#,##0.00", new System.Globalization.CultureInfo("fr-FR"));
            materialLabel26.Text = formattedtotalAchat.Replace(".", ",") + " Da";
            string formattedtotalMontant = totalMontant.ToString("#,##0.00", new System.Globalization.CultureInfo("fr-FR"));
            materialLabel28.Text = formattedtotalMontant.Replace(".", ",") + " Da";
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            AjoutClient ajoutClient = new AjoutClient(this, tourD);
            ajoutClient.Show();
        }

        private void materialButton8_Click(object sender, EventArgs e)
        {
            // Pass the value to the ModifierStock form and open it
            ModifierClient modifierClient = new ModifierClient(this, IdC, tourD);
            modifierClient.Show();
        }

        int numRowsClient;
        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Get the number of rows in the DataGridView
            numRowsClient = dataGridView2.Rows.Count - 1;

            // Set the label's Text property to the number of rows
            materialLabel11.Text = "" + numRowsClient.ToString();
            //acceille
            materialLabel5.Text = "" + numRowsClient.ToString();

            // Calculate the sum of the values in column 4
            double sum = 0;
            for (int i = 0; i < dataGridView2.Rows.Count; ++i)
            {
                DataGridViewRow row = dataGridView2.Rows[i];
                if (row.Cells[4].Value != null && row.Cells[4].Value != DBNull.Value)
                {
                    sum += Convert.ToDouble(row.Cells[4].Value);
                }
            }

            // Display the sum in the label
            materialLabel7.Text = sum.ToString("0.00")+" Da";
        }

        string IdC;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the currently selected row
            DataGridViewRow selectedRow = dataGridView2.CurrentRow;

            // Get the value of the second cell (index 1) in the selected row
            object nomObject = selectedRow.Cells[1].Value;
            string nom = nomObject != DBNull.Value ? nomObject.ToString() : null;

            // Check if the value of the second cell is not null
            if (nom != null)
            {
                // Get the value of the first cell in the selected row
                IdC = selectedRow.Cells[0].Value.ToString();

                // Set the current row selection to true and enable a button
                selectedRow.Selected = true;
                materialButton8.Enabled = true;
                materialButton7.Enabled = true;
            }
        }

        private void materialButton7_Click(object sender, EventArgs e)
        {
            // Prompt the user for confirmation before deleting the row
            DialogResult result = MessageBox.Show("Êtes-vous Sûr de Vouloir Supprimer Cette Ligne ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SqlConnection connection = null;
                try
                {
                    // Define connection string
                    string connectionString = Properties.Settings.Default.DatabaseApplicationConnectionString;

                    // Define SQL query to delete row with specific Id
                    string query = "DELETE FROM [dbo].[ClientV1] WHERE Id = @id";

                    // Create connection object
                    using (connection = new SqlConnection(connectionString))
                    {
                        // Create command object with query and connection
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Add parameter for Id value
                            command.Parameters.AddWithValue("@id", IdC); // Replace idValue with the actual Id value you want to delete

                            // Open connection
                            connection.Open();

                            // Execute command
                            int rowsAffected = command.ExecuteNonQuery();

                            LoadDB();

                            // Row was deleted successfully
                            MessageBox.Show(string.Format("Le Client Sélectionné a été supprimé avec Succès."), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    MessageBox.Show("Une Erreur s'est Produite Lors de la Suppression de la Ligne. Détails de l'erreur : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Close connection
                    if (connection != null)
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void materialButton6_Click(object sender, EventArgs e)
        {
            materialTextBox2.Text = "";
            clientV1BindingSource.Filter = string.Format("Nom LIKE '%" + materialTextBox2.Text + "%'");
        }

        private void materialTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(materialTextBox2.Text))
                {
                    clientV1BindingSource.Filter = string.Format("Nom LIKE '%" + materialTextBox2.Text + "%'");
                }
                else
                {
                    clientV1BindingSource.Filter = string.Empty;
                }
            }
        }

        string nom;
        decimal quantite;
        decimal prixAchat;
        decimal prixVente;
        int IdArticle;
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // make sure the click is on a valid row
            {
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() != "")
                {
                    dataGridView3.CurrentRow.Selected = true;

                    IdArticle = Convert.ToInt32(dataGridView3.CurrentRow.Cells[0].Value);
                    nom = dataGridView3.CurrentRow.Cells[1].Value.ToString();
                    quantite = Convert.ToDecimal(dataGridView3.CurrentRow.Cells[2].Value.ToString());
                    prixAchat = Convert.ToDecimal(dataGridView3.CurrentRow.Cells[3].Value.ToString());
                    prixVente = Convert.ToDecimal(dataGridView3.CurrentRow.Cells[4].Value.ToString());

                    materialTextBox24.Text = prixVente.ToString();

                }
                else
                {
                    
                }
            }
            else
            {

            }
        }

        private void materialTextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(materialTextBox3.Text))
                {
                    stockV4BindingSource.Filter = string.Format("Nom LIKE '%" + materialTextBox3.Text + "%'");
                }
                else
                {
                    stockV4BindingSource.Filter = string.Empty;
                }
            }
        }

        private void materialButton9_Click(object sender, EventArgs e)
        {
            materialTextBox3.Text = "";
            stockV4BindingSource.Filter = string.Format("Nom LIKE '%" + materialTextBox3.Text + "%'");
        }

        private void materialTextBox22_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != ',' && ch != 8)
            {
                e.Handled = true;
                return;
            }
        }

        private void materialTextBox24_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != ',' && ch != 8)
            {
                e.Handled = true;
                return;
            }
        }

        String quantity, price, mon; // pour la liste
        String date2;
        List<string> printList = new List<string>();
        List<string> printListCF = new List<string>();
        List<int> idHistoriqueList = new List<int>();
        List<int> idFACTUREList = new List<int>();

        String day = DateTime.Now.ToString("d");

        decimal Mont = 0;
        decimal TP = 0;

        decimal qteSaisi;

        private void materialFloatingActionButton5_Click(object sender, EventArgs e)
        {
            materialLabel13.Text = "0";
            materialLabel30.Text = "0";
            materialLabel32.Text = "0";
            materialTextBox22.Text = "";
            materialTextBox24.Text = "";
            TP = 0;
            Tttc = 0;
            Ttva = 0;
            dataGridView4.Rows.Clear();
            printList.Clear();
            idHistoriqueList.Clear();
            materialButton13.Enabled = false;
            dataGridView3.ClearSelection();

            nom = "";
            qteSaisi = 0;
            quantite = 0;
            prixSaisi = 0;
            prixVente = 0;
            Mont = 0;
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView4.Rows[e.RowIndex].Cells[0].Value != null)
            {
                dataGridView4.Rows[e.RowIndex].Selected = true;
                materialButton13.Enabled = true;

                // Get the corresponding index from the idHistoriqueList
                IDH = idHistoriqueList[e.RowIndex];
            }
        }
        int IDH,IDF;
        private void materialButton13_Click(object sender, EventArgs e)
        {
            if (dataGridView4.CurrentRow.Selected == true)
            {
                if (dataGridView4.CurrentRow.Cells[0].Value != null)
                {
                    // Get the values of the selected row
                    string nomRetour = dataGridView4.CurrentRow.Cells[0].Value.ToString();
                    decimal quantiteRetour = Convert.ToDecimal(dataGridView4.CurrentRow.Cells[1].Value.ToString());
                    decimal mont = Convert.ToDecimal(dataGridView4.CurrentRow.Cells[3].Value.ToString());
                    decimal tva = Convert.ToDecimal(dataGridView4.CurrentRow.Cells[4].Value.ToString());
                    decimal ttc = Convert.ToDecimal(dataGridView4.CurrentRow.Cells[5].Value.ToString());

                    // Use the retrieved values as needed
                    // Update the 'Quantité' value in the 'stockV4' table for the selected 'Nom'
                    try
                    {
                        string connectionString = Properties.Settings.Default.DatabaseApplicationConnectionString;

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open(); 

                            string query = "SELECT Id FROM [dbo].[stockV4] WHERE Nom = @nom";

                            using (SqlCommand command1 = new SqlCommand(query, connection))
                            {
                                command1.Parameters.AddWithValue("@nom", nomRetour);

                                SqlDataReader reader = command1.ExecuteReader();

                                if (reader.Read())
                                {
                                    int id = reader.GetInt32(0);
                                    reader.Close(); // close the reader before executing the update query
                                    string updateQuery = "UPDATE stockV4 SET Quantité = Quantité + @QuantiteRetour WHERE Nom = @NomRetour AND id = @id";

                                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                                    {
                                        command.Parameters.AddWithValue("@NomRetour", nomRetour);
                                        command.Parameters.AddWithValue("@QuantiteRetour", quantiteRetour);
                                        command.Parameters.AddWithValue("@id", id);

                                        int rowsAffected = command.ExecuteNonQuery();
                                        
                                        dataGridView4.Rows.RemoveAt(dataGridView4.SelectedRows[0].Index);
                                    }
                                }
                                reader.Close();

                                //deliting row 
                                // Define SQL query to delete row with specific Id
                                string query2 = "DELETE FROM [dbo].[HistoriqueV2] WHERE Id = @id";

                                // Create command object with query and connection
                                using (SqlCommand command = new SqlCommand(query2, connection))
                                {
                                    // Add parameter for Id value
                                    command.Parameters.AddWithValue("@id", IDH); // Replace idValue with the actual Id value you want to delete

                                    // Execute command
                                    int rowsAffected = command.ExecuteNonQuery();

                                    dataGridView5.CurrentRow.Selected = false;

                                    idHistoriqueList.RemoveAll(x => x == IDH);
                                }
                            }
                            connection.Close();
                        }
                        LoadDB();

                        TP = TP - mont;
                        string formattedTP = TP.ToString("#,##0.00", new System.Globalization.CultureInfo("fr-FR"));
                        materialLabel13.Text = formattedTP.Replace(".", ",");

                        Ttva = Ttva - tva;
                        Tttc = Tttc - ttc;
                        materialLabel30.Text = "" + Ttva;
                        materialLabel32.Text = "" + Tttc;

                        tva = 0;
                        ttc = 0;
                    }
                    catch (Exception ex)
                    {
                        // Display error message
                        MessageBox.Show("Une Erreur est survenue lors du Retour du produit:\n" + ex.Message);
                    }

                    materialButton13.Enabled = false;
                }
                else
                {
                    // Show an error message if the 'Nom' value is null
                    MessageBox.Show("Veuillez choisir un Article à retourner au Stock.", "Erreur");
                }
            }
            else
            {
                // Show an error message or handle the case where no row is selected
                MessageBox.Show("Veuillez choisir un Article à retourner au Stock.", "Erreur");
            }
        }

        private void materialButton11_Click(object sender, EventArgs e)
        {
            materialTextBox4.Text = "";
            historiqueV2BindingSource.Filter = string.Format("Nom LIKE '%" + materialTextBox4.Text + "%'");
        }

        private void materialTextBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(materialTextBox4.Text))
                {
                    historiqueV2BindingSource.Filter = string.Format("Nom LIKE '%" + materialTextBox4.Text + "%'");
                }
                else
                {
                    historiqueV2BindingSource.Filter = string.Empty;
                }
            }
        }

        string IdH;
        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the currently selected row
            DataGridViewRow selectedRow = dataGridView5.CurrentRow;

            // Get the value of the second cell (index 1) in the selected row
            object nomObject = selectedRow.Cells[1].Value;
            string nom = nomObject != DBNull.Value ? nomObject.ToString() : null;

            // Check if the value of the second cell is not null
            if (nom != null)
            {
                // Get the value of the first cell in the selected row
                IdH = selectedRow.Cells[0].Value.ToString();

                // Set the current row selection to true and enable a button
                selectedRow.Selected = true;
                materialButton17.Enabled = true;
                materialButton16.Enabled = true;
            }
        }

        private void materialButton16_Click(object sender, EventArgs e)
        {
            // Prompt the user for confirmation before deleting the row
            DialogResult result = MessageBox.Show("Êtes-vous Sûr de Vouloir Supprimer Cette Ligne ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SqlConnection connection = null;
                try
                {
                    // Define connection string
                    string connectionString = Properties.Settings.Default.DatabaseApplicationConnectionString;

                    // Define SQL query to delete row with specific Id
                    string query = "DELETE FROM [dbo].[HistoriqueV2] WHERE Id = @id";

                    // Create connection object
                    using (connection = new SqlConnection(connectionString))
                    {
                        // Create command object with query and connection
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Add parameter for Id value
                            command.Parameters.AddWithValue("@id", IdH); // Replace idValue with the actual Id value you want to delete

                            // Open connection
                            connection.Open();

                            // Execute command
                            int rowsAffected = command.ExecuteNonQuery();

                            LoadDB();

                            // Row was deleted successfully
                            MessageBox.Show(string.Format("La ligne sélectionnée a été supprimée avec succès."), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    MessageBox.Show("Une erreur s'est produite lors de la suppression de la ligne. Détails de l'erreur : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Close connection
                    if (connection != null)
                    {
                        connection.Close();
                    }
                }
            }

        }

        private void materialButton17_Click(object sender, EventArgs e)
        {
            if (dataGridView5.CurrentRow.Selected == true)
            {
                if (dataGridView5.CurrentRow.Cells[0].Value != null)
                {
                    // Get the values of the selected row
                    int idh = Convert.ToInt32(dataGridView5.CurrentRow.Cells[0].Value);
                    string nomRetour = dataGridView5.CurrentRow.Cells[1].Value.ToString();
                    decimal quantiteRetour = Convert.ToDecimal(dataGridView5.CurrentRow.Cells[2].Value.ToString());

                    // Use the retrieved values as needed
                    // Update the 'Quantité' value in the 'stockV4' table for the selected 'Nom'
                    try
                    {
                        string connectionString = Properties.Settings.Default.DatabaseApplicationConnectionString;

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open(); 

                            string query = "SELECT Id FROM [dbo].[stockV4] WHERE Nom = @nom";

                            using (SqlCommand command1 = new SqlCommand(query, connection))
                            {
                                command1.Parameters.AddWithValue("@nom", nomRetour);

                                SqlDataReader reader = command1.ExecuteReader();

                                if (reader.Read())
                                {
                                    int id = reader.GetInt32(0);
                                    reader.Close(); // close the reader before executing the update query
                                    string updateQuery = "UPDATE stockV4 SET Quantité = Quantité + @QuantiteRetour WHERE Nom = @NomRetour AND id = @id";

                                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                                    {
                                        command.Parameters.AddWithValue("@NomRetour", nomRetour);
                                        command.Parameters.AddWithValue("@QuantiteRetour", quantiteRetour);
                                        command.Parameters.AddWithValue("@id", id);

                                        int rowsAffected = command.ExecuteNonQuery();
                                    }
                                }
                                reader.Close();

                                //deliting row 
                                // Define SQL query to delete row with specific Id
                                string query2 = "DELETE FROM [dbo].[HistoriqueV2] WHERE Id = @id";

                                // Create command object with query and connection
                                using (SqlCommand command = new SqlCommand(query2, connection))
                                {
                                    // Add parameter for Id value
                                    command.Parameters.AddWithValue("@id", idh); // Replace idValue with the actual Id value you want to delete

                                    // Execute command
                                    int rowsAffected = command.ExecuteNonQuery();

                                    dataGridView5.CurrentRow.Selected = false;

                                    // Row was deleted successfully
                                    MessageBox.Show("L'article a été retourné aux stock.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }
                                LoadDB();
                            }
                            connection.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        // Display error message
                        MessageBox.Show("Une Erreur est survenue lors du Retour du produit:\n" + ex.Message);
                    }

                    materialButton17.Enabled = false;
                }
                else
                {
                    // Show an error message if the 'Nom' value is null
                    MessageBox.Show("Veuillez choisir un Article à retourner au Stock.", "Erreur");
                }
            }
            else
            {
                // Show an error message or handle the case where no row is selected
                MessageBox.Show("Veuillez choisir un Article à retourner au Stock.", "Erreur");
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        int tourD = 0;
        private void materialFloatingActionButton6_Click(object sender, EventArgs e)
        {
            if(tourD == 0)
            {
                var materialSkinManager = MaterialSkinManager.Instance;
                materialSkinManager.AddFormToManage(this);
                materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
                tourD = 1;
            }
            else if(tourD == 1)
            {
                var materialSkinManager = MaterialSkinManager.Instance;
                materialSkinManager.AddFormToManage(this);
                materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                tourD = 0;
            }
            
        }

        decimal tp2 = 0;
        decimal ttcBenefice = 0;
        decimal tpnet = 0;
        private void materialButton12_Click(object sender, EventArgs e)
        {
            tp2 = 0;
            ttcBenefice = 0;
            tpnet = 0;
            materialLabel17.Text = "" + tp2;
            materialLabel34.Text = "" + ttcBenefice;
            materialLabel36.Text = "" + tpnet;

            DateTime from = DateTime.ParseExact(dateTimePicker1.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tod = DateTime.ParseExact(dateTimePicker2.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            int leng = dataGridView5.Rows.Count - 1;

            for (int i = 0; i < leng; i++)
            {
                dataGridView5.Rows[i].Selected = true;

                String dateCompare = dataGridView5.Rows[i].Cells[8].Value.ToString();
                DateTime dc = DateTime.Parse(dateCompare);

                dataGridView5.Rows[i].Selected = false;

                if (from <= dc && tod >= dc)
                {

                    decimal price = Convert.ToDecimal(dataGridView5.Rows[i].Cells[4].Value);
                    decimal quantity = Convert.ToDecimal(dataGridView5.Rows[i].Cells[2].Value);
                    decimal pa = Convert.ToDecimal(dataGridView5.Rows[i].Cells[3].Value);
                    decimal ttc = Convert.ToDecimal(dataGridView5.Rows[i].Cells[7].Value);

                    decimal montant = price * quantity;
                    decimal net = (price - pa) * quantity;

                    tp2 = tp2 + montant;
                    ttcBenefice = ttcBenefice + ttc;
                    tpnet = tpnet + net;

                    string formattedTP = tp2.ToString("#,##0.00", new System.Globalization.CultureInfo("fr-FR"));
                    materialLabel34.Text = formattedTP.Replace(".", ",") + " Da";

                    string formattedTtcBenefice = ttcBenefice.ToString("#,##0.00", new System.Globalization.CultureInfo("fr-FR"));
                    materialLabel17.Text = formattedTtcBenefice.Replace(".", ",") + " Da";

                    string formattednet = tpnet.ToString("#,##0.00", new System.Globalization.CultureInfo("fr-FR"));
                    materialLabel36.Text = formattednet.Replace(".", ",") + " Da";

                    montant = 0;
                    net = 0;
                    ttc = 0;
                }

            }
        }

        private void materialButton14_Click(object sender, EventArgs e)
        {
            // Prompt the user for confirmation before deleting the row
            DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer toutes les lignes de l'historique ?", "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string connectionString = Properties.Settings.Default.DatabaseApplicationConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "DELETE FROM HistoriqueV2 WHERE Id >= 0";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"Supprimé {rowsAffected} lignes de l'historique.", "Confirmation de suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    LoadDB();
                    connection.Close();
                }
            }

        }

        private void dataGridView5_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int salesCount = 0;
            decimal profit = 0.00m;
            DateTime today = DateTime.Today;

            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                if (row.Cells[8].Value != null && DateTime.Parse(row.Cells[8].Value.ToString()).Date == today)
                {
                    salesCount++;
                    profit += Convert.ToDecimal(row.Cells[5].Value);
                }
            }


            materialLabel9.Text = salesCount.ToString();

            string formattedTP = profit.ToString("#,##0.00", new System.Globalization.CultureInfo("fr-FR"));
            materialLabel21.Text = formattedTP.Replace(".", ",")+" Da";
        }

        private void tabPage5_Paint(object sender, PaintEventArgs e)
        {
            // Show a message box to confirm that the user wants to exit the application
            DialogResult result = MessageBox.Show("Voulez-vous vraiment quitter l'application ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // If the user clicked the "Yes" button, close the application
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void dataGridView7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // make sure the click is on a valid row
            {
                DataGridViewRow row = dataGridView7.Rows[e.RowIndex];
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() != "")
                {
                    dataGridView7.CurrentRow.Selected = true;

                    IdArticle = Convert.ToInt32(dataGridView7.CurrentRow.Cells[0].Value);
                    nom = dataGridView7.CurrentRow.Cells[1].Value.ToString();
                    quantite = Convert.ToDecimal(dataGridView7.CurrentRow.Cells[2].Value.ToString());
                    prixAchat = Convert.ToDecimal(dataGridView7.CurrentRow.Cells[3].Value.ToString());
                    prixVente = Convert.ToDecimal(dataGridView7.CurrentRow.Cells[4].Value.ToString());

                    materialTextBox21.Text = prixVente.ToString();

                }
                else
                {

                }
            }
            else
            {

            }
        }

        private void materialTextBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != ',' && ch != 8)
            {
                e.Handled = true;
                return;
            }
        }

        private void materialTextBox23_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != ',' && ch != 8)
            {
                e.Handled = true;
                return;
            }
        }

        private void materialTextBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(materialTextBox5.Text))
                {
                    stockV4BindingSource.Filter = string.Format("Nom LIKE '%" + materialTextBox5.Text + "%'");
                }
                else
                {
                    stockV4BindingSource.Filter = string.Empty;
                }
            }
        }

        private void materialButton19_Click(object sender, EventArgs e)
        {
            materialTextBox5.Text = "";
            stockV4BindingSource.Filter = string.Format("Nom LIKE '%" + materialTextBox5.Text + "%'");
        }

        private void materialButton20_Click(object sender, EventArgs e)
        {
            materialTextBox6.Text = "";
            stockV4BindingSource.Filter = string.Format("Nom LIKE '%" + materialTextBox6.Text + "%'");

            materialTextBox29.Text = "";
            materialTextBox28.Text = "";
            materialTextBox27.Text = "";
            materialTextBox26.Text = "";
            materialTextBox25.Text = "";
        }

        private void materialTextBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(materialTextBox6.Text))
                {
                    int id;
                    if (int.TryParse(materialTextBox6.Text, out id))
                    {
                        string connectionString = Properties.Settings.Default.DatabaseApplicationConnectionString;
                        string query = "SELECT Nom, Téléphone, Addresse, Crédit FROM ClientV1 WHERE Id = @id";

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", id);
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    materialTextBox29.Text = id.ToString();
                                    materialTextBox28.Text = reader.GetString(0);
                                    materialTextBox27.Text = reader.GetString(1);
                                    materialTextBox26.Text = reader.GetString(2);
                                    materialTextBox25.Text = reader.GetDecimal(3).ToString();
                                }
                            }
                            connection.Close();
                        }
                        
                    }
                    else
                    {
                        // handle invalid input
                    }
                }
                else
                {
                    // clear the text boxes
                    materialTextBox28.Text = "";
                    materialTextBox27.Text = "";
                    materialTextBox26.Text = "";
                    materialTextBox25.Text = "";
                    materialTextBox29.Text = "";
                }
            }
        }

        private void materialButton15_Click(object sender, EventArgs e)
        {
            if (dataGridView6.CurrentRow.Selected == true)
            {
                if (dataGridView6.CurrentRow.Cells[0].Value != null)
                {
                    // Get the values of the selected row
                    string nomRetour = dataGridView6.CurrentRow.Cells[0].Value.ToString();
                    decimal quantiteRetour = Convert.ToDecimal(dataGridView6.CurrentRow.Cells[1].Value.ToString());
                    decimal prix = Convert.ToDecimal(dataGridView6.CurrentRow.Cells[2].Value.ToString());
                    decimal mont = Convert.ToDecimal(dataGridView6.CurrentRow.Cells[3].Value.ToString());
                    decimal tva = Convert.ToDecimal(dataGridView6.CurrentRow.Cells[4].Value.ToString());
                    decimal ttc = Convert.ToDecimal(dataGridView6.CurrentRow.Cells[5].Value.ToString());

                    // Use the retrieved values as needed
                    // Update the 'Quantité' value in the 'stockV4' table for the selected 'Nom'
                    try
                    {
                        string connectionString = Properties.Settings.Default.DatabaseApplicationConnectionString;

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string query = "SELECT Id FROM [dbo].[stockV4] WHERE Nom = @nom";

                            using (SqlCommand command1 = new SqlCommand(query, connection))
                            {
                                command1.Parameters.AddWithValue("@nom", nomRetour);

                                SqlDataReader reader = command1.ExecuteReader();

                                if (reader.Read())
                                {
                                    int id = reader.GetInt32(0);
                                    reader.Close(); // close the reader before executing the update query
                                    string updateQuery = "UPDATE stockV4 SET Quantité = Quantité + @QuantiteRetour WHERE Nom = @NomRetour AND id = @id";

                                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                                    {
                                        command.Parameters.AddWithValue("@NomRetour", nomRetour);
                                        command.Parameters.AddWithValue("@QuantiteRetour", quantiteRetour);
                                        command.Parameters.AddWithValue("@id", id);

                                        int rowsAffected = command.ExecuteNonQuery();

                                        dataGridView6.Rows.RemoveAt(dataGridView6.SelectedRows[0].Index);
                                    }
                                }
                                reader.Close();

                                //deliting row 
                                // Define SQL query to delete row with specific Id
                                string query2 = "DELETE FROM [dbo].[HistoriqueV2] WHERE Id = @id";
                                // Create command object with query and connection
                                using (SqlCommand command = new SqlCommand(query2, connection))
                                {
                                    // Add parameter for Id value
                                    command.Parameters.AddWithValue("@id", IDH); // Replace idValue with the actual Id value you want to delete

                                    // Execute command
                                    int rowsAffected = command.ExecuteNonQuery();

                                    dataGridView5.CurrentRow.Selected = false;

                                    idHistoriqueList.RemoveAll(x => x == IDH);
                                }


                                string query3 = "DELETE FROM [dbo].[FactureV1] WHERE Id = @id";
                                // Create command object with query and connection
                                using (SqlCommand command = new SqlCommand(query3, connection))
                                {
                                    // Add parameter for Id value
                                    command.Parameters.AddWithValue("@id", IDF); // Replace idValue with the actual Id value you want to delete

                                    // Execute command
                                    int rowsAffected = command.ExecuteNonQuery();

                                    dataGridView5.CurrentRow.Selected = false;

                                    idFACTUREList.RemoveAll(x => x == IDF);
                                }

                                //printList
                                foreach (string item in printList)
                                {
                                    if (item == nomRetour)
                                    {
                                        Console.WriteLine("nom=" + item);
                                        int index = printList.IndexOf(item);

                                        if (index < printList.Count - 5)
                                        {
                                            string nextItem2 = printList[index + 1];
                                            Console.WriteLine("qte=" + nextItem2);

                                            string nextItem3 = printList[index + 2];
                                            Console.WriteLine("pv=" + nextItem3);

                                            string nextItem4 = printList[index + 3];
                                            Console.WriteLine("mont=" + nextItem4);

                                            string nextItem5 = printList[index + 4];
                                            Console.WriteLine("tva=" + nextItem5);

                                            string nextItem6 = printList[index + 5];
                                            Console.WriteLine("ttc=" + nextItem6);

                                            // Remove the items in reverse order to avoid changing the indices of the items to be removed
                                            printList.RemoveAt(index + 5);
                                            printList.RemoveAt(index + 4);
                                            printList.RemoveAt(index + 3);
                                            printList.RemoveAt(index + 2);
                                            printList.RemoveAt(index + 1);
                                            printList.RemoveAt(index);
                                            break;
                                        }
                                    }
                                }

                                foreach (string item in printList)
                                {
                                    if (item == nomRetour)
                                    {
                                        //nom
                                        Console.WriteLine("nom=" + item);
                                        int index = printList.IndexOf(item);
                                        if (index < printList.Count - 1)
                                        {
                                            string nextItem2 = printList[index + 1];
                                            //qte
                                            Console.WriteLine("qte=" + nextItem2);

                                            int index2 = printList.IndexOf(nextItem2);
                                            if (index2 < printList.Count - 1)
                                            {
                                                string nextItem3 = printList[index2 + 1];
                                                //pv
                                                Console.WriteLine("pv=" + nextItem3);

                                                int index3 = printList.IndexOf(nextItem3);
                                                if (index3 < printList.Count - 1)
                                                {
                                                    string nextItem4 = printList[index3 + 1];
                                                    //mont
                                                    Console.WriteLine("mont=" + nextItem4);

                                                    int index4 = printList.IndexOf(nextItem4);
                                                    if (index4 < printList.Count - 1)
                                                    {
                                                        string nextItem5 = printList[index4 + 1];
                                                        //tva
                                                        Console.WriteLine("tva=" + nextItem5);

                                                        int index5 = printList.IndexOf(nextItem5);
                                                        if (index5 < printList.Count - 1)
                                                        {
                                                            string nextItem6 = printList[index5 + 1];
                                                            //ttc
                                                            Console.WriteLine("ttc=" + nextItem6);
                                                            int index6 = printList.IndexOf(nextItem6);

                                                            // Remove the items in reverse order to avoid changing the indices of the items to be removed
                                                            printList.RemoveAt(index + 5);
                                                            printList.RemoveAt(index + 4);
                                                            printList.RemoveAt(index + 3);
                                                            printList.RemoveAt(index + 2);
                                                            printList.RemoveAt(index + 1);
                                                            printList.RemoveAt(index);
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                Console.WriteLine("\nupdated list :\n");
                                // Print the updated list to the console
                                foreach (string item in printList)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            connection.Close();
                        }
                        LoadDB();

                        TP = TP - mont;
                        string formattedTP = TP.ToString("#,##0.00", new System.Globalization.CultureInfo("fr-FR"));
                        materialLabel44.Text = formattedTP.Replace(".", ",");

                        Ttva = Ttva - tva;
                        Tttc = Tttc - ttc;
                        materialLabel42.Text = "" + Ttva;
                        materialLabel40.Text = "" + Tttc;

                        tva = 0;
                        ttc = 0;
                    }
                    catch (Exception ex)
                    {
                        // Display error message
                        MessageBox.Show("Une Erreur est survenue lors du Retour du produit:\n" + ex.Message);
                    }

                    materialButton15.Enabled = false;
                }
                else
                {
                    // Show an error message if the 'Nom' value is null
                    MessageBox.Show("Veuillez choisir un Article à retourner au Stock.", "Erreur");
                }
            }
            else
            {
                // Show an error message or handle the case where no row is selected
                MessageBox.Show("Veuillez choisir un Article à retourner au Stock.", "Erreur");
            }
        }

        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView6.Rows[e.RowIndex].Cells[0].Value != null)
            {
                dataGridView6.Rows[e.RowIndex].Selected = true;
                materialButton15.Enabled = true;

                // Get the corresponding index from the idHistoriqueList
                IDH = idHistoriqueList[e.RowIndex];
                IDF = idFACTUREList[e.RowIndex];
            }
        }

        decimal Credit;
        decimal CreditCF;
        String code, nomC, tel, addr, credit;
        Boolean sameNF = false;
        String NIF;
        String NIS;
        String RC;
        String RIB;
        decimal TPCredit;
        String typeImprimer = "";

        private void materialButton21_Click(object sender, EventArgs e)
        {
            //type de facture/bon
            typeImprimer = "Facture N°:";
            //les coordonnes
            NIF = textBox12.Text;
            NIS = textBox14.Text;
            RC = textBox13.Text;
            RIB = textBox10.Text;

            //recupere le client
            //code
            code = materialTextBox29.Text;
            
            //nom
            nom = materialTextBox28.Text;

            //tel
            tel = materialTextBox27.Text;

            //addr
            addr = materialTextBox26.Text;

            //credit
            if (materialTextBox25.Text != "")
            {
                String C = materialTextBox25.Text;
                decimal c = Convert.ToDecimal(C);
                Credit = c;
            }
            else
            {
                String C = "0";
                decimal c = Convert.ToDecimal(C);
                Credit = c;
            }


            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
{
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap myBitmap1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(myBitmap1, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            myBitmap1.SetResolution(80, 80);
            e.Graphics.DrawImage(myBitmap1, 680, 10);
            e.Graphics.DrawString("Sarl Tayebette", new Font("Abeezee", 10), Brushes.Black, new Point(660, 70));

            e.Graphics.DrawString("_________________________________________________________________", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(50, 70));
            e.Graphics.DrawString("Adresse: Cité Houaoura N°43 Sidi Moussa Alger,Algérie", new Font("Abeezee", 12), Brushes.Black, new Point(60, 105));
            e.Graphics.DrawString("Date: " + day, new Font("Abeezee", 12), Brushes.Black, new Point(590, 105));
            e.Graphics.DrawString("NIF: " + NIF, new Font("Abeezee", 10), Brushes.Black, new Point(60, 140));
            e.Graphics.DrawString("NIS: " + NIS, new Font("Abeezee", 10), Brushes.Black, new Point(240, 140));
            e.Graphics.DrawString("RC: " + RC, new Font("Abeezee", 10), Brushes.Black, new Point(440, 140));
            e.Graphics.DrawString("RIB: " + RIB, new Font("Abeezee", 10), Brushes.Black, new Point(610, 140));
            e.Graphics.DrawString("_________________________________________________________________", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(50, 145));

            e.Graphics.DrawString(typeImprimer + " " + NF, new Font("Abeezee", 14), Brushes.Black, new Point(350, 185));
            e.Graphics.DrawString("Date: " + day, new Font("Abeezee", 14), Brushes.Black, new Point(610, 185));

            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(47, 161));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(47, 170));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(47, 180));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(47, 185));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(47, 190));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(47, 195));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(47, 198));

            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(773, 161));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(773, 170));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(773, 180));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(773, 185));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(773, 190));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(773, 195));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(773, 198));

            e.Graphics.DrawString("_________________________________________________________________", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(50, 200));

            e.Graphics.DrawString("Code: " + code, new Font("Abeezee", 14), Brushes.Black, new Point(60, 235));
            e.Graphics.DrawString("Client: " + nom, new Font("Abeezee", 14), Brushes.Black, new Point(60, 280));
            e.Graphics.DrawString("Adresse: " + addr, new Font("Abeezee", 14), Brushes.Black, new Point(400, 235));
            e.Graphics.DrawString("Tel: " + tel, new Font("Abeezee", 14), Brushes.Black, new Point(400, 280));

            e.Graphics.DrawString("_________________________________________________________________", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(50, 315));
            e.Graphics.DrawString("Désignation", new Font("Abeezee", 12), Brushes.Black, new Point(53, 345));
            e.Graphics.DrawString("Qte", new Font("Abeezee", 12), Brushes.Black, new Point(400, 345));
            e.Graphics.DrawString("Prix", new Font("Abeezee", 12), Brushes.Black, new Point(450, 345));
            e.Graphics.DrawString("Montant", new Font("Abeezee", 12), Brushes.Black, new Point(500, 345));
            e.Graphics.DrawString("TVA", new Font("Abeezee", 12), Brushes.Black, new Point(600, 345));
            e.Graphics.DrawString("Montant TTC", new Font("Abeezee", 12), Brushes.Black, new Point(680, 345));
            e.Graphics.DrawString("_________________________________________________________________", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(50, 361));

            int H = 390;
            int W = 50;

            for (int i = 0; i < printList.Count; i++)
            {
                int j = i;
                e.Graphics.DrawString("" + printList[j], new Font("Abeezee", 12), Brushes.Black, new Point(W, H));
                j++;
                W = W + 350;
                e.Graphics.DrawString("" + printList[j], new Font("Abeezee", 12), Brushes.Black, new Point(W, H));
                j++;
                W = W + 50;
                e.Graphics.DrawString("" + printList[j], new Font("Abeezee", 12), Brushes.Black, new Point(W, H));
                j++;
                W = W + 60;
                e.Graphics.DrawString("" + printList[j], new Font("Abeezee", 12), Brushes.Black, new Point(W, H));
                //tva
                j++;
                W = W + 90;
                e.Graphics.DrawString("" + printList[j], new Font("Abeezee", 12), Brushes.Black, new Point(W, H));
                //ttc
                j++;
                W = W + 100;
                e.Graphics.DrawString("" + printList[j], new Font("Abeezee", 12), Brushes.Black, new Point(W, H));

                i = i + 5;
                if (i == printList.Count - 1)
                {
                    W = 50;
                    H = H + 20;
                    e.Graphics.DrawString("_________________________________________________________________", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(W, H));
                    H = H + 30;
                    e.Graphics.DrawString("Total : " + TP, new Font("Abeezee", 12), Brushes.Black, new Point(600, H));
                    H = H + 30;
                    e.Graphics.DrawString("TVA : " + Ttva, new Font("Abeezee", 12), Brushes.Black, new Point(600, H));
                    H = H + 30;
                    e.Graphics.DrawString("Crédit Client : " + Credit, new Font("Abeezee", 12), Brushes.Black, new Point(600, H));
                    TPCredit = 0;
                    TPCredit = TP + Credit + Ttva;
                    H = H + 30;
                    e.Graphics.DrawString("Total TTC : " + TPCredit, new Font("Abeezee", 12), Brushes.Black, new Point(600, H));
                }
                H = H + 30;
                W = 50;
            }
        }

        private void materialButton22_Click(object sender, EventArgs e)
        {
            //type de facture/bon
            typeImprimer = "Bon N°:";
            //les coordonnes
            NIF = textBox12.Text;
            NIS = textBox14.Text;
            RC = textBox13.Text;
            RIB = textBox10.Text;

            //recupere le client
            //code
            code = materialTextBox29.Text;

            //nom
            nom = materialTextBox28.Text;

            //tel
            tel = materialTextBox27.Text;

            //addr
            addr = materialTextBox26.Text;

            //credit
            if (materialTextBox25.Text != "")
            {
                String C = materialTextBox25.Text;
                decimal c = Convert.ToDecimal(C);
                Credit = c;
            }
            else
            {
                String C = "0";
                decimal c = Convert.ToDecimal(C);
                Credit = c;
            }

            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void materialTextBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(materialTextBox7.Text))
                {
                    String rech = materialTextBox7.Text;
                    int Rid = Convert.ToInt32(String.Format(rech));

                    factureV1BindingSource.Filter = string.Format("[Code Client] = " + Rid + " ");
                    calculs();

                    int c = dataGridView9.Rows.Count;
                    c = c - 1;
                    materialLabel51.Text = "" + c + "";
                }
            }
        }

        private void materialFloatingActionButton12_Click_1(object sender, EventArgs e)
        {
            materialTextBox210.Text = "";
            materialTextBox7.Text = "";
            factureV1BindingSource.RemoveFilter();
            factureV1BindingSource.Filter = string.Format("Article LIKE '%{0}%'", materialTextBox210.Text);
            factureV1BindingSource.ResetBindings(false);
            calculs();

            int c = dataGridView9.Rows.Count;
            c = c - 1;
            materialLabel51.Text = "" + c + "";
        }

        private void materialFloatingActionButton13_Click(object sender, EventArgs e)
        {
            materialTextBox210.Text = "";
            materialTextBox7.Text = "";
            factureV1BindingSource.RemoveFilter();
            factureV1BindingSource.Filter = string.Format("Article LIKE '%{0}%'", materialTextBox210.Text);
            factureV1BindingSource.ResetBindings(false);
            calculs();

            int c = dataGridView9.Rows.Count;
            c = c - 1;
            materialLabel51.Text = "" + c + "";
        }

        private void materialFloatingActionButton11_Click_1(object sender, EventArgs e)
        {
            DateTime dt1 = DateTime.ParseExact(dateTimePicker4.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dt2 = DateTime.ParseExact(dateTimePicker3.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            factureV1BindingSource.Filter = string.Format("Date >= '" + dt1 + "' AND Date <= '" + dt2 + "' ");

            calculs();

            int c = dataGridView9.Rows.Count;
            c = c - 1;
            materialLabel51.Text = "" + c + "";
        }

        private void materialTextBox210_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(materialTextBox210.Text))
                {
                    String rech = materialTextBox210.Text;
                    int Rid = Convert.ToInt32(String.Format(rech));

                    factureV1BindingSource.Filter = string.Format("[Num F] = " + Rid + " ");
                    calculs();

                    int c = dataGridView9.Rows.Count;
                    c = c - 1;
                    materialLabel51.Text = "" + c + "";
                }
            }
        }
        int N_F;
        private void materialButton24_Click(object sender, EventArgs e)
        {
            //type de facture/bon
            typeImprimer = "Facture N°:";
            int leng = dataGridView9.Rows.Count - 1;

            for (int i = 0; i < leng; i++)
            {
                dataGridView9.Rows[i].Selected = true;

                String nf = dataGridView9.Rows[i].Cells[1].Value.ToString();
                N_F = Convert.ToInt32(String.Format(nf));

                code = dataGridView9.Rows[i].Cells[2].Value.ToString();
                nom = dataGridView9.Rows[i].Cells[3].Value.ToString();

                date2 = DateTime.Parse(dataGridView9.Rows[i].Cells[10].Value.ToString()).ToString("dd/MM/yyyy");

                string n = dataGridView9.Rows[i].Cells[4].Value.ToString();
                string quantity = dataGridView9.Rows[i].Cells[5].Value.ToString();
                string price = dataGridView9.Rows[i].Cells[6].Value.ToString();
                string mon = dataGridView9.Rows[i].Cells[7].Value.ToString();
                string tvaCF = dataGridView9.Rows[i].Cells[8].Value.ToString();
                string ttcCF = dataGridView9.Rows[i].Cells[9].Value.ToString();

                TtvaCF = decimal.Parse(materialLabel49.Text);

                printListCF.Add(n);
                printListCF.Add(quantity);
                printListCF.Add(price);
                printListCF.Add(mon);
                printListCF.Add(tvaCF);
                printListCF.Add(ttcCF);

                dataGridView9.Rows[i].Selected = false;
            }

            int leng2 = clientV1DataGridView.Rows.Count - 1;

            if (!string.IsNullOrEmpty(code) && leng2 >= 0)
            {
                String rech = code;
                int Rid = Convert.ToInt32(String.Format(rech));

                clientV1BindingSource.Filter = string.Format("Id = " + Rid + " ");

                //tel
                if (clientV1DataGridView.Rows[0].Cells[2].Value != "")
                {
                    tel = clientV1DataGridView.Rows[0].Cells[2].Value.ToString();
                }

                //addr
                if (clientV1DataGridView.Rows[0].Cells[3].Value != "")
                {
                    addr = clientV1DataGridView.Rows[0].Cells[3].Value.ToString();
                }

                //credit
                if (clientV1DataGridView.Rows[0].Cells[4].Value != "")
                {
                    String C = clientV1DataGridView.Rows[0].Cells[4].Value.ToString();
                    decimal c = Convert.ToDecimal(C);
                    CreditCF = c;
                }
            }

            if (printPreviewDialog2.ShowDialog() == DialogResult.OK)
            {
                printDocument2.Print();
            }

            code = "";
            nom = "";
            tel = "";
            addr = "";
            CreditCF = 0;
            printListCF.Clear();
        }

        private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap myBitmap1 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            pictureBox1.DrawToBitmap(myBitmap1, new Rectangle(0, 0, pictureBox2.Width, pictureBox2.Height));
            myBitmap1.SetResolution(80, 80);
            e.Graphics.DrawImage(myBitmap1, 680, 10);
            e.Graphics.DrawString("Sarl Tayebette", new Font("Abeezee", 10), Brushes.Black, new Point(660, 70));

            e.Graphics.DrawString("_________________________________________________________________", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(50, 70));
            e.Graphics.DrawString("Adresse: Cité Houaoura N°43 Sidi Moussa Alger,Algérie", new Font("Abeezee", 12), Brushes.Black, new Point(60, 105));
            e.Graphics.DrawString("Date: " + day, new Font("Abeezee", 12), Brushes.Black, new Point(590, 105));
            e.Graphics.DrawString("NIF: " + NIF, new Font("Abeezee", 10), Brushes.Black, new Point(60, 140));
            e.Graphics.DrawString("NIS: " + NIS, new Font("Abeezee", 10), Brushes.Black, new Point(240, 140));
            e.Graphics.DrawString("RC: " + RC, new Font("Abeezee", 10), Brushes.Black, new Point(440, 140));
            e.Graphics.DrawString("RIB: " + RIB, new Font("Abeezee", 10), Brushes.Black, new Point(610, 140));
            e.Graphics.DrawString("_________________________________________________________________", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(50, 145));

            e.Graphics.DrawString(typeImprimer + " " + N_F, new Font("Abeezee", 14), Brushes.Black, new Point(350, 185));
            e.Graphics.DrawString("Date: " + date2, new Font("Abeezee", 14), Brushes.Black, new Point(610, 185));

            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(47, 161));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(47, 170));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(47, 180));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(47, 185));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(47, 190));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(47, 195));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(47, 198));

            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(773, 161));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(773, 170));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(773, 180));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(773, 185));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(773, 190));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(773, 195));
            e.Graphics.DrawString("|", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(773, 198));

            e.Graphics.DrawString("_________________________________________________________________", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(50, 200));

            e.Graphics.DrawString("Code: " + code, new Font("Abeezee", 14), Brushes.Black, new Point(60, 235));
            e.Graphics.DrawString("Client: " + nom, new Font("Abeezee", 14), Brushes.Black, new Point(60, 280));
            e.Graphics.DrawString("Adresse: " + addr, new Font("Abeezee", 14), Brushes.Black, new Point(400, 235));
            e.Graphics.DrawString("Tel: " + tel, new Font("Abeezee", 14), Brushes.Black, new Point(400, 280));

            e.Graphics.DrawString("_________________________________________________________________", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(50, 315));
            e.Graphics.DrawString("Désignation", new Font("Abeezee", 12), Brushes.Black, new Point(53, 345));
            e.Graphics.DrawString("Qte", new Font("Abeezee", 12), Brushes.Black, new Point(400, 345));
            e.Graphics.DrawString("Prix", new Font("Abeezee", 12), Brushes.Black, new Point(450, 345));
            e.Graphics.DrawString("Montant", new Font("Abeezee", 12), Brushes.Black, new Point(500, 345));
            e.Graphics.DrawString("TVA", new Font("Abeezee", 12), Brushes.Black, new Point(600, 345));
            e.Graphics.DrawString("Montant TTC", new Font("Abeezee", 12), Brushes.Black, new Point(680, 345));
            e.Graphics.DrawString("_________________________________________________________________", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(50, 361));

            int H = 390;
            int W = 50;

            for (int i = 0; i < printListCF.Count; i++)
            {
                int j = i;
                e.Graphics.DrawString("" + printListCF[j], new Font("Abeezee", 12), Brushes.Black, new Point(W, H));
                j++;
                W = W + 350;
                e.Graphics.DrawString("" + printListCF[j], new Font("Abeezee", 12), Brushes.Black, new Point(W, H));
                j++;
                W = W + 50;
                e.Graphics.DrawString("" + printListCF[j], new Font("Abeezee", 12), Brushes.Black, new Point(W, H));
                j++;
                W = W + 60;
                e.Graphics.DrawString("" + printListCF[j], new Font("Abeezee", 12), Brushes.Black, new Point(W, H));
                //tva
                j++;
                W = W + 90;
                e.Graphics.DrawString("" + printListCF[j], new Font("Abeezee", 12), Brushes.Black, new Point(W, H));
                //ttc
                j++;
                W = W + 100;
                e.Graphics.DrawString("" + printListCF[j], new Font("Abeezee", 12), Brushes.Black, new Point(W, H));

                i = i + 5;
                if (i == printListCF.Count - 1)
                {
                    W = 50;
                    H = H + 20;
                    e.Graphics.DrawString("_________________________________________________________________", new Font("Abeezee", 14, FontStyle.Bold), Brushes.Black, new Point(W, H));
                    H = H + 30;
                    e.Graphics.DrawString("Total : " + tpcf, new Font("Abeezee", 12), Brushes.Black, new Point(600, H));
                    H = H + 30;
                    e.Graphics.DrawString("TVA : " + TVAf, new Font("Abeezee", 12), Brushes.Black, new Point(600, H));
                    H = H + 30;
                    e.Graphics.DrawString("Crédit Client : " + CreditCF, new Font("Abeezee", 12), Brushes.Black, new Point(600, H));
                    TPCredit = 0;
                    TPCredit = TP + CreditCF + TTCf;
                    H = H + 30;
                    e.Graphics.DrawString("Total TTC : " + TPCredit, new Font("Abeezee", 12), Brushes.Black, new Point(600, H));
                }
                H = H + 30;
                W = 50;
            }
        }

        private void materialButton23_Click(object sender, EventArgs e)
        {
            //type de facture/bon
            typeImprimer = "Bon N°:";
            int leng = dataGridView9.Rows.Count - 1;

            for (int i = 0; i < leng; i++)
            {
                dataGridView9.Rows[i].Selected = true;

                String nf = dataGridView9.Rows[i].Cells[1].Value.ToString();
                N_F = Convert.ToInt32(String.Format(nf));

                code = dataGridView9.Rows[i].Cells[2].Value.ToString();
                nom = dataGridView9.Rows[i].Cells[3].Value.ToString();

                date2 = DateTime.Parse(dataGridView9.Rows[i].Cells[10].Value.ToString()).ToString("dd/MM/yyyy");

                string n = dataGridView9.Rows[i].Cells[4].Value.ToString();
                string quantity = dataGridView9.Rows[i].Cells[5].Value.ToString();
                string price = dataGridView9.Rows[i].Cells[6].Value.ToString();
                string mon = dataGridView9.Rows[i].Cells[7].Value.ToString();
                string tvaCF = dataGridView9.Rows[i].Cells[8].Value.ToString();
                string ttcCF = dataGridView9.Rows[i].Cells[9].Value.ToString();

                TtvaCF = decimal.Parse(materialLabel49.Text);

                printListCF.Add(n);
                printListCF.Add(quantity);
                printListCF.Add(price);
                printListCF.Add(mon);
                printListCF.Add(tvaCF);
                printListCF.Add(ttcCF);

                dataGridView9.Rows[i].Selected = false;
            }

            int leng2 = clientV1DataGridView.Rows.Count - 1;

            if (!string.IsNullOrEmpty(code) && leng2 >= 0)
            {
                String rech = code;
                int Rid = Convert.ToInt32(String.Format(rech));

                clientV1BindingSource.Filter = string.Format("Id = " + Rid + " ");

                //tel
                if (clientV1DataGridView.Rows[0].Cells[2].Value != "")
                {
                    tel = clientV1DataGridView.Rows[0].Cells[2].Value.ToString();
                }

                //addr
                if (clientV1DataGridView.Rows[0].Cells[3].Value != "")
                {
                    addr = clientV1DataGridView.Rows[0].Cells[3].Value.ToString();
                }

                //credit
                if (clientV1DataGridView.Rows[0].Cells[4].Value != "")
                {
                    String C = clientV1DataGridView.Rows[0].Cells[4].Value.ToString();
                    decimal c = Convert.ToDecimal(C);
                    CreditCF = c;
                }
            }

            if (printPreviewDialog2.ShowDialog() == DialogResult.OK)
            {
                printDocument2.Print();
            }

            code = "";
            nom = "";
            tel = "";
            addr = "";
            CreditCF = 0;
            printListCF.Clear();
        }

        private void materialTextBox210_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
                return;
            }
        }

        private void materialTextBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
                return;
            }
        }

        int indexF = 0;
        Double NF;
        private void materialButton18_Click(object sender, EventArgs e)
        {
            //qte
            if (!string.IsNullOrEmpty(materialTextBox23.Text))
            {
                try
                {
                    //quantity
                    qteSaisi = decimal.Parse(materialTextBox23.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("La Quantité Saisie n'est pas Valide", "Erreur");
                }


                if (qteSaisi > quantite || qteSaisi == 0)
                {
                    MessageBox.Show("Quantité insuffisante", "Erreur");
                }
                else
                {
                    //prix
                    if (!string.IsNullOrEmpty(materialTextBox21.Text))
                    {
                        if (true/*c <= 10*/)
                        {
                            if (materialTextBox25.Text != "")
                            {
                                String C = materialTextBox25.Text;
                                decimal c = Convert.ToDecimal(C);
                                Credit = c;
                            }
                            else
                            {
                                String C = "0";
                                decimal c = Convert.ToDecimal(C);
                                Credit = c;
                            }
                            prixSaisi = decimal.Parse(materialTextBox21.Text);

                            Mont = (prixSaisi * qteSaisi);
                            TP = TP + (prixSaisi * qteSaisi);

                            decimal rest;

                            rest = quantite - qteSaisi;

                            //TVA
                            decimal TVA = 0, TTC = 0;
                            if (materialCheckbox2.Checked == true)
                            {
                                TVA = (Mont * 19) / 100;
                                TTC = TVA + Mont;

                                Ttva = Ttva + TVA;
                                Tttc = Tttc + TTC;

                                materialLabel42.Text = "" + Ttva;
                                materialLabel40.Text = "" + Tttc;
                            }
                            else if (materialCheckbox2.Checked == false)
                            {
                                TVA = 0;
                                TTC = 0;

                                Ttva = Ttva + TVA;
                                Tttc = Tttc + TTC;

                                materialLabel42.Text = "" + Ttva;
                                materialLabel40.Text = "" + Tttc;
                            }

                            string formattedTP = TP.ToString("#,##0.00", new System.Globalization.CultureInfo("fr-FR"));
                            materialLabel44.Text = formattedTP.Replace(".", ",");

                            //historique & stock 
                            try
                            {
                                string connectionString = Properties.Settings.Default.DatabaseApplicationConnectionString;
                                using (SqlConnection connection = new SqlConnection(connectionString))
                                {
                                    connection.Open();
                                    string insertQuery = "INSERT INTO HistoriqueV2 (Nom, Quantité, Prix, Date, Montant, [prix achat], TVA, TTC) VALUES (@Nom, @Quantite, @Prix, @Date, @Montant, @PrixAchat, @TVA, @TTC); SELECT CAST(scope_identity() AS int)";
                                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                                    {
                                        command.Parameters.AddWithValue("@Nom", nom);
                                        command.Parameters.AddWithValue("@Quantite", qteSaisi);
                                        command.Parameters.AddWithValue("@Prix", prixSaisi);
                                        string dayString = Convert.ToDateTime(day).ToString("dd/MM/yyyy");
                                        command.Parameters.AddWithValue("@Date", dayString);
                                        command.Parameters.AddWithValue("@Montant", Mont);
                                        command.Parameters.AddWithValue("@PrixAchat", prixAchat); // replace with the actual value
                                        command.Parameters.AddWithValue("@TVA", TVA); // replace with the actual value
                                        command.Parameters.AddWithValue("@TTC", TTC); // replace with the actual value


                                        // Execute the insert command and get the ID of the inserted row
                                        int newId = (int)command.ExecuteScalar();

                                        // Check the 'newId' value to ensure that the insert was successful
                                        idHistoriqueList.Add(newId);
                                    }

                                    //stock
                                    string updateQuery = "UPDATE stockV4 SET Quantité = @Quantite WHERE Id = @id";
                                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                                    {
                                        command.Parameters.AddWithValue("@Quantite", rest); // replace newQuantiteValue with the new quantity value you want to set
                                        command.Parameters.AddWithValue("@id", IdArticle); // replace IdArticle with the actual Id value you want to update

                                        int rowsAffected = command.ExecuteNonQuery();
                                        // Check the 'rowsAffected' value to ensure that the update was successful
                                    }

                                    //num fact & facture
                                    if (sameNF == true || indexF == 0)
                                    {
                                        //num fact
                                        if (indexF == 0)
                                        {
                                            // Assume you have established a SQL connection object called "connection"
                                            string insertQuery3 = "INSERT INTO [dbo].[NumFactV1] DEFAULT VALUES; SELECT SCOPE_IDENTITY() AS num_fct;";
                                            using (SqlCommand command = new SqlCommand(insertQuery3, connection))
                                            {
                                                NF = Convert.ToInt32(command.ExecuteScalar());
                                            }
                                        }
                                        string client = materialTextBox28.Text;
                                        string codeClient = materialTextBox29.Text;
                                        //facture 
                                        // Assume you have established a SQL connection object called "connection"
                                        string insertQuery2 = "INSERT INTO FactureV1 ([Date], [Code Client], [Client], [Article], [Quantité], [Prix de vente], [Montant], [Num F], [TVA], [TTC]) VALUES (@Date, @CodeClient, @Client, @Article, @Quantite, @PrixVente, @Montant, @NumF, @TVA, @TTC); SELECT CAST(scope_identity() AS int)";
                                        using (SqlCommand command = new SqlCommand(insertQuery2, connection))
                                        {
                                            string dayString = Convert.ToDateTime(day).ToString("dd/MM/yyyy");
                                            command.Parameters.AddWithValue("@Date", dayString);
                                            if (!string.IsNullOrEmpty(codeClient))
                                            {
                                                command.Parameters.AddWithValue("@CodeClient", codeClient);
                                            }
                                            else
                                            {
                                                command.Parameters.AddWithValue("@CodeClient", DBNull.Value);
                                            }

                                            if (!string.IsNullOrEmpty(client))
                                            {
                                                command.Parameters.AddWithValue("@Client", client);
                                            }
                                            else
                                            {
                                                command.Parameters.AddWithValue("@Client", DBNull.Value);
                                            }
                                            command.Parameters.AddWithValue("@Article", nom);
                                            command.Parameters.AddWithValue("@Quantite", qteSaisi);
                                            command.Parameters.AddWithValue("@PrixVente", prixSaisi);
                                            command.Parameters.AddWithValue("@Montant", Mont);
                                            command.Parameters.AddWithValue("@NumF", NF);
                                            command.Parameters.AddWithValue("@TVA", TVA);
                                            command.Parameters.AddWithValue("@TTC", TTC);

                                            // Execute the insert command and get the ID of the inserted row
                                            int newId = (int)command.ExecuteScalar();

                                            // Check the 'newId' value to ensure that the insert was successful
                                            idFACTUREList.Add(newId);
                                        }
                                        indexF = 1;
                                    }
                                    
                                    connection.Close();
                                    LoadDB();
                                }
                            }
                            catch (Exception ex)
                            {
                                // Display error message
                                MessageBox.Show("Une Erreur est survenue lors de l'insertion des données:\n" + ex.Message);
                            }
                            sameNF = true;

                            dataGridView6.Rows.Add(nom, qteSaisi, prixSaisi, Mont, TVA, TTC, NF);

                            dataGridView7.CurrentRow.Selected = false;
                            materialTextBox23.Text = "";
                            materialTextBox21.Text = "";

                            quantity = Convert.ToString(qteSaisi);
                            price = Convert.ToString(prixSaisi);
                            mon = Convert.ToString(Mont);

                            printList.Add(nom);
                            printList.Add(quantity);
                            printList.Add(price);
                            printList.Add(mon);
                            printList.Add(TVA.ToString());
                            printList.Add(TTC.ToString());

                            nom = "";
                            qteSaisi = 0;
                            quantite = 0;
                            prixSaisi = 0;
                            prixVente = 0;
                            Mont = 0;
                        }
                        else if (false/*c > 10*/)
                        {
                            MessageBox.Show("votre logiciel est en mode démonstration \n" +
                               "la version est limmité : \n" +
                               "- 10 Ventes \n", "Information");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Veuillez Entrer un Prix", "Erreur");
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez Entrer une Quantité", "Erreur");
            }
        }

        private void materialFloatingActionButton10_Click(object sender, EventArgs e)
        {
            materialLabel44.Text = "0";
            materialLabel40.Text = "0";
            materialLabel42.Text = "0";
            materialTextBox21.Text = "";
            materialTextBox23.Text = "";

            materialTextBox29.Text = "";
            materialTextBox28.Text = "";
            materialTextBox27.Text = "";
            materialTextBox26.Text = "";
            materialTextBox25.Text = "";

            dataGridView7.ClearSelection();

            TP = 0;
            Tttc = 0;
            Ttva = 0;
            dataGridView6.Rows.Clear();
            printList.Clear();
            idHistoriqueList.Clear();
            idFACTUREList.Clear();
            materialButton15.Enabled = false;
            sameNF = false;
            indexF = 0;
            NF = 0;

            code = "";
            nomC = "";
            addr = "";
            tel = "";
            credit = "";

            nom = "";
            qteSaisi = 0;
            quantite = 0;
            prixSaisi = 0;
            prixVente = 0;
            Mont = 0;
        }

        private void materialTextBox25_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsDigit(ch) && ch != ',' && ch != 8)
            {
                e.Handled = true;
                return;
            }
        }

        decimal prixSaisi, Ttva = 0, TtvaCF = 0, Tttc = 0;
        private void materialButton10_Click(object sender, EventArgs e)
        {
            //qte
            if (!string.IsNullOrEmpty(materialTextBox22.Text))
            {
                try
                {
                    //quantity
                    qteSaisi = decimal.Parse(materialTextBox22.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("La Quantité Saisie n'est pas Valide", "Erreur");
                }
                

                if (qteSaisi > quantite || qteSaisi == 0)
                {
                    MessageBox.Show("Quantité insuffisante", "Erreur");
                }
                else
                {
                    //prix
                    if (!string.IsNullOrEmpty(materialTextBox24.Text))
                    {
                        if (true/*c <= 10*/)
                        {
                            prixSaisi = decimal.Parse(materialTextBox24.Text);

                            Mont = (prixSaisi * qteSaisi);
                            TP = TP + (prixSaisi * qteSaisi);

                            decimal rest;

                            rest = quantite - qteSaisi;

                            //TVA
                            decimal TVA = 0, TTC = 0;
                            if (materialCheckbox1.Checked == true)
                            {
                                TVA = (Mont * 19) / 100;
                                TTC = TVA + Mont;

                                Ttva = Ttva + TVA;
                                Tttc = Tttc + TTC;

                                materialLabel30.Text = "" + Ttva;
                                materialLabel32.Text = "" + Tttc;
                            }
                            else if (materialCheckbox1.Checked == false)
                            {
                                TVA = 0;
                                TTC = 0;

                                Ttva = Ttva + TVA;
                                Tttc = Tttc + TTC;

                                materialLabel30.Text = "" + Ttva;
                                materialLabel32.Text = "" + Tttc;
                            }

                            string formattedTP = TP.ToString("#,##0.00", new System.Globalization.CultureInfo("fr-FR"));
                            materialLabel13.Text = formattedTP.Replace(".", ",");

                            dataGridView4.Rows.Add(nom, qteSaisi, prixSaisi, Mont, TVA, TTC);

                            //historique
                            try
                            {
                                string connectionString = Properties.Settings.Default.DatabaseApplicationConnectionString;
                                using (SqlConnection connection = new SqlConnection(connectionString))
                                {
                                    connection.Open();
                                    string insertQuery = "INSERT INTO HistoriqueV2 (Nom, Quantité, Prix, Date, Montant, [prix achat], TVA, TTC) VALUES (@Nom, @Quantite, @Prix, @Date, @Montant, @PrixAchat, @TVA, @TTC); SELECT CAST(scope_identity() AS int)";
                                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                                    {
                                        command.Parameters.AddWithValue("@Nom", nom);
                                        command.Parameters.AddWithValue("@Quantite", qteSaisi);
                                        command.Parameters.AddWithValue("@Prix", prixSaisi);
                                        string dayString = Convert.ToDateTime(day).ToString("dd/MM/yyyy");
                                        command.Parameters.AddWithValue("@Date", dayString);
                                        command.Parameters.AddWithValue("@Montant", Mont);
                                        command.Parameters.AddWithValue("@PrixAchat", prixAchat); // replace with the actual value
                                        command.Parameters.AddWithValue("@TVA", TVA); // replace with the actual value
                                        command.Parameters.AddWithValue("@TTC", TTC); // replace with the actual value


                                        // Execute the insert command and get the ID of the inserted row
                                        int newId = (int)command.ExecuteScalar();

                                        // Check the 'newId' value to ensure that the insert was successful
                                        idHistoriqueList.Add(newId);
                                    }

                                    string updateQuery = "UPDATE stockV4 SET Quantité = @Quantite WHERE Id = @id";
                                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                                    {
                                        command.Parameters.AddWithValue("@Quantite", rest); // replace newQuantiteValue with the new quantity value you want to set
                                        command.Parameters.AddWithValue("@id", IdArticle); // replace IdArticle with the actual Id value you want to update

                                        int rowsAffected = command.ExecuteNonQuery();
                                        // Check the 'rowsAffected' value to ensure that the update was successful
                                    }

                                    connection.Close();
                                    LoadDB();
                                }
                            }
                            catch (Exception ex)
                            {
                                // Display error message
                                MessageBox.Show("Une Erreur est survenue lors de l'insertion des données dans l'historique:\n" + ex.Message);
                            }

                            dataGridView3.CurrentRow.Selected = false;
                            materialTextBox22.Text = "";
                            materialTextBox24.Text = "";

                            nom = "";
                            qteSaisi = 0;
                            quantite = 0;
                            prixSaisi = 0;
                            prixVente = 0;
                            Mont = 0;
                        }
                        else if (false/*c > 10*/)
                        {
                            MessageBox.Show("votre logiciel est en mode démonstration \n" +
                               "la version est limmité : \n" +
                               "- 10 Ventes \n", "Information");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Veuillez Entrer un Prix", "Erreur");
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez Entrer une Quantité", "Erreur");
            }
        }

    }
}
