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

            //ToDoList.ItemsSource = TasksParam;
        }
        public void AddTask()
        {
            Tasks task = new Tasks();
            /*
            task.Title = Title.Text;
            task.Description = Title.Text;
            task.Deadline = Date.SelectedDate;
            task.Priority = Priority.Text;
            task.IsCompleted = "NIEUKOŃCZONE";
            */
            TasksParam.Add(task);

            if(File.Exists(path))
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
        public void DeleteTask()
        {
            Tasks task = ToDoList.SelectedItem;
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
        public void FinishTask()
        {
            Tasks task = ToDoList.SelectedItem;
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
        public void EditTask()
        {
            Tasks task = ToDOList.SelectedItem;
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