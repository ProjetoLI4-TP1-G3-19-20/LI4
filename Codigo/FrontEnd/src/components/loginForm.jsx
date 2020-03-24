import React from "react";
import "./helper.css";
import { Component } from "react";

class LoginForm extends Component {
  render() {
    return (
      <div className="position-relative m-4">
        <form>
          <div className="form-group-auto m-2">
            <label>E-mail</label>
            <input
              type="email"
              className="form-control"
              id="inputEmail"
              placeholder="Insira aqui o seu e-mail"
            />
          </div>
          <div className="form-group-auto m-2">
            <label htmlFor="inputPassword">Password</label>
            <input
              type="password"
              className="form-control"
              id="inputPassword"
              placeholder="Insira aqui a sua password"
            />
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
          <a className="btn btn-primary" href="/main">
            {" "}
            aqui
          </a>
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
