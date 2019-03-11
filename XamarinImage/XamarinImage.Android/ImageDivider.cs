using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Color = Android.Graphics.Color;

namespace XamarinImage.Droid
{
    static class ImageDivider
    {
        static Rectangle pulseRect = new Rectangle(46, 264, 628, 214); //46,264,674,478 Зона пульс
        static Rectangle pressureRect = new Rectangle(102, 284, 572, 251); //102,283,674,534 Зона давление
        static public List<int> PulseDivider(Bitmap bmp)
        {
            var pulsePoints = FindPeaks(Bitmap.CreateBitmap(bmp,pulseRect.X, pulseRect.Y, pulseRect.Width, pulseRect.Height), Color.Rgb(225, 225, 225));
            return PulseDivider(pulsePoints);
        }
        //static public List<Tuple<int, int>> PressureDivider(Bitmap bmp)
        //{
        //    var pressurePoints = FindPressurePeaks(bmp.Clone(pressureRect, bmp.PixelFormat), Color.FromArgb(225, 225, 225));

        //    List<Tuple<int, int>> pressureDate = new List<Tuple<int, int>>();
        //    foreach (var point in pressurePoints)
        //    {
        //        pressureDate.Add(Tuple.Create(PressureDivider(point.Item1.Item2), PressureDivider(point.Item2.Item2)));
        //    }
        //    return pressureDate;
        //}
        static List<Tuple<int, int>> FindPeaks(Bitmap bmp, Color color)
        {
            List<Tuple<int, int>> pulsePoints = new List<Tuple<int, int>>();
            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height && x < bmp.Width; y++)
                {
                    if (Color.GetGreenComponent(bmp.GetPixel(x, y)) >= color.G && Color.GetRedComponent( bmp.GetPixel(x, y)) >= color.R && Color.GetBlueComponent(bmp.GetPixel(x, y)) >= color.B)
                    {
                        while (x + 1 < bmp.Width && Color.GetBlueComponent(bmp.GetPixel(x + 1, y)) >= color.G && Color.GetRedComponent(bmp.GetPixel(x + 1, y)) >= color.R && Color.GetBlueComponent(bmp.GetPixel(x + 1, y)) >= color.B)
                        {
                            x++;
                            while (y > 0 && Color.GetBlueComponent(bmp.GetPixel(x, y - 1)) >= color.G && Color.GetRedComponent(bmp.GetPixel(x, y - 1)) >= color.R && Color.GetBlueComponent(bmp.GetPixel(x, y - 1)) >= color.B)
                            {
                                y--;
                            }
                        }
                        if (pulsePoints.Count == 0)
                            pulsePoints.Add(Tuple.Create(x, bmp.Height - y));
                        else
                            if (pulsePoints[pulsePoints.Count - 1].Item1 < x - 7) // избавляемся от повторных точек в одной окружности
                        {
                            pulsePoints.Add(Tuple.Create(x, bmp.Height - y));
                        }
                        x += 5;
                        break;
                    }
                }
            return pulsePoints;
        }
        static List<int> PulseDivider(List<Tuple<int, int>> points)
        {
            List<int> pulseDate = new List<int>();
            foreach (var point in points)
            {
                double result = 0;
                if (point.Item2 >= 100)
                    result = point.Item2 / 1.11;
                else if (point.Item2 >= 95)
                    result = point.Item2 / 1.12;
                else if (point.Item2 >= 85)
                    result = point.Item2 / 1.13;
                else if (point.Item2 >= 78)
                    result = point.Item2 / 1.14;
                else
                    result = point.Item2 / 1.17;
                pulseDate.Add((int)result);
            }

            return pulseDate;
        }
        static int PressureDivider(int height)
        {
            double result = 0;
            if (height >= 145)
                result = height / 1.02;
            else if (height >= 135)
                result = height / 1.01;
            else if (height >= 125)
                result = height;
            else if (height >= 115)
                result = height / 0.98;
            else if (height >= 105)
                result = height / 0.97;
            else if (height >= 95)
                result = height / 0.96;
            else if (height >= 85)
                result = height / 0.94;
            else if (height >= 75)
                result = height / 0.9;
            else if (height >= 65)
                result = height / 0.86;
            else if (height >= 55)
                result = height / 0.83;
            else if (height >= 45)
                result = height / 0.80;
            else if (height >= 35)
                result = height / 0.75;

            return (int)result;
        }
        //static List<Tuple<Tuple<int, int>, Tuple<int, int>>> FindPressurePeaks(Bitmap bmp, Color color)
        //{
        //    List<Tuple<Tuple<int, int>, Tuple<int, int>>> pressurePoints = new List<Tuple<Tuple<int, int>, Tuple<int, int>>>();
        //    for (int x = 0; x < bmp.Width; x++)
        //        for (int y = 0; y < bmp.Height && x < bmp.Width; y++)
        //        {
        //            if (Color.GetGreenComponent(bmp.GetPixel(x, y)) >= color.G && bmp.GetPixel(x, y).R >= color.R && bmp.GetPixel(x, y).B >= color.B)
        //            {
        //                while (x + 1 < bmp.Width && (bmp.GetPixel(x + 1, y).G >= color.G && bmp.GetPixel(x + 1, y).R >= color.R && bmp.GetPixel(x + 1, y).B >= color.B))
        //                {
        //                    x++;
        //                    while (y > 0 && (bmp.GetPixel(x, y - 1).G >= color.G && bmp.GetPixel(x, y - 1).R >= color.R && bmp.GetPixel(x, y - 1).B >= color.B))
        //                        y--;
        //                }
        //                var topPoint = Tuple.Create(x, bmp.Height - y);
        //                if (pressurePoints.Count > 0 && pressurePoints[pressurePoints.Count - 1].Item1.Item1 > x - 7) // избавляемся от повторных точек в одной окружности
        //                    break;

        //                while (y < bmp.Height && (bmp.GetPixel(x, y + 1).G >= color.G && bmp.GetPixel(x, y + 1).R >= color.R && bmp.GetPixel(x, y + 1).B >= color.B))
        //                    y++;
        //                var downPoint = Tuple.Create(x, bmp.Height - y);
        //                pressurePoints.Add(Tuple.Create(topPoint, downPoint));
        //                x += 5;
        //                break;
        //            }
        //        }
        //    return pressurePoints;
        //}
    }
}