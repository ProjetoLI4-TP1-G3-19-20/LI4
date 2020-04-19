import React from "react";
import "./helper.css";
import { Component } from "react";
import { login } from "../HTTPRequests";

class LoginForm extends Component {
  constructor(props) {
    super(props);
    this.state = { email: "", password: "", current: 0 };
    this.handleEmail = this.handleEmail.bind(this);
    this.handlePassword = this.handlePassword.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.renderErrorMessage = this.renderErrorMessage.bind(this);
    this.handleKeyDown = this.handleKeyDown.bind(this);
  }

  handleEmail(event) {
    this.setState({ email: event.target.value });
  }

  handlePassword(event) {
    this.setState({ password: event.target.value });
  }

  handleKeyDown(event) {
    if (event.key === "Enter") {
      this.handleSubmit();
    }
  }

  handleSubmit() {
    login(this.state.email, this.state.password).then((r) => {
      r.text().then((rr) => {
        console.log(rr);
        if (String(rr) === "false" || String(rr) === "naoExiste") {
          this.setState({ current: 1 });
        } else {
          this.props.login(String(rr));
        }
      });
    });
  }

  renderErrorMessage() {
    if (this.state.current === 0) {
      return;
    } else if (this.state.current === 1) {
      return <div className="text-danger">Email ou Password Errada</div>;
    }
  }

  render() {
    return (
      <div className="position-relative m-4">
        <form>
          <div className="form-group-auto m-2">
            <label>E-mail</label>
            <input
              type="email"
              value={this.state.email}
              onChange={this.handleEmail}
              onKeyDown={this.handleKeyDown}
              className="form-control"
              placeholder="Insira aqui o seu e-mail"
            />
          </div>
          <div className="form-group-auto m-2">
            <label htmlFor="inputPassword">Password</label>
            <input
              type="password"
              value={this.state.password}
              onChange={this.handlePassword}
              onKeyDown={this.handleKeyDown}
              className="form-control"
              placeholder="Insira aqui a sua password"
            />
            {this.renderErrorMessage()}
          </div>

          <div className="form-check m-4">
            <input
              className="form-check-input"
              type="checkbox"
              value=""
              id="defaultCheck1"
            />
            <label className="form-check-label" htmlFor="defaultCheck1">
              Manter-me logado
            </label>
          </div>
          <button
            type="button"
            className="btn btn-primary"
            onClick={this.handleSubmit}
          >
            {" "}
            Login
          </button>
        </form>
        <small>
          <label className="form-check-label" htmlFor="defaultCheck1">
            Não tem conta? Pode registar-se
            <a href="/regUser"> aqui</a>!
          </label>
        </small>
      </div>
    );
  }
}

export default LoginForm;
