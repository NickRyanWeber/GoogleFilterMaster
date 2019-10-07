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
        <h1>Home Page</h1>
        <a href="/user/login">Get Started</a>
      </main>
    </>
  )
}

export default HomePage
