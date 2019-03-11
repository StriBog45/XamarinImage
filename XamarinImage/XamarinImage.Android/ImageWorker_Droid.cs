using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinImage.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(ImageWorker_Droid))]
namespace XamarinImage.Droid
{
    public class ImageWorker_Droid : IImageWorker
    {
        public void ImageCheck(ImageSource image)
        {
            var test = GetBitmapFromImageSourceAsync(image, Android.App.Application.Context);
            var bitmap = test.Result;
            var result = ImageDivider.PulseDivider(bitmap);
            int i = 0;
            i++;
        }

        public static async Task<Bitmap> GetBitmapFromImageSourceAsync(ImageSource source, Context context)
        {
            var handler = ImageExtension_Droid.GetHandler(source);
            var returnValue = (Bitmap)null;
            returnValue = await handler.LoadImageAsync(source, context);
            return returnValue;
        }

        //public static async Task<Bitmap> GetBitmapFromImageSourceAsync(ImageSource source, Context context)
        //{
        //    var handler = GetHandler(source);
        //    var returnValue = (Bitmap)null;
        //    returnValue = await handler.LoadImageAsync(source, context);
        //    return returnValue;
        //}
    }
}