using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace StoryboardImages.Core
{
    public class TreeViewImage
    {
        public static BitmapImage Img(string fileName)
        {
            return new BitmapImage(new Uri($"{fileName}"));
        }

        private static DrawingVisual DrawImages(object obj, DrawArgs args, ref double imageHeight)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext context = drawingVisual.RenderOpen();
            double imageWidth = 0;
            imageHeight = 0;

            if (obj is Row row)
            {
                imageWidth = args.Width / row.Nodes.Count;

                for (int i = 0; i < row.Nodes.Count; i++)
                {
                    if (row.Nodes[i] is BitmapImage frame)
                    {
                        if (imageHeight == 0)
                        {
                            imageHeight = frame.Height;
                        }
                        context.DrawImage(frame, new Rect(imageWidth * i, 0, imageWidth, imageHeight));
                    }
                    if (row.Nodes[i] is Column col)
                    {
                        DrawPositions(context, col, args, imageHeight, imageWidth, i);
                    }
                }
            }
            context.Close();
            return drawingVisual;
        }

        private static void DrawPositions(DrawingContext context, object obj, DrawArgs args, double imageHeight, double imageWidth, int index)
        {
            imageHeight = 0;
            if (obj is Column column)
            {
                var size = GetSizeImage(col: column);
                var count = column.Nodes.Count;
                for (int i = 0; i < count; i++)
                {
                    if (column.Nodes[i] is BitmapImage frame)
                    {
                        if (imageHeight == 0)
                        {
                            imageHeight = size.height / count * 2;
                        }
                        context.DrawImage(frame, new Rect(imageWidth, imageHeight * i, imageWidth, imageHeight));
                    }
                    if (column.Nodes[i] is Row row)
                    {
                        DrawPositions(context, row, args, imageHeight, imageWidth, i);
                    }
                }
            }
        }

        private static (double height, double width) GetSizeImage(Column col = null, Row row = null)
        {
            (double height, double width) size = (0, 0);
            if (col != null)
            {
                for (int i = 0; i < col.Nodes.Count; i++)
                {
                    if (col.Nodes[i] is BitmapImage frame)
                    {
                        size.height += frame.PixelHeight;
                        size.width += frame.PixelWidth;
                    }
                    if (col.Nodes[i] is Row colRow)
                    {
                        var rowSize = GetSizeImage(row: colRow);
                        size.height += rowSize.height;
                        size.height += rowSize.width;
                    }
                }
            }
            if (row != null)
            {
                for (int i = 0; i < row.Nodes.Count; i++)
                {
                    if (row.Nodes[i] is BitmapImage frame)
                    {
                        size.height += frame.PixelHeight;
                        size.width += frame.PixelWidth;
                    }
                    if (row.Nodes[i] is Column rowCol)
                    {
                        var colSize = GetSizeImage(col: rowCol);
                        size.height += colSize.height;
                        size.height += colSize.width;
                    }
                }
            }
            return size;
        }

        public static void DrawStoryboard(object obj, DrawArgs args)
        {
            Window window = new Window();

            double imageHeight = 0;
            var drawingVisual = DrawImages(obj, args, ref imageHeight);
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)args.Width, (int)imageHeight, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));

            using (Stream stream = File.Create(@"C:\Users\gaste\Desktop\Images\MyImage.png"))
                encoder.Save(stream);

            //BitmapFrame frame1 = BitmapDecoder.Create(new Uri(@"C:\Users\gaste\source\repos\StoryboardImages\StoryboardImages\Images\1.jpg"), BitmapCreateOptions.None, BitmapCacheOption.OnLoad).Frames.First();
            //BitmapFrame frame2 = BitmapDecoder.Create(new Uri(@"C:\Users\gaste\source\repos\StoryboardImages\StoryboardImages\Images\2.jpg"), BitmapCreateOptions.None, BitmapCacheOption.OnLoad).Frames.First();
            //BitmapFrame frame3 = BitmapDecoder.Create(new Uri(@"C:\Users\gaste\source\repos\StoryboardImages\StoryboardImages\Images\3.jpg"), BitmapCreateOptions.None, BitmapCacheOption.OnLoad).Frames.First();
            //BitmapFrame frame4 = BitmapDecoder.Create(new Uri(@"C:\Users\gaste\source\repos\StoryboardImages\StoryboardImages\Images\7.jpg"), BitmapCreateOptions.None, BitmapCacheOption.OnLoad).Frames.First();


            //double imageWidth = args.Width;
            //double imageHeight = frame1.PixelHeight;

            //// Draws the images into a DrawingVisual component
            //DrawingVisual drawingVisual = new DrawingVisual();
            //using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            //{
            //    drawingContext.DrawImage(frame1, new Rect(0, 0, imageWidth, imageHeight));
            //    //drawingContext.DrawImage(frame2, new Rect(imageWidth, 0, imageWidth, imageHeight));
            //    drawingContext.DrawImage(frame3, new Rect(imageWidth, 0, imageWidth, imageHeight));
            //    drawingContext.DrawImage(frame4, new Rect(imageWidth*2, 0, imageWidth, imageHeight));
            //}

            //// Converts the Visual (DrawingVisual) into a BitmapSource
            //RenderTargetBitmap bmp = new RenderTargetBitmap((int)imageWidth, (int)imageHeight * 2, 96, 96, PixelFormats.Pbgra32);
            //bmp.Render(drawingVisual);

            //// Creates a PngBitmapEncoder and adds the BitmapSource to the frames of the encoder
            //PngBitmapEncoder encoder = new PngBitmapEncoder();
            //encoder.Frames.Add(BitmapFrame.Create(bmp));

            //// Saves the image into a file using the encoder
            //using (Stream stream = File.Create(@"C:\Users\gaste\Desktop\Images\MyImage.png"))
            //    encoder.Save(stream);
            ////grid.Width = args.Width;

            window.Show();
        }

        public interface IDraw
        {
            public object Add(object obj);
        }

        public interface INode
        {
            public List<object> Nodes { get; set; }
        }

        public class Row : INode, IDraw
        {
            public Row()
            {
                Nodes = new List<object>();
            }
            public List<object> Nodes { get; set; }

            public object Add(object obj)
            {
                Nodes.Add(obj);
                return obj;
            }
        }

        public class Column : INode, IDraw
        {
            public Column()
            {
                Nodes = new List<object>();
            }
            public List<object> Nodes { get; set; }

            public object Add(object obj)
            {
                Nodes.Add(obj);
                return obj;
            }
        }
        public class DrawArgs
        {
            public double Width { get; set; }
            public double PaddingTop { get; set; }
            public double PaddingRight { get; set; }
            public double PaddingBottom { get; set; }
            public double PaddingLeft { get; set; }
        }
    }
}
