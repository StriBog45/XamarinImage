using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Color = Android.Graphics.Color;

namespace XamarinImage.Droid
{
    static class ImageDivider
    {
        static Rectangle pulseRect = new Rectangle(46, 264, 628, 214); //46,264,674,478 Зона пульс
        static Rectangle pressureRect = new Rectangle(102, 284, 572, 251); //102,283,674,534 Зона давление
        static public List<Tuple<int, Tuple<int, int>>> PulseDivider(Bitmap bmp)
        {
            var pulsePoints = FindPeaks(Bitmap.CreateBitmap(bmp,(int)pulseRect.X, (int)pulseRect.Y, (int)pulseRect.Width, (int)pulseRect.Height), Color.Rgb(225, 225, 225));
            return PulseDivider(pulsePoints);
        }
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
                        
                        var extraX = x;
                        int resultX = x;

                        while (extraX - 1 > 0 &&
                            (Color.GetGreenComponent( bmp.GetPixel(extraX - 1, y)) >= color.G && Color.GetRedComponent( bmp.GetPixel(extraX - 1, y)) >= color.R && Color.GetBlueComponent( bmp.GetPixel(extraX - 1, y)) >= color.B))
                            extraX--;
                        if (extraX + 2 < x)
                            resultX = (extraX + x) / 2;

