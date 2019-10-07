import React, { Component } from 'react'
import 'materialize-css/dist/css/materialize.min.css'
import { Switch, BrowserRouter as Router, Route } from 'react-router-dom'

import HomePage from './Pages/HomePage.jsx'
import MainApp from './Pages/MainApp.jsx'
import TestPages from './Pages/TestPages.jsx'

export default class App extends Component {
  static displayName = App.name

  render() {
    return (
      <main>
        <Router>
          <Switch>
            <Route exact path="/" component={HomePage}></Route>
            <Route exact path="/app" component={MainApp}></Route>
            <Route exact path="/test" component={TestPages}></Route>
          </Switch>
        </Router>
      </main>
    )
  }
}
