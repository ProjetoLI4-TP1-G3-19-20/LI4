import React, { Component } from "react";
import { getUserName, getVisitasMarcadas, finishVisita } from "../HTTPRequests";
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
      events: [],
      confirmedEvents: [],
      pedidos: [],
      panel: 0,
      selectedEvent: [],
      disabled: true,
      user: u,
      aval: "",
      state: 0,
    };

    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleSelectEvent = this.handleSelectEvent.bind(this);
    this.updatePedidos = this.updatePedidos.bind(this);
    this.handleAval = this.handleAval.bind(this);

    this.updatePedidos();
  }

  handleSelectEvent(event) {
    this.setState({ selectedEvent: event, disabled: event.set });
  }

  handleAval(event) {
    this.setState({ aval: event.target.value });
  }

  handleSubmit() {
    if (this.state.aval !== "") {
      finishVisita(
        this.state.pedidos[this.state.selectedEvent.id].visitado,
        this.state.aval,
        this.state.selectedEvent.start
      );
      this.setState({ state: 1 });
    }
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
                set: false,
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

  render() {
    if (!this.state.auth) {
      return <div>Acesso Negado</div>;
    } else {
      if (this.state.state === 0) {
        return (
          <div className="position-relative m-4">
            <form>
              <div className="form-group-auto m-2">
                <a
                  className="badge badge-primary"
                  style={{ fontSize: "20px" }}
                  href={"/internoMain?u=" + this.state.user}
                >
                  {" "}
                  Voltar atrás
                </a>
              </div>
              <div className="form-group">
                <label htmlFor="input">Data e Hora</label>
                <div>
                  <Calendar
                    style={{ height: 600, width: "120%" }}
                    eventPropGetter={(event, start, end, isSelected) => {
                      var backgroundColor = "#3DD120";
                      if (event.set === false) backgroundColor = "#1A37D7";
                      if (event.set === false && isSelected === true)
                        backgroundColor = "#404995";
                      return { style: { backgroundColor } };
                    }}
                    events={this.state.confirmedEvents.concat(
                      this.state.events
                    )}
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
                  onClick={this.handleSubmit}
                  disabled={this.state.disabled}
                >
                  Terminar
                </button>
              </div>
            </form>
          </div>
        );
      } else if (this.state.state === 1) {
        return (
          <FeedbackForm
            href={"/internoMain?u=" + this.state.user}
            text="A visita foi concluída"
          />
        );
      }
    }
  }
}

export default IFvisits;
