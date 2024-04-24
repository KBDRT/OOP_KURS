using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OOP_KURS
{
    /// <summary>
    /// Логика взаимодействия для DocumentForm.xaml
    /// </summary>
    public partial class DocumentForm : Window
    {

        private Document DocTemp = new Document();
        private int? PrevPopupIndx = null;

        private PositionReference DocPositions = new PositionReference();


        public DocumentForm(string Mode = "Create", Document DocEdit = null)
        {
            InitializeComponent();

            PopupSuggest.StaysOpen = true;
            ComboBox_Customer.ItemsSource = ReferenceHelper.GetElementsByRefName("Customer");
            ComboBox_Customer.DisplayMemberPath = "Name";

            FieldCatalog.SetColumnsForDataGrid(DataGrid_Pos, "Position");

            ObservableCollection<ProductAndService> Elements;

            ComboBox_DocType.ItemsSource = ReferenceHelper.GetElementsByRefName("TypeDocument");
            ComboBox_DocType.DisplayMemberPath = "Name";

            Elements = ReferenceHelper.GetElementsByRefName("ProductAndService");

            if (Elements.Count > 0)
            {
                ListBox.ItemsSource = Elements;
                ListBox.DisplayMemberPath = "FullName";
            }
            else
            {
                ListBox.Items.Add("Test222");
                ListBox.Items.Add("Test111");
            }

            DataContext = DocTemp;


            if (Mode == "Edit" && DocEdit != null)
            {
                DocTemp = DocEdit;
                //foreach (var Elem in DocEdit.Positions)
                //{
                //    DocPositions.AddToList(Elem);
                //}
            }

            else
            {
                DocTemp.Positions = DocPositions.GetElements();

                DatePicker.SelectedDate = DateTime.Now;

                //DataGrid_Pos.ItemsSource = DocPositions.GetElements();

                if (ComboBox_DocType.Items.Count > 0)
                    DocTemp.Type = (TypeDocument)ComboBox_DocType.Items[0];
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DocPositions.AddToList(new Position { });
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox.SelectedIndex != -1)
                PopupSuggest.IsOpen = false;
        }

        private void DataGrid_Pos_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            PopupSuggest.IsOpen = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ReferenceHelper.AddCopy(DocTemp);
        }

        private void DataGrid_Pos_KeyUp(object sender, KeyEventArgs e)
        {
            DataGrid Grid = sender as DataGrid;

            int RowIndex = Grid.Items.IndexOf(Grid.CurrentItem);

            if (Grid.CurrentCell.Column.SortMemberPath != "FullName")
                return;

            if (PrevPopupIndx != RowIndex || (PrevPopupIndx == RowIndex && !PopupSuggest.IsOpen))
            {
                PopupSuggest.IsOpen = false;
                DataGridCell CurrentCell = DataGrid_Pos.GetCell(RowIndex, 1);
                PopupSuggest.Width = CurrentCell.ActualWidth;
                PopupSuggest.PlacementTarget = CurrentCell;
                PopupSuggest.IsOpen = true;
                PrevPopupIndx = RowIndex;
                ListBox.DataContext = DocTemp.Positions[RowIndex];
            }

            TextBox Cell = e.OriginalSource as TextBox;
            if (Cell != null)
            {
                string CellText = Cell.Text;
                if (!string.IsNullOrEmpty(CellText) && CellText != " ")
                {
                    ListBox.Items.Filter = f =>
                    {
                        string _text = f as string;
                        return _text.StartsWith(CellText, StringComparison.CurrentCultureIgnoreCase) || _text.IndexOf(CellText, StringComparison.OrdinalIgnoreCase) >= 0;
                    };
                }
                else
                {
                    ListBox.Items.Filter = f =>
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
                    DocPositions.DeleteFromList(Item);
                }
            }
        }
    }

    public static class DataGridExtensions
    {
        /// <summary>
        /// Returns a DataGridCell for the given row and column
        /// </summary>
        /// <param name="grid">The DataGrid</param>
        /// <param name="row">The zero-based row index</param>
        /// <param name="column">The zero-based column index</param>
        /// <returns>The requested DataGridCell, or null if the indices are out of range</returns>
        public static DataGridCell GetCell(this DataGrid grid, Int32 row, Int32 column)
        {
            DataGridRow gridrow = grid.GetRow(row);
            if (gridrow != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(gridrow);

                // try to get the cell but it may possibly be virtualized
                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(4); // ???
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

        /// <summary>
        /// Gets the DataGridRow based on the given index
        /// </summary>
        /// <param name="idx">The zero-based index of the container to get</param>
        public static DataGridRow GetRow(this DataGrid dataGrid, Int32 idx)
        {
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(idx);
            if (row == null)
            {
                // may be virtualized, bring into view and try again
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
