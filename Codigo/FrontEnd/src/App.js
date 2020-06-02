import React, { Component } from "react";
import NavBar from "./components/banner";
import LoginForm from "./components/loginForm";
import UserLatNavBar from "./components/userLatNavBar";
import UserRegForm from "./components/userRegForm";
import VisitHistory from "./components/VisitHistory";
import AdminRegForm from "./components/AdminRegForm";
import ColabRegForm from "./components/ColabRegForm";
import VisitReqForm from "./components/visitReqForm";
import AcceptVisitForm from "./components/AcceptVisitForm";
import NewInstForm from "./components/newInstForm";
import CreateVaga from "./components/CreateVaga";
import AdminMain from "./components/AdminMain";
import NewDepForm from "./components/newDepForm";
import InternoMain from "./components/internoMain";
import UserDataUpdate from "./components/UserDataUpdate";
import IFvisits from "./components/IFvisits";

class App extends Component {
  constructor(props) {
    super(props);
    this.state = { currentUser: "hey" };
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
      case "/visitReq": {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <VisitReqForm />
            </main>
          </React.Fragment>
        );
      }
      case "/AcceptVisit": {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <AcceptVisitForm />
            </main>
          </React.Fragment>
        );
      }
      case "/newInst": {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <NewInstForm />
            </main>
          </React.Fragment>
        );
      }
      case "/newDep": {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <NewDepForm />
            </main>
          </React.Fragment>
        );
      }
      case "/createVaga": {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <CreateVaga />
            </main>
          </React.Fragment>
        );
      }
      case "/internoMain": {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <InternoMain />
            </main>
          </React.Fragment>
        );
      }
      case "/adminMain": {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <AdminMain />
            </main>
          </React.Fragment>
        );
      }
      case "/userDataUpdate": {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <UserDataUpdate />
            </main>
          </React.Fragment>
        );
      }
      case "/IFvisits": {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">
              <IFvisits />
            </main>
          </React.Fragment>
        );
      }
      default: {
        return (
          <React.Fragment>
            <NavBar />
            <main className="container">Oops! Este página não existe!</main>
          </React.Fragment>
        );
      }
    }
  }
}

export default App;
