import React, { Component } from "react";
import NavBar from "./components/banner";
import LoginForm from "./components/loginForm";
import userLatNavBar from "./components/userLatNavBar";
import userRegForm from "./components/userRegForm";

class Teste extends Component {
  render() {
    let num = 0;
    let dir = window.location.pathname;

    if (dir == "/main") num = 1;
    if (dir == "/regUser") num = 2;
    switch (num) {
      case 2: {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <userRegForm />
            </main>
          </React.Fragment>
        );
      }
      case 1: {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <userLatNavBar />
            </main>
          </React.Fragment>
        );
      }

      case 0: {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <LoginForm />
            </main>
          </React.Fragment>
        );
      }
    }
  }
}

export default Teste;
