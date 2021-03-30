using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfSMSApp.View.User
{
    /// <summary>
    /// MyAccountView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserList : Page
    {
        public UserList()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = Common.LOGINED_USER;

                
            }
            catch (Exception ex)
            {
                Common.LOGGER.Error($"예외발생 MyAccount Loaded : {ex}");
                throw ex;
            }
        }

        private void BtnEditMyAccount_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new EditAccountView());//계정정보 수정 화면으로 변경
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    ActiveControl.Content = new MyAccount();
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }

        private void BtnEditUser_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    ActiveControl.Content = new UserList();
            //}
            //catch (Exception ex)
            //{
            //    Common.LOGGER.Error($"예외발생 BtnUser_Click : {ex}");
            //    await this.ShowMessageAsync("예외", $"예외발생 : {ex}");
            //}
        }

        private void BtnDeactivateUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnExportPdf_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddUser_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
