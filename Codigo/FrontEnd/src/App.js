import React, { Component } from "react";
import LoginForm from "./components/loginForm";
import UserMain from "./components/UserMain";
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
import FooterA from "./components/FooterA";
import FooterI from "./components/FooterI";
import FooterU from "./components/FooterU";
import Footer from "./components/Footer";
import InfoComponente from "./components/InfoComponent";

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
            <div className="wrapper">
              <LoginNavBar />
              <main className="container">
                <LoginForm login={this.login} />
              </main>
            </div>
            <Footer />
          </React.Fragment>
        );
      }
      case "/main": {
        return (
          <React.Fragment>
            <div className="wrapper">
              <NavBar />
              <main className="container">
                <UserMain name={this.state.currentUser} />
              </main>
            </div>
            <FooterU />
          </React.Fragment>
        );
      }
      case "/regUser": {
        return (
          <React.Fragment>
            <div className="wrapper">
              <LoginNavBar />
              <main className="container">
                <UserRegForm />
              </main>
            </div>
            <Footer />
          </React.Fragment>
        );
      }
      case "/userHistory": {
        return (
          <React.Fragment>
            <div className="wrapper">
              <NavBar />
              <main className="container">
                <VisitHistory />
              </main>
            </div>
            <FooterU />
          </React.Fragment>
        );
      }
      case "/regAdmin": {
        return (
          <React.Fragment>
            <div className="wrapper">
              <AdminNavBar />
              <main className="container">
                <AdminRegForm />
              </main>
            </div>
            <FooterA />
          </React.Fragment>
        );
      }
      case "/colabRegForm": {
        return (
          <React.Fragment>
            <div className="wrapper">
              <AdminNavBar />
              <main className="container">
                <ColabRegForm />
              </main>
            </div>
            <FooterA />
          </React.Fragment>
        );
      }
      case "/visitReq": {
        return (
          <React.Fragment>
            <div className="wrapper">
              <NavBar />
              <main className="container">
                <VisitReqForm />
              </main>
            </div>
            <FooterU />
          </React.Fragment>
        );
      }
      case "/AcceptVisit": {
        return (
          <React.Fragment>
            <div className="wrapper">
              <InternoNavBar />
              <main className="container">
                <AcceptVisitForm />
              </main>
            </div>
            <FooterI />
          </React.Fragment>
        );
      }
      case "/newInst": {
        return (
          <React.Fragment>
            <div className="wrapper">
              <AdminNavBar />
              <main className="container">
                <NewInstForm />
              </main>
            </div>
            <FooterA />
          </React.Fragment>
        );
      }
      case "/newDep": {
        return (
          <React.Fragment>
            <div className="wrapper">
              <AdminNavBar />
              <main className="container">
                <NewDepForm />
              </main>
            </div>
            <FooterA />
          </React.Fragment>
        );
      }
      case "/createVaga": {
        return (
          <React.Fragment>
            <div className="wrapper">
              <InternoNavBar />
              <main className="container">
                <CreateVaga />
              </main>
            </div>
            <FooterI />
          </React.Fragment>
        );
      }
      case "/internoMain": {
        return (
          <React.Fragment>
            <div className="wrapper">
              <InternoNavBar />
              <main className="container">
                <InternoMain />
              </main>
            </div>
            <FooterI />
          </React.Fragment>
        );
      }
      case "/internoDataUpdate": {
        return (
          <React.Fragment>
            <div className="wrapper">
              <InternoNavBar />
              <main className="container">
                <InternoDataUpdate />
              </main>
            </div>
            <FooterI />
          </React.Fragment>
        );
      }
      case "/adminMain": {
        return (
          <React.Fragment>
            <div className="wrapper">
              <AdminNavBar />
              <main className="container">
                <AdminMain />
              </main>
            </div>
            <FooterA />
          </React.Fragment>
        );
      }
      case "/userDataUpdate": {
        return (
          <React.Fragment>
            <div className="wrapper">
              <NavBar />
              <main className="container">
                <UserDataUpdate />
              </main>
            </div>
            <FooterU />
          </React.Fragment>
        );
      }
      case "/IFvisits": {
        return (
          <React.Fragment>
            <div className="wrapper">
              <InternoNavBar />
              <main className="container">
                <IFvisits />
              </main>
            </div>
            <FooterI />
          </React.Fragment>
        );
      }
      case "/infoI": {
        return (
          <React.Fragment>
            <div className="wrapper">
              <InternoNavBar />
              <main className="container">
                <InfoComponente />
              </main>
            </div>
            <FooterI />
          </React.Fragment>
        );
      }
      case "/infoA": {
        return (
          <React.Fragment>
            <div className="wrapper">
              <AdminNavBar />
              <main className="container">
                <InfoComponente />
              </main>
            </div>
            <FooterA />
          </React.Fragment>
        );
      }
      case "/infoU": {
        return (
          <React.Fragment>
            <div className="wrapper">
              <NavBar />
              <main className="container">
                <InfoComponente />
              </main>
            </div>
            <FooterU />
          </React.Fragment>
        );
      }
      default: {
        return (
          <React.Fragment>
            <div className="wrapper">
              <main className="container">Oops! Este página não existe!</main>
            </div>
            <Footer />
          </React.Fragment>
        );
      }
    }
  }
}

export default App;
