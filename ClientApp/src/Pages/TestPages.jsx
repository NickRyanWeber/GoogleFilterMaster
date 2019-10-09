import React, { useState, useEffect } from 'react'
import axios from 'axios'
import TestMasterFilter from '../components/TestMasterFilter'
import NewMasterFilter from '../components/NewMasterFilter'
import M from 'materialize-css'

const TestPages = () => {
  const [masterFilters, setMasterFilters] = useState(null)

  const fetchData = async () => {
    const resp = await axios.get('/api/filter')
    console.log(resp.data)
    setMasterFilters(resp.data)
  }

  useEffect(() => {
    fetchData()
    M.AutoInit()
  }, [])

  return masterFilters === null ? (
    <>
      <h1>Data is loading</h1>
    </>
  ) : (
    <>
      <nav className="nav-wrapper">
        <section className="container">
          <a href="/app" className="brand-logo">
            Filter Master
          </a>
          <ul id="nav-mobile" className="right hide-on-med-and-down">
            <li>
              <a href="/">Logout</a>
            </li>
          </ul>
        </section>
      </nav>
      <main className="">
        <section className="row">
          {masterFilters.masterFilters.map((masterFilterData, i) => {
            return <TestMasterFilter key={i} data={masterFilterData} />
          })}
        </section>
      </main>
      <div className="fixed-action-btn">
        <a
          href="#modal-new-master-filter"
          className="btn-floating btn-large waves-effect waves-circle waves-light red modal-trigger"
          onClick={() => {
            console.log('FAB click')
          }}
        >
          <i className="large material-icons">add</i>
        </a>
      </div>
      <div id="modal-new-master-filter" className="modal modal-fixed-footer">
        <NewMasterFilter />
      </div>
    </>
  )
}

export default TestPages
