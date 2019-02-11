using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TaskDoTo.Models;

namespace TaskDoTo.Controls
{
    /// <summary>
    /// Interaction logic for TaskTabControl.xaml
    /// </summary>
    public partial class TaskTabControl : UserControl
    {
        private ObservableCollection<Task> assgined;
        private int assg_index = 0;
        private ObservableCollection<Task> done;
        private int done_index = 0;

        public TaskTabControl()
        {
            InitializeComponent();
            this.onLoad();
            this.events();
        }

        private void onLoad()
        {
            this.assgined = Task.AssignedTasks();
            this.AssignedTasks.ItemsSource = this.assgined;

            this.done = Task.DoneTasks();
            this.DoneTasks.ItemsSource = this.done;

            if (this.assgined.Count > 1)
                this.AssignedTasks.SelectedIndex = assg_index;
            if (this.done.Count > 1)
                this.DoneTasks.SelectedIndex = done_index;
        }

        private void events()
        {
            this.CreateTask.OnAdd = new RoutedEventHandler(AddTask);
            this.AssignedTasks.SelectionChanged += assgSelChanged;
            this.DoneTasks.SelectionChanged += doneSelChanged;

            this.UpdateTask.OnUpdate = new RoutedEventHandler(UpdateTaskHandler);
            this.UpdateTask.OnDelete = new RoutedEventHandler(OnDeleteHandler);
            this.UpdateTask.OnDone = new RoutedEventHandler(OnDoneHandler);

            this.Swap.Click += SwapHandler;
            this.Clear.Click += ClearHandler;
        }

        private void ClearHandler(object sender, RoutedEventArgs e)
        {
            if (this.done.Count <= 0)
                return;

            this.done[this.done_index].Delete();
            this.done.RemoveAt(this.done_index);

            if (this.done_index - 1 >= 0)
                this.done_index--;
            else if (this.done_index + 1 < this.done.Count)
                this.done_index++;
            else
                this.done_index++;
        }

        private void SwapHandler(object sender, RoutedEventArgs e)
        {
            if (this.done.Count <= 0)
                return;

            this.done[this.done_index].StatusVal = 0;
            this.done[this.done_index].Update();

            this.assgined.Add(this.done[this.done_index]);
            this.done.RemoveAt(this.done_index);

            if (this.done_index - 1 >= 0)
                this.done_index--;
            else if (this.done_index + 1 < this.done.Count)
                this.done_index++;
            else
                this.done_index++;
        }

        private void OnDoneHandler(object sender, RoutedEventArgs e)
        {
            this.assgined[this.assg_index].StatusVal = 1;
            this.assgined[assg_index].Update();

            this.done.Add(this.assgined[assg_index]);
            this.assgined.RemoveAt(this.assg_index);

            this.UpdateTask.reset();
        }

        private void OnDeleteHandler(object sender, RoutedEventArgs e)
        {
            this.assgined[this.assg_index].Delete();
            this.assgined.RemoveAt(assg_index);

            if (this.assg_index - 1 >= 0)
                this.assg_index--;
            else if (this.assg_index + 1 < this.assgined.Count)
                this.assg_index++;
            else
                this.assg_index++;
        }

        private void UpdateTaskHandler(object sender, RoutedEventArgs e)
        {
            ulong _id = this.assgined[this.assg_index].ID;
            string _title = this.UpdateTask.TaskItem.Title;
            string _desc = this.UpdateTask.TaskItem.Description;
            int _status = 0;

            this.assgined[this.assg_index] = new Task(_id, _title, _desc, _status);
            this.UpdateTask.TaskItem = this.assgined[this.assg_index];
            this.assgined[this.assg_index].Update();
        }

        private void assgSelChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.AssignedTasks.Items.Count == 0)
            {
                this.UpdateTask.reset();
                return;
            }

            //if (this.assg_index > this.assgined.Count-1 || this.assg_index < 0)
            //    return;

            if (this.AssignedTasks.SelectedIndex == -1)
                return;

            this.assg_index = this.AssignedTasks.SelectedIndex;
            this.UpdateTask.TaskItem = this.assgined[this.assg_index];
        }

        private void doneSelChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DoneTasks.Items.Count == 0)
                return;

            if (this.done_index > this.done.Count || this.done_index < 0)
                return;

            this.done_index = this.DoneTasks.SelectedIndex;
        }

        private void AddTask(object sender, RoutedEventArgs e)
        {
            Task toAdd = this.CreateTask.TaskItem;
            toAdd.Save();
            this.assgined.Add(toAdd);
        }


    }
}
