import React, { Component } from "react";
import NavBar from "./components/banner";
import LoginForm from "./components/loginForm";
import userLatNavBar from "./components/userLatNavBar";

class Teste extends Component {
  render() {
    console.log(window.location.pathname);
    if (window.location.pathname != "/main") {
      return (
        <React.Fragment>
          <NavBar />
          <main className="container">
            <LoginForm />
          </main>
        </React.Fragment>
      );
    } else {
      return (
        <React.Fragment>
          <NavBar />
          <main class="container">
            <userLatNavBar />
          </main>
        </React.Fragment>
      );
    }
  }
}

export default Teste;
