import React from "react";
import { Component } from "react";
import "./helper.css";

class NavBar extends Component {
  constructor(props) {
    super(props);

    const urlParams = new URLSearchParams(window.location.search);
    const u = urlParams.get("u");

    this.state = {
      user: u,
    };

    this.logout = this.logout.bind(this);
  }

  logout() {
    console.log("tou aqui");
    sessionStorage.removeItem("token");
    window.location.href = "/";
  }

  render() {
    return (
      <nav className="navbar navbar-expand-lg navbar navbar-dark bg-dark">
        <a className="navbar-brand" href={"/main?u=" + this.state.user}>
          VisitasUminho
        </a>
        <div className="collapse navbar-collapse" id="navbarSupportedContent">
          <ul className="navbar-nav mr-auto">
            <li className="nav-item active">
              <a
                className="nav-link"
                href={"/userHistory?u=" + this.state.user}
              >
                Histórico <span className="sr-only">(current)</span>
              </a>
            </li>
            <li className="nav-item active">
              <a className="nav-link" href={"/visitReq?u=" + this.state.user}>
                Faça um pedido <span className="sr-only">(current)</span>
              </a>
            </li>
            <li className="nav-item active">
              <a
                className="nav-link"
                href={"/userDataUpdate?u=" + this.state.user}
              >
                Os meus dados pessoais{" "}
                <span className="sr-only">(current)</span>
              </a>
            </li>
          </ul>
          <form className="form-inline my-2 my-lg-0">
            <button
              className="btn btn-outline-success my-2 my-sm-0"
              type="submit"
              onClick={this.logout}
            >
              <a className="logout" href="/">
                Log Out
              </a>
            </button>
          </form>
        </div>
      </nav>
    );
  }
}

export default NavBar;
