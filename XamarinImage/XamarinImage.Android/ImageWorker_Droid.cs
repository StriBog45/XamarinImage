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
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinImage.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(ImageWorker_Droid))]
namespace XamarinImage.Droid
{
    public class ImageWorker_Droid : IImageWorker
    {
        Context context = Android.App.Application.Context;

        async Task<Bitmap> tempFuncAsync(ImageSource image)
        {
            return await AndroidImageHelper.GetBitmapFromImageSourceAsync(image, Android.App.Application.Context);
        }

        public static async Task<Bitmap> GetBitmapFromImageSourceAsync(ImageSource source, Context context)
        {
            var handler = ImageExtension_Droid.GetHandler(source);
            var returnValue = (Bitmap)null;
            returnValue = await handler.LoadImageAsync(source, context);
            return returnValue;
        }

        public async Task<List<Tuple<int, Tuple<int, int>>>> PulseDivider(ImageSource image)
        {
            Bitmap bmp = await tempFuncAsync(image);
            var result = ImageDivider.PulseDivider(bmp);

            return result;
        }

        public async Task<List<Tuple<int, int, Tuple<int, int>>>> PressureDivider(ImageSource image)
        {
            Bitmap bmp = await tempFuncAsync(image);
            var result = ImageDivider.PressureDivider(bmp);

            return result;
        }
    }
}