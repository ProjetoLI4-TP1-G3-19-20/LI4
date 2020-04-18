import React, { Component } from "react";

class VisitComponent extends Component {
  constructor(props) {
    super(props);
    this.state = {
      begin_date: "20-12-2019 05:00",
      end_date: "20-12-2019 06:00",
      concluded: 1,
      inst: "Universidade do Minho",
      dep: "Comissão de Ética",
      visited: "Shigechi",
      comments:
        "My name is Yoshikage Kira. I'm 33 years old. My house is in the northeast section of Morioh, where all the villas are, and I am not married. ",
      current: 0,
    };
  }
  render() {
    return (
      <span
        class="card border shadow p-3 mb-5 bg-white rounded position=relative"
        style={{ width: 500 }}
      >
        <div>
          <p>
            <b>Instituição:</b> {this.state.inst}
          </p>
          <p>
            <b>Departamento:</b> {this.state.dep}
          </p>
          <p>
            <b>Visitado:</b> {this.state.visited}
          </p>
          <p>
            <b>Hora Entrada:</b> {this.state.begin_date}
          </p>
          <p>
            <b>Hora Saída:</b> {this.state.end_date}
          </p>
          <p>
            <b>Comentários:</b> {this.state.comments}
          </p>
        </div>
      </span>
    );
  }
}

export default VisitComponent;
