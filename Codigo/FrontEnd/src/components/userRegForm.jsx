import React, { Component } from "react";

class UserRegForm extends Component {
  render() {
    return (
      <div className="position-relative m-4">
        <form>
          <div className="form-group">
            <label htmlFor="exampleInputUsername">Nome de Utilizador</label>
            <input
              type="username"
              className="form-control"
              id="exampleInputUsername"
            />
          </div>
          <div className="form-group">
            <label htmlFor="exampleInputEmail1">E-mail</label>
            <input
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
            <label htmlFor="exampleInputPassword1">Password</label>
            <input
              type="password"
              className="form-control"
              id="exampleInputPassword1"
            />
          </div>
          <button type="Registar" className="btn btn-primary">
            Submit
          </button>
        </form>
      </div>
    );
  }
}

export default UserRegForm;
