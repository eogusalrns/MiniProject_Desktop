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
    public partial class AddUser : Page
    {
        public AddUser()
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

        public bool IsValidInput()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(TxtUserIdentityNumber.Text))
            {
                LblUserSurName.Visibility = Visibility.Visible;
                LblUserSurName.Text = "사번을 입력하세요.";
                isValid = false;
            }
            else
            {
                var cnt = Logic.DataAccess.GetUsers().Where(u => u.UserIdentityNumber.Equals(TxtUserIdentityNumber.Text)).Count();
                if (cnt > 0)
                {
                    LblUserIdentityNumber.Visibility = Visibility.Visible;
                    LblUserIdentityNumber.Text = "중복된 사번이 존재합니다.";
                    isValid = false;
                }
            }
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
            else
            {
                var cnt = Logic.DataAccess.GetUsers().Where(u => u.UserEmail.Equals(TxtUserEmail.Text)).Count();
                if (cnt > 0)
                {
                    LblUserEmail.Visibility = Visibility.Visible;
                    LblUserEmail.Text = "중복된 이메일이 존재합니다.";
                    isValid = false;
                }
            }
            if (string.IsNullOrEmpty(TxtUserPassword.Password))
            {
                LblUserPassword.Visibility = Visibility.Visible;
                LblUserPassword.Text = "패스워드를 입력하세요.";
                isValid = false;
            }
            if (CboUserAdmin.SelectedIndex < 0)
            {
                LblUserAdmin.Visibility = Visibility.Visible;
                LblUserAdmin.Text = "관리자여부를 선택하세요";
                isValid = false;
            }
            if (CboUserActivated.SelectedIndex < 0)
            {
                LblUserActivated.Visibility = Visibility.Visible;
                LblUserActivated.Text = "활성여부를 선택하세요";
                isValid = false;
            }
            if (!Common.IsValidEmail(TxtUserEmail.Text))
            {
                LblUserEmail.Visibility = Visibility.Visible;
                LblUserEmail.Text = "이메일 형식이 올바르지 않습니다.";
                isValid = false;
            }
            return isValid;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true; // 입력된 값이 모두 만족하는지 판별하는 플래그

            LblUserIdentityNumber.Visibility = LblUserSurName.Visibility =
            LblUserName.Visibility = LblUserEmail.Visibility =
            LblUserPassword.Visibility = LblUserAdmin.Visibility =
            LblUserActivated.Visibility = Visibility.Hidden;

            var user = new Model.User();

            isValid = IsValidInput();

 
            if (isValid)
            {
                //MessageBox.Show("DB수정처리!");
                user.UserIdentityNumber = TxtUserIdentityNumber.Text;
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
                        LblResult.Text = "사용자 입력에 오류가 발생.";
                        LblResult.Foreground = Brushes.OrangeRed;
                    }
                    else
                    {
                        //정상적 수정됨
                        NavigationService.Navigate(new UserList());
                    }
                }
                catch (Exception ex)
                {
                    Common.LOGGER.Error($"예외발생 : {ex}");
                }
            }
        }

        private void TxtUserIdentityNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
