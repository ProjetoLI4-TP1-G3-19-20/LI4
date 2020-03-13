import React, { Component } from "react";
import "./helper.css";

//Stateless Functional Component

const NavBar = () => {
  return (
    <div className="jumbotron jumbotron-fluid">
      <div className="container">
        <h1 className="display-7">VisitUM20</h1>
        <p className="lead">A facilitar visitas desde 2020</p>
      </div>
    </div>
  );
};

export default NavBar;