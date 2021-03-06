import React, { Component } from "react";
import Select from "react-select";
import { getAllInsts, validateMeAdmin, createDep } from "../HTTPRequests";
import FeedbackForm from "./feedbackForm";

class NewDepForm extends Component {
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
      auth: false,
      instituicoes: insts,
      selectedInst: "",
      dep: "",
      current: 0,
      user: -1,
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

    this.handleKeyDown = this.handleKeyDown.bind(this);
    this.handleInst = this.handleInst.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleDep = this.handleDep.bind(this);
  }

  handleInst = (selectedOption) => {
    this.setState({ selectedInst: selectedOption });
  };

  handleDep(event) {
    this.setState({ dep: event.target.value });
  }

  handleKeyDown(event) {
    if (event.key === "Enter") {
      this.handleSubmit();
    }
  }

  handleSubmit() {
    if (this.state.selectedInst !== "" && this.state.dep !== "") {
      createDep(this.state).then((r) => {
        r.text().then((r) => {
          if (String(r) === "erro") {
            this.setState({ current: 1 });
          } else {
            this.setState({ state: 1 });
          }
        });
      });
    }
  }

  handleSelectEvent(event) {
    this.setState({ selectedEvent: event });
  }

  renderErrorMessage() {
    if (this.state.current === 0) {
      return;
    } else if (this.state.current === 1) {
      return <div className="text-danger">Departamento já existe</div>;
    }
  }

  render() {
    if (!this.state.auth) {
      return <div>Acesso Negado</div>;
    } else {
      if (this.state.state === 0) {
        return (
          <div className="position-relative m-4">
            <form>
              <div className="form-group-auto m-2">
                <label htmlFor="inputInst">Instituição</label>
                <Select
                  onChange={this.handleInst}
                  className="basic-single"
                  options={this.state.instituicoes}
                />
              </div>
              <div className="form-group-auto m-2">
                <label>Nome do departamento</label>
                <input
                  type="email"
                  value={this.state.dep}
                  onChange={this.handleDep}
                  onKeyDown={this.handleKeyDown}
                  className="form-control"
                  placeholder="Nome do departamento"
                  id="inputEmail"
                />
              </div>
              {this.renderErrorMessage()}
              <div className="form-group-auto m-2">
                <button
                  type="button"
                  className="btn btn-primary"
                  onClick={this.handleSubmit}
                >
                  Submeter
                </button>
              </div>
            </form>
          </div>
        );
      } else if (this.state.state === 1) {
        return (
          <FeedbackForm
            href={"/adminMain?u=" + this.state.user}
            text="O departamento foi criado com sucesso"
          />
        );
      }
    }
  }
}

export default NewDepForm;
