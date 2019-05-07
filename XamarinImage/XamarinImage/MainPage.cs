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
        ImageSource img = ImageSource.FromUri(new Uri("http://xamarin.com/content/images/pages/forms/example-app.png"));
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

            var webImage = new Image() { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            Button button = new Button() { Text = "ConvertImageSource",VerticalOptions = LayoutOptions.FillAndExpand };
            button.Clicked += Button_ClickedAsync;
            Button buttonPulse = new Button() { Text = "Pulse From Image", VerticalOptions = LayoutOptions.FillAndExpand };
            buttonPulse.Clicked += ButtonPulse_ClickedAsync;
            Button buttonPressure = new Button() { Text = "Pressure From Image", VerticalOptions = LayoutOptions.FillAndExpand };
            buttonPressure.Clicked += ButtonPressure_ClickedAsync;
            webImage.Source = img;

            Content = new StackLayout
            {
                Children = {
                    new StackLayout()
                    {
                        Spacing = 0,
                        Orientation = StackOrientation.Horizontal,
                    },
                    buttonPulse,
                    buttonPressure,
                    webImage,
                    
                },
                Padding = new Thickness(0, 20, 0, 0),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };



        }

        private async void ButtonPressure_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PressurePage());
        }

        private async void ButtonPulse_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PulsePage());
        }

        private async void Button_ClickedAsync(object sender, EventArgs e)
        {
            var list = await DependencyService.Get<IImageWorker>().PulseDivider(img);
        }
    }
}