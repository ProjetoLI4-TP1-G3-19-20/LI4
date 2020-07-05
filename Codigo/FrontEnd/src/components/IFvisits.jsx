import React, { Component } from "react";
import {
  getUserName,
  getVisitasMarcadas,
  finishVisita,
  initVisita,
} from "../HTTPRequests";
import { Calendar, momentLocalizer } from "react-big-calendar";
import moment from "moment";
import "react-big-calendar/lib/css/react-big-calendar.css";
import FeedbackForm from "./feedbackForm";

const localizer = momentLocalizer(moment);

class IFvisits extends Component {
  constructor(props) {
    super(props);

    const urlParams = new URLSearchParams(window.location.search);
    const u = urlParams.get("u");

    this.state = {
      auth: true,
      confirmedEvents: [],
      pedidos: [],
      panel: 0,
      selectedEvent: [],
      user: u,
      aval: "",
      state: 0,
      buttonState: -1,
    };

    this.handleFinish = this.handleFinish.bind(this);
    this.handleSelectEvent = this.handleSelectEvent.bind(this);
    this.updatePedidos = this.updatePedidos.bind(this);
    this.handleAval = this.handleAval.bind(this);
    this.renderButton = this.renderButton.bind(this);
    this.handleInit = this.handleInit.bind(this);

    this.updatePedidos();
  }

  handleSelectEvent(event) {
    this.setState({
      selectedEvent: event,
      buttonState: parseInt(event.estado),
    });
  }

  handleAval(event) {
    this.setState({ aval: event.target.value });
  }

  handleFinish() {
    if (this.state.aval !== "") {
      finishVisita(
        this.state.pedidos[this.state.selectedEvent.id].visitado,
        this.state.aval,
        this.state.selectedEvent.start
      );
      this.setState({ state: 1 });
    }
  }

  handleInit() {
    initVisita(
      this.state.pedidos[this.state.selectedEvent.id].visitado,
      this.state.selectedEvent.start
    );
    this.setState({ state: 1 });
  }

  updatePedidos() {
    var e = [];
    var id = 0;

    getVisitasMarcadas(this.state.user).then((r) => {
      r.json().then((rr) => {
        rr.forEach((element) => {
          getUserName(element.visitante).then((r) => {
            r.text().then((r) => {
              element.visitante = r;
              e.push({
                estado: element.estado,
                id: id,
                title:
                  (
                    "0" + new Date(parseInt(element.data_inicio)).getHours()
                  ).slice(-2) +
                  "h" +
                  (
                    "0" + new Date(parseInt(element.data_inicio)).getMinutes()
                  ).slice(-2) +
                  "m - " +
                  r,
                start: new Date(parseInt(element.data_inicio)),
                end: new Date(parseInt(element.data_saida)),
              });
              this.setState({ pedidos: rr, confirmedEvents: e });
              id++;
            });
          });
        });
      });
    });
  }

  renderButton() {
    console.log(this.state.buttonState);
    if (this.state.buttonState === -1) {
      return;
    } else if (this.state.buttonState === 2) {
      return (
        <div>
          <div className="form-group-auto m-2">
            <label>Avaliação</label>
            <input
              type="email"
              value={this.state.aval}
              onChange={this.handleAval}
              onKeyDown={this.handleKeyDown}
              className="form-control"
              placeholder="Insira aqui a sua avaliacao"
              id="inputAval"
              disabled={this.state.disabled}
            />
          </div>
          <div className="form-group-auto m-2">
            <button
              type="button"
              className="btn btn-primary"
              onClick={this.handleFinish}
            >
              Terminar
            </button>
          </div>
        </div>
      );
    } else {
      return (
        <div className="form-group-auto m-2">
          <button
            type="button"
            className="btn btn-primary"
            onClick={this.handleInit}
          >
            Iniciar
          </button>
        </div>
      );
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
              <div className="form-group">
                <label htmlFor="input">Data e Hora</label>
                <div>
                  <Calendar
                    style={{ height: 600, width: "120%" }}
                    eventPropGetter={(event, start, end, isSelected) => {
                      var backgroundColor = "#000000";
                      if (event.estado === "0" && isSelected === false)
                        backgroundColor = "#0000cc";
                      else if (event.estado === "0" && isSelected === true)
                        backgroundColor = "#4d4dff";
                      else if (event.estado === "2" && isSelected === true)
                        backgroundColor = "#ff8c1a";
                      else if (event.estado === "2" && isSelected === false)
                        backgroundColor = "#b35900";
                      return { style: { backgroundColor } };
                    }}
                    events={this.state.confirmedEvents}
                    startAccessor="start"
                    endAccessor="end"
                    defaultDate={moment().toDate()}
                    localizer={localizer}
                    onSelectEvent={this.handleSelectEvent}
                  />
                </div>
              </div>
              {this.renderButton()}
            </form>
            <hr style={{ height: "40pt", visibility: "hidden" }} />
          </div>
        );
      } else if (this.state.state === 1) {
        return (
          <FeedbackForm
            href={"/internoMain?u=" + this.state.user}
            text="Atualização concluída"
          />
        );
      }
    }
  }
}

export default IFvisits;
