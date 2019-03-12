using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinImage.UWP;

[assembly: Xamarin.Forms.Dependency(typeof(ImageWorker_UWP))]
namespace XamarinImage.UWP
{
    public class ImageWorker_UWP : IImageWorker
    {
        public Task<List<int>> ImageCheck(ImageSource image)
        {
            throw new NotImplementedException();
        }
    }
}
