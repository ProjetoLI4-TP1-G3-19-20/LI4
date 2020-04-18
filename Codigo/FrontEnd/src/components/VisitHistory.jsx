import React, { Component } from "react";
import { userVisitHistory } from "../HTTPRequests";
import PhoneInput from "react-phone-input-2";
import "react-phone-input-2/lib/style.css";
import VisitComponent from "./VisitComponent";
import { render } from "@testing-library/react";

class VisitHistory extends Component {
  constructor(props) {
    super(props);
    this.state = {
      visitas: [VisitComponent, VisitComponent, VisitComponent],
    };
    this.renderVisit = this.renderVisit.bind(this);
  }
  renderVisit() {
    return <VisitComponent />;
  }
  render() {
    return (
      <ul>
        {this.state.visitas.map(function (item) {
          return <li onClick={this.renderVisit()}>Visita</li>;
        })}
      </ul>
    );
  }
}

export default VisitHistory;
