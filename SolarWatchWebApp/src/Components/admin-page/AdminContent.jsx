import PropTypes from 'prop-types';
import './AdminPage.css';
import CityElement from './CityElement';

const AdminContent = ({ content, setCityContent }) => {
  return (
    <>
      <ul>
        {content.map((city) => {
          return <CityElement city={city} setCityContent={setCityContent} key={city.id}/>;
        })}
      </ul>
    </>
  );
};

AdminContent.propTypes = {
  content: PropTypes.array.isRequired,
  setCityContent: PropTypes.func.isRequired,
};

export default AdminContent;
