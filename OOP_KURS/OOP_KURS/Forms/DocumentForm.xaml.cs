using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace DocCreator
{
    /// <summary>
    /// Логика взаимодействия для DocumentForm.xaml
    /// </summary>
    public partial class DocumentForm : Window
    {
        private Document DocTemp = new Document();
        private int? PrevPopupIndx = null;
        private string FormMode;

        public DocumentForm(string Mode = "Create", Document DocEdit = null)
        {
            InitializeComponent();

            FormMode = Mode;

            PopupSuggest.StaysOpen = true;
            ComboBox_Customer.ItemsSource = ReferenceHelper.GetElementsByRefName("Customer");
            ComboBox_Customer.DisplayMemberPath = "Name";

            FieldCatalog.SetColumnsForDataGrid(DataGrid_Pos, "Position");
            ComboBox_DocType.ItemsSource = ReferenceHelper.GetElementsByRefName("TypeDocument");
            ComboBox_DocType.DisplayMemberPath = "Name";
           
            if (Mode == "Edit" && DocEdit != null)
            {
                DocTemp = DocEdit;
                Btn_Add.Visibility = Visibility.Hidden;
            }

            else
            {
                DatePicker.SelectedDate = DateTime.Now;
            }

            DataGrid_Pos.ItemsSource = DocTemp.Positions.GetElements();
            DataContext = DocTemp;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DocTemp.Positions.AddToList(new Position { });
        }

        private void ListBoxSuggest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxSuggest.SelectedIndex != -1)
                PopupSuggest.IsOpen = false;
        }

        private void DataGrid_Pos_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            PopupSuggest.IsOpen = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ReferenceHelper.Add(DocTemp);
            DocTemp = new Document();
            DataGrid_Pos.ItemsSource = DocTemp.Positions.GetElements();
            DataContext = DocTemp;
        }

        private void DataGrid_Pos_KeyUp(object sender, KeyEventArgs e)
        {
            DataGrid Grid = sender as DataGrid;

            int RowIndex = Grid.Items.IndexOf(Grid.CurrentItem);

            if (Grid.CurrentCell.Column.SortMemberPath != "FullName" || ListBoxSuggest.Items.Count < 1)
                return;

            if (PrevPopupIndx != RowIndex || (PrevPopupIndx == RowIndex && !PopupSuggest.IsOpen))
            {
                PopupSuggest.IsOpen = false;
                DataGridCell CurrentCell = DataGrid_Pos.GetCell(RowIndex, 1);
                PopupSuggest.Width = CurrentCell.ActualWidth;
                PopupSuggest.PlacementTarget = CurrentCell;
                PopupSuggest.IsOpen = true;
                PrevPopupIndx = RowIndex;
                ListBoxSuggest.DataContext = DocTemp.Positions.GetElements()[RowIndex];
            }

            TextBox Cell = e.OriginalSource as TextBox;
            if (Cell != null)
            {
                string CellText = Cell.Text;
                if (!string.IsNullOrEmpty(CellText) && CellText != " ")
                {
                    ListBoxSuggest.Items.Filter = f =>
                    {
                        if (f is string _text)
                            return _text.StartsWith(CellText, StringComparison.CurrentCultureIgnoreCase) || _text.IndexOf(CellText, StringComparison.OrdinalIgnoreCase) >= 0;
                        else
                            return true;
                    };
                }
                else
                {
                    ListBoxSuggest.Items.Filter = f =>
                    {
                        return true;
                    };
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (DataGrid_Pos.SelectedItems.Count > 0)
            {
                List<dynamic> SelectedItemsList = DataGrid_Pos.SelectedItems.OfType<dynamic>().ToList();
                foreach (var Item in SelectedItemsList)
                {
                    DocTemp.Positions.DeleteFromList(Item);
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ExcelHelper _ = new ExcelHelper(DocTemp);
        }

        private void ListBoxSuggest_Initialized(object sender, EventArgs e)
        {
            ObservableCollection<ProductAndService> Elements = ReferenceHelper.GetElementsByRefName("ProductAndService");

            foreach (ProductAndService chel in Elements)
            {
                ListBoxSuggest.Items.Add(chel.FullName);
            }
        }
    }

    public static class DataGridExtensions
    {
        public static DataGridCell GetCell(this DataGrid grid, Int32 row, Int32 column)
        {
            DataGridRow gridrow = grid.GetRow(row);
            if (gridrow != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(gridrow);

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(4); 
                if (cell == null)
                {
                    // now try to bring into view and retreive the cell
                    grid.ScrollIntoView(gridrow, grid.Columns[column]);

                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                }

                return (cell);
            }

            return (null);
        }

        public static DataGridRow GetRow(this DataGrid dataGrid, Int32 idx)
        {
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(idx);
            if (row == null)
            {
                dataGrid.ScrollIntoView(dataGrid.Items[idx]);
                dataGrid.UpdateLayout();

                row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(idx);
            }

            return (row);
        }

        private static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);

            Int32 numvisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (Int32 i = 0; i < numvisuals; ++i)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                    child = GetVisualChild<T>(v);
                else
                    break;
            }

            return child;
        }
    }



}
