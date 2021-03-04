using LibraryBLL;
using LibraryModel;
using LibraryProject;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LibraryProject_Uwp_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        LibraryItemBLL libraryItemBLL;
        public ComboBox TypeComboBox, genrecomboBox, bookComboBox;
        public ComboBox JournalYear, JournalMonth, JournalDay;
        public ComboBox BookYear, BookMonth, BookDay;
     
        public MainPage()
        {
            this.InitializeComponent();
            libraryItemBLL = new LibraryItemBLL();          
            libraryItemBLL.Initialization().ContinueWith((e)=> ShowListOnXaml());
            Window.Current.Closed += Current_Closed;
            this.Unloaded += MainPage_Unloaded;
            CreateGenreComboBox();
            CreateChooseTypeComboBox();
            CreateBookLengthComboBox();
            CreateTxtBlocks();
        }

        private async void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
           await libraryItemBLL.ShutDown();
        }

        private void Current_Closed(object sender, Windows.UI.Core.CoreWindowEventArgs e)
        {
            
        }

        //only numbers allowed event
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox tb = (TextBox)sender;

                if (System.Text.RegularExpressions.Regex.IsMatch(tb.Text, "[^0-9]"))
                {
                    tb.Text = tb.Text.Remove(tb.Text.Length - 1);
                }
            }
        }
        //Intefaces elements init
        public void CreateGenreComboBox()
        {
            genrecomboBox = new ComboBox();
            genrecomboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            genrecomboBox.VerticalAlignment = VerticalAlignment.Stretch;
            genrecomboBox.PlaceholderText = "Choose Genre";
            genrecomboBox.Background = new SolidColorBrush(Windows.UI.Colors.White);
            for (int i = 1; i <= 10; i++)
            {
                genrecomboBox.Items.Add((Book.Genre)i);
            }
            Grid.SetColumn(genrecomboBox, 1);
            Grid.SetRow(genrecomboBox, 4);
            gridOfBook.Children.Add(genrecomboBox);
        }
        private void ListOfItems_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var list = libraryItemBLL.GetAllItems();
            for (int i = 0; i < list.Count; i++)
            {              
                if (i == list.Count - 1)
                {
                    libraryItemBLL.RemoveItem(list[i]);
                }
            }
            ListOfItems.ItemsSource = list;
        }
        public void CreateBookLengthComboBox()
        {
            bookComboBox = new ComboBox();
            bookComboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            bookComboBox.VerticalAlignment = VerticalAlignment.Stretch;
            bookComboBox.PlaceholderText = "Book Length";
            bookComboBox.Background = new SolidColorBrush(Windows.UI.Colors.White);
            for (int i = 0; i <= 2; i++)
            {
                bookComboBox.Items.Add((Book.BookLength)i);
            }
            Grid.SetColumn(bookComboBox, 1);
            Grid.SetRow(bookComboBox, 5);
            gridOfBook.Children.Add(bookComboBox);
        }
        public void CreateChooseTypeComboBox()
        {
            TypeComboBox = new ComboBox();
            TypeComboBox.Items.Add("Book");
            TypeComboBox.Items.Add("Journal");
            TypeComboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            TypeComboBox.VerticalAlignment = VerticalAlignment.Top;
            TypeComboBox.FontSize = 30;
            TypeComboBox.Height = 70;
            TypeComboBox.Width = 250;
            TypeComboBox.Background = new SolidColorBrush(Windows.UI.Colors.Black);
            TypeComboBox.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            TypeComboBox.PlaceholderText = "Choose Type";
            TypeComboBox.PlaceholderForeground = new SolidColorBrush(Windows.UI.Colors.White);
            TypeComboBox.SelectionChanged += C_SelectionChanged;
            Grid.SetColumn(TypeComboBox, 2);
            Grid.SetRow(TypeComboBox, 1);
            mainGrid.Children.Add(TypeComboBox);
        }
        public void CreateTxtBlocks()
        {
            string[] txtBlocksName = new string[] { "Book name:", "Print date:", "Price:", "Writer name", "Choose genre:", "Choose length:", "Short discription:" };
            for (int i = 0; i < txtBlocksName.Length; i++)
            {
                TextBlock tb = new TextBlock();
                tb.FontSize = 20;
                tb.FontWeight = Windows.UI.Text.FontWeights.Bold;
                tb.Text = txtBlocksName[i].ToString();
                Grid.SetColumn(tb, 0);
                Grid.SetRow(tb, i);
                if (i == txtBlocksName.Length - 1) Grid.SetRow(tb, i + 1);         
                gridOfBook.Children.Add(tb);
            }
        }
        private void C_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (TypeComboBox.SelectedIndex)
            {
                case 0://book
                    gridOfJournal.Visibility = Visibility.Collapsed;
                    gridOfBook.Visibility = Visibility.Visible;
                    break;
                case 1://journal
                    gridOfBook.Visibility = Visibility.Collapsed;
                    gridOfJournal.Visibility = Visibility.Visible;
                    break;
            }
        }
        public void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TypeComboBox.SelectedItem is null)
            {
                TypeChooseWarn.Visibility = Visibility.Visible;
                return;
            }
            else TypeChooseWarn.Visibility = Visibility.Collapsed;

            switch (TypeComboBox.SelectedIndex)
            {
                case 0://Book
                    //checks that all the field arent empty
                    if (BooktxtBoxName.Text == "" || BooktxtBoxDiscription.Text == "" || genrecomboBox.SelectedItem.ToString() == null || BooktxtBoxPrice.Text == "" || datePickerBook.Date == null || BooktxtBoxWriterName.Text == "" || bookComboBox.SelectedItem.ToString() == null)
                    {
                        emptyFieldsWarn.Visibility = Visibility.Visible;
                        return;
                    }
                    else emptyFieldsWarn.Visibility = Visibility.Collapsed;
                    libraryItemBLL.AddItem( new Book(BooktxtBoxName.Text, BooktxtBoxDiscription.Text, (Book.Genre)genrecomboBox.SelectedItem, decimal.Parse(BooktxtBoxPrice.Text))
                    {
                        bookLength = (Book.BookLength)bookComboBox.SelectedItem,
                        ReleaseYear = datePickerBook.Date,
                        WriterName = BooktxtBoxWriterName.Text,
                        ISBN = libraryItemBLL.IsbnGenerator(BooktxtBoxName.Text, BooktxtBoxWriterName.Text),
                        CopyNumber = libraryItemBLL.CalculateQuantity(BooktxtBoxName.Text, BooktxtBoxWriterName.Text, new Book("", "", (Book.Genre)genrecomboBox.SelectedItem, 34))
                    }); 
                    break;
                case 1://Journal
                    //checks that all the field arent empty
                    if (JournaltxtBoxName.Text == "" || JournaltxtBoxTitle.Text == "" || JournaltxtBoxEditor.Text == "" || JournaltxtBoxPrice.Text == "" || datePickerJournal.Date == null)
                    {
                        emptyFieldsWarn.Visibility = Visibility.Visible;
                        return;
                    }
                    else emptyFieldsWarn.Visibility = Visibility.Collapsed;
                    libraryItemBLL.AddItem(new Journal(JournaltxtBoxName.Text, JournaltxtBoxTitle.Text, JournaltxtBoxEditor.Text, decimal.Parse(JournaltxtBoxPrice.Text))
                    {
                        ReleaseYear = datePickerJournal.Date,
                        CopyNumber = libraryItemBLL.CalculateQuantity(JournaltxtBoxName.Text, JournaltxtBoxEditor.Text, new Journal("", "", "", 34))
                    });
                    break;
            }
            ShowListOnXaml();
        }
        public async void ShowListOnXaml()
        {
          await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High , () =>  ListOfItems.ItemsSource = libraryItemBLL.GetAllItems()); 
        }
    }
}
