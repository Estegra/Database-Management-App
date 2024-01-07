using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace Database_Management_App.MVVM.View
{
    public partial class UpdateView : UserControl
    {
        public UpdateView()
        {
            InitializeComponent();

            GetPassword("Password.json");

            TableNamesComboBox.Items.Add("Thesis");
            TableNamesComboBox.Items.Add("University");
            TableNamesComboBox.Items.Add("Institute");
            TableNamesComboBox.Items.Add("Person");

            
            AddPanel.Visibility = Visibility.Hidden;
            UpdatePanel.Visibility = Visibility.Hidden;
            DeletePanel.Visibility = Visibility.Hidden;

            AddInstitute.Visibility = Visibility.Hidden;
        }
        public class Credentials
        {
            public string Password { get; set; }
        }

        string connection_str;

        private void GetPassword(string jsonFile)
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, jsonFile);

            var credentials = JsonConvert.DeserializeObject<Credentials>(File.ReadAllText(filePath));

            connection_str = $"server=localhost;user=root;database=test;port=3306;password={credentials.Password}";
        }


        private void PopulateUpdateComboBox()
        {
            if (TableNamesComboBox.SelectedItem?.ToString() == "Thesis")
            {
                ColumnNamesComboBox.Items.Clear();
                ColumnNamesComboBox.Items.Add("Title");
                ColumnNamesComboBox.Items.Add("Abstract");
                ColumnNamesComboBox.Items.Add("AuthorID");
                ColumnNamesComboBox.Items.Add("Year");
                ColumnNamesComboBox.Items.Add("Type");
                ColumnNamesComboBox.Items.Add("SupervisorID");
                ColumnNamesComboBox.Items.Add("CoSupervisorID");
                ColumnNamesComboBox.Items.Add("UniversityID");
                ColumnNamesComboBox.Items.Add("InstituteID");
                ColumnNamesComboBox.Items.Add("NumberOfPages");
                ColumnNamesComboBox.Items.Add("Language");
                ColumnNamesComboBox.Items.Add("SubmissionDate");
            }
            else if (TableNamesComboBox.SelectedItem?.ToString() == "University")
            {
                ColumnNamesComboBox.Items.Clear();
                ColumnNamesComboBox.Items.Add("UniversityName");
            }
            else if (TableNamesComboBox.SelectedItem?.ToString() == "Institute")
            {
                ColumnNamesComboBox.Items.Clear();
                ColumnNamesComboBox.Items.Add("InstituteName");
                ColumnNamesComboBox.Items.Add("UniversityID");

            }
            else if (TableNamesComboBox.SelectedItem?.ToString() == "Person")
            {
                ColumnNamesComboBox.Items.Clear();
                ColumnNamesComboBox.Items.Add("PersonName");
                ColumnNamesComboBox.Items.Add("PersonTitle");
            }
        }

        private void TableSelectionChanged()
        {
            PopulateUpdateComboBox();
            AddPanel.Visibility = Visibility.Hidden;
            UpdatePanel.Visibility = Visibility.Hidden;
            DeletePanel.Visibility = Visibility.Hidden;
            AddInstitute.Visibility = Visibility.Hidden;

            if (TableNamesComboBox.SelectedItem.ToString() == "Thesis")
            {
                PopulateThesisNo(UpdateNoComboBox);
                PopulateThesisNo(DeleteNoComboBox);
                OperationComboBox.Items.Clear();
                OperationComboBox.Items.Add("Update");
                OperationComboBox.Items.Add("Delete");


                string query = @"SELECT
thesis.ThesisNo AS 'No',    
thesis.Title,
Author.PersonName AS 'Author',
Supervisor.PersonName AS 'Supervisor',
thesis.Type,
university.UniversityName AS 'University',
thesis.Language,
thesis.NumberOfPages as 'Pages',
thesis.Year,
thesis.Keywords
FROM thesis
JOIN Person AS Author ON thesis.AuthorID = Author.PersonID
JOIN Person AS Supervisor ON thesis.SupervisorID = Supervisor.PersonID
JOIN University ON thesis.UniversityID = University.UniversityID";

                using (var connection = new MySqlConnection(connection_str))
                {
                    try
                    {
                        connection.Open();

                        MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        Table.ItemsSource = dt.DefaultView;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }


            }

            else
            {
                OperationComboBox.Items.Clear();
                OperationComboBox.Items.Add("Add");
                OperationComboBox.Items.Add("Update");
                OperationComboBox.Items.Add("Delete");
                AddInstitute.Visibility = Visibility.Hidden;

                using (var connection = new MySqlConnection(connection_str))
                {
                    try
                    {
                        connection.Open();

                        MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT * FROM {TableNamesComboBox.SelectedItem}", connection);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        Table.ItemsSource = dt.DefaultView;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }


                if (TableNamesComboBox.SelectedItem.ToString() == "Institute")
                {
                    PopulateInstituteID(DeleteNoComboBox);
                    PopulateInstituteID(UpdateNoComboBox);
                    OperationComboBox.Items.Clear();
                    OperationComboBox.Items.Add("Add");
                    OperationComboBox.Items.Add("Update");
                    OperationComboBox.Items.Add("Delete");
                    if (OperationComboBox.SelectedItem?.ToString() == "Add") { AddInstitute.Visibility = Visibility.Visible; }

                }
                if(TableNamesComboBox.SelectedItem.ToString() == "University")
                {
                    PopulateUniversityID(UpdateNoComboBox);
                    PopulateUniversityID(DeleteNoComboBox);
                }
                if (TableNamesComboBox.SelectedItem.ToString() == "Person")
                {
                    PopulatePersonID(UpdateNoComboBox);
                    PopulatePersonID(DeleteNoComboBox);
                }
            }
        }


        private void PopulateThesisNo(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            try
            {
                using (var connection = new MySqlConnection(connection_str))
                {
                    MySqlCommand command = new MySqlCommand("SELECT ThesisNo FROM thesis;", connection);

                    connection.Open();

                    MySqlDataReader reader = command.ExecuteReader();

                    List<int> thesisNos = new List<int>();
                    while (reader.Read())
                    {
                        int thesisNo = reader.GetInt32(0);
                        thesisNos.Add(thesisNo);

                    }

                    thesisNos.Sort();

                    foreach (int thesisNo in thesisNos)
                    {
                        comboBox.Items.Add(thesisNo);
                    }


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void PopulateInstituteID(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            try
            {
                using (var connection = new MySqlConnection(connection_str))
                {
                    MySqlCommand command = new MySqlCommand("SELECT InstituteID FROM institute;", connection);

                    connection.Open();

                    MySqlDataReader reader = command.ExecuteReader();

                    List<int> array = new List<int>();
                    while (reader.Read())
                    {
                        int ID = reader.GetInt32(0);
                        array.Add(ID);

                    }

                    array.Sort();

                    foreach (int ID in array)
                    {
                        comboBox.Items.Add(ID);
                    }


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void PopulateUniversityID(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            try
            {
                using (var connection = new MySqlConnection(connection_str))
                {
                    MySqlCommand command = new MySqlCommand("SELECT UniversityID FROM university;", connection);

                    connection.Open();

                    MySqlDataReader reader = command.ExecuteReader();

                    List<int> array = new List<int>();
                    while (reader.Read())
                    {
                        int ID = reader.GetInt32(0);
                        array.Add(ID);

                    }

                    array.Sort();

                    foreach (int ID in array)
                    {
                        comboBox.Items.Add(ID);
                    }


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void PopulatePersonID(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            try
            {
                using (var connection = new MySqlConnection(connection_str))
                {
                    MySqlCommand command = new MySqlCommand("SELECT PersonID FROM person;", connection);

                    connection.Open();

                    MySqlDataReader reader = command.ExecuteReader();

                    List<int> array = new List<int>();
                    while (reader.Read())
                    {
                        int ID = reader.GetInt32(0);
                        array.Add(ID);

                    }

                    array.Sort();

                    foreach (int ID in array)
                    {
                        comboBox.Items.Add(ID);
                    }


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }









        private void TableNamesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TableSelectionChanged();

        }

        private void OperationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (OperationComboBox.SelectedItem) 
            {
                case "Add":

                    if(TableNamesComboBox.SelectedItem.ToString() == "Institute")
                    {
                        AddInstitute.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        AddInstitute.Visibility = Visibility.Hidden;
                    }


                    AddPanel.Visibility = Visibility.Visible;
                    UpdatePanel.Visibility = Visibility.Hidden;
                    DeletePanel.Visibility = Visibility.Hidden;
                    break;

                case "Update":
                    AddPanel.Visibility = Visibility.Hidden;
                    UpdatePanel.Visibility = Visibility.Visible;
                    DeletePanel.Visibility = Visibility.Hidden;
                    break;

                case "Delete":
                    AddPanel.Visibility = Visibility.Hidden;
                    UpdatePanel.Visibility = Visibility.Hidden;
                    DeletePanel.Visibility = Visibility.Visible;
                    break;
            }

        }

        private void txt_AddName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_AddName.Text))
            {
                textBlock_PlaceholderAddName.Visibility = Visibility.Visible;
            }
            else
            {
                textBlock_PlaceholderAddName.Visibility = Visibility.Hidden;
            }
        }

        private void btnClearAddName_Click(object sender, RoutedEventArgs e)
        {
            txt_AddName.Clear();
            txt_AddName.Focus();
        }

        private void txt_UpdateName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_UpdateName.Text))
            {
                textBlock_PlaceholderUpdateName.Visibility = Visibility.Visible;
            }
            else
            {
                textBlock_PlaceholderUpdateName.Visibility = Visibility.Hidden;
            }
        }

        private void btnClearUpdateName_Click(object sender, RoutedEventArgs e)
        {
            txt_UpdateName.Clear();
            txt_UpdateName.Focus();
        }
        private void ColumnNamesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void btn_AddSubmit_Click(object sender, RoutedEventArgs e)
        {
            string query;
            using (var connection = new MySqlConnection(connection_str))
            {
                switch (TableNamesComboBox.SelectedItem.ToString())
                {
                    case "University":
                        query = @"INSERT INTO university (UniversityName) VALUES (@UniversityName);";
                        connection.Open();
                        try
                        {
                            using (var command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@UniversityName", txt_AddName.Text);
                                command.ExecuteNonQuery();

                                MessageBox.Show("Success!", "Successful Update", MessageBoxButton.OK, MessageBoxImage.Information);

                            }

                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message,"Error!",MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        
                        break;

                    case "Institute":
                        query = @"INSERT INTO institute (InstituteName,UniversityID) VALUES (@InstituteName,@UniversityID);";
                        connection.Open();
                        try
                        {
                            using (var command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@InstituteName", txt_AddName.Text);
                                command.Parameters.AddWithValue("@UniversityID", UniversitiesComboBox.SelectedItem.ToString());
                                command.ExecuteNonQuery();

                                MessageBox.Show("Success!", "Successful Update", MessageBoxButton.OK,MessageBoxImage.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        break;

                    case "Person":
                        query = @"INSERT INTO person (PersonName) VALUES (@PersonName);";
                        connection.Open();
                        try
                        {
                            using (var command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@PersonName", txt_AddName.Text);
                                command.ExecuteNonQuery();
                                MessageBox.Show("Success!", "Successful Update", MessageBoxButton.OK);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        break;

                }
            }


            TableSelectionChanged();
        }

        private void btn_UpdateSubmit_Click_1(object sender, RoutedEventArgs e)
        {
            string query;
            using (var connection = new MySqlConnection(connection_str))
            {
                switch (TableNamesComboBox.SelectedItem.ToString())
                {
                    case "Thesis":
                        try
                        {
                            connection.Open();
                            query = $@"UPDATE thesis SET {ColumnNamesComboBox.SelectedItem?.ToString()} = @NewValue WHERE ThesisNo = @ThesisNo;";

                            using (var command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@NewValue", txt_UpdateName?.Text);
                                command.Parameters.AddWithValue("@ThesisNo", Convert.ToInt32(UpdateNoComboBox.SelectedItem?.ToString()));
                                command.ExecuteNonQuery();
                            }
                            MessageBox.Show("Success!", "Successful Update", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                        break;

                    case "University":
                        try
                        {
                            connection.Open();
                            query = $@"UPDATE university SET {ColumnNamesComboBox.SelectedItem?.ToString()} = @NewValue WHERE UniversityID = @UniversityID;";

                            using (var command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@NewValue", txt_UpdateName?.Text);
                                command.Parameters.AddWithValue("@UniversityID", Convert.ToInt32(UpdateNoComboBox.SelectedItem?.ToString()));
                                command.ExecuteNonQuery();
                            }

                            MessageBox.Show("Success!", "Successful Update", MessageBoxButton.OK, MessageBoxImage.Information);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                        break;

                    case "Institute":
                        try
                        {


                            connection.Open();
                            query = $@"UPDATE institute SET {ColumnNamesComboBox.SelectedItem?.ToString()} = @NewValue WHERE InstituteID = @InstituteID;";

                            using (var command = new MySqlCommand(query, connection))
                            {
                                
                                if(ColumnNamesComboBox.SelectedItem?.ToString() == "UniversityID")
                                {
                                    command.Parameters.AddWithValue("@NewValue", Convert.ToInt32(txt_UpdateName?.Text));

                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@NewValue", txt_UpdateName?.Text);
                                }

                                
                                command.Parameters.AddWithValue("@InstituteID", Convert.ToInt32(UpdateNoComboBox.SelectedItem?.ToString()));
                                command.ExecuteNonQuery();
                            }
                            MessageBox.Show("Success!", "Successful Update", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                        break;

                    case "Person":
                        try
                        {
                            string test = txt_UpdateName?.Text;
                            bool check = int.TryParse(test, out int result);
                            if (!check)
                            {
                                connection.Open();
                                query = $@"UPDATE person SET {ColumnNamesComboBox.SelectedItem?.ToString()} = @NewValue WHERE PersonID = @PersonID;";

                                using (var command = new MySqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@NewValue", txt_UpdateName?.Text);
                                    command.Parameters.AddWithValue("@PersonID", Convert.ToInt32(UpdateNoComboBox.SelectedItem?.ToString()));
                                    command.ExecuteNonQuery();
                                }
                                MessageBox.Show("Success!", "Successful Update", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("Values Should Be Meaningful!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                        break;
                }
            }
            TableSelectionChanged();
         }

        private void btn_DeleteSubmit_Click(object sender, RoutedEventArgs e)
        {
            string query;
            using (var connection = new MySqlConnection(connection_str))
            {  
                switch (TableNamesComboBox.SelectedItem.ToString())              
                {       
                    case "Thesis":
                        try
                        {
                            connection.Open();
                            query = @"DELETE FROM thesistopic WHERE ThesisNo = @ThesisNo;";

                            using (var command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@ThesisNo", Convert.ToInt32(DeleteNoComboBox.SelectedItem?.ToString()));
                                command.ExecuteNonQuery();

                            }

                            query = @"DELETE FROM thesis WHERE ThesisNo = @ThesisNo;";

                            using (var command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@ThesisNo", Convert.ToInt32(DeleteNoComboBox.SelectedItem?.ToString()));
                                command.ExecuteNonQuery();

                            }
                            MessageBox.Show("Success!", "Successful Update", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                        break;

                    case "Institute":

                        try
                        {
                            connection.Open();
                            query = @"DELETE FROM institute WHERE InstituteID = @InstituteID;";

                            using (var command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@InstituteID", Convert.ToInt32(DeleteNoComboBox.SelectedItem?.ToString()));
                                command.ExecuteNonQuery();
                            }
                            MessageBox.Show("Success!", "Successful Update", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                        break;

                    case "University":

                        try
                        {
                            connection.Open();
                            query = @"DELETE FROM university WHERE UniversityID = @UniversityID;";

                            using (var command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@UniversityID", Convert.ToInt32(DeleteNoComboBox.SelectedItem?.ToString()));
                                command.ExecuteNonQuery();
                            }
                            MessageBox.Show("Success!", "Successful Update", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                        break;

                    case "Person":

                        try
                        {
                            connection.Open();
                            query = @"DELETE FROM person WHERE PersonID = @PersonID;";

                            using (var command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@PersonID", Convert.ToInt32(DeleteNoComboBox.SelectedItem?.ToString()));
                                command.ExecuteNonQuery();
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                            MessageBox.Show("Success!", "Successful Update", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                }
                
                

                
            }

            TableSelectionChanged();
        }

        
    }
}
