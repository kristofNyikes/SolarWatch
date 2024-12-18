import PropTypes from 'prop-types'
const SearchSpecific = ({search, setSearch}) => {
  return (
    <form action="">
          <label htmlFor="">Search</label>
          <input type="text" name="" id="" value={search} onChange={(e) => setSearch(e.target.value)} />
          <button type="submit">Submit</button>
        </form>
  )
}

SearchSpecific.propTypes = {
  search: PropTypes.string.isRequired,
  setSearch: PropTypes.func.isRequired,
}

export default SearchSpecific
