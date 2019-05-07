using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using SkiaSharp;
using Entry = Microcharts.Entry;
using Microcharts;
using Microcharts.Forms;

namespace XamarinImage
{
    public class PulsePage : ContentPage
    {
        //bool start = true;
        ImageSource img;
        List<Entry> entries = new List<Entry>
        {
            new Entry(200)
            {

                Color = SKColor.Parse("#FF1493"),
                Label = "January",
                ValueLabel = "200"
            },
            new Entry(400)
            {
                Color = SKColor.Parse("#00BFFF"),
                Label = "February",
                ValueLabel = "400"
            },
            new Entry(212)
            {
                Label = "UWP",
                ValueLabel = "212",
                Color = SKColor.Parse("#2c3e50")
            },
            new Entry(248)
            {
                Label = "Android",
                ValueLabel = "248",
                Color = SKColor.Parse("#77d065")
            },
            new Entry(128)
            {
                Label = "iOS",
                ValueLabel = "128",
                Color = SKColor.Parse("#b455b6")
            },
            new Entry(514)
            {
                Label = "Shared",
                ValueLabel = "514",
                Color = SKColor.Parse("#3498db")
            },
            new Entry(-100)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "March",
                ValueLabel = "-100"
            }

        };
        ChartView chartView;
        ListView listView;
        List<string> listPulse;

        public PulsePage()
        {
            var chart = new LineChart() { Entries = entries };
            listPulse = new List<string>();
            listView = new ListView() { ItemsSource = listPulse};

            img = ImageSource.FromUri(new Uri("https://pp.userapi.com/c849432/v849432519/122051/DgB4RT4VEDQ.jpg"));
            var webImage = new Image() { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand, Source = img };
            var buttonGetData = new Button() { Text = "Get Data", HorizontalOptions = LayoutOptions.FillAndExpand };
            var buttonCreateFile = new Button() { Text = "Create File", HorizontalOptions = LayoutOptions.FillAndExpand };
            buttonCreateFile.Clicked += ButtonCreateFile_ClickedAsync;
            chartView = new ChartView { Chart = chart, VerticalOptions = LayoutOptions.FillAndExpand };
            buttonGetData.Clicked += ButtonGetData_ClickedAsync;
            Content = new StackLayout
            {
                Children = {
                    webImage,
                    buttonGetData,
                    buttonCreateFile
                },

            };
        }

        private async void ButtonCreateFile_ClickedAsync(object sender, EventArgs e)
        {
            var path = await DependencyService.Get<IFileWorker>().GetPath("PulseExcel.xlsx");
            var list = await DependencyService.Get<IImageWorker>().PulseDivider(img);
            ExcelHelper.DataTableToExcel(ExcelHelper.MakeTable(list), path);
        }



        private async void ButtonGetData_ClickedAsync(object sender, EventArgs e)
        {
            var list = await DependencyService.Get<IImageWorker>().PulseDivider(img);
            
            entries.Clear();
            foreach (var point in list)
            {
                entries.Add(
                new Entry(point.Item1)
                {
                    Color = SKColor.Parse("#FF1493"),
                    ValueLabel = point.Item1.ToString()
                });
                string time = point.Item2.Item2 < 10 ? "0" + point.Item2.Item2 : point.Item2.Item2.ToString();
                listPulse.Add(String.Format("Пульс: {0} Время: {1}:{2}", point.Item1, point.Item2.Item1, time));

            }
            Content = new StackLayout
            {
                Children = {
                        new ChartView { Chart = new LineChart() { Entries = entries }, VerticalOptions = LayoutOptions.FillAndExpand, HeightRequest = 250  },
                        new ListView() { ItemsSource = listPulse, VerticalOptions = LayoutOptions.FillAndExpand }},
            };
            //start = false;
        }

        

        //protected override void OnSizeAllocated(double width, double height)
        //{
        //    base.OnSizeAllocated(width, height);

        //    if (!start)
        //        if (width > height)
        //            Content = new StackLayout
        //            {
        //                Children = {
        //            new ChartView { Chart = new LineChart() { Entries = entries }, VerticalOptions = LayoutOptions.FillAndExpand }},
        //            };
        //        else
        //            Content = new StackLayout
        //            {
        //                Children = {
        //                new ChartView { Chart = new LineChart() { Entries = entries }, VerticalOptions = LayoutOptions.FillAndExpand, HeightRequest = 250},
        //                new ListView() { ItemsSource = listPulse, VerticalOptions = LayoutOptions.FillAndExpand }},};
        //}
    }
}