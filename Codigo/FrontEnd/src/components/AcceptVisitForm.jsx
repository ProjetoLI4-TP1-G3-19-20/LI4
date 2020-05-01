import React, { Component } from "react";
import { getPedidos } from "../HTTPRequests";
import { Calendar, momentLocalizer } from "react-big-calendar";
import moment from "moment";
import "react-big-calendar/lib/css/react-big-calendar.css";

const localizer = momentLocalizer(moment);

class AcceptVisitForm extends Component {
  constructor(props) {
    super(props);

    const urlParams = new URLSearchParams(window.location.search);
    const u = urlParams.get("u");

    /*
    validateMe(u).then((r) => {
      r.text().then((r) => {
        if (String(r) === "True") {
          this.setState({ auth: true, user: u });
        }
      });
    });
    */
    this.state = {
      auth: true,
      events: [],
      pedidos: [],
    };

    getPedidos(u).then((r) => {
      r.json().then((r) => {
        var e = [];
        var id = 0;
        r.forEach((element) => {
          e.push({
            id: id,
            title:
              "Vaga - " +
              new Date(parseInt(element.inicio)).getHours() +
              "h" +
              new Date(parseInt(element.inicio)).getMinutes() +
              "m",
            start: new Date(parseInt(element.inicio)),
            end: new Date(parseInt(element.fim)),
          });
          id++;
        });
        this.setState({ pedidos: r, events: e });
      });
    });

    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleSelectEvent = this.handleSelectEvent.bind(this);
  }

  handleSelectEvent(event) {
    this.setState({ selectedEvent: event });
  }

  handleSubmit() {
    console.log("fixe");
  }

  render() {
    if (!this.state.auth) {
      return <div>Acesso Negado</div>;
    } else {
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
    }
  }
}

export default AcceptVisitForm;
