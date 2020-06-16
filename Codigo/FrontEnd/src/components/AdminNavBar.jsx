import React from "react";
import { Component } from "react";
import "./helper.css";
import "bootstrap/dist/css/bootstrap.min.css";

class AdminNavBar extends Component {
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
    sessionStorage.removeItem("token");
    window.location.href = "/";
  }

  render() {
    return (
      <nav className="navbar navbar-expand-lg navbar navbar-dark bg-dark">
        <a className="navbar-brand" href={"/adminMain?u=" + this.state.user}>
          VisitasUminho
        </a>
        <div className="collapse navbar-collapse" id="navbarSupportedContent">
          <ul className="navbar-nav mr-auto">
            <li className="nav-item dropdown">
              <a
                className="nav-link dropdown-toggle"
                href="/"
                id="navbarDropdown"
                role="button"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
              >
                Criar
              </a>
              <div className="dropdown-menu" aria-labelledby="navbarDropdown">
                <a
                  className="dropdown-item"
                  href={"/newInst?u=" + this.state.user}
                >
                  Instituição
                </a>
                <a
                  className="dropdown-item"
                  href={"/newDep?u=" + this.state.user}
                >
                  Departamento
                </a>
              </div>
            </li>
            <li className="nav-item dropdown">
              <a
                className="nav-link dropdown-toggle"
                href="/"
                id="navbarDropdown"
                role="button"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
              >
                Registar
              </a>
              <div className="dropdown-menu" aria-labelledby="navbarDropdown">
                <a
                  className="dropdown-item"
                  href={"/regAdmin?u=" + this.state.user}
                >
                  Administrador
                </a>
                <a
                  className="dropdown-item"
                  href={"/colabRegForm?u=" + this.state.user}
                >
                  Interno
                </a>
              </div>
            </li>
          </ul>
          <form className="form-inline my-2 my-lg-0">
            <button
              className="btn btn-outline-success my-2 my-sm-0"
              type="submit"
              onClick={this.logout}
            >
              <a href="/">Log Out</a>
            </button>
          </form>
        </div>
      </nav>
    );
  }
}

export default AdminNavBar;
