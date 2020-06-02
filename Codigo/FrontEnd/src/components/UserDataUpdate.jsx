import React, { Component } from "react";
import { updateUser, getUserFullInfo, validateMe } from "../HTTPRequests";
import PhoneInput from "react-phone-input-2";
import "react-phone-input-2/lib/style.css";

class UserDataUpdate extends Component {
  constructor(props) {
    super(props);
    this.state = {
      email: "",
      password: "",
      secondPassword: "",
      username: "",
      phone: "",
      morada: "",
      postCode: "",
      current: 0,
      user: "",
    };
    this.handleEmail = this.handleEmail.bind(this);
    this.handlePassword = this.handlePassword.bind(this);
    this.handleSecondPassword = this.handleSecondPassword.bind(this);
    this.handleUsername = this.handleUsername.bind(this);
    this.renderFirstPassword = this.renderFirstPassword.bind(this);
    this.renderSecondPassword = this.renderSecondPassword.bind(this);
    this.handleKeyDown = this.handleKeyDown.bind(this);
    this.handleMorada = this.handleMorada.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handlePostCode = this.handlePostCode.bind(this);

    const urlParams = new URLSearchParams(window.location.search);
    const u = urlParams.get("u");

    validateMe(u).then((r) => {
      r.text().then((r) => {
        console.log(r);
        if (String(r) === "True") {
          this.setState({ auth: true, user: u });
        }
      });
    });

    getUserFullInfo(u).then((r) => {
      r.json().then((r) => {
        this.setState({
          email: r.email,
          password: r.password,
          secondPassword: r.password,
          username: r.nome,
          postCode: r.cod_postal,
          phone: r.telefone,
          morada: r.morada,
        });
      });
    });
  }

  handleEmail(event) {
    this.setState({ email: event.target.value });
  }

  handlePassword(event) {
    this.setState({ password: event.target.value });
  }

  handleSecondPassword(event) {
    this.setState({ secondPassword: event.target.value });
  }

  handleUsername(event) {
    this.setState({ username: event.target.value });
  }

  handlePhone(phone) {
    this.setState({ phone: phone.phone });
  }

  handleMorada(event) {
    this.setState({ morada: event.target.value });
  }

  handlePostCode(event) {
    this.setState({ postCode: event.target.value });
  }

  renderFirstPassword() {
    var classN;

    if (this.state.password.length === 0 || this.state.password.length >= 8) {
      classN = "form-control";
    } else {
      classN = "form-control is-invalid";
    }

    return (
      <div className="form-group">
        <label htmlFor="InputPassword2">Introduza a Password</label>
        <input
          onKeyDown={this.handleKeyDown}
          onChange={this.handlePassword}
          value={this.state.password}
          type="password"
          className={classN}
          id="InputPassword"
        />
      </div>
    );
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
          value={this.state.secondPassword}
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
    if (this.state.secondPassword === this.state.password) {
      updateUser(this.state).then((r) => {
        r.text().then((rr) => {
          if (String(rr) === "sucesso") {
            window.location.href = "/main?u=" + this.state.user;
          }
        });
      });
    }
  }

  render() {
    if (this.state.auth) {
      return (
        <div className="position-relative m-4">
          <form>
            <div className="form-group">
              <label htmlFor="exampleInputUsername">Nome Completo</label>
              <input
                onKeyDown={this.handleKeyDown}
                onChange={this.handleUsername}
                value={this.state.username}
                type="username"
                className="form-control"
                id="InputName"
              />
            </div>
            <div className="form-group">
              <label htmlFor="exampleInputEmail1">E-mail</label>
              <input
                onKeyDown={this.handleKeyDown}
                onChange={this.handleEmail}
                value={this.state.email}
                type="email"
                className="form-control"
                id="InputEmail"
                aria-describedby="emailHelp"
              />
              <small id="emailHelp" className="form-text text-muted">
                Nunca partilharemos o seu e-mail com ninguém.
              </small>
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
              <label>Morada</label>
              <input
                onKeyDown={this.handleKeyDown}
                onChange={this.handleMorada}
                value={this.state.morada}
                type="text"
                className="form-control"
                id="inputAdress"
              />
            </div>
            <div className="form-group">
              <label>Código-Postal</label>
              <input
                style={{ width: "200px" }}
                onKeyDown={this.handleKeyDown}
                onChange={this.handlePostCode}
                value={this.state.postCode}
                type="text"
                className="form-control"
                id="inputPostCode"
              />
            </div>
            {this.renderFirstPassword()}
            {this.renderSecondPassword()}
            <button
              type="button"
              className="btn btn-primary"
              onClick={this.handleSubmit}
            >
              Submeter
            </button>
          </form>
          <br></br>
        </div>
      );
    } else {
      return <div>Acesso Negado</div>;
    }
  }
}

export default UserDataUpdate;
