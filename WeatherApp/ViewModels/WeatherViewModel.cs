using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private string _weatherCondition;
        private ImageSource _backgroundImage;

        public string WeatherCondition
        {
            get { return _weatherCondition; }
            set
            {
                if (_weatherCondition != value)
                {
                    _weatherCondition = value;
                    OnPropertyChanged();
                    UpdateBackgroundImage();
                }
            }
        }

        public ImageSource BackgroundImage
        {
            get { return _backgroundImage; }
            set
            {
                if (_backgroundImage != value)
                {
                    _backgroundImage = value;
                    OnPropertyChanged();
                }
            }
        }

        public WeatherViewModel()
        {
            // Set the initial weather condition and update the background image
            WeatherCondition = "sunny";
        }

        private void UpdateBackgroundImage()
        {
            switch (WeatherCondition)
            {
                case "rainy":
                    BackgroundImage = ImageSource.FromFile("rainy.png");
                    break;
                case "sunny":
                    BackgroundImage = ImageSource.FromFile("sunny.png");
                    break;
                case "foggy":
                    BackgroundImage = ImageSource.FromFile("foggy.png");
                    break;
                case "cloudy":
                    BackgroundImage = ImageSource.FromFile("cloudy.png");
                    break;
                case "storm":
                    BackgroundImage = ImageSource.FromFile("storm.png");
                    break;
                case "overcast":
                    BackgroundImage = ImageSource.FromFile("overcast.png");
                    break;
                default:
                    BackgroundImage = null;
                    break;
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}