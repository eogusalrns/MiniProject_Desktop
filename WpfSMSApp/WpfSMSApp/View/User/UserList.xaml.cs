using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                RdoAll.IsChecked = true;
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
            try
            {
                NavigationService.Navigate(new AddUser());
            }
            catch (Exception ex)
            {
                Common.LOGGER.Error($"예외발생 BtnAddUser_Click : {ex}");
                throw ex;
            }
        }

        private void BtnEditUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new EditUser());
            }
            catch (Exception ex)
            {
                Common.LOGGER.Error($"예외발생 BtnEditUser_Click : {ex}");
                throw ex;
            }
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
            try
            {
                NavigationService.Navigate(new DeActiveUser());
            }
            catch (Exception ex)
            {
                Common.LOGGER.Error($"예외발생 BtnEditUser_Click : {ex}");
                throw ex;
            }
        }

        private void BtnExportPdf_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PDF File(*.pdf)|*.pdf";
            saveDialog.FileName = "";
            if (saveDialog.ShowDialog() == true)
            {
                //PDF변환
                try
                {
                    iTextSharp.text.Font font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA,12);
                    string pdfFilePath = saveDialog.FileName;

                    iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4);
                    //1 PDF객체생성
                    PdfPTable pdfTable = new PdfPTable(GrdData.Columns.Count);
                    //2 PDF내용 만들기
                    string nanumtf = Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), @"Fonts\NanumGothic.ttf");
                    BaseFont nanumBase = BaseFont.CreateFont(nanumtf, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    var nanumFont = new iTextSharp.text.Font(nanumBase, 16f);

                    Paragraph title = new Paragraph($"PKNU Stock Management System : {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");

                    //3 PDF 파일생성
                    using (FileStream stream = new FileStream(pdfFilePath, FileMode.OpenOrCreate))
                    {
                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        //2번에서 만든 내용 추가
                        pdfDoc.Close();
                        stream.Close();
                    }
                }
                catch (Exception ex)
                {
                    Common.LOGGER.Error($"예외발생 BtnExportPdf : {ex}");
                }
            }
        }

        private void BtnAddUser_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void RdoAll_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                List<WpfSMSApp.Model.User> users = new List<Model.User>();

                if ( RdoAll.IsChecked == true)
                {
                    users = Logic.DataAccess.GetUsers();
                }
                this.DataContext = users;
            }
            catch (Exception ex)
            {
                Common.LOGGER.Error($"예외발생 : {ex}");
            }
        }

        private void RdoActive_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                List<WpfSMSApp.Model.User> users = new List<Model.User>();

                if (RdoActive.IsChecked == true)
                {
                    users = Logic.DataAccess.GetUsers().Where(u=>u.UserActivated == true).ToList();
                }
                this.DataContext = users;
            }
            catch (Exception ex)
            {
                Common.LOGGER.Error($"예외발생 : {ex}");
            }
        }

        private void RdoUnActive_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                List<WpfSMSApp.Model.User> users = new List<Model.User>();

                if (RdoUnActive.IsChecked == true)
                {
                    users = Logic.DataAccess.GetUsers().Where(u => u.UserActivated == false).ToList();
                }
                this.DataContext = users;
            }
            catch (Exception ex)
            {
                Common.LOGGER.Error($"예외발생 : {ex}");
            }
        }

        private void GrdData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
