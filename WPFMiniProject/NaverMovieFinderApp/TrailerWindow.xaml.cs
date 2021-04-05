using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using MahApps.Metro.Controls;
using NaverMovieFinderApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NaverMovieFinderApp
{
    /// <summary>
    /// TrailerWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TrailerWindow : MetroWindow
    {
        List<YoutubeItem> youtubes; // youtube api 검색 결과담음
        public TrailerWindow()
        {
            InitializeComponent();
        }

        public TrailerWindow(string movieName) : this()
        {
            LblMovieName.Content = $"{movieName} 예고편";
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            youtubes = new List<YoutubeItem>();//초기화
            // 유튜브 API로 검색
            //MessageBox.Show("유튜브 검색!");
            ProcSearchYoutubeApi();
        }

        private async void ProcSearchYoutubeApi()
        {
            await LoadDataCollection();
            LsvYoutubeSearch.ItemsSource = youtubes;
        }

        private async Task LoadDataCollection()
        {
            var youtubeService = new YouTubeService(
                new BaseClientService.Initializer()
                {
                    ApiKey = "AIzaSyD3vOLfJ0Tiv0FBfQGg8RulTE9HSajf0hQ",
                    ApplicationName = this.GetType().ToString()
                });

            var request = youtubeService.Search.List("snippet");
            request.Q = LblMovieName.Content.ToString();//영화이름 예고편
            request.MaxResults = 20; // 사이즈가 크면 멈춤 

            var response = await request.ExecuteAsync();//검색시작 youtube openapi 호출

            foreach (var item in response.Items)
            {
                if (item.Id.Kind.Equals("youtube#video"))
                {
                    YoutubeItem youtube = new YoutubeItem()
                    {
                        Title = item.Snippet.Title,
                        Author = item.Snippet.ChannelTitle,
                        URL = $"https://www.youtube.com/watch?v={item.Id.VideoId}"
                    };
                    //썸네일 이미지
                    //Process.Start(youtube.URL);
                    //byte[] imgBytes = new WebClient().DownloadData(item.Snippet.Thumbnails.Default__.Url);
                    //using ( var ms = new MemoryStream(imgBytes))
                    //{
                    //    var source = new BitmapImage();
                    //    source.BeginInit();
                    //    source.StreamSource = ms;
                    //    source.EndInit();
                    //    //할당
                    //    youtube.Thumbnail = new Image() { Source = source };
                    //}
                    youtube.Thumbnail = new BitmapImage(new Uri(item.Snippet.Thumbnails.Default__.Url,UriKind.RelativeOrAbsolute));

                    youtubes.Add(youtube);
                    
                }
            }
        }

        private async void LsvYoutubeSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LsvYoutubeSearch.SelectedItems.Count == 0)
            {
                Commons.ShowMessageAsync("유튜브보기", "예고편을 볼 영화를 선택하세요");
                return;
            }
            if (LsvYoutubeSearch.SelectedItems.Count > 1)
            {
                Commons.ShowMessageAsync("유튜브보기", "예고편을 한 편만 선택하세요");
                return;
            }
            if(LsvYoutubeSearch.SelectedValue is YoutubeItem)
            {
                var video = LsvYoutubeSearch.SelectedItem as YoutubeItem;
                BrwYoutubeWatch.Source = new Uri(video.URL,UriKind.RelativeOrAbsolute);
                //Process.Start(video.URL);
            }
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BrwYoutubeWatch.Source = null;//해제
            BrwYoutubeWatch.Dispose();//리소스 즉시해제
        }
    }
}
