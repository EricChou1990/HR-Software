using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace Asign2
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : DevExpress.Xpf.Core.DXWindow
    {
        public Report()
        {
            InitializeComponent();
            Loaded  += new RoutedEventHandler(OnLoaded);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            if (tab1.SelectedIndex==0)
            {
                if (datagrid2.SelectedItem != null)
                {   
                    object item = datagrid2.SelectedItem;
                    string ID = (datagrid2.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                    Clipboard.SetText(ID);
                    MessageBox.Show("Email address is copied to clipboard");
                }
                else
                {
                    MessageBox.Show("You have to select a record first");
                }
            }
            else if (tab1.SelectedIndex == 1)
            {
                if (datagrid3.SelectedItem != null)
                {
                    object item = datagrid3.SelectedItem;
                    string ID = (datagrid3.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                    Clipboard.SetText(ID);
                    MessageBox.Show("Email address is copied to clipboard");
                }
                else
                {
                    MessageBox.Show("You have to select a record first");
                }
            }
            else if (tab1.SelectedIndex==2)
            {
                if (datagrid4.SelectedItem != null)
                {
                    object item = datagrid4.SelectedItem;
                    string ID = (datagrid4.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                    Clipboard.SetText(ID);
                    MessageBox.Show("Email address is copied to clipboard");
                }
                else
                {
                    MessageBox.Show("You have to select a record first");
                }
            }
            else if (tab1.SelectedIndex==3)
            {

                if (datagrid5.SelectedItem != null)
                {
                    object item = datagrid5.SelectedItem;
                    string ID = (datagrid5.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                    Clipboard.SetText(ID);
                    MessageBox.Show("Email address is copied to clipboard");
                }
                else
                {
                    MessageBox.Show("You have to select a record first");
                }
            }                 
        }
        private void sorting(DataGrid datagrid_sort, ListSortDirection sortDirection )
        {
            int columnIndex = 1;
            //sortDirection = ListSortDirection.Descending;
            //MessageBox.Show(datagrid_sort.Columns.Count.ToString());
            var column = datagrid_sort.Columns[columnIndex];
            // Clear current sort descriptions
            datagrid_sort.Items.SortDescriptions.Clear();
            // Add the new sort description
            datagrid_sort.Items.SortDescriptions.Add(new SortDescription(column.SortMemberPath, sortDirection));
            // Apply sort
            foreach (var col in datagrid_sort.Columns)
            {
                col.SortDirection = null;
            }
            column.SortDirection = sortDirection;
            // Refresh items to display sort
            datagrid_sort.Items.Refresh();
        }       

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            sorting(datagrid2, ListSortDirection.Descending);  
            sorting(datagrid3, ListSortDirection.Descending);
            sorting(datagrid4, ListSortDirection.Ascending);
            sorting(datagrid5, ListSortDirection.Ascending);
            
        }
    }
}
