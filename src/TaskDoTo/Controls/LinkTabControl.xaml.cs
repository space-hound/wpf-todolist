using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TaskDoTo.Models;

namespace TaskDoTo.Controls
{
    public partial class LinkTabControl : UserControl
    {
        private ObservableCollection<Link> links;
        private int index = 0;

        public LinkTabControl()
        {
            InitializeComponent();
            this.onLoad();
            this.events();
        }

        private void onLoad()
        {
            links = Link.Links();
            this.LinkList.ItemsSource = links;
            if (links.Count >= 1)
            {
                this.LinkList.SelectedIndex = index;
            }
        }
        private void events()
        {
            this.LinkList.SelectionChanged += onChangedSelection;
            this.LinkControl.OnAdd = new RoutedEventHandler(AddLink);
            this.Open.Click += onOpen;
            this.Delete.Click += onDelete;
        }

        private void onChangedSelection(object sender, SelectionChangedEventArgs e)
        {
            this.index = this.LinkList.SelectedIndex;
        }

        private void onDelete(object sender, RoutedEventArgs e)
        {
            this.links[index].Delete();
            this.links.RemoveAt(index);
        }

        private void onOpen(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(this.links[index].HREF);
            }
            catch
            {
                string dialog = "Wrong URL! Do you wish to delete it ?";
                if (MessageBox.Show(dialog, "Finaly", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.links[index].Delete();
                    this.links.RemoveAt(index);
                }
                else
                {
                    return;
                }
            }
        }

        private void AddLink(object sender, RoutedEventArgs e)
        {
            this.LinkControl.LinkItem.Save();
            this.links.Add(this.LinkControl.LinkItem);
        }
    }
}
