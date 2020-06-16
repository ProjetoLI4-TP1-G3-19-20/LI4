import React from "react";
import "./helper.css";
import { Component } from "react";

//Stateless Functional Component

class Banner extends Component {
  render() {
    return (
      <div className="jumbotron jumbotron-fluid">
        <div className="container">
          <h1 className="display-7">VisitUM20</h1>
        </div>
      </div>
    );
  }
}

export default Banner;
