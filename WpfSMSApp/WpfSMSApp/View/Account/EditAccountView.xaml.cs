using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MahApps.Metro.Controls.Dialogs;

namespace WpfSMSApp.View.Account
{
    /// <summary>
    /// MyAccountView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class EditAccountView : Page
    {
        public EditAccountView()
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
                LblUserIdentityNumber.Visibility = LblUserSurName.Visibility =
                LblUserName.Visibility = LblUserEmail.Visibility =
                LblUserPassword.Visibility = LblUserAdmin.Visibility =
                LblUserActivated.Visibility = Visibility.Hidden;

                //콤보박스 초기화
                List<string> comboValues = new List<string>
                {
                    "False", // 0 index
                    "True"   // 1 index
                };
                CboUserAdmin.ItemsSource = comboValues;
                CboUserActivated.ItemsSource = comboValues;

                var user = Common.LOGINED_USER;
                TxtUserID.Text = user.UserID.ToString();
                TxtUserIdentityNumber.Text = user.UserIdentityNumber;
                TxtUserSurName.Text = user.UserSurname;
                TxtUserName.Text = user.UserName;
                TxtUserEmail.Text = user.UserEmail;
                //TxtUserPassword.Password = user.UserPassword;
                CboUserAdmin.SelectedIndex = user.UserAdmin == false ? 0 : 1;
                CboUserActivated.SelectedIndex = user.UserActivated == false ? 0 : 1;
                
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

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true; // 입력된 값이 모두 만족하는지 판별하는 플래그

            LblUserIdentityNumber.Visibility = LblUserSurName.Visibility =
            LblUserName.Visibility = LblUserEmail.Visibility =
            LblUserPassword.Visibility = LblUserAdmin.Visibility =
            LblUserActivated.Visibility = Visibility.Hidden;

            var user = Common.LOGINED_USER;
            if (string.IsNullOrEmpty(TxtUserSurName.Text))
            {
                LblUserSurName.Visibility = Visibility.Visible;
                LblUserSurName.Text = "이름(성)을 입력하세요.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(TxtUserName.Text))
            {
                LblUserName.Visibility = Visibility.Visible;
                LblUserName.Text = "이름을 입력하세요.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(TxtUserEmail.Text))
            {
                LblUserEmail.Visibility = Visibility.Visible;
                LblUserEmail.Text = "이메일을 입력하세요.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(TxtUserPassword.Password))
            {
                LblUserPassword.Visibility = Visibility.Visible;
                LblUserPassword.Text = "패스워드를 입력하세요.";
                isValid = false;
            }
            if (isValid)
            {
                //MessageBox.Show("DB수정처리!");
                user.UserSurname = TxtUserSurName.Text;
                user.UserName = TxtUserName.Text;
                user.UserEmail = TxtUserEmail.Text;
                user.UserPassword = TxtUserPassword.Password;
                user.UserAdmin = bool.Parse((string)CboUserAdmin.SelectedValue);
                user.UserActivated = bool.Parse((string)CboUserActivated.SelectedValue);

                try
                {
                    var md5Hash = MD5.Create();
                    user.UserPassword = Common.GetMd5Hash(md5Hash, user.UserPassword);

                    var result = Logic.DataAccess.SetUser(user);
                    if (result == 0)
                    {
                        //수정 안됨
                        LblResult.Text = "수정에 오류가 발생.";
                        LblResult.Foreground = Brushes.OrangeRed;
                    }
                    else
                    {
                        //정상적 수정됨
                        LblResult.Text = "정상적으로 수정했습니다.";
                        LblResult.Foreground = Brushes.LightSkyBlue;
                    }
                }
                catch (Exception ex)
                {
                    Common.LOGGER.Error($"예외발생 : {ex}");
                }
            }
        }
    }
}
