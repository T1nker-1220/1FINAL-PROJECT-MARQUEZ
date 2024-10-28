using Xamarin.Forms;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        private WeatherViewModel weatherViewModel;

        public MainPage()
        {
            InitializeComponent();
            weatherViewModel = new WeatherViewModel();
            BindingContext = weatherViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await weatherViewModel.LoadWeatherData();
            UpdateBackgroundImage();
        }

        private void UpdateBackgroundImage()
        {
            string weatherCondition = weatherViewModel.Weather.Condition;

            switch (weatherCondition)
            {
                case "Rainy":
                    BackgroundImageSource = "rainy.png";
                    break;
                case "Sunny":
                    BackgroundImageSource = "sunny.png";
                    break;
                case "Foggy":
                    BackgroundImageSource = "foggy.png";
                    break;
                case "Cloudy":
                    BackgroundImageSource = "cloudy.png";
                    break;
                case "Storm":
                    BackgroundImageSource = "storm.png";
                    break;
                case "Overcast":
                    BackgroundImageSource = "overcast.png";
                    break;
                default:
                    BackgroundImageSource = "";
                    break;
            }
        }
    }
}