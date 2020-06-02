import React, { Component } from "react";
import question from "./questionMark.png";

import { Calendar, momentLocalizer, Views } from "react-big-calendar";
import moment from "moment";
import "react-big-calendar/lib/css/react-big-calendar.css";
import { createVaga, getVagas, validateMePI } from "../HTTPRequests";
import { Popup } from "semantic-ui-react";
import FeedbackForm from "./feedbackForm";

const localizer = momentLocalizer(moment);

class CreateVaga extends Component {
  constructor(props) {
    super(props);

    this.state = {
      events: [],
      oldEvents: [],
      user: "",
      auth: false,
      state: 0,
    };

    const urlParams = new URLSearchParams(window.location.search);
    const u = urlParams.get("u");

    validateMePI(u).then((r) => {
      r.text().then((r) => {
        console.log(r);
        if (String(r) === "True") {
          this.setState({ auth: true, user: u });
        }
      });
    });

    var id = 0;
    var pdi = [];
    getVagas(u).then((r) => {
      r.text().then((r) => {
        var p = JSON.parse(r);

        p.forEach((element) => {
          pdi.push({
            id: id,
            set: true,
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
        this.setState({ oldEvents: pdi });
      });
    });

    this.handleSelect = this.handleSelect.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleSelect = ({ start, end }) => {
    const title = "Nova vaga";
    if (title)
      this.setState({
        events: [
          ...this.state.events,
          {
            start,
            end,
            title,
            set: false,
          },
        ],
      });
  };

  remove(event) {
    for (var i = 0; i < this.state.events.length; i++) {
      if (event.start === this.state.events[i].start) {
        break;
      }
    }
    this.state.events.splice(i, 1);
    this.setState({ events: this.state.events });
  }

  handleSubmit() {
    this.state.events.forEach((element) => {
      createVaga(element.start, element.end, this.state.user);
    });
    this.setState({ state: 1 });
  }

  render() {
    if (this.state.auth === false) {
      return <div>Acesso Negado</div>;
    } else {
      if (this.state.state === 0) {
        return (
          <div className="position-relative m-4">
            <form>
              <div className="form-group-auto m-2">
                <Calendar
                  selectable
                  localizer={localizer}
                  style={{ height: 700, width: "120%" }}
                  events={this.state.oldEvents.concat(this.state.events)}
                  defaultView={Views.WEEK}
                  scrollToTime={new Date(1970, 1, 1, 6)}
                  onSelectEvent={(event) => this.remove(event)}
                  onSelectSlot={this.handleSelect}
                  dayLayoutAlgorithm={this.state.dayLayoutAlgorithm}
                />
              </div>
              <Popup
                content="Arraste na vertical, na data e hora correspondente, para criar uma nova vaga. Clique para apagar uma existente"
                trigger={
                  <img src={question} alt="Logo" width="50" height="50" />
                }
                position="right center"
              />
              <div className="form-group-auto m-2">
                <button
                  type="button"
                  className="btn btn-primary"
                  onClick={this.handleSubmit}
                >
                  {" "}
                  Submeter
                </button>
              </div>
            </form>
          </div>
        );
      } else if (this.state.state === 1) {
        return (
          <FeedbackForm
            href={"/internoMain?u=" + this.state.user}
            text="Vagas atualizadas com sucesso"
          />
        );
      }
    }
  }
}

export default CreateVaga;
