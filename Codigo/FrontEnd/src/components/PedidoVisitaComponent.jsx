import React, { Component } from "react";

class PedidoVisitaComponent extends Component {
  constructor(props) {
    super(props);
    this.state = {
      begin_date: this.props.data_inicio,
      end_date: this.props.data_saida,
      inst: this.props.id_inst,
      dep: this.props.departamentosID,
      comments: this.props.comentario,
      visitante: this.props.visitante,
    };
  }
  render() {
    var bd = new Date(this.state.begin_date);
    var ed = new Date(this.state.end_date);
    return (
      <span className="card border shadow p-3 mb-5 bg-white rounded position=relative">
        <div>
          <p>
            <b>Visitante:</b> {this.state.visitante}
          </p>
          <p>
            <b>Hora Entrada:</b>{" "}
            {bd.toDateString() +
              ", " +
              ("0" + bd.getHours()).slice(-2) +
              "h" +
              ("0" + bd.getMinutes()).slice(-2) +
              "m"}
          </p>
          <p>
            <b>Hora Saída:</b>{" "}
            {ed.toDateString() +
              ", " +
              ("0" + ed.getHours()).slice(-2) +
              "h" +
              ("0" + ed.getMinutes()).slice(-2) +
              "m"}
          </p>
          <p>
            <b>Comentários:</b> {this.state.comments}
          </p>
        </div>
      </span>
    );
  }
}

export default PedidoVisitaComponent;
