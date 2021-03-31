using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MahApps.Metro.Controls.Dialogs;

namespace WpfSMSApp.View.User
{
    /// <summary>
    /// MyAccountView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DeActiveUser : Page
    {
        public DeActiveUser()
        {
            InitializeComponent();
        }

        private void ChangeLabelVisible(Visibility visible)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
               
             
            }
            catch (Exception ex)
            {
                Common.LOGGER.Error($"예외발생 MyAccount Loaded : {ex}");
                throw ex;
            }
        }


        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }


 
    }
}
