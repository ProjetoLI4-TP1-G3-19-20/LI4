import React, { Component } from "react";

import { Calendar, momentLocalizer, Views } from "react-big-calendar";
import moment from "moment";
import "react-big-calendar/lib/css/react-big-calendar.css";
import { createVaga, getVagas } from "../HTTPRequests";

const localizer = momentLocalizer(moment);

class CreateVaga extends Component {
  constructor(props) {
    super(props);

    const urlParams = new URLSearchParams(window.location.search);
    const u = urlParams.get("u");

    this.state = {
      events: [],
      oldEvents: [],
      user: u,
    };

    var id = 0;
    var pdi = [];
    getVagas(u).then((r) => {
      r.text().then((r) => {
        var p = JSON.parse(r);

        p.forEach((element) => {
          pdi.push({
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
  }

  render() {
    return (
      <div className="position-relative m-4">
        <form>
          <div className="form-group-auto m-2">
            <Calendar
              selectable
              localizer={localizer}
              events={this.state.oldEvents.concat(this.state.events)}
              defaultView={Views.WEEK}
              scrollToTime={new Date(1970, 1, 1, 6)}
              onSelectEvent={(event) => this.remove(event)}
              onSelectSlot={this.handleSelect}
              dayLayoutAlgorithm={this.state.dayLayoutAlgorithm}
            />
          </div>
          <div className="form-group-auto m-2">
            <button
              type="button"
              className="btn btn-primary"
              onClick={this.handleSubmit}
            >
              {" "}
              Submit
            </button>
          </div>
        </form>
      </div>
    );
  }
}

export default CreateVaga;
