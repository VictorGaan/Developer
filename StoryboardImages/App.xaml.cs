using StoryboardImages.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using static StoryboardImages.Core.TreeViewImage;

namespace StoryboardImages
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var r1 = new Row();
            var r2 = new Row();
            var r3 = new Row();
            var c1 = new Column();
            var c2 = new Column();

            r1.Add(Img(@"C:\Users\gaste\source\repos\StoryboardImages\StoryboardImages\Images\1.jpg"));
            r1.Add(c1);
            r1.Add(Img(@"C:\Users\gaste\source\repos\StoryboardImages\StoryboardImages\Images\2.jpg"));

            //c1.Add(r2);
            c1.Add(Img(@"C:\Users\gaste\source\repos\StoryboardImages\StoryboardImages\Images\4.jpg"));
            c1.Add(Img(@"C:\Users\gaste\source\repos\StoryboardImages\StoryboardImages\Images\5.jpg"));

            //r2.Add(Img(@"C:\Users\gaste\source\repos\StoryboardImages\StoryboardImages\Images\4.jpg"));
            //r2.Add(Img(@"C:\Users\gaste\source\repos\StoryboardImages\StoryboardImages\Images\5.jpg"));

            //r2.Add(Img(@"C:\Users\gaste\source\repos\StoryboardImages\StoryboardImages\Images\4.jpg"));
            //r2.Add(c2);

            //c2.Add(r3);
            //c2.Add(Img(@"C:\Users\gaste\source\repos\StoryboardImages\StoryboardImages\Images\5.jpg"));

            //r3.Add(Img(@"C:\Users\gaste\source\repos\StoryboardImages\StoryboardImages\Images\6.jpg"));
            //r3.Add(Img(@"C:\Users\gaste\source\repos\StoryboardImages\StoryboardImages\Images\7.jpg"));
            //r1.Add(Img(@"C:\Users\gaste\source\repos\StoryboardImages\StoryboardImages\Images\1.jpg"));
            //r1.Add(Img(@"C:\Users\gaste\source\repos\StoryboardImages\StoryboardImages\Images\2.jpg"));
            //r1.Add(Img(@"C:\Users\gaste\source\repos\StoryboardImages\StoryboardImages\Images\3.jpg"));
            //r1.Add(Img(@"C:\Users\gaste\source\repos\StoryboardImages\StoryboardImages\Images\4.jpg"));
            //r1.Add(Img(@"C:\Users\gaste\source\repos\StoryboardImages\StoryboardImages\Images\5.jpg"));
            DrawStoryboard(r1, new DrawArgs { Width = 600 });
        }
    }
}
