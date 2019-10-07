import React from 'react'

const NewMasterFilter = () => {
  return (
    <>
      <div className="modal-content">
        <div className="row">
          <div className="col l7 filter-info">
            <div className="input-field">
              <input
                id="filter_name"
                type="text"
                className="validate"
                placeholder="Filter Name"
              />
              <label htmlFor="filter_name">Filter Name</label>
            </div>
            <div className="input-field">
              <input
                id="filter_value"
                type="text"
                className="validate"
                placeholder="Filter Value"
              />
              <label htmlFor="filter_value">Filter Value</label>
            </div>
          </div>
        </div>
        <p>Add code here TEST</p>
      </div>
      <div className="modal-footer">
        <a href="#!" className="modal-close waves-effect waves-green btn-flat">
          Save
        </a>
      </div>
    </>
  )
}

export default NewMasterFilter
