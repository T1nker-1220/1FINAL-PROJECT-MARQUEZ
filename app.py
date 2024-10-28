import json
import os
import logging
from flask import Flask, render_template, request
import requests
from dotenv import load_dotenv

# Load environment variables from .env file
load_dotenv()

# Initialize the Flask application
app = Flask(__name__, template_folder='templates')

# Get the OpenWeatherMap API key from environment variables
api_key = os.getenv('API_KEY')

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
    complete_url = f"{base_url}?{requests.compat.urlencode(params)}"

    try:
        # Send a GET request to the API
        response = requests.get(complete_url)
        response.raise_for_status()
        data = response.json()

        # Extract important weather details from the API response
        weather = {
            'city': city,
            'temp': data.get('main', {}).get('temp', 'No temperature data available'),
            'humidity': data.get('main', {}).get('humidity', 'No humidity data available'),
            'description': data.get('weather', [{}])[0].get('description', 'No weather description available'),
            'condition': data.get('weather', [{}])[0].get('main', 'No weather condition available')
        }

        return weather
    except requests.exceptions.RequestException as e:
        logging.error(f"Request failed: {e}")
        return {}

# Run the Flask app in debug mode
if __name__ == '__main__':
    logging.basicConfig(level=logging.DEBUG)
    app.run(debug=True)