import React, { Component } from "react";
import Select from "react-select";
import {
  createPedido,
  getAllInsts,
  getDepartamentosByInst,
  getPessoasByDepartamento,
  getVagas,
  validateMe,
} from "../HTTPRequests";
import { Calendar, momentLocalizer } from "react-big-calendar";
import moment from "moment";
import "react-big-calendar/lib/css/react-big-calendar.css";
import FeedbackForm from "./feedbackForm";

const localizer = momentLocalizer(moment);

class VisitReqForm extends Component {
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
      depsDisabled: true,
      departamentos: [],
      selectedDep: "",
      pdis: [],
      isPdiDisabled: true,
      selectedPdi: "",
      comentario: "",
      current: 0,
      events: [],
      selectedEvent: "",
      user: -1,
      state: 0,
    };

    const urlParams = new URLSearchParams(window.location.search);
    const u = urlParams.get("u");

    validateMe(u).then((r) => {
      r.text().then((r) => {
        if (String(r) === "True") {
          this.setState({ auth: true, user: u });
        }
      });
    });

    this.handleDuracao = this.handleDuracao.bind(this);
    this.handleComentario = this.handleComentario.bind(this);
    this.handlePdi = this.handlePdi.bind(this);
    this.handleHoraInicio = this.handleHoraInicio.bind(this);
    this.handleKeyDown = this.handleKeyDown.bind(this);
    this.handleInst = this.handleInst.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleSelectEvent = this.handleSelectEvent.bind(this);
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
    this.setState({ selectedDep: selectedOption, isPdiDisabled: false });
    var p = [];

    getPessoasByDepartamento(
      this.state.selectedInst.label,
      selectedOption.label
    ).then((r) => {
      r.text().then((r) => {
        var j = JSON.parse(r);
        j.forEach((element) => {
          p.push({ value: element, label: element });
        });

        this.setState({ pdis: p });
      });
    });
  };

  handlePdi = (selectedOption) => {
    this.setState({ selectedPdi: selectedOption });
    var pdi = [];
    var id = 0;

    getVagas(selectedOption.label).then((r) => {
      r.text().then((r) => {
        var p = JSON.parse(r);

        p.forEach((element) => {
          pdi.push({
            id: id,
            title:
              "Vaga - " +
              ("0" + new Date(parseInt(element.inicio)).getHours()).slice(-2) +
              "h" +
              ("0" + new Date(parseInt(element.inicio)).getMinutes()).slice(
                -2
              ) +
              "m",
            start: new Date(parseInt(element.inicio)),
            end: new Date(parseInt(element.fim)),
          });
          id++;
        });
        this.setState({ events: pdi });
      });
    });
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

  handleKeyDown(event) {
    if (event.key === "Enter") {
      this.handleSubmit();
    }
  }

  handleSubmit() {
    if (
      this.state.selectedInst !== "" &&
      this.state.selectedPdi !== "" &&
      this.state.selectedDep !== "" &&
      this.state.selectedEvent !== "" &&
      this.state.comentario !== ""
    ) {
      const urlParams = new URLSearchParams(window.location.search);
      const u = urlParams.get("u");
      createPedido(this.state, u).then((r) => {
        r.text().then((r) => {
          if (String(r) === "sucesso") {
            this.setState({ state: 1 });
          }
        });
      });
    }
  }

  handleSelectEvent(event) {
    this.setState({ selectedEvent: event });
  }

  render() {
    if (!this.state.auth) {
      return <div>Acesso Negado</div>;
    } else {
      if (this.state.state === 0) {
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
                  isDisabled={this.state.depsDisabled}
                  onChange={this.handleDep}
                  className="basic-single"
                  options={this.state.departamentos}
                />
              </div>
              <div className="form-group">
                <label htmlFor="input">Pessoa de Interesse</label>
                <Select
                  isDisabled={this.state.isPdiDisabled}
                  onChange={this.handlePdi}
                  className="basic-single"
                  options={this.state.pdis}
                />
              </div>
              <div className="form-group">
                <label htmlFor="input">Data e Hora</label>
                <div style={{ height: "500pt" }}>
                  <Calendar
                    events={this.state.events}
                    startAccessor="start"
                    endAccessor="end"
                    defaultDate={moment().toDate()}
                    localizer={localizer}
                    onSelectEvent={this.handleSelectEvent}
                  />
                </div>
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
                Submeter pedido
              </button>
            </form>
          </div>
        );
      } else if (this.state.state === 1) {
        return (
          <FeedbackForm
            href={"/main?u=" + this.state.user}
            text="O seu pedido foi realizado. Receberá um SMS se este for aceite."
          />
        );
      }
    }
  }
}

export default VisitReqForm;
