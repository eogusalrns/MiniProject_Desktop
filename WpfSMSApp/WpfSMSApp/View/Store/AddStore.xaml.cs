using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MahApps.Metro.Controls.Dialogs;

namespace WpfSMSApp.View.Store
{
    /// <summary>
    /// MyAccountView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AddStore : Page
    {
        public AddStore()
        {
            InitializeComponent();
        }

        private void ChangeLabelVisible(Visibility visible)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LblStoreLocation.Visibility = LblStoreName.Visibility = Visibility.Hidden;
            TxtStordID.Text = TxtStoreName.Text = TxtStoreLocation.Text = "";

            //콤보박스 초기화
            //List<string> comboValues = new List<string>
            //{
            //    "False", // 0 index
            //    "True"   // 1 index
            //};
        }


        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        public bool IsValid = true;

        public bool IsValidInput()
        {
            
            if (string.IsNullOrEmpty(TxtStoreName.Text))
            {
                LblStoreName.Visibility = Visibility.Visible;
                LblStoreName.Text = "창고명을 입력하세요.";
                IsValid = false;
            }
            else
            {
                var cnt = Logic.DataAccess.GetStores().Where(u => u.StoreName.Equals(TxtStoreName.Text)).Count();
                if (cnt > 0)
                {
                    LblStoreName.Visibility = Visibility.Visible;
                    LblStoreName.Text = "중복된 창고명이 존재합니다.";
                    IsValid = false;
                }
            }
            if (string.IsNullOrEmpty(TxtStoreLocation.Text))
            {
                LblStoreLocation.Visibility = Visibility.Visible;
                LblStoreLocation.Text = "창고위치를 입력하세요.";
                IsValid = false;
            }
            return IsValid;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true; // 입력된 값이 모두 만족하는지 판별하는 플래그

            LblStoreLocation.Visibility = LblStoreName.Visibility = Visibility.Hidden;

            var store = new Model.Store();

            isValid = IsValidInput(); // 유효성체크

 
            if (isValid)
            {
                //MessageBox.Show("DB수정처리!");
                store.StoreName = TxtStoreName.Text;
                store.StoreLocation = TxtStoreLocation.Text;
                
                try
                {
                    var result = Logic.DataAccess.SetStore(store); // Logic.DataAccess.SetUser(user);

                    if (result == 0)
                    {
                        //수정 안됨
                        Common.LOGGER.Error("AddStroe.xaml.cs 창고정보 저장오류 발생");
                        Common.ShowMessageAsync("오류", "저장시 오류가 발생했습니다");
                    }
                    else
                    {
                        //정상적 수정됨
                        NavigationService.Navigate(new StoreList());
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

        private void TxtStoreName_LostFocus(object sender, RoutedEventArgs e)
        {
            IsValidInput();
        }

        private void TxtStoreLocation_LostFocus(object sender, RoutedEventArgs e)
        {
            IsValidInput();
        }
    }
}
