import React, { Component } from "react";
import NavBar from "./components/banner";
import LoginForm from "./components/loginForm";
import UserLatNavBar from "./components/userLatNavBar";
import UserRegForm from "./components/userRegForm";

class App extends Component {
  render() {
    switch (window.location.pathname) {
      case "/": {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <LoginForm updatePath={this.updatePath} />
            </main>
            ;
          </React.Fragment>
        );
      }
      case "/main": {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <UserLatNavBar />
            </main>
          </React.Fragment>
        );
      }
      case "/regUser": {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <UserRegForm />
            </main>
          </React.Fragment>
        );
      }

      default:
        console.log("Algo foi mal cara");
    }
  }

  updatePath(path) {
    window.location.href = path;
  }
}

export default App;
