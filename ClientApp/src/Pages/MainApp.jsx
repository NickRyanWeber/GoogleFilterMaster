import React, { useEffect } from 'react'

import M from 'materialize-css'
import MasterFilter from '../components/MasterFilter'
import NewMasterFilter from '../components/NewMasterFilter'

const MainApp = () => {
  useEffect(() => {
    M.AutoInit()
  }, [])

  const masterFiltersArray = [
    {
      id: 1,
      test: 'This is data',
      masterFilterTitle: 'Nick Weber Home',
      filterValue: '108.190.41.10',
      gaFilters: [
        {
          id: '1',
          gaAccountSelector: '1.1dummydata',
          gaPropertySelector: '1.1dummydata',
          gaViewSelector: '1.1dummydata',
          gaFilterSelector: '1.1dummydata'
        },
        {
          id: '2',
          gaAccountSelector: '1.2dummydata',
          gaPropertySelector: '1.2dummydata',
          gaViewSelector: '1.2dummydata',
          gaFilterSelector: '1.2dummydata'
        },
        {
          id: '3',
          gaAccountSelector: '1.3dummydata',
          gaPropertySelector: '1.3dummydata',
          gaViewSelector: '1.3dummydata',
          gaFilterSelector: '1.3dummydata'
        },
        {
          id: '4',
          gaAccountSelector: '1.4dummydata',
          gaPropertySelector: '1.4dummydata',
          gaViewSelector: '1.4dummydata',
          gaFilterSelector: '1.4dummydata'
        },
        {
          id: '5',
          gaAccountSelector: '1.5dummydata',
          gaPropertySelector: '1.5dummydata',
          gaViewSelector: '1.5dummydata',
          gaFilterSelector: '1.5dummydata'
        }
      ]
    },
    {
      id: 2,
      test: 'This is data',
      masterFilterTitle: 'SDG Office',
      filterValue: '108.190.41.10',
      gaFilters: [
        {
          id: '1',
          gaAccountSelector: '2.1dummydata',
          gaPropertySelector: '2.1dummydata',
          gaViewSelector: '2.1dummydata',
          gaFilterSelector: '2.1dummydata'
        },
        {
          id: '2',
          gaAccountSelector: '2.2dummydata',
          gaPropertySelector: '2.2dummydata',
          gaViewSelector: '2.2dummydata',
          gaFilterSelector: '2.2dummydata'
        },
        {
          id: '3',
          gaAccountSelector: '2.3dummydata',
          gaPropertySelector: '2.3dummydata',
          gaViewSelector: '2.3dummydata',
          gaFilterSelector: '2.3dummydata'
        },
        {
          id: '4',
          gaAccountSelector: '2.4dummydata',
          gaPropertySelector: '2.4dummydata',
          gaViewSelector: '2.4dummydata',
          gaFilterSelector: '2.4dummydata'
        },
        {
          id: '5',
          gaAccountSelector: '2.5dummydata',
          gaPropertySelector: '2.5dummydata',
          gaViewSelector: '2.5dummydata',
          gaFilterSelector: '2.5dummydata'
        },
        {
          id: '6',
          gaAccountSelector: '2.6dummydata',
          gaPropertySelector: '2.6dummydata',
          gaViewSelector: '2.6dummydata',
          gaFilterSelector: '2.6dummydata'
        }
      ]
    },
    {
      id: 3,
      test: 'This is data',
      masterFilterTitle: 'Marketing in Color Office',
      filterValue: '108.190.41.10',
      gaFilters: [
        {
          id: '1',
          gaAccountSelector: '3.1dummydata',
          gaPropertySelector: '3.1dummydata',
          gaViewSelector: '3.1dummydata',
          gaFilterSelector: '3.1dummydata'
        },
        {
          id: '2',
          gaAccountSelector: '3.2dummydata',
          gaPropertySelector: '3.2dummydata',
          gaViewSelector: '3.2dummydata',
          gaFilterSelector: '3.2dummydata'
        },
        {
          id: '3',
          gaAccountSelector: '3.3dummydata',
          gaPropertySelector: '3.3dummydata',
          gaViewSelector: '3.3dummydata',
          gaFilterSelector: '3.3dummydata'
        },
        {
          id: '4',
          gaAccountSelector: '3.4dummydata',
          gaPropertySelector: '3.4dummydata',
          gaViewSelector: '3.4dummydata',
          gaFilterSelector: '3.4dummydata'
        },
        {
          id: '5',
          gaAccountSelector: '3.5dummydata',
          gaPropertySelector: '3.5dummydata',
          gaViewSelector: '3.5dummydata',
          gaFilterSelector: '3.5dummydata'
        }
      ]
    },
    {
      id: 4,
      test: 'This is data',
      masterFilterTitle: 'Mark Dewey Home',
      filterValue: '108.190.41.10',
      gaFilters: [
        {
          id: '1',
          gaAccountSelector: '4.1dummydata',
          gaPropertySelector: '4.1dummydata',
          gaViewSelector: '4.1dummydata',
          gaFilterSelector: '4.1dummydata'
        }
      ]
    },
    {
      id: 5,
      test: 'This is data',
      masterFilterTitle: 'Iron Yard Office',
      filterValue: '108.190.41.10',
      gaFilters: [
        {
          id: '1',
          gaAccountSelector: '5.1dummydata',
          gaPropertySelector: '5.1dummydata',
          gaViewSelector: '5.1dummydata',
          gaFilterSelector: '5.1dummydata'
        },
        {
          id: '2',
          gaAccountSelector: '5.2dummydata',
          gaPropertySelector: '5.2dummydata',
          gaViewSelector: '5.2dummydata',
          gaFilterSelector: '5.2dummydata'
        }
      ]
    },
    {
      id: 6,
      test: 'This is data',
      masterFilterTitle: 'The Bikery',
      filterValue: '108.190.41.10',
      gaFilters: [
        {
          id: '1',
          gaAccountSelector: '6.1dummydata',
          gaPropertySelector: '6.1dummydata',
          gaViewSelector: '6.1dummydata',
          gaFilterSelector: '6.1dummydata'
        },
        {
          id: '2',
          gaAccountSelector: '6.2dummydata',
          gaPropertySelector: '6.2dummydata',
          gaViewSelector: '6.2dummydata',
          gaFilterSelector: '6.2dummydata'
        },
        {
          id: '3',
          gaAccountSelector: '6.3dummydata',
          gaPropertySelector: '6.3dummydata',
          gaViewSelector: '6.3dummydata',
          gaFilterSelector: '6.3dummydata'
        }
      ]
    }
  ]

  const addMasterFilter = () => {
    console.log('FAB Clicked')
  }

  return (
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
      {/* <main className="container"> */}
      <main className="">
        <section className="row">
          {masterFiltersArray.map((masterFilterData, i) => {
            return <MasterFilter key={i} data={masterFilterData} />
          })}
        </section>
      </main>
      <div className="fixed-action-btn">
        <a
          href="#modal-new-master-filter"
          className="btn-floating btn-large waves-effect waves-circle waves-light red modal-trigger"
          onClick={() => {
            console.log('FAB click')
            addMasterFilter()
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

export default MainApp
