import React, { useState, useEffect } from 'react'
import axios from 'axios'
import TestMasterFilter from '../components/TestMasterFilter'
import M from 'materialize-css'

const TestPages = () => {
  const [masterFilters, setMasterFilters] = useState(null)
  const [userId, setUserId] = useState(null)

  const fetchData = async () => {
    const resp = await axios.get('/api/filter')
    console.log(resp.data)
    setMasterFilters(resp.data)
    setUserId(resp.data.id)
  }

  useEffect(() => {
    fetchData()
    M.AutoInit()
  }, [])

  const addMasterFilter = () => {
    setMasterFilters(prev => {
      prev.masterFilters.push({
        name: '',
        filterValue: '',
        userId: userId
      })
      return prev
    })
  }

  return masterFilters === null ? (
    <>
      <h1>Loading...</h1>
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
            return (
              <TestMasterFilter
                key={i}
                data={masterFilterData}
                cache={masterFilters.accountsCache}
              />
            )
          })}
        </section>
      </main>
      <div className="fixed-action-btn">
        <a
          // href="#modal-new-master-filter"
          className="btn-floating btn-large waves-effect waves-circle waves-light red"
          onClick={() => {
            addMasterFilter()
          }}
        >
          <i className="large material-icons">add</i>
        </a>
      </div>
    </>
  )
}

export default TestPages
