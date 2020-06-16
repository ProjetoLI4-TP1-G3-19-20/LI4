import React from "react";
import { Component } from "react";
import "./helper.css";

class LoginNavBar extends Component {
  render() {
    return (
      <nav className="navbar navbar-expand-lg navbar navbar-dark bg-dark">
        <a className="navbar-brand" href={"/"}>
          VisitasUminho
        </a>
      </nav>
    );
  }
}

export default LoginNavBar;
