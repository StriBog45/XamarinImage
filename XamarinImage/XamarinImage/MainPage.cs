using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Image = Xamarin.Forms.Image;

namespace XamarinImage
{
	public class MainPage : ContentPage
	{
		public MainPage ()
		{
            //         ImageSource testImage = Device.RuntimePlatform == Device.Android ? ImageSource.FromFile("test1.jpg") : ImageSource.FromFile("Images/test1.jpg");
            //         //Bitmap bitmap = new Bitmap(testImage);
            //         var image = new Xamarin.Forms.Image() {
            //             VerticalOptions = LayoutOptions.EndAndExpand,

            //             //HorizontalOptions = LayoutOptions.EndAndExpand,
            //             Aspect = Aspect.AspectFit,
            //             MinimumHeightRequest = 600
            //             //HeightRequest = 600,
            //         };
            //         image.Source = testImage;
            //         Content = new StackLayout {
            //             Children = {
            //                 new Label { Text = "Welcome to Xamarin.Forms!" },
            //                 image
            //	}
            //};

            var webImage = new Image { Aspect = Aspect.AspectFit };

            //ImageSource img = ImageSource.FromUri(new Uri("http://xamarin.com/content/images/pages/forms/example-app.png"));
            ImageSource img = ImageSource.FromUri(new Uri("https://pp.userapi.com/c849432/v849432519/122051/DgB4RT4VEDQ.jpg"));
            webImage.Source = img;

            DependencyService.Get<IImageWorker>().ImageCheck(img);

            Content = new StackLayout
            {
                Children = {
                    new Label {
                        Text = "ImageSource.FromUri",
                        FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
                        FontAttributes = FontAttributes.Bold
                    },
                    webImage,
                    new Label { Text = "example-app.png gets downloaded from xamarin.com" }
                },
                Padding = new Thickness(0, 20, 0, 0),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };



        }
        //public static ImageSource ConvertBitmap(Bitmap source)
        //{
        //    return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
        //             source.GetHbitmap(),
        //            IntPtr.Zero,
        //            Int32Rect.Empty,
        //            BitmapSizeOptions.FromEmptyOptions());
        //}

        //public static Bitmap BitmapFromSource(BitmapSource bitmapsource)
        //{
        //    Bitmap bitmap;
        //    using (var outStream = new MemoryStream())
        //    {
        //        BitmapEncoder enc = new BmpBitmapEncoder();
        //        enc.Frames.Add(BitmapFrame.Create(bitmapsource));
        //        enc.Save(outStream);
        //        bitmap = new Bitmap(outStream);
        //    }
        //    return bitmap;
        //}
    }
}