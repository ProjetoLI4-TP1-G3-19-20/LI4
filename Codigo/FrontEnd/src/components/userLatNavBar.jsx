import React from "react";
import { Component } from "react";

class UserLatNavBar extends Component {
  render() {
    const urlParams = new URLSearchParams(window.location.search);
    const u = urlParams.get("u");
    return (
      <span className="border m-4">
        <a href={"/userHistory?u=" + u}> Histórico</a>
        <ul className="nav flex-column">
          <span className="border m-4">
            <li> {this.props.name} </li>
            <li> Teste2 </li>
            <li> Teste3 </li>
          </span>
        </ul>
      </span>
    );
  }
}

export default UserLatNavBar;
