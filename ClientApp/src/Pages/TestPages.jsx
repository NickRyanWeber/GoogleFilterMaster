import React, { useState, useEffect } from 'react'

import M from 'materialize-css'
import TestMasterFilter from '../components/TestMasterFilter'
import axios from 'axios'
import { Redirect } from 'react-router-dom'

const TestPages = () => {
  const [masterFilters, setMasterFilters] = useState(null)
  const [userId, setUserId] = useState(null)
  const [needsToLeave, setNeedsToLeave] = useState(false)

  const fetchData = async () => {
    axios
      .get('/api/filter')
      .then(resp => {
        console.log(resp.data)
        setMasterFilters(resp.data)
        setUserId(resp.data.id)
      })
      .catch(ex => {
        console.log('thumped', ex)
        // setNeedsToLeave(true)
        window.location.replace('/user/login')
      })
  }

  useEffect(() => {
    M.AutoInit()
    fetchData()
  }, [])

  const addMasterFilter = () => {
    setMasterFilters(prev => {
      prev.masterFilters.push({
        name: '',
        filterValue: '',
        userId: userId,
        selectedFilter: []
      })
      console.log({ ...prev })
      return { ...prev }
    })
  }

  return masterFilters === null ? (
    <>{needsToLeave ? <Redirect to={'/user/login'} /> : <h1>Loading...</h1>}</>
  ) : (
    <>
      <nav className="nav-wrapper">
        <section className="container">
          <a href="/app" className="brand-logo">
            Analytics Filter Master
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
                test={i}
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
