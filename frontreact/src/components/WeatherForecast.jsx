import React, { useState, useEffect } from 'react';

function WeatherForecast() {
  const [forecasts, setForecasts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchWeatherData = async () => {
      try {
        const response = await fetch('http://localhost:5103/weatherforecast');
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.json();
        setForecasts(data);
        setLoading(false);
      } catch (e) {
        setError(e);
        setLoading(false);
      }
    };

    fetchWeatherData();
  }, []);

  if (loading) {
    return <div>Cargando pronóstico del tiempo...</div>;
  }

  if (error) {
    return <div>Error al cargar el pronóstico del tiempo: {error.message}</div>;
  }

  return (
    <div>
      <h2>Pronóstico del Tiempo</h2>
      <table>
        <thead>
          <tr>
            <th>Fecha</th>
            <th>Temperatura (°C)</th>
            <th>Temperatura (°F)</th>
            <th>Resumen</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map((forecast, index) => (
            <tr key={index}>
              <td>{forecast.date}</td>
              <td>{forecast.temperatureC}</td>
              <td>{forecast.temperatureF}</td>
              <td>{forecast.summary}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default WeatherForecast;