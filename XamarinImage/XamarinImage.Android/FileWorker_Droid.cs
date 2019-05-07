using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;
using XamarinImage.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(FileWorker_Droid))]
namespace XamarinImage.Droid
{
    public class FileWorker_Droid : IFileWorker
    {

        public Task<string> GetPath(string filename)
        {
            const string permission = Manifest.Permission.WriteExternalStorage;
            if (ContextCompat.CheckSelfPermission(Application.Context, permission) == (int)Permission.Granted)
            {
                return Task<string>.FromResult(Path.Combine(GetDocsPath(), filename));
            }
            else
            {
                ActivityCompat.RequestPermissions(CrossCurrentActivity.Current.Activity, new String[] { Manifest.Permission.WriteExternalStorage }, 1);
                return Task<string>.FromResult(Path.Combine(GetDocsPath(), filename));
            }


        }
        // получаем путь к папке MyDocuments
        string GetDocsPath()
        {
            return Android.OS.Environment.ExternalStorageDirectory.Path;
        }


    }
}