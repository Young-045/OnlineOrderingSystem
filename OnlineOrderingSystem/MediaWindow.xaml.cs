using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Vlc.DotNet.Core.Interops.Signatures;
using Vlc.DotNet.Wpf;

namespace OnlineOrderingSystem
{
    /// <summary>
    /// MediaWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MediaWindow : Window
    {

        public MediaWindow()
        {
            InitializeComponent();
            
            Init(string.Empty);
        }

        public void Init(string source)
        {
            var libDirectory = new DirectoryInfo(System.IO.Directory.GetCurrentDirectory() + "\\LibVlc");
            myControl.SourceProvider.CreatePlayer(libDirectory);//创建视频播放器

            //http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi
            //"https://fallra.in/radio/bbc/bbc_world_service";
            //https://fallra.in/radio/bbc/bbc_asian_network
            //myControl.MediaPlayer.Play(new Uri("rtsp://184.72.239.149/vod/mp4://BigBuckBunny_175k.mov"));
            //@"H:\DZBStudyRecord\2019-5-9VLCTest\VLCTest\FFMETest\wuyawang.mp4"
            string videourl = "https://fallra.in/radio/bbc/bbc_asian_network";
            myControl.SourceProvider.MediaPlayer.Play(new Uri(videourl));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
