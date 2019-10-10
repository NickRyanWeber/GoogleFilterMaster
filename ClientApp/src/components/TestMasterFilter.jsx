import React, { useEffect, useState } from 'react'

import M from 'materialize-css'
import NewFilter from './NewFilter'

const TestMasterFilter = props => {
  const [name, setName] = useState(props.data.name)
  const [displayName, setDisplayName] = useState(props.data.name)
  const [value, setValue] = useState(props.data.filterValue)
  const [displayValue, setDisplayValue] = useState(props.data.filterValue)
  const [filters, setFilters] = useState(props.data.selectedFilter)
  const [displayFilters, setDisplayFilters] = useState(
    props.data.selectedFilter
  )
  const [cachedAccounts, setCachedAccounts] = useState(props.cache)
  const [cachedFilters, setCachedFilters] = useState([])
  const [newSelectedAccount, setNewSelectedAccount] = useState('')
  const [newSelectedFilter, setNewSelectedFilter] = useState('')

  const createCachedFilters = () => {
    let _cachedFilters = []
    props.cache.forEach(account => {
      account.filtersCache.map(filter => {
        _cachedFilters.push({
          accountId: account.googleAccountId,
          accountName: account.name,
          filterName: filter.name
        })
      })
    })
    setCachedFilters(_cachedFilters)
  }

  const removeSelectedFilter = index => {
    setDisplayFilters(prev => prev.filter((_, i) => i !== index))
  }

  const cancelChanges = () => {
    console.log('cancel')
    setDisplayName(name)
    setDisplayValue(value)
    setDisplayFilters(filters)
  }

  const saveChanges = () => {
    console.log('save')
    setName(displayName)
    setValue(displayValue)
    setFilters(displayFilters)
    console.log('api call')
  }

  useEffect(() => {
    M.AutoInit()
    createCachedFilters()
  }, [])

  // useEffect(() => {

  // }, [masterFilterSelectedFilters])

  return (
    <>
      <main className="col l3 m4 s6">
        <section className="card">
          <a href={`#modal${props.data.id}`} className="modal-trigger">
            <section className="card-content">
              <div className="section">
                <h6 className="truncate">{displayName}</h6>
                <div className="divider"></div>
              </div>
              <div className="section">
                <p>Value - {displayValue}</p>
                <p>{displayFilters.length} filters</p>
              </div>
            </section>
          </a>
        </section>
      </main>
      {/* View Master Filter Modal */}
      {/* turn into a component, need to figure out how to open component modal */}
      <div id={`modal${props.data.id}`} className="modal modal-fixed-footer">
        <div className="modal-content">
          <div className="row">
            <div className="col l7 filter-info">
              <div className="input-field">
                <input
                  id="filter_name"
                  type="text"
                  className="validate"
                  value={displayName}
                  onChange={e => setDisplayName(e.target.value)}
                />
                <label htmlFor="filter_name">Filter Name</label>
              </div>
              <div className="input-field">
                <input
                  id="filter_value"
                  type="text"
                  className="validate"
                  value={displayValue}
                  onChange={e => setDisplayValue(e.target.value)}
                />
                <label htmlFor="filter_value">Filter Value</label>
              </div>
            </div>
            <div className="col l4 filter-count right">
              <br />
              <h2 className="center">{displayFilters.length}</h2>
              <p className="center">Filter Count</p>
            </div>
          </div>
          <ul className="collection">
            {displayFilters.map((filter, i) => {
              return (
                <li key={i} className="collection-item">
                  <div>
                    {filter.googleAccountName} > {filter.googleFilterName}
                    <i
                      className="material-icons secondary-content"
                      onClick={() => removeSelectedFilter(i)}
                    >
                      remove_circle_outline
                    </i>
                  </div>
                </li>
              )
            })}
          </ul>
          {/* <div className="input-field"> */}
          <select
            className="browser-default"
            name="accounts"
            onChange={e => setNewSelectedAccount(e.target.value)}
          >
            <option value="" disabled selected>
              Choose an account
            </option>
            {cachedAccounts.map((account, i) => {
              return (
                <option value={account.googleAccountId}>{account.name}</option>
              )
            })}
          </select>
          <label for="accounts">Account</label>
          {/* </div> */}
          {/* <div className="input-field"> */}
          <select
            className="browser-default"
            name="filters"
            onChange={e => setNewSelectedFilter(e.target.value)}
          >
            <option value="" disabled selected>
              Choose a filter
            </option>
            {cachedFilters
              .filter(filter => filter.accountId === newSelectedAccount)
              .map((filter, i) => {
                return (
                  <option value={filter.filterName}>{filter.filterName}</option>
                )
              })}
          </select>
          <label for="filters">Filter</label>
          {/* </div> */}
          <p className="btn">Add</p>
        </div>
        <div className="modal-footer">
          <a
            href="#modal-new-filter"
            className="waves-effect waves-green btn-flat modal-trigger"
          >
            Add New Filter
          </a>
          <p
            className="modal-close waves-effect waves-green btn-flat"
            onClick={() => {
              cancelChanges()
            }}
          >
            Cancel
          </p>
          <p
            className="modal-close waves-effect waves-green btn-flat"
            onClick={() => {
              saveChanges()
            }}
          >
            Save
          </p>
        </div>
      </div>
      <div
        id="modal-new-filter"
        className="modal modal-fixed-footer new-filter-modal"
      >
        <NewFilter />
      </div>
    </>
  )
}

export default TestMasterFilter
