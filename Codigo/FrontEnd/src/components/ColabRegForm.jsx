import React, { Component } from "react";
import {
  createColab,
  validateMeAdmin,
  getAllInsts,
  getDepartamentosByInst,
} from "../HTTPRequests";
import PhoneInput from "react-phone-input-2";
import "react-phone-input-2/lib/style.css";
import Select from "react-select";
import FeedbackForm from "./feedbackForm";

class ColabRegForm extends Component {
  constructor(props) {
    super(props);

    var i;
    var insts = [];
    getAllInsts().then((r) => {
      r.text().then((rr) => {
        i = JSON.parse(rr);

        i.forEach((element) => {
          insts.push({ value: element, label: element });
        });
      });
    });

    this.state = {
      email: "",
      password: "",
      username: "",
      phone: "",
      current: 0,
      instituicoes: insts,
      selectedInst: "",
      selectedDep: "",
      state: 0,
    };

    const urlParams = new URLSearchParams(window.location.search);
    const u = urlParams.get("u");
    validateMeAdmin(u).then((r) => {
      r.text().then((r) => {
        if (String(r) === "True") {
          this.setState({ auth: true, user: u });
        }
      });
    });

    this.handleEmail = this.handleEmail.bind(this);
    this.handlePassword = this.handlePassword.bind(this);
    this.handleUsername = this.handleUsername.bind(this);
    this.handleKeyDown = this.handleKeyDown.bind(this);
    this.handleDep = this.handleDep.bind(this);
    this.handleInst = this.handleInst.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleDep = (selectedOption) => {
    this.setState({ selectedDep: selectedOption });
  };

  handleInst = (selectedOption) => {
    this.setState({ selectedInst: selectedOption, depsDisabled: false });
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

  handleSubmit = () => {
    createColab(this.state).then((r) => {
      r.text().then((r) => {
        if (String(r) === "ok") {
          this.setState({ state: 1 });
        }
      });
    });
  };

  render() {
    if (this.state.auth === true) {
      if (this.state.state === 0) {
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
              <div className="form-group">
                <label htmlFor="inputInst">Instituição</label>
                <Select
                  onChange={this.handleInst}
                  className="basic-single"
                  options={this.state.instituicoes}
                />
              </div>
              <div className="form-group">
                <label htmlFor="inputDep">Departamento</label>
                <Select
                  isDisabled={this.state.depsDisabled}
                  onChange={this.handleDep}
                  className="basic-single"
                  options={this.state.departamentos}
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
      } else if (this.state.state === 1) {
        return (
          <FeedbackForm
            href={"/adminMain?u=" + this.state.user}
            text="O interno foi adicionado com sucesso"
          />
        );
      }
    } else {
      return <div>Acesso Negado</div>;
    }
  }
}

export default ColabRegForm;
