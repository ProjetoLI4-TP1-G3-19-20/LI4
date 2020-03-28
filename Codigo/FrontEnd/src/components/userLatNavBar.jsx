import React from "react";
import { Component } from "react";

class UserLatNavBar extends Component {
  render() {
    return (
      <span className="border m-4">
        <ul className="nav flex-column">
          <span className="border m-4">
            <li> Teste1 </li>
            <li> Teste2 </li>
            <li> Teste3 </li>
          </span>
        </ul>
      </span>
    );
  }
}

export default UserLatNavBar;
