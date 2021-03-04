using LibraryBLL;
using LibraryModel;
using LibraryProject_Uwp_;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibraryProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
       
        LibraryItemBLL libraryItemBLL;
        public Employee user { get; set; }
        DispatcherTimer timer;
        
        public LoginPage()
        {
            this.InitializeComponent();
            libraryItemBLL = new LibraryItemBLL();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            
            if (txtbxUserName.Text == "") userNameblck.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            else  userNameblck.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            if (txtbxPassWord.Password == "") passwodblck.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            else passwodblck.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);              
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            var list = libraryItemBLL.GetWorkers();
            if(txtbxUserName.Text != "" && txtbxPassWord.Password != "")
            {
                txtblkEmptyFields.Visibility = Visibility.Collapsed;
                for (int i = 0; i < list.Count; i++)
                {
                    if (txtbxUserName.Text == "yoni" && txtbxPassWord.Password == "yoni")
                    {
                        user = list[i];
                        Frame.Navigate(typeof(MainPage));
                    }            
                }
            }
            else
            {
                txtblkEmptyFields.Visibility = Visibility.Visible;
                txtbxUserName.Text = "";
                txtbxPassWord.Password = "";
            }

        }
    }
}
