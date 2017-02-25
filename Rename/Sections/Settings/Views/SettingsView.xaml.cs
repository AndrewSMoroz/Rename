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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rename.Sections.Settings.Views
{

    public partial class SettingsView : UserControl
    {

        //------------------------------------------------------------------------------------------------------------------------
        public SettingsView()
        {
            InitializeComponent();
        }

        ////------------------------------------------------------------------------------------------------------------------------
        // Doesn't work as-is, but the event does fire when the ListView is clicked
        //private void lvExtensionMap_Click(object sender, RoutedEventArgs e)
        //{

        //    GridViewColumnHeader currentHeader = e.OriginalSource as GridViewColumnHeader;
        //    if (currentHeader != null && currentHeader.Role != GridViewColumnHeaderRole.Padding)
        //    {
        //        using (this.Source.DeferRefresh())
        //        {

        //            Func<SortDescription, bool> lamda = item => item.PropertyName.Equals(currentHeader.Column.Header.ToString());
        //            if (this.Source.SortDescriptions.Count(lamda) > 0)
        //            {
        //                SortDescription currentSortDescription = this.Source.SortDescriptions.First(lamda);
        //                ListSortDirection sortDescription = currentSortDescription.Direction == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;


        //                currentHeader.Column.HeaderTemplate = currentSortDescription.Direction == ListSortDirection.Ascending ?
        //                    this.Resources["HeaderTemplateArrowDown"] as DataTemplate : this.Resources["HeaderTemplateArrowUp"] as DataTemplate;

        //                this.Source.SortDescriptions.Remove(currentSortDescription);
        //                this.Source.SortDescriptions.Insert(0, new SortDescription(currentHeader.Column.Header.ToString(), sortDescription));
        //            }
        //            else
        //                this.Source.SortDescriptions.Add(new SortDescription(currentHeader.Column.Header.ToString(), ListSortDirection.Ascending));
        //        }
        //    }

        //}

    }

}
