import React, { Component } from "react";
import {
  getPedidos,
  getUserName,
  aceitePedido,
  getVisitasMarcadas,
  getPhoneNumber,
  sendSMS,
} from "../HTTPRequests";
import PedidoVisitaComponent from "./PedidoVisitaComponent";
import { Calendar, momentLocalizer } from "react-big-calendar";
import moment from "moment";
import "react-big-calendar/lib/css/react-big-calendar.css";

const localizer = momentLocalizer(moment);

class AcceptVisitForm extends Component {
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
    };

    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleSelectEvent = this.handleSelectEvent.bind(this);
    this.updatePedidos = this.updatePedidos.bind(this);
    this.handleAccept = this.handleAccept.bind(this);
    this.handleRefuse = this.handleRefuse.bind(this);

    this.updatePedidos();
  }

  handleSelectEvent(event) {
    this.setState({ selectedEvent: event, disabled: event.set });
  }

  handleSubmit() {
    this.setState({ panel: 1 });
  }

  handleAccept() {
    aceitePedido(true, this.state.pedidos[this.state.selectedEvent.id].id).then(
      (r) => {
        // eslint-disable-next-line
        this.state.pedidos = [];
        // eslint-disable-next-line
        this.state.events = [];
        this.updatePedidos();
        this.setState({ panel: 0 });
      }
    );

    getPhoneNumber(this.state.pedidos[this.state.selectedEvent.id].uid).then(
      (r) => {
        r.json().then((r) => {
          var date = new Date(this.state.selectedEvent.start);
          sendSMS(
            "Informamos que a sua visita com " +
              this.state.user +
              ", dia " +
              ("0" + date.getDate()).slice(-2) +
              "/" +
              ("0" + date.getMonth()).slice(-2) +
              ", pelas " +
              ("0" + date.getHours()).slice(-2) +
              ":" +
              ("0" + date.getMinutes()).slice(-2) +
              ", foi confirmada com sucesso. Esperamos vê-lo brevemente. \n\nEzVisits",
            r.phone.slice(-9)
          );
        });
      }
    );
  }

  handleRefuse() {
    aceitePedido(
      false,
      this.state.pedidos[this.state.selectedEvent.id].id
    ).then((r) => {
      this.updatePedidos();
      console.log(this.state.selectedEvent);
      this.setState({ panel: 0 });
    });
  }

  updatePedidos() {
    var e = [];
    var id = 0;
    getPedidos(this.state.user).then((r) => {
      r.json().then((rr) => {
        console.log(rr);
        rr.forEach((element) => {
          element.uid = element.visitante;
          getUserName(element.visitante).then((r) => {
            r.text().then((r) => {
              element.visitante = r;
              e.push({
                set: false,
                id: id,
                title:
                  ("0" + new Date(parseInt(element.inicio)).getHours()).slice(
                    -2
                  ) +
                  "h" +
                  ("0" + new Date(parseInt(element.inicio)).getMinutes()).slice(
                    -2
                  ) +
                  "m - " +
                  r,
                start: new Date(parseInt(element.inicio)),
                end: new Date(parseInt(element.fim)),
              });
              id++;
              this.setState({ pedidos: rr, events: e });
            });
          });
        });
      });
    });

    getVisitasMarcadas(this.state.user).then((r) => {
      var e = [];
      var id = 10000;
      r.json().then((r) => {
        r.forEach((element) => {
          getUserName(element.visitante).then((r) => {
            r.text().then((r) => {
              element.visitante = r;
              e.push({
                set: true,
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
              this.setState({ confirmedEvents: e });
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
      if (this.state.panel === 0) {
        return (
          <div className="position-relative m-4">
            <form>
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
              <button
                type="button"
                className="btn btn-primary"
                onClick={this.handleSubmit}
                disabled={this.state.disabled}
              >
                Verificar
              </button>
            </form>
            <hr style={{ height: "40pt", visibility: "hidden" }} />
          </div>
        );
      } else if (this.state.panel === 1) {
        return (
          <div>
            <PedidoVisitaComponent
              data_inicio={this.state.selectedEvent.start}
              data_saida={this.state.selectedEvent.end}
              visitante={
                this.state.pedidos[this.state.selectedEvent.id].visitante
              }
              comentario={
                this.state.pedidos[this.state.selectedEvent.id].comentario
              }
            ></PedidoVisitaComponent>
            <span>
              <button
                type="button"
                className="btn btn-success"
                onClick={this.handleAccept}
              >
                Aceitar
              </button>
              <button
                type="button"
                className="btn btn-danger"
                onClick={this.handleRefuse}
              >
                Recusar
              </button>
            </span>
          </div>
        );
      }
    }
  }
}

export default AcceptVisitForm;
