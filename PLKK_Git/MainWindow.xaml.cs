using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace PLKK_Git
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ToDo.json");
        public List<Tasks> TasksParam = new List<Tasks>();
        public MainWindow()
        {
            InitializeComponent();

            //ReadTasks();
        }
        public void ReadTasks()
        {
            string serializedTasks = File.ReadAllText(path);
            List<Tasks> tasks = JsonConvert.DeserializeObject<List<Tasks>>(serializedTasks);

            TasksParam = tasks;

            TaskList.ItemsSource = TasksParam;
        }
        public void AddTask(object sender, RoutedEventArgs e)
        {
            try
            {


                Tasks task = new Tasks();

                task.Title = title.Text;
                task.Description = description.Text;
                task.Deadline = date.SelectedDate.Value;
                task.Priority = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();
                task.IsCompleted = "NIEUKOŃCZONE";

                TasksParam.Add(task);

                if (File.Exists(path))
                {
                    string serializedTasks = JsonConvert.SerializeObject(TasksParam);

                    File.WriteAllText(path, serializedTasks);

                    ReadTasks();
                }
                else
                {
                    string serializedTasks = JsonConvert.SerializeObject(new List<Tasks>());

                    File.WriteAllText(path, serializedTasks);

                    ReadTasks();
                }
            }
            catch
            {
                MessageBox.Show("Uzupełnij wszystkie pola", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeleteTask(object sender, RoutedEventArgs e)
        {
            Tasks task = (Tasks)TaskList.SelectedItem;
            if (task != null)
            {
                string title = task.Title;
                List<Tasks> tasks = TasksParam;
                Tasks result = tasks.Find(t => t.Title == title);
                TasksParam.Remove(task);

                if (File.Exists(path))
                {
                    string serializedTasks = JsonConvert.SerializeObject(TasksParam);

                    File.WriteAllText(path, serializedTasks);

                    ReadTasks();
                }
                else
                {
                    string serializedTasks = JsonConvert.SerializeObject(new List<Tasks>());

                    File.WriteAllText(path, serializedTasks);

                    ReadTasks();
                }
            }
            else
            {
                MessageBox.Show("Zaznacz element listy", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void FinishTask(object sender, RoutedEventArgs e)
        {
            Tasks task = (Tasks)TaskList.SelectedItem;
            if (task != null)
            {
                string title = task.Title;
                List<Tasks> tasks = TasksParam;
                Tasks result = tasks.Find(t => t.Title == title);

                result.IsCompleted = "UKOŃCZONE";

                if (File.Exists(path))
                {
                    string serializedTasks = JsonConvert.SerializeObject(TasksParam);

                    File.WriteAllText(path, serializedTasks);

                    ReadTasks();
                }
                else
                {
                    string serializedTasks = JsonConvert.SerializeObject(new List<Tasks>());

                    File.WriteAllText(path, serializedTasks);

                    ReadTasks();
                }
            }
            else
            {
                MessageBox.Show("Zaznacz element listy", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void EditTask(object sender, RoutedEventArgs e)
        {
            Tasks task = (Tasks)TaskList.SelectedItem;
            if (task != null)
            {
                EditWindow editWindow = new EditWindow(task);
                editWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Zaznacz element listy", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}