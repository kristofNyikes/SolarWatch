import PropTypes from 'prop-types';
import './AdminPage.css';

const CityElement = ({city, setCityContent}) => {

  const handleClick = (city) => {
    setCityContent(null);
    setCityContent(city);
  }

  return (
    <li className='city-element' onClick={() => handleClick(city)}>
      <div className='city-id'>Id: {city.id}</div>
      <div className='city-country'>Country: {city.country}</div>
      <div className="city-name">Name: {city.name}</div>
      <div className="city-latitude">Latitude: {city.latitude}</div>
      <div className="city longitude">Longitude: {city.longitude}</div>
      {city.sunriseSunsets && <div className='city-sunriseSunsets'>Sunrise and Sunsets: </div>}
    </li>
  )
}

CityElement.propTypes = {
  city: PropTypes.object.isRequired,
  setCityContent: PropTypes.func.isRequired,
}

export default CityElement
