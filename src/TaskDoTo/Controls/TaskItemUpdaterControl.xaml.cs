using System.Windows;
using System.Windows.Controls;
using TaskDoTo.Models;

namespace TaskDoTo.Controls
{
    /// <summary>
    /// Interaction logic for TaskItemUpdaterControl.xaml
    /// </summary>
    public partial class TaskItemUpdaterControl : UserControl
    {
        public TaskItemUpdaterControl()
        {
            InitializeComponent();
            this.events();
        }

        private void events()
        {
            this.Update.Click += onUpdate;
            this.Delete.Click += onDelete;
            this.Done.Click += onDone;
        }

        public RoutedEventHandler OnDone;
        private void onDone(object sender, RoutedEventArgs e)
        {
            if (this.OnDone is null)
                return;
            this.OnDone(sender, e);
        }

        public RoutedEventHandler OnDelete;
        private void onDelete(object sender, RoutedEventArgs e)
        {
            if (this.OnDelete is null)
                return;
            this.OnDelete(sender, e);
        }

        public RoutedEventHandler OnUpdate;
        private void onUpdate(object sender, RoutedEventArgs e)
        {
            if (this.OnUpdate is null)
                return;
            this.OnUpdate(sender, e);
        }

        public void reset()
        {
            this.Title = "";
            this.Desc = "";
        }

        public string Title
        {
            get
            {
                return this.TitleItem.Text;
            }
            set
            {
                this.TitleItem.Text = value;
            }
        }
        public string Desc
        {
            get
            {
                return this.DescItem.Text;
            }
            set
            {
                this.DescItem.Text = value;
            }
        }

        public Task TaskItem
        {
            get
            {
                return new Task(this.Title, this.Desc, 0);
            }
            set
            {
                this.Title = value.Title;
                this.Desc = value.Description;
            }
        }
    }
}
