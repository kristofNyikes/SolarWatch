import PropTypes from 'prop-types';
import './AdminPage.css';
import { useEffect, useState } from 'react';

const CityContent = ({ cityContent }) => {
  const [country, setCountry] = useState(cityContent.country);
  const [name, setName] = useState(cityContent.name);
  const [latitude, setLatitude] = useState(cityContent.latitude);
  const [longitude, setLongitude] = useState(cityContent.longitude);

  useEffect(() => {
    setCountry(cityContent.country);
    setName(cityContent.name);
    setLatitude(cityContent.latitude);
    setLongitude(cityContent.longitude);
  }, [cityContent]);

  const onReset = () => {
    setCountry(cityContent.country);
    setName(cityContent.name);
    setLatitude(cityContent.latitude);
    setLongitude(cityContent.longitude);
  }

  const onSubmit = (e) => {
    e.preventDefault();
  }

  const onDelete = async () => {
    const response = await fetch(`/api/City?name=${cityContent.name}`, {
      method: 'DELETE'
    })

    if(response.ok){
      alert('city deleted')
    }
  } 

  return (
    <div className="city-content">
      <form action="" onSubmit={onSubmit}>
        <div>Id: {cityContent.id}</div>

        <div className="input-container">
          <label htmlFor="country">Country: </label>
          <input type="text" name="country" id="" value={country} onChange={(e) => setCountry(e.target.value)} />
        </div>

        <div className="input-container">
          <label htmlFor="name">Name: </label>
          <input type="text" name="name" id="" value={name} onChange={(e) => setName(e.target.value)} />
        </div>

        <div className="input-container">
          <label htmlFor="latitude">Latitude: </label>
          <input type="text" name="latitude" id="" value={latitude} onChange={(e) => setLatitude(e.target.value)} />
        </div>

        <div className="input-container">
          <label htmlFor="longitude">Longitude: </label>
          <input type="text" name="longitude" id="" value={longitude} onChange={(e) => setLongitude(e.target.value)} />
        </div>

        <div className="buttons">
          <button>Update</button>
          <button onClick={onDelete}>Delete</button>
          <button onClick={onReset}>Reset</button>
        </div>
      </form>
    </div>
  );
};

CityContent.propTypes = {
  cityContent: PropTypes.object,
};

export default CityContent;
