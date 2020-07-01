import React, { Component } from "react";
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
import NavBar from "./components/NavBar";
import LoginNavBar from "./components/LoginNavBar";
import AdminNavBar from "./components/AdminNavBar";
import InternoNavBar from "./components/InternoNavBar";
import InternoDataUpdate from "./components/InternoDataUpdate";

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
            <LoginNavBar />
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
            <LoginNavBar />
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
            <AdminNavBar />
            <main className="container">
              <AdminRegForm />
            </main>
          </React.Fragment>
        );
      }
      case "/colabRegForm": {
        return (
          <React.Fragment>
            <AdminNavBar />
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
            <InternoNavBar />
            <main className="container">
              <AcceptVisitForm />
            </main>
          </React.Fragment>
        );
      }
      case "/newInst": {
        return (
          <React.Fragment>
            <AdminNavBar />
            <main className="container">
              <NewInstForm />
            </main>
          </React.Fragment>
        );
      }
      case "/newDep": {
        return (
          <React.Fragment>
            <AdminNavBar />
            <main className="container">
              <NewDepForm />
            </main>
          </React.Fragment>
        );
      }
      case "/createVaga": {
        return (
          <React.Fragment>
            <InternoNavBar />
            <main className="container">
              <CreateVaga />
            </main>
          </React.Fragment>
        );
      }
      case "/internoMain": {
        return (
          <React.Fragment>
            <InternoNavBar />
            <main className="container">
              <InternoMain />
            </main>
          </React.Fragment>
        );
      }
      case "/internoDataUpdate": {
        return (
          <React.Fragment>
            <InternoNavBar />
            <main className="container">
              <InternoDataUpdate />
            </main>
          </React.Fragment>
        );
      }
      case "/adminMain": {
        return (
          <React.Fragment>
            <AdminNavBar />
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
            <InternoNavBar />
            <main className="container">
              <IFvisits />
            </main>
          </React.Fragment>
        );
      }
      default: {
        return (
          <React.Fragment>
            <main className="container">Oops! Este página não existe!</main>
          </React.Fragment>
        );
      }
    }
  }
}

export default App;
