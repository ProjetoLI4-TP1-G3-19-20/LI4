import React, { Component } from "react";
import {
  getInternoFullInfo,
  validateMePI,
  getAllInsts,
  getDepartamentosByInst,
  updateInterno,
} from "../HTTPRequests";
import PhoneInput from "react-phone-input-2";
import "react-phone-input-2/lib/style.css";
import FeedbackForm from "./feedbackForm";
import Select from "react-select";

class InternoDataUpdate extends Component {
  constructor(props) {
    super(props);

    this.state = {
      email: "",
      password: "",
      secondPassword: "",
      username: "",
      ogname: "",
      phone: "",
      dep: "",
      deps: [],
      current: 0,
      user: "",
      inst: "",
      insts: [],
      state: 0,
    };

    const urlParams = new URLSearchParams(window.location.search);
    const u = urlParams.get("u");

    validateMePI(u).then((r) => {
      r.text().then((r) => {
        console.log(r);
        if (String(r) === "True") {
          this.setState({ auth: true, user: u });
        }
      });
    });

    getInternoFullInfo(u).then((r) => {
      r.json().then((r) => {
        this.setState({
          email: r.email,
          username: r.nome,
          ogname: r.nome,
          phone: r.telefone,
          inst: r.inst,
          dep: r.dep,
        });

        var i;
        var insts = [];
        getAllInsts().then((r) => {
          r.text().then((rr) => {
            i = JSON.parse(rr);

            i.forEach((element) => {
              insts.push({ value: element, label: element });
            });
            this.setState({ insts: insts });

            var deps = []; //Departamentos da Instituição escolhida
            getDepartamentosByInst(this.state.inst).then((r) => {
              r.text().then((rr) => {
                var d = JSON.parse(rr);
                d.forEach((element) => {
                  deps.push({ value: element, label: element });
                });

                this.setState({ deps: deps });
              });
            });
          });
        });
      });
    });

    this.handleEmail = this.handleEmail.bind(this);
    this.handlePassword = this.handlePassword.bind(this);
    this.handleSecondPassword = this.handleSecondPassword.bind(this);
    this.handleUsername = this.handleUsername.bind(this);
    this.renderFirstPassword = this.renderFirstPassword.bind(this);
    this.renderSecondPassword = this.renderSecondPassword.bind(this);
    this.handleKeyDown = this.handleKeyDown.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleInst = this.handleInst.bind(this);
  }

  handleInst = (selectedOption) => {
    this.setState({ inst: selectedOption.label });
    var deps = []; //Departamentos da Instituição escolhida

    getDepartamentosByInst(selectedOption.label).then((r) => {
      r.text().then((rr) => {
        var d = JSON.parse(rr);
        d.forEach((element) => {
          deps.push({ value: element, label: element });
        });

        this.setState({ departamentos: deps, pdis: [] });
      });
    });
  };

  handleDep = (selectedOption) => {
    this.setState({ dep: selectedOption.label });
  };

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
      updateInterno(this.state).then((r) => {
        r.text().then((rr) => {
          if (String(rr) === "sucesso") {
            this.setState({ state: 1 });
          }
        });
      });
    }
  }

  render() {
    if (this.state.auth) {
      if (this.state.state === 0) {
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
                <label htmlFor="inputInst">Instituição</label>
                <Select
                  value={{ value: this.state.inst, label: this.state.inst }}
                  onChange={this.handleInst}
                  className="basic-single"
                  options={this.state.insts}
                />
              </div>
              <div className="form-group">
                <label htmlFor="inputDep">Departamento</label>
                <Select
                  value={{ value: this.state.dep, label: this.state.dep }}
                  onChange={this.handleDep}
                  className="basic-single"
                  options={this.state.deps}
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
      } else if (this.state.state === 1) {
        sessionStorage.removeItem("token");
        return (
          <FeedbackForm
            href={"/"}
            text="Os seus dados foram atualizados. Por favor volte a fazer login"
          />
        );
      }
    } else {
      return <div>Acesso Negado</div>;
    }
  }
}

export default InternoDataUpdate;
