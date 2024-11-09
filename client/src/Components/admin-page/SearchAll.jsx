import './AdminPage.css';
import PropTypes from 'prop-types'

const SearchAll = ({onCheckBox, onSearchAllCities}) => {
  return (
    <div className="search-all">
          <form action="" onSubmit={onSearchAllCities}>
            <button type='submit'>Search all</button>
            <input type="checkbox" name="with-cities" id="" onChange={onCheckBox} />
            <label htmlFor="with-cities">with cities</label>
          
          </form>
        </div>
  )
}

SearchAll.propTypes = {
  onCheckBox: PropTypes.func.isRequired,
  onSearchAllCities: PropTypes.func.isRequired,
}

export default SearchAll
