using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;

namespace WpfSMSApp.View
{
    /// <summary>
    /// LoginView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginView : MetroWindow
    {
        public LoginView()
        {
            InitializeComponent();
            Common.LOGGER.Info("LoginView 초기화!");
        }

        private async void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = await this.ShowMessageAsync("종료","프로그램을 종료하시겠습니까?",
                                    MessageDialogStyle.AffirmativeAndNegative,null);

            if (result == MessageDialogResult.Affirmative)
            {
                Common.LOGGER.Info("프로그램 종료");
                Application.Current.Shutdown();//프로그램 종료
            }
                

        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TxtEmail.Focus();
            LblResult.Visibility = Visibility.Hidden;
        }

        private void TxtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                TxtPassword.Focus();

        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            LblResult.Visibility = Visibility.Hidden;

            if (string.IsNullOrEmpty(TxtEmail.Text)||string.IsNullOrEmpty(TxtPassword.Password))
            {
                LblResult.Visibility = Visibility.Visible;
                LblResult.Content = "아이디나 패스워드를 입력하세요.";
                Common.LOGGER.Warn("아이디/패스워드 미입력 접속시도");
                return;
            }

            try
            {
                var email = TxtEmail.Text;
                var password = TxtPassword.Password;

                var mdHash = MD5.Create();
                //password = Common.GetMd5Hash(mdHash, password);

                var isOurUser = Logic.DataAccess.GetUsers()
                    .Where(u => u.UserEmail.Equals(email) &&
                                u.UserPassword.Equals(password) &&
                                u.UserActivated == true).Count();

                if (isOurUser == 0)
                {
                    LblResult.Visibility = Visibility.Visible;
                    LblResult.Content = "아이디나 패스워드가 일치하지 않습니다.";
                    Common.LOGGER.Warn("아이디/패스워드 불일치");
                    return;
                }
                else
                {
                    //접속 처리
                    Common.LOGINED_USER = Logic.DataAccess.GetUsers().Where(u => u.UserEmail.Equals(email)).FirstOrDefault();
                    Common.LOGGER.Info($"{email} 접속성공");
                    this.Visibility = Visibility.Hidden;

                }
            }
            catch(Exception ex)
            {
                //예외 처리
                Common.LOGGER.Error($"예외발생 : {ex}");
                await this.ShowMessageAsync("예외", $"예외발생 {ex}");
            }
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                BtnLogin_Click(sender, e);//로그인버튼 클릭
        }
    }
}
