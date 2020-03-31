import React, { Component } from "react";
import { createUser } from "../HTTPRequests";

class UserRegForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      email: "",
      password: "",
      secondPassword: "",
      username: "",
      current: 0
    };
    this.handleEmail = this.handleEmail.bind(this);
    this.handlePassword = this.handlePassword.bind(this);
    this.handleSecondPassword = this.handleSecondPassword.bind(this);
    this.handleUsername = this.handleUsername.bind(this);
    this.renderSecondPassword = this.renderSecondPassword.bind(this);
    this.handleKeyDown = this.handleKeyDown.bind(this);
  }

  handleEmail(event) {
    this.setState({ email: event.target.value });
  }

  handlePassword(event) {
    this.setState({ password: event.target.value });
  }

  handleSecondPassword(event) {
    this.setState({ secondPassword: event.target.value });
    console.log(this.state.secondPassword);
  }

  handleUsername(event) {
    this.setState({ username: event.target.value });
  }

  renderSecondPassword() {
    var classN;

    if (
      this.state.secondPassword.length === 0 ||
      this.state.secondPassword === this.state.password
    ) {
      classN = "form-control";
    } else {
      classN = "form-control is-invalid";
    }

    return (
      <div className="form-group">
        <label htmlFor="InputPassword2">Introduza novamente a Password</label>
        <input
          onKeyDown={this.handleKeyDown}
          onChange={this.handleSecondPassword}
          type="password"
          className={classN}
          id="InputPassword2"
        />
      </div>
    );
  }

  handleKeyDown(event) {
    if (event.key === "Enter") {
      this.handleSubmit();
    }
  }

  handleSubmit() {
    createUser(this.state.email, this.state.password, this.state.username).then(
      function(r) {
        console.log(r);
      }
    );
  }

  render() {
    return (
      <div className="position-relative m-4">
        <form>
          <div className="form-group">
            <label htmlFor="exampleInputUsername">Nome de Utilizador</label>
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
            <small id="emailHelp" className="form-text text-muted">
              Nunca partilharemos o seu e-mail com ninguém.
            </small>
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
          {this.renderSecondPassword()}
          <button
            type="button"
            className="btn btn-primary"
            onClick={this.handleSubmit}
          >
            Submit
          </button>
        </form>
      </div>
    );
  }
}

export default UserRegForm;
