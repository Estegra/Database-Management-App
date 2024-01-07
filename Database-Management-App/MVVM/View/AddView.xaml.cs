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
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Database_Management_App.MVVM.View
{
    public partial class AddView : UserControl
    {
        public AddView()
        {
            InitializeComponent();
            this.DataContext = this;

            GetPassword("Password.json");

            TypeComboBox.Items.Add("Master");
            TypeComboBox.Items.Add("Doctorate");
            TypeComboBox.Items.Add("Specialization in Medicine");
            TypeComboBox.Items.Add("Proficiency in Art");

            LanguageComboBox.Items.Add("Turkish");
            LanguageComboBox.Items.Add("English");
            LanguageComboBox.Items.Add("French");
            LanguageComboBox.Items.Add("Russian");
            LanguageComboBox.Items.Add("Arabic");

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






        int universityID = 0;
        private int GetOrCreateUniID(string universityName, MySqlConnection connection)
        {
            string checkQuery = "SELECT UniversityID FROM University WHERE UniversityName = @UniversityName;";

            using (var command = new MySqlCommand(checkQuery, connection))
            {
                command.Parameters.AddWithValue("@UniversityName", universityName);
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    universityID = Convert.ToInt32(result);
                }
                else
                {
                    string insertUniversityQuery = "INSERT INTO University (UniversityName) VALUES (@UniversityName); SELECT LAST_INSERT_ID();";
                    command.CommandText = insertUniversityQuery;
                    universityID = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return universityID;
        }

        private int GetOrCreateInsID(string instituteName, int universityID, MySqlConnection connection)
        {
            int instituteID = 0;
            string checkQuery = "SELECT InstituteID FROM institute WHERE InstituteName = @InstituteName AND UniversityID = @UniversityID;";

            using (var command = new MySqlCommand(checkQuery, connection))
            {
                command.Parameters.AddWithValue("@InstituteName", instituteName);
                command.Parameters.AddWithValue("@UniversityID", universityID);
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    instituteID = Convert.ToInt32(result);
                }
            }

            if (instituteID == 0)
            {
                string insertInstituteQuery = "INSERT INTO institute (InstituteName, UniversityID) VALUES (@InstituteName, @UniversityID); SELECT LAST_INSERT_ID();";
                using (var insertCommand = new MySqlCommand(insertInstituteQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@InstituteName", instituteName);
                    insertCommand.Parameters.AddWithValue("@UniversityID", universityID);
                    instituteID = Convert.ToInt32(insertCommand.ExecuteScalar());
                }
            }

            return instituteID;
        }

        private int GetOrCreateAuthorID(string authorName, MySqlConnection connection)
        {
            int authorID = 0;
            string checkQuery = "SELECT PersonID FROM person WHERE PersonName = @AuthorName;";

            using (var command = new MySqlCommand(checkQuery, connection))
            {
                command.Parameters.AddWithValue("@AuthorName", authorName);
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    authorID = Convert.ToInt32(result);
                }
            }

            if (authorID == 0)
            {
                string insertAuthorQuery = "INSERT INTO person (PersonID, PersonName) VALUES (@PersonID, @PersonName); SELECT LAST_INSERT_ID();";
                using (var insertCommand = new MySqlCommand(insertAuthorQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@PersonID", authorID);
                    insertCommand.Parameters.AddWithValue("@PersonName", authorName);
                    authorID = Convert.ToInt32(insertCommand.ExecuteScalar());
                }
            }

            return authorID;
        }

        private int GetOrCreateSupervisorID(string supervisorName, MySqlConnection connection)
        {
            int supervisorID = 0;
            string checkQuery = "SELECT PersonID FROM person WHERE PersonName = @SupervisorName;";

            using (var command = new MySqlCommand(checkQuery, connection))
            {
                command.Parameters.AddWithValue("@SupervisorName", supervisorName);
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    supervisorID = Convert.ToInt32(result);
                }
            }

            if (supervisorID == 0)
            {
                string insertSupervisorQuery = "INSERT INTO person (PersonID, PersonName) VALUES (@PersonID, @PersonName); SELECT LAST_INSERT_ID();";
                using (var insertCommand = new MySqlCommand(insertSupervisorQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@PersonID", supervisorID);
                    insertCommand.Parameters.AddWithValue("@PersonName", supervisorName);
                    supervisorID = Convert.ToInt32(insertCommand.ExecuteScalar());
                }
            }

            return supervisorID;
        }

       private int GetOrCreateTopicID(string topicName, MySqlConnection connection)
        {
            int topicID = 0;
            string checkQuery = "SELECT TopicName FROM topic WHERE TopicName = @TopicName";

            using (var command = new MySqlCommand(checkQuery, connection))
            {
                command.Parameters.AddWithValue("@TopicName", topicName);
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    topicID = Convert.ToInt32(result);
                }
            }

            if(topicID == 0)
            {
                string insertTopicQuery = "INSERT INTO topic (TopicName) VALUES(@TopicName); SELECT LAST_INSERT_ID();";
                using (var insertCommand = new MySqlCommand(insertTopicQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@TopicName", topicName);
                    topicID = Convert.ToInt32(insertCommand.ExecuteScalar());
                }
            }

            return topicID;


        }


        private void SubmitThesis()
        {
            int thesisNo = 0;

            // Collect data from input controls
            string title = txt_Title.Text;
            string abstractText = txt_Abstract.Text;
            string authorName = txt_Author.Text;
            string supervisorName = txt_Supervisor.Text;
            string type = TypeComboBox.SelectedItem?.ToString();
            string universityName = txt_University.Text;
            string instituteName = txt_Institute.Text;
            string topicName = txt_Topic.Text;
            string keyword = txt_Keyword.Text;
            string language = LanguageComboBox.SelectedItem?.ToString();
            int numberOfPages = int.TryParse(txt_NoP.Text, out int pages) ? pages : 0;
            DateTime submissionDate = DateTime.Now;
            int year = Convert.ToInt32(((DateTime)submissionDate).Year);

            // Null Check
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(abstractText) ||
                string.IsNullOrWhiteSpace(authorName) || string.IsNullOrWhiteSpace(supervisorName) ||
                string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(universityName) ||
                string.IsNullOrWhiteSpace(instituteName) || string.IsNullOrWhiteSpace(language) ||
                numberOfPages <= 0)
            {
                MessageBox.Show("Please fill all the fields correctly.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var connection = new MySqlConnection(connection_str))
            {
                try
                {
                    connection.Open();

                    int universityID = GetOrCreateUniID(universityName, connection);
                    int instituteID = GetOrCreateInsID(instituteName, universityID, connection);
                    int authorID = GetOrCreateAuthorID(authorName, connection);
                    int supervisorID = GetOrCreateSupervisorID(supervisorName, connection);

                    string insertThesisQuery = @"
                INSERT INTO Thesis (Title, Abstract, AuthorID, Year, Type, SupervisorID, UniversityID, InstituteID, NumberOfPages, Language, SubmissionDate)
                VALUES (@Title, @Abstract, @AuthorID, @Year, @Type, @SupervisorID, @UniversityID, @InstituteID, @NumberOfPages, @Language, @SubmissionDate); SELECT LAST_INSERT_ID();";

                    using (var command = new MySqlCommand(insertThesisQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@Abstract", abstractText);
                        command.Parameters.AddWithValue("@AuthorID", authorID);
                        command.Parameters.AddWithValue("@Year", year);
                        command.Parameters.AddWithValue("@Type", type);
                        command.Parameters.AddWithValue("@SupervisorID", supervisorID);
                        command.Parameters.AddWithValue("@UniversityID", universityID);
                        command.Parameters.AddWithValue("@InstituteID", instituteID);
                        command.Parameters.AddWithValue("@NumberOfPages", numberOfPages);
                        command.Parameters.AddWithValue("@Language", language);
                        command.Parameters.AddWithValue("@SubmissionDate", submissionDate);


                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            thesisNo = Convert.ToInt32(result);
                        }
                    }

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error Submitting Thesis:\n{ex.Message}", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            using (var connection = new MySqlConnection(connection_str))
            {
                try
                {
                    connection.Open();

                    int topicID = GetOrCreateTopicID(topicName, connection);
                    string thesisTopicQuery = @"INSERT INTO thesistopic(ThesisNo,TopicID) VALUES (@ThesisNo, @TopicID);";
                    string keywordQuery = @"UPDATE thesis SET Keywords = @Keywords WHERE ThesisNo = @ThesisNo;";

                    using (var command = new MySqlCommand(thesisTopicQuery, connection))
                    {
                        command.Parameters.AddWithValue(@"ThesisNo", thesisNo);
                        command.Parameters.AddWithValue(@"TopicID", topicID);
                        
                        command.ExecuteNonQuery();
                    }

                    using (var command = new MySqlCommand(keywordQuery, connection))
                    {
                        command.Parameters.AddWithValue(@"Keywords", keyword);
                        command.Parameters.AddWithValue(@"ThesisNo", thesisNo);

                        command.ExecuteNonQuery();
                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error Submitting Topics:\n{ex.Message}", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            MessageBox.Show("Thesis submitted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

       

        private void txt_Title_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Title.Text))
            {
                textBlock_PlaceholderTitle.Visibility = Visibility.Visible;
            }
            else
            {
                textBlock_PlaceholderTitle.Visibility = Visibility.Hidden;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txt_Title.Text = null;
            txt_Title.Focus();  
        }

        private void txt_Abstract_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Abstract.Text))
            {
                textBlock_PlaceholderAbstract.Visibility = Visibility.Visible;
            }
            else
            {
                textBlock_PlaceholderAbstract.Visibility = Visibility.Hidden;
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
            txt_Author.Text = null;
            txt_Author.Focus();
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

        private void btnClearKeyword_Click(object sender, RoutedEventArgs e)
        {
            txt_Keyword.Text = null;
            txt_Keyword.Focus();

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
            txt_Supervisor.Text = null;
            txt_Supervisor.Focus();
        }

        

        private void txt_NoP_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_NoP.Text))
            {
                textBlock_PlaceholderNoP.Visibility = Visibility.Visible;
            }
            else
            {
                textBlock_PlaceholderNoP.Visibility = Visibility.Hidden;
            }
        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            SubmitThesis();
        }

        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            TypeComboBox.Text = null;
            LanguageComboBox.Text = null;
            txt_University.Text = null;
            txt_Institute.Text = null;
            txt_Title.Text = null;
            txt_Keyword.Text = null;
            txt_Author.Text = null;
        }

        private void btnClearNoP_Click(object sender, RoutedEventArgs e)
        {
            txt_NoP.Text = null;
            txt_NoP.Focus();
        }

        private void txt_Institute_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Institute.Text))
            {
                textBlock_PlaceholderInstitute.Visibility = Visibility.Visible;
            }
            else
            {
                textBlock_PlaceholderInstitute.Visibility = Visibility.Hidden;
            }
        }

        private void btnClearInstitute_Click(object sender, RoutedEventArgs e)
        {
            txt_Institute.Text = null;
            txt_Institute.Focus();
        }

        private void txt_University_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_University.Text))
            {
                textBlock_PlaceholderUniversity.Visibility = Visibility.Visible;
            }
            else
            {
                textBlock_PlaceholderUniversity.Visibility = Visibility.Hidden;
            }
        }

        private void btnClearUniversity_Click(object sender, RoutedEventArgs e)
        {
            txt_University.Text = null;
            txt_University.Focus();
        }

        private void txt_CoSupervisor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_CoSupervisor.Text))
            {
                textBlock_PlaceholderCoSupervisor.Visibility = Visibility.Visible;
            }
            else
            {
                textBlock_PlaceholderCoSupervisor.Visibility = Visibility.Hidden;
            }
        }

        private void btnClearCoSupervisor_Click(object sender, RoutedEventArgs e)
        {
            txt_CoSupervisor.Text = null;
            txt_CoSupervisor.Focus();
        }

        private void txt_Topic_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Topic.Text))
            {
                textBlock_PlaceholderTopic.Visibility = Visibility.Visible;
            }
            else
            {
                textBlock_PlaceholderTopic.Visibility = Visibility.Hidden;
            }
        }

        private void btnClearTopic_Click(object sender, RoutedEventArgs e)
        {
            txt_Topic.Text = null;
            txt_Topic.Focus();
        }
    }
}
