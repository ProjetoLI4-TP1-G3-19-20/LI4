import React, { Component } from "react";
import NavBar from "./components/banner";
import LoginForm from "./components/loginForm";
import UserLatNavBar from "./components/userLatNavBar";
import UserRegForm from "./components/userRegForm";
import VisitHistory from "./components/VisitHistory";
import AdminRegForm from "./components/AdminRegForm";
import ColabRegForm from "./components/ColabRegForm";

class App extends Component {
  constructor(props) {
    super(props);
    this.state = { currentUser: "hey" };
    this.login = this.login.bind(this);
  }

  render() {
    switch (window.location.pathname.split("?")[0]) {
      case "/": {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <LoginForm login={this.login} />
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
              <UserLatNavBar name={this.state.currentUser} />
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
      case "/userHistory": {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <VisitHistory />
            </main>
          </React.Fragment>
        );
      }
      case "/regAdmin": {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <AdminRegForm />
            </main>
          </React.Fragment>
        );
      }
      case "/colabRegForm": {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <ColabRegForm />
            </main>
          </React.Fragment>
        );
      }

      default:
        console.log("Algo foi mal cara");
    }
  }

  login(user) {
    window.location.href = "/main?u=" + user;
  }
}

export default App;
