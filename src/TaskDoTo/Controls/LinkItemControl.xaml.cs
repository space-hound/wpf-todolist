using System.Windows;
using System.Windows.Controls;
using TaskDoTo.Models;

namespace TaskDoTo.Controls
{
    public partial class LinkItemControl : UserControl
    {
        public LinkItemControl()
        {
            InitializeComponent();
            this.events();
        }

        private void events()
        {
            this.AddLink.Click += onAdd;
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
            this.Names = "";
            this.Href = "";
        }

        public string Names
        {
            get
            {
                return this.LinkName.Text;
            }
            set
            {
                this.LinkName.Text = value;
            }
        }

        public string Href
        {
            get
            {
                return this.LinkHref.Text;
            }
            set
            {
                this.LinkHref.Text = value;
            }
        }

        public Link LinkItem
        {
            get
            {
                return new Link(this.Href, this.Names);
            }
            set
            {
                this.Names = value.Name;
                this.Href = value.HREF;
            }
        }
    }
}
