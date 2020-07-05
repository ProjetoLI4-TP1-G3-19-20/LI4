import React, { Component } from "react";
import "./helper.css";

class WelcomeComponent extends Component {
  constructor(props) {
    super(props);
    this.state = {
        text1: this.props.text1,
        text2: this.props.text2,
        text3: this.props.text3
    };
  }
  render() {
    return (
      <span className="card border shadow p-3 mb-5 bg-white rounded position=relative">
        <div>
          <p style={{fontSize: "30px"}}>
            <strong>{this.state.text1}</strong>
          </p>
          <p>
            {this.state.text2}
          </p>
          <p>
            {this.state.text3}
          </p>
        </div>
      </span>
    );
  }
}

export default WelcomeComponent;