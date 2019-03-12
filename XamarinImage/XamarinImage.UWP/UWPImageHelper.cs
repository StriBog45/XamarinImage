using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

namespace XamarinImage.UWP
{
    public class UWPImageHelper
    {
        private static IImageSourceHandler GetHandler(ImageSource source)
        {
            //IImageSourceHandler returnValue = null;
            //if (source is UriImageSource)
            //{
            //    returnValue = new ImageLoaderSourceHandler();
            //}
            //else if (source is FileImageSource)
            //{
            //    returnValue = new FileImageSourceHandler();
            //}
            //else if (source is StreamImageSource)
            //{
            //    returnValue = new StreamImagesourceHandler();
            //}
            return null;
        }

        private async static Task<Image> GetImageAsync(ImageSource source, int height, int width)
        {
            //var image = new Image();
            //var handler = GetHandler(source);
            //var imageSource = await handler.LoadImageAsync(source);
            //image.Source = imageSource;
            //image.Height = Convert.ToDouble(height);
            //image.Width = Convert.ToDouble(width);
            return null;
        }
    }
}
