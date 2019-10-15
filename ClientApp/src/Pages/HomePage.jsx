import React, { useEffect } from 'react'
import M from 'materialize-css'

const HomePage = () => {
  useEffect(() => {
    M.AutoInit()
    fetch('/api/SampleData/WeatherForecasts')
  }, [])

  return (
    <>
      <main className="container">
        <h1>Analytics Filter Master</h1>
        <p>
          The tool for organizing your Google Analytics Filters across Accounts,
          Properties, and Views.
        </p>
        <a href="/user/login">Get Started</a>
      </main>
    </>
  )
}

export default HomePage
