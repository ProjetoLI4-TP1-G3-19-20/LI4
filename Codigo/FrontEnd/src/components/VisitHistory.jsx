import React, { Component } from "react";
import VisitComponent from "./VisitComponent";
import ReactList from "react-list";
import { getVisitas } from "../HTTPRequests";

class VisitHistory extends Component {
  constructor(props) {
    super(props);
    this.state = {
      visitas: [],
    };

    const urlParams = new URLSearchParams(window.location.search);
    const u = urlParams.get("u");

    getVisitas(u).then((r) => {
      r.text().then((r) => {
        var json = JSON.parse(r);
        this.setState({ visitas: json });
      });
    });

    this.renderItem = this.renderItem.bind(this);
  }

  renderItem(index, key) {
    return (
      <div key={key}>
        {
          <VisitComponent
            data_saida={this.state.visitas[index].data_saida}
            data_inicio={this.state.visitas[index].data_inicio}
            visitado={this.state.visitas[index].visitado}
            avaliacao={this.state.visitas[index].avaliacao}
            comentario={this.state.visitas[index].comentario}
            id_inst={this.state.visitas[index].id_inst}
            departamentosID={this.state.visitas[index].departamentosID}
          />
        }
      </div>
    );
  }

  render() {
    return (
      <ReactList
        itemRenderer={this.renderItem}
        length={this.state.visitas.length}
        type="uniform"
      />
    );
  }
}

export default VisitHistory;
