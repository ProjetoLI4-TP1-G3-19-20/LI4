import React, { Component } from "react";
import { createInst } from "../HTTPRequests";

class NewInstForm extends Component {
  constructor(props) {
    super(props);
    this.state = { email: "", nome: "", localizacao: "", current: 0 };
    this.handleEmail = this.handleEmail.bind(this);
    this.handleNome = this.handleNome.bind(this);
    this.handleLocalizacao = this.handleLocalizacao.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.renderErrorMessage = this.renderErrorMessage.bind(this);
    this.handleKeyDown = this.handleKeyDown.bind(this);
  }

  handleEmail(event) {
    this.setState({ email: event.target.value });
  }

  handleNome(event) {
    this.setState({ nome: event.target.value });
  }

  handleLocalizacao(event) {
    this.setState({ localizacao: event.target.value });
  }

  handleKeyDown(event) {
    if (event.key === "Enter") {
      this.handleSubmit();
    }
  }

  handleSubmit() {
    createInst(this.state).then((r) => {
      r.text().then((r) => {
        if (String(r) === "erro") {
          this.setState({ current: 1 });
        }
      });
    });
  }

  renderErrorMessage() {
    if (this.state.current === 0) {
      return;
    } else if (this.state.current === 1) {
      return <div className="text-danger">Instituição já existe</div>;
    }
  }

  render() {
    return (
      <div className="position-relative m-4">
        <form>
          <div className="form-group-auto m-2">
            <a
              className="badge badge-primary"
              style={{ fontSize: "20px" }}
              href={"/main?u=" + this.state.user}
            >
              {" "}
              Voltar atrás
            </a>
          </div>
          <div className="form-group-auto m-2">
            <label>E-mail institucional</label>
            <input
              type="email"
              value={this.state.email}
              onChange={this.handleEmail}
              onKeyDown={this.handleKeyDown}
              className="form-control"
              placeholder="Email institucional"
              id="inputEmail"
            />
          </div>
          <div className="form-group-auto m-2">
            <label htmlFor="inputPassword">Nome</label>
            <input
              type="name"
              value={this.state.nome}
              onChange={this.handleNome}
              onKeyDown={this.handleKeyDown}
              className="form-control"
              placeholder="Insira aqui o nome da instituição"
            />
          </div>
          <div className="form-group-auto m-2">
            <label htmlFor="inputPassword">Localização</label>
            <input
              type="name"
              value={this.state.localizacao}
              onChange={this.handleLocalizacao}
              onKeyDown={this.handleKeyDown}
              className="form-control"
              placeholder="Localização"
            />
            {this.renderErrorMessage()}
          </div>
          <button
            type="button"
            className="btn btn-primary"
            onClick={this.handleSubmit}
          >
            {" "}
            Submit
          </button>
        </form>
      </div>
    );
  }
}

export default NewInstForm;
