using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

            Title.Text = task.Title;
            Description.Text = task.Description;
            Date.SelectedDate = task.Deadline;
            Priority.Text = task.Priority;
        }
        public void EditTask()
        {

            string title = task.Title;
            List<Tasks> tasks = TasksParam;
            Tasks result = tasks.Find(t => t.Title == title);

            result.Title = Title.Text;
            result.Description = Title.Text;
            result.Deadline = Title.Text;
            result.Priority = Title.Text;

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
    }
}
