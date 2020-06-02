import React, { Component } from "react";

class FeedbackForm extends Component {
  render() {
    return (
      <span className="card border shadow p-3 mb-5 bg-white rounded position=relative">
        <div>{this.props.text}</div>
        <div>
          <a
            className="badge badge-primary"
            style={{ fontSize: "20px" }}
            href={this.props.href}
          >
            {" "}
            Voltar ao menu inicial
          </a>
        </div>
      </span>
    );
  }
}

export default FeedbackForm;
