using System.Windows;
using System.Windows.Controls;
using TaskDoTo.Models;

namespace TaskDoTo.Controls
{
    /// <summary>
    /// Interaction logic for TaskItemControl.xaml
    /// </summary>
    public partial class TaskItemControl : UserControl
    {
        public TaskItemControl()
        {
            InitializeComponent();
            this.events();
        }

        private void events()
        {
            this.Add.Click += onAdd;
        }

        public RoutedEventHandler OnAdd;
        private void onAdd(object sender, RoutedEventArgs e)
        {
            if (this.OnAdd is null)
                return;
            this.OnAdd(sender, e);
            this.reset();
        }
        private void reset()
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
