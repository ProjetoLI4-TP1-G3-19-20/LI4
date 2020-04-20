import React, { Component } from "react";
import Select from "react-select";
import { createAdmin, getAllInsts } from "../HTTPRequests";
import PhoneInput from "react-phone-input-2";
import "react-phone-input-2/lib/style.css";

class VisitReqForm extends Component {
  constructor(props) {
    super(props);

    var i;
    var insts = [];
    var deps = []; //Departamentos da Instituição escolhida
    var pdi = []; //Pessoas de Interesse disponíveis
    var available_slots = []; //Horas disponíveis da pessoa de interesse escolhida
    getAllInsts().then((r) => {
      r.text().then((rr) => {
        i = JSON.parse(rr);

        i.forEach((element) => {
          insts.push({ value: element, label: element });
        });
      });
    });

    this.state = {
      instituicoes: insts,
      selectedInst: "",
      departamentos: deps,
      selectedDep: "",
      pdis: pdi,
      selectedPdi: "",
      as: available_slots,
      hora_inicio: "",
      duracao: "",
      comentario: "",
      current: 0,
    };

    this.handleDuracao = this.handleDuracao.bind(this);
    this.handleComentario = this.handleComentario.bind(this);
    this.handlePdi = this.handlePdi.bind(this);
    this.handleHoraInicio = this.handleHoraInicio.bind(this);
    this.handleKeyDown = this.handleKeyDown.bind(this);
    this.handleInst = this.handleInst.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }
  handleComentario(event) {
    this.setState({ comentario: event.target.value });
  }
  handleHoraInicio(event) {
    this.setState({ hora_inicio: event.target.value });
  }
  handleDuracao(event) {
    this.setState({ duracao: Number(event.target.value) });
  }
  handleDep = (selectedOption) => {
    this.setState({ selectedIns: selectedOption });
    console.log(this.state.selectedIns.label);
  };
  handlePdi = (selectedOption) => {
    this.setState({ selectedIns: selectedOption });
    console.log(this.state.selectedIns.label);
  };
  handleInst = (selectedOption) => {
    this.setState({ selectedIns: selectedOption });
    console.log(this.state.selectedIns.label);
  };

  handleKeyDown(event) {
    if (event.key === "Enter") {
      this.handleSubmit();
    }
  }

  handleSubmit() {
    if (this.state.secondPassword === this.state.password) {
      createAdmin(this.state).then((r) => {
        r.text().then((rr) => {
          console.log(rr);
        });
      });
    }
  }

  render() {
    return (
      <div className="position-relative m-4">
        <form>
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
              onChange={this.handleDep}
              className="basic-single"
              options={this.state.departamentos}
            />
          </div>
          <div className="form-group">
            <label htmlFor="input">Pessoa de Interesse</label>
            <Select
              onChange={this.handlePdi}
              className="basic-single"
              options={this.state.pdis}
            />
          </div>

          <div class="col-lg-9">
            <label for="dtpickerdemo" class="col-sm-2 control-label">
              Select date/time:
            </label>

            <div class="col-sm-4 input-group date" id="dtpickerdemo">
              <input type="text" class="form-control" />

              <span class="input-group-addon">
                <span class="glyphicon glyphicon-calendar"></span>
              </span>
            </div>
          </div>
          <div className="form-group">
            <label htmlFor="inputHora">Hora de Início</label>
            <input
              onKeyDown={this.handleKeyDown}
              onChange={this.handleHoraInicio}
              className="form-control"
              id="inputHora"
            />
          </div>
          <div className="form-group">
            <label htmlFor="inputDuracao">Duração</label>
            <input
              onKeyDown={this.handleKeyDown}
              onChange={this.handleDuracao}
              className="form-control"
              id="inputDuracao"
            />
          </div>
          <div>
            <div id="bootstrap-datetimepicker-widget"></div>
          </div>
          <div className="form-group">
            <label htmlFor="inputComentario">Comentários:</label>
            <input
              onKeyDown={this.handleKeyDown}
              onChange={this.handleComentario}
              className="form-control"
              id="inputComentario"
            />
          </div>
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

export default VisitReqForm;
