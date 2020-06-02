import React, { Component } from "react";
import { createColab } from "../HTTPRequests";
import PhoneInput from "react-phone-input-2";
import "react-phone-input-2/lib/style.css";

class ColabRegForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      email: "",
      password: "",
      username: "",
      phone: "",
      current: 0,
    };
    this.handleEmail = this.handleEmail.bind(this);
    this.handlePassword = this.handlePassword.bind(this);
    this.handleUsername = this.handleUsername.bind(this);
    this.handleKeyDown = this.handleKeyDown.bind(this);
  }

  handleEmail(event) {
    this.setState({ email: event.target.value });
  }

  handlePassword(event) {
    this.setState({ password: event.target.value });
  }

  handleUsername(event) {
    this.setState({ username: event.target.value });
  }

  handlePhone(phone) {
    this.setState({ phone: phone.phone });
  }

  handleKeyDown(event) {
    if (event.key === "Enter") {
      this.handleSubmit();
    }
  }

  handleSubmit() {
    createColab(
      this.state.email,
      this.state.password,
      this.state.username
    ).then(function (r) {
      console.log(r);
    });
  }

  render() {
    return (
      <div className="position-relative m-4">
        <form>
          <div className="form-group">
            <label htmlFor="exampleInputUsername">Nome Completo</label>
            <input
              onKeyDown={this.handleKeyDown}
              onChange={this.handleUsername}
              type="username"
              className="form-control"
              id="exampleInputUsername"
            />
          </div>
          <div className="form-group">
            <label htmlFor="exampleInputEmail1">E-mail</label>
            <input
              onKeyDown={this.handleKeyDown}
              onChange={this.handleEmail}
              type="email"
              className="form-control"
              id="exampleInputEmail1"
              aria-describedby="emailHelp"
            />
          </div>
          <div className="form-group">
            <label>Telemovel</label>
            <PhoneInput
              country={"pt"}
              value={this.state.phone}
              onChange={(phone) => this.handlePhone({ phone })}
            />
          </div>
          <div className="form-group">
            <label htmlFor="InputPassword1">Password</label>
            <input
              onKeyDown={this.handleKeyDown}
              onChange={this.handlePassword}
              type="password"
              className="form-control"
              id="InputPassword1"
            />
          </div>
          <button
            type="button"
            className="btn btn-primary"
            onClick={this.handleSubmit}
          >
            Submeter
          </button>
        </form>
      </div>
    );
  }
}

export default ColabRegForm;
