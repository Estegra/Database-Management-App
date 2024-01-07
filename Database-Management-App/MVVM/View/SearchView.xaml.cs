using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Database_Management_App.MVVM.View
{

    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();
            this.DataContext = this;

            GetPassword("Password.json");

            GetComboBoxTexts();
            
        }
 
        public class Credentials
        {
            public string Password { get; set; }
        }

        string connection_str;
        private void GetPassword(string jsonFile)
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory , jsonFile);

            var credentials = JsonConvert.DeserializeObject<Credentials>(File.ReadAllText(filePath));

            connection_str = $"server=localhost;user=root;database=test;port=3306;password={credentials.Password}";
        }

        //connection_str = $"server=localhost;user=root;database=test;port=3306;password=YOURPASSWORDHERE";

        string query = "";
        string abstractText = "";
        private void SetQuery()
        {
            //Sets the query of the table to be displayed

            query = @"SELECT
thesis.ThesisNo AS 'No',    
thesis.Title,
Author.PersonName AS 'Author',
Supervisor.PersonName AS 'Supervisor',
thesis.Type,
university.UniversityName AS 'University',
institute.InstituteName AS 'Institute',
thesis.Language,
thesis.NumberOfPages as 'Pages',
thesis.Year,
thesis.Keywords,
GROUP_CONCAT(topic.TopicName SEPARATOR ', ') AS Topics
FROM thesis
JOIN Person AS Author ON thesis.AuthorID = Author.PersonID
JOIN Person AS Supervisor ON thesis.SupervisorID = Supervisor.PersonID
JOIN University ON thesis.UniversityID = University.UniversityID
JOIN Institute ON thesis.InstituteID = Institute.InstituteID
LEFT JOIN ThesisTopic ON thesis.ThesisNo = ThesisTopic.ThesisNo
LEFT JOIN Topic ON ThesisTopic.TopicID = Topic.TopicID
";
        }

        private void LoadTable()
        {
            //Resets the query then applies the filters,
            //after that sends the query to database to retrive the table.


            SetQuery();

            //**********FILTERS*********
            
            bool isFirstCondition = true;

            if (!string.IsNullOrWhiteSpace(txt_Input.Text))
            {
                query = AddCondition(query, $"Thesis.Title LIKE '%{txt_Input.Text}%'", isFirstCondition);
                isFirstCondition = false;
            }
            if (!string.IsNullOrWhiteSpace(txt_Keyword.Text))
            {
                query = AddCondition(query, $"Thesis.Keywords LIKE '%{txt_Keyword.Text}%'", isFirstCondition);
                isFirstCondition = false;
            }
            if (!string.IsNullOrWhiteSpace(txt_Author.Text))
            {
                query = AddCondition(query, $"Author.PersonName LIKE '%{txt_Author.Text}%'", isFirstCondition);
                isFirstCondition = false;
            }
            if (TypeComboBox.SelectedItem != null)
            {
                string TypeInput = TypeComboBox.SelectedItem.ToString();
                query = AddCondition(query, $"Thesis.Type='{TypeInput}'", isFirstCondition);
                isFirstCondition = false;
            }
            if (LanguageComboBox.SelectedItem != null)
            {
                string LanguageInput = LanguageComboBox.SelectedItem.ToString();
                query = AddCondition(query, $"Thesis.Language='{LanguageInput}'", isFirstCondition);
                isFirstCondition = false;
            }
            if (UniversityComboBox.SelectedItem != null)
            {
                string UniversityInput = UniversityComboBox.SelectedItem.ToString();
                query = AddCondition(query, $"university.UniversityName='{UniversityInput}'", isFirstCondition);
                isFirstCondition = false;
            }
            if (InstituteComboBox.SelectedItem != null)
            {
                string InstituteInput = InstituteComboBox.SelectedItem.ToString();
                query = AddCondition(query, $"institute.InstituteName='{InstituteInput}'", isFirstCondition);
                isFirstCondition = false;
            }
            if (YearComboBox.SelectedItem != null)
            {
                string YearInput = YearComboBox.SelectedItem.ToString();
                query = AddCondition(query, $"Thesis.Year='{YearInput}'", isFirstCondition);
                isFirstCondition = false;
            }
            if (TopicComboBox.SelectedItem != null)
            {
                string topicName = TopicComboBox.SelectedItem.ToString();
                query = AddCondition(query, $"Topic.TopicName='{topicName}'", isFirstCondition);
                isFirstCondition = false;
            }

            query += " GROUP BY thesis.ThesisNo";

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

        private string AddCondition(string query, string condition, bool isFirstCondition)
        {
            //Helper function to easily add filters to the search query


            if (isFirstCondition)
            {
                query += " WHERE " + condition;
            }
            else
            {
                query += " AND " + condition;
            }
            return query;
        }

        private void FetchAbstract(int thesisNo)
        {
            //Sets the abstractText variable's value by retrieving the information from the database.

            string Abstquery = $@"SELECT Abstract FROM thesis WHERE ThesisNo ={thesisNo}";

            using (var connection = new MySqlConnection(connection_str))
            {
                try
                {
                    connection.Open();

                    MySqlDataAdapter adapter = new MySqlDataAdapter(Abstquery, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        abstractText = dt.Rows[0]["Abstract"].ToString();
                    }

                    Console.WriteLine(abstractText);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void GetComboBoxTexts()
        {
            //Fills each ComboBox with the distinct values from the database.


            string typeComboBoxQuery = "SELECT DISTINCT Type FROM thesis;";
            string languageComboBoxQuery = "SELECT DISTINCT Language FROM thesis;";
            string yearComboBoxQuery = "SELECT DISTINCT Year FROM thesis;";
            string topicComboBoxQuery = "SELECT DISTINCT TopicName FROM topic;";


            using (var connection = new MySqlConnection(connection_str))
            {
                connection.Open();

                using (var command = new MySqlCommand(typeComboBoxQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string type = reader.GetString(0);
                            TypeComboBox.Items.Add(type);
                        }
                    }
                }
                using (var command = new MySqlCommand(languageComboBoxQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string language = reader.GetString(0);
                            LanguageComboBox.Items.Add(language);
                        }
                    }
                }
                using (var command = new MySqlCommand(yearComboBoxQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        List<int> years = new List<int>();
                        while (reader.Read())
                        {
                            int year = reader.GetInt32(0);
                            years.Add(year);

                        }

                        years.Sort();

                        foreach (int year in years)
                        {
                            YearComboBox.Items.Add(year);
                        }
                    }
                }

                using (var command = new MySqlCommand(topicComboBoxQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string type = reader.GetString(0);
                            TopicComboBox.Items.Add(type);
                        }
                    }
                }


                using (var command = new MySqlCommand("SELECT UniversityName FROM University", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string universityName = reader.GetString(0);
                            UniversityComboBox.Items.Add(universityName);
                        }
                    }
                }

                using (var command = new MySqlCommand("SELECT DISTINCT InstituteName FROM Institute", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string instituteName = reader.GetString(0);
                            InstituteComboBox.Items.Add(instituteName);
                        }
                    }
                }


            }

        }


        //----CONNECTION END-------------------------------------------------------------------------------------

        //ACTIONS
        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            TypeComboBox.Text = null;
            LanguageComboBox.Text = null;
            UniversityComboBox.Text = null;
            InstituteComboBox.Text = null;
            YearComboBox.Text = null;
            TopicComboBox.Text = null;
            txt_Input.Text = null;
            txt_Keyword.Text = null;
            txt_Author.Text = null;
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            LoadTable();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txt_Input.Clear();
            txt_Input.Focus();
        }

        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txt_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Input.Text))
            {
                textBlock_Placeholder.Visibility = Visibility.Visible;
            }
            else
            {
                textBlock_Placeholder.Visibility = Visibility.Hidden;
            }
        }

        private void btnClearKeyword_Click(object sender, RoutedEventArgs e)
        {
            txt_Keyword.Clear();
            txt_Keyword.Focus();
        }

        private void txt_Keyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Keyword.Text))
            {
                textBlock_PlaceholderKeyword.Visibility = Visibility.Visible;
            }
            else
            {
                textBlock_PlaceholderKeyword.Visibility = Visibility.Hidden;
            }
        }
        
        private void Table_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (Table.SelectedItem != null)
            {
                var thesis = Table.SelectedItem as DataRowView;
                int thesisNo = Convert.ToInt32(thesis["No"]);

                
                FetchAbstract(thesisNo);
                MessageBox.Show(abstractText, "Abstract", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                
            }
        }

        private void txt_Author_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Author.Text))
            {
                textBlock_PlaceholderAuthor.Visibility = Visibility.Visible;
            }
            else
            {
                textBlock_PlaceholderAuthor.Visibility = Visibility.Hidden;
            }
        }

        private void btnClearAuthor_Click(object sender, RoutedEventArgs e)
        {
            txt_Author.Clear();
            txt_Author.Focus();
        }

        private void txt_Supervisor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Supervisor.Text))
            {
                textBlock_PlaceholderSupervisor.Visibility = Visibility.Visible;
            }
            else
            {
                textBlock_PlaceholderSupervisor.Visibility = Visibility.Hidden;
            }
        }

        private void btnClearSupervisor_Click(object sender, RoutedEventArgs e)
        {

            txt_Supervisor.Clear();
            txt_Supervisor.Focus();
        }

        
    }
}
