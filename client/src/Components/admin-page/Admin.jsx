import { useState } from 'react';
import Header from '../Header';
import SearchAll from './SearchAll';
import SearchSpecific from './SearchSpecific';
import AdminContent from './AdminContent';
import CityContent from './CityContent';
import './AdminPage.css';

const Admin = () => {
  const [search, setSearch] = useState('');
  const [showSunriseSunset, setShowSunriseSunset] = useState(false);
  const [content, setContent] = useState(null);
  const [cityContent, setCityContent] = useState(null);

  const onSearchAllCities = async (e) => {
    e.preventDefault();
    const response = await fetch(`/api/city?sunriseSunset=${showSunriseSunset}`);

    if (response.ok) {
      const data = await response.json();
      setContent(data);
    }
  };

  const onCheckBox = () => {
    setShowSunriseSunset((prev) => !prev);
  };

  return (
    <>
      <Header />
      <div className="main-admin">
        <div className="search">
          <SearchSpecific search={search} setSearch={setSearch} />
          <SearchAll onCheckBox={onCheckBox} onSearchAllCities={onSearchAllCities} />
          {content && <AdminContent content={content} setCityContent={setCityContent} />}
        </div>
        {cityContent && <CityContent cityContent={cityContent} />}
      </div>
    </>
  );
};

export default Admin;
