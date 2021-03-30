using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfSMSApp.View.Account
{
    /// <summary>
    /// MyAccountView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MyAccountView : Page
    {
        public MyAccountView()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = Common.LOGINED_USER;
                TxtUserID.Text = user.UserID.ToString();
                TxtUserIdentityNumber.Text = user.UserIdentityNumber;
                TxtUserSurName.Text = user.UserSurname;
                TxtUserName.Text = user.UserName;
                TxtUserEmail.Text = user.UserEmail;
                TxtUserAdmin.Text = user.UserAdmin.ToString();
                TxtUserActivated.Text = user.UserActivated.ToString();
                
            }
            catch (Exception ex)
            {
                Common.LOGGER.Error($"예외발생 MyAccount Loaded : {ex}");
                throw ex;
            }
        }

        private void BtnEditMyAccount_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditAccountView());//계정정보 수정 화면으로 변경
        }
    }
}
