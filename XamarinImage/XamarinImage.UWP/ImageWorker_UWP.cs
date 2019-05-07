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
        public Task<List<Tuple<int, int, Tuple<int, int>>>> PressureDivider(ImageSource image)
        {
            throw new NotImplementedException();
        }

        public Task<List<Tuple<int, Tuple<int, int>>>> PulseDivider(ImageSource image)
        {
            throw new NotImplementedException();
        }
    }
}
