import React, { useState, useEffect } from 'react'

import M from 'materialize-css'
import TestMasterFilter from '../components/TestMasterFilter'
import axios from 'axios'
import { Redirect } from 'react-router-dom'
import Loading from '../components/Loading'

const App2 = () => {
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

  if (masterFilters === null) {
    return needsToLeave ? <Redirect to={'/user/login'} /> : <Loading />
  } else {
    return (
      <>
        <nav className="nav-wrapper light-blue">
          <section className="container">
            <a href="/" className="brand-logo truncate">
              Analytics Filter Master
            </a>
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
}

export default App2
