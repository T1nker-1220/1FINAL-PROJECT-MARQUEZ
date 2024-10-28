import json
import os
from flask import Flask, render_template, request
import requests
from dotenv import load_dotenv

# Load environment variables from .env file
load_dotenv()

# Initialize the Flask application
app = Flask(__name__, template_folder='templates')

# Get the OpenWeatherMap API key from environment variables
api_key = os.getenv('API_KEY')
print(f"API Key: {api_key}")  # Debugging statement

# Check if the API key is loaded correctly
if not api_key:
    raise ValueError("No API key found. Please set the API_KEY environment variable in the .env file.")

# Define the home route (the main page)
@app.route('/', methods=['GET', 'POST'])
def home():
    weather_data = {}
    if request.method == 'POST':
        # Get city name from the form input
        city = request.form['city']
        print(f"City received: {city}")  # Debugging statement
        # Fetch weather data for the city
        weather_data = get_weather(city)

    # Render the HTML template with weather data
    return render_template('index.html', weather=weather_data)

# Function to fetch weather data from the OpenWeatherMap API
def get_weather(city):
    # Construct the API request URL using the city name and API key
    base_url = "http://api.openweathermap.org/data/2.5/weather"
    params = {
        'q': city,
        'appid': api_key,
        'units': 'metric'
    }
    complete_url = f"{base_url}?q={params['q']}&appid={params['appid']}&units={params['units']}"
    print(f"Request URL: {complete_url}")  # Debugging statement

    # Send a GET request to the API
    response = requests.get(complete_url)
    print(f"Response Status Code: {response.status_code}")  # Debugging statement

    # If the response status is OK (200), process the data
    if response.status_code == 200:
        data = response.json()
        print(f"Response Data: {json.dumps(data, indent=4)}")  # Debugging statement

        # Extract important weather details from the API response
        weather = {
            'city': city,
            'temp': data.get('main', {}).get('temp', 'No temperature data available'),
            'humidity': data.get('main', {}).get('humidity', 'No humidity data available'),
            'description': data.get('weather', [{}])[0].get('description', 'No weather description available'),
            'condition': data.get('weather', [{}])[0].get('main', 'No weather condition available')
        }

        return weather
    else:
        # Return an empty dictionary if the API request fails
        return {}

# Run the Flask app in debug mode
if __name__ == '__main__':
    app.run(debug=True)