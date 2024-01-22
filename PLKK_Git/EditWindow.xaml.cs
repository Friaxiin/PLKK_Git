using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PLKK_Git
{
    /// <summary>
    /// Logika interakcji dla klasy EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ToDo.json");
        public List<Tasks> TasksParam = new List<Tasks>();
        string OldTitle = "";
        public EditWindow(Tasks task)
        {
            InitializeComponent();
            OldTitle = task.Title;

            title.Text = task.Title;
            description.Text = task.Description;
            date.SelectedDate = task.Deadline;
            comboBox.Text = task.Priority;
        }
        private void EditTask(object sender, RoutedEventArgs e)
        {
            string newTitle = title.Text;
            List<Tasks> tasks = TasksParam;
            Tasks result = tasks.Find(t => t.Title == newTitle);

            result.Title = title.Text;
            result.Description = description.Text;
            result.Deadline = date.SelectedDate.Value;
            result.Priority = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();

            if (File.Exists(path))
            {
                string serializedTasks = JsonConvert.SerializeObject(TasksParam);

                File.WriteAllText(path, serializedTasks);

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                string serializedTasks = JsonConvert.SerializeObject(new List<Tasks>());

                File.WriteAllText(path, serializedTasks);

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
