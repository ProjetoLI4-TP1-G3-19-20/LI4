import React, { Component } from "react";
import { getPedidos, getUserName, aceitePedido } from "../HTTPRequests";
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
      pedidos: [],
      panel: 0,
      selectedEvent: [],
      u: u,
    };

    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleSelectEvent = this.handleSelectEvent.bind(this);
    this.updatePedidos = this.updatePedidos.bind(this);
    this.handleAccept = this.handleAccept.bind(this);
    this.handleRefuse = this.handleRefuse.bind(this);

    this.updatePedidos();
  }

  handleSelectEvent(event) {
    this.setState({ selectedEvent: event });
  }

  handleSubmit() {
    this.setState({ panel: 1 });
  }

  handleAccept() {
    aceitePedido(true, this.state.pedidos[this.state.selectedEvent.id].id).then(
      (r) => {
        this.updatePedidos();
        this.setState({ panel: 0 });
      }
    );
  }

  handleRefuse() {
    aceitePedido(false, this.state.pedidos[this.selectedEvent.id].id).then(
      (r) => {
        this.updatePedidos();
        this.setState({ panel: 0 });
      }
    );
  }

  updatePedidos() {
    getPedidos(this.state.u).then((r) => {
      r.json().then((r) => {
        console.log(r);
        var e = [];
        var id = 0;
        r.forEach((element) => {
          getUserName(element.visitante).then((r) => {
            r.text().then((r) => {
              element.visitante = r;
              e.push({
                id: id,
                title:
                  new Date(parseInt(element.inicio)).getHours() +
                  "h" +
                  new Date(parseInt(element.inicio)).getMinutes() +
                  "m - " +
                  r,
                start: new Date(parseInt(element.inicio)),
                end: new Date(parseInt(element.fim)),
              });
              id++;
            });
          });
        });
        this.setState({ pedidos: r, events: e });
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
              <button
                type="button"
                className="btn btn-primary"
                onClick={this.handleSubmit}
              >
                Verificar
              </button>
            </form>
          </div>
        );
      } else if (this.state.panel === 1) {
        return (
          <div>
            <PedidoVisitaComponent
              data_inicio={new Date(
                parseInt(this.state.pedidos[this.state.selectedEvent.id].fim)
              ).toString()}
              data_saida={new Date(
                parseInt(this.state.pedidos[this.state.selectedEvent.id].inicio)
              ).toString()}
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