                        if (pulsePoints.Count == 0)
                            pulsePoints.Add(Tuple.Create(resultX, bmp.Height - y));
                        else
                            if (pulsePoints[pulsePoints.Count - 1].Item1 < x - 7) // избавляемся от повторных точек в одной окружности
                        {
                            pulsePoints.Add(Tuple.Create(resultX, bmp.Height - y));
                        }
                        x += 5;
                        break;
                    }
                }
            return pulsePoints;
        }
        static List<Tuple<int, Tuple<int, int>>> PulseDivider(List<Tuple<int, int>> points)
        {
            List<Tuple<int, Tuple<int, int>>> pulseDate = new List<Tuple<int, Tuple<int, int>>>();
            foreach (var point in points)
            {
                //pulse
                double result = 0;
                if (point.Item2 >= 110)
                    result = point.Item2 / 1.08;
                else if (point.Item2 >= 105)
                    result = point.Item2 / 1.1;
                else if (point.Item2 >= 100)
                    result = point.Item2 / 1.11;
                else if (point.Item2 >= 95)
                    result = point.Item2 / 1.12;
                else if (point.Item2 >= 85)
                    result = point.Item2 / 1.13;
                else if (point.Item2 >= 78)
                    result = point.Item2 / 1.14;
                else
                    result = point.Item2 / 1.17;

                //time
                //int hour = (point.Item1 / 13)/2;
                //int minute = (((int)(point.Item1 / 13)) % 2) > 0 ? 30 : 0;

                //1440 минут / 628 пикселей 2,293 минуты в пикселе, сдвиг ~8 минут
                int hour = (int)(((point.Item1 * 2.293) - 8) / 60);
                int minute = (int)(((point.Item1 * 2.293) - 8) % 60);

                var time = Tuple.Create(hour, MinuteRoundOff(minute));

                pulseDate.Add(Tuple.Create((int)result, time));
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
            else if (height >= 90)
                result = height / 0.98;
            else if (height >= 85)
                result = height / 0.98;
            else if (height >= 80)
                result = height / 0.97;
            else if (height >= 75)
                result = height / 0.96;
            else if (height >= 70)
                result = height / 0.94;
            else if (height >= 60)
                result = height / 0.92;

            //old
            //else if (height >= 85)
            //    result = height / 0.94;
            //else if (height >= 75)
            //    result = height / 0.9;
            //else if (height >= 65)
            //    result = height / 0.86;
            //else if (height >= 55)
            //    result = height / 0.83;
            //else if (height >= 45)
            //    result = height / 0.80;
            //else if (height >= 35)
            //    result = height / 0.75;

            return (int)result;
        }
        static List<Tuple<Tuple<int, int>, Tuple<int, int>>> FindPressurePeaks(Bitmap bmp, Color color)
        {
            List<Tuple<Tuple<int, int>, Tuple<int, int>>> pressurePoints = new List<Tuple<Tuple<int, int>, Tuple<int, int>>>();
            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height && x < bmp.Width; y++)
                {
                    if (Color.GetGreenComponent(bmp.GetPixel(x, y)) >= color.G && Color.GetRedComponent(bmp.GetPixel(x, y)) >= color.R && Color.GetBlueComponent(bmp.GetPixel(x, y)) >= color.B)
                    {
                        while (x + 1 < bmp.Width && Color.GetGreenComponent(bmp.GetPixel(x + 1, y)) >= color.G && Color.GetRedComponent(bmp.GetPixel(x + 1, y)) >= color.R && Color.GetBlueComponent(bmp.GetPixel(x + 1, y)) >= color.B)
                        {
                            x++;
                            while (y > 0 && ((Color.GetGreenComponent(bmp.GetPixel(x, y - 1))) >= color.G && Color.GetRedComponent( bmp.GetPixel(x, y - 1)) >= color.R && Color.GetBlueComponent( bmp.GetPixel(x, y - 1)) >= color.B))
                                y--;
                        }
                        // выравнивание точки на центр по оси x
                        var extraX = x;
                        int resultX = x;

                        while (extraX - 1 > 0 &&
                            (Color.GetGreenComponent(bmp.GetPixel(extraX - 1, y)) >= color.G && Color.GetRedComponent(bmp.GetPixel(extraX - 1, y)) >= color.R &&  Color.GetBlueComponent(bmp.GetPixel(extraX - 1, y)) >= color.B))
                            extraX--;
                        if (extraX + 2 < x)
                            resultX = (extraX + x) / 2;

                        var topPoint = Tuple.Create(resultX, bmp.Height - y);
                        if (pressurePoints.Count > 0 && pressurePoints[pressurePoints.Count - 1].Item1.Item1 > resultX - 7) // избавляемся от повторных точек в одной окружности
                            break;

                        while (y < bmp.Height && (Color.GetGreenComponent(bmp.GetPixel(resultX, y + 1)) >= color.G && Color.GetRedComponent(bmp.GetPixel(resultX, y + 1)) >= color.R && Color.GetBlueComponent(bmp.GetPixel(resultX, y + 1)) >= color.B))
                            y++;
                        y -= 8;
                        var downPoint = Tuple.Create(resultX, bmp.Height - y);
                        pressurePoints.Add(Tuple.Create(topPoint, downPoint));
                        x += 5;
                        break;
                    }
                }
            return pressurePoints;
        }
        static public List<Tuple<int, int, Tuple<int, int>>> PressureDivider(Bitmap bmp)
        {
            var pressurePoints = FindPressurePeaks(Bitmap.CreateBitmap(bmp,(int)pressureRect.X, (int)pressureRect.Y, (int)pressureRect.Width, (int)pressureRect.Height), Color.Rgb(225, 225, 225));

            List<Tuple<int, int, Tuple<int, int>>> pressureDate = new List<Tuple<int, int, Tuple<int, int>>>();
            foreach (var point in pressurePoints)
            {
                pressureDate.Add(Tuple.Create(PressureDivider(point.Item1.Item2), PressureDivider(point.Item2.Item2), PressureTimeDivier(point.Item1.Item1)));
            }
            return pressureDate;
        }
        private static Tuple<int, int> PressureTimeDivier(int x)
        {
            //1440 минут / 572 пикселей 2,517 минуты в пикселе, сдвиг ~ +15 минут
            int hour = (int)(((x * 2.217) + 15) / 60);
            int minute = (int)(((x * 2.217) + 15) % 60);

            return Tuple.Create(hour, minute);
        }
        static int MinuteRoundOff(int minute)
        {
            var result = minute;
            if (minute != 0 || minute != 30)
            {
                if (minute > 30)
                    result = 30;
                else
                    result = 0;
            }
            return result;
        }
    }
}