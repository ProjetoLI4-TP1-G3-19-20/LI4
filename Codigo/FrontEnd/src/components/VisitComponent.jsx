import React, { Component } from "react";

class VisitComponent extends Component {
  constructor(props) {
    super(props);
    this.state = {
      begin_date: this.props.data_inicio,
      end_date: this.props.data_saida,
      concluded: this.props.concluded,
      inst: this.props.id_inst,
      dep: this.props.departamentosID,
      visited: this.props.visitado,
      comments: this.props.comentario,
      avaliacao: this.props.avaliacao
    };
  }
  render() {
    return (
      <span className="card border shadow p-3 mb-5 bg-white rounded position=relative">
        <div>
          <p>
            <b>Concluída:</b>{" "}
            {this.state.concluded === '1' ? "Concluído" : "Por realizar"}
          </p>
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
            <b>Avaliacao:</b> {this.state.avaliacao}
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
