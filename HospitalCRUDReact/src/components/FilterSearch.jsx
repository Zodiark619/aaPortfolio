const FilterSearch = ({ handleChange, handleSearchClick, name }) => {
  return (
    <>
      {/* 2. Filters + Search   */}
      <div className="card mb-4">
        <div className="card-body">
          <div className="row g-3">
            {/* Search   */}
            <div className="col-md-6">
              <div className="input-group">
                <input
                  type="text"
                  className="form-control"
                  placeholder="Search..."
                  onChange={handleChange}
                  value={name}
                />
                <button
                  className="btn btn-outline-secondary"
                  type="button"
                  onClick={handleSearchClick}
                >
                  Search
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default FilterSearch;
{
  /* Filter  */
}
{
  /* <div className="col-md-3">
              <select className="form-select">
                <option selected>All Status</option>
                <option value="1">Active</option>
                <option value="2">Inactive</option>
              </select>
            </div> */
}

{
  /* Another filter (optional)   */
}
{
  /* <div className="col-md-3">
              <select className="form-select">
                <option selected>All Categories</option>
                <option value="1">Electronics</option>
                <option value="2">Clothing</option>
              </select>
            </div> */
}
