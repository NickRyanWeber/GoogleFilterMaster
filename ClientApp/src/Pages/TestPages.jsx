import React, { useState, useEffect } from 'react'
import axios from 'axios'

const TestPages = () => {
  const fetchData = async () => {
    const resp = await axios.get('/api/test')
    console.log(resp)
  }

  useEffect(() => {
    fetchData()
  }, [])

  return (
    <>
      <h1>Test Page</h1>
    </>
  )
}

export default TestPages
