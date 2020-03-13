import React, { Component } from "react";
import NavBar from "./components/banner";
import LoginForm from "./components/loginForm";
import UserLatNavBar from "./components/userLatNavBar";
import UserRegForm from "./components/userRegForm";

class Teste extends Component {
  render() {
    let num = 0;
    let dir = window.location.pathname;

    if (dir == "/main") num = 1;
    if (dir == "/regUser") num = 2;
    switch (num) {
      case 2: {
        console.log("Rendering User Registration Form");
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <UserRegForm />
            </main>
          </React.Fragment>
        );
      }
      case 1: {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <UserLatNavBar />
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
