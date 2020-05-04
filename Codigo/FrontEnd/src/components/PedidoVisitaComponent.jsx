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
    return (
      <span className="card border shadow p-3 mb-5 bg-white rounded position=relative">
        <div>
          <p>
            <b>Visitante:</b> {this.state.visitante}
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

export default PedidoVisitaComponent;
