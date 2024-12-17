import { useState } from 'react';
import Header from './Header';
import './SolarWatch.css';

const SolarWatch = () => {
  const BASE_URL = import.meta.env.VITE_API_BASE_URL;
  const [cityName, setCityName] = useState('Budapest');
  const todaysDate = new Date().toISOString().split('T')[0];
  const [date, setDate] = useState(todaysDate);
  const [sunriseSunset, setSunriseSunset] = useState(null);

  const requestOptions = {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
    },
    credentials: 'include',
  };

  const handleFetch = async (e) => {
    e.preventDefault();
    const response = await fetch(`${BASE_URL}/api/SunriseSunset/GetSunriseAndSunset?cityName=${cityName}&date=${date}`, requestOptions);

    if (response.ok) {
      const data = await response.json();
      setSunriseSunset(data);
    }
  };

  const formatTime = (date) => {
    const time = new Date(date);
    const hours = time.getHours() < 10 ? `0${time.getHours()}` : time.getHours();
    const minutes = time.getMinutes() < 10 ? `0${time.getMinutes()}` : time.getMinutes();
    return `${hours}:${minutes}`;
  };

  return (
    <div>
      <Header />
      <div className="solar-watch-main">
        <form action="" onSubmit={handleFetch}>
          <label htmlFor="">City</label>
          <input type="text" name="" id="" required value={cityName} onChange={(e) => setCityName(e.target.value)} />
          <label htmlFor="">Date</label>

          <input type="date" name="" id="" max={todaysDate} value={date} onChange={(e) => setDate(e.target.value)} />

          <button type="submit">Submit</button>
        </form>

        {sunriseSunset && (
          <div className='fetched-data'>
            <h2>Sunrise and Sunset Information:</h2>
            <p>City: {sunriseSunset.city.name}</p>
            <p>Sunrise: {formatTime(sunriseSunset.sunrise)}</p>
            <p>Sunset: {formatTime(sunriseSunset.sunset)}</p>
            <p>Date: {date}</p>
          </div>
        )}
      </div>
    </div>
  );
};

export default SolarWatch;
