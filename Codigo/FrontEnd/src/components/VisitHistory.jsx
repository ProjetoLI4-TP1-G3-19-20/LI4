import React, { Component } from "react";
import VisitComponent from "./VisitComponent";
import ReactList from "react-list";
import { getVisitas, validateMe } from "../HTTPRequests";

class VisitHistory extends Component {
  constructor(props) {
    super(props);
    this.state = {
      auth: false,
      visitas: [],
      user: -1,
    };

    const urlParams = new URLSearchParams(window.location.search);
    const u = urlParams.get("u");

    validateMe(u).then((r) => {
      r.text().then((r) => {
        console.log(r);
        if (String(r) === "True") {
          this.setState({ auth: true, user: u });
          this.updateVisitas();
        }
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
            concluded={this.state.visitas[index].estado}
          />
        }
      </div>
    );
  }

  updateVisitas() {
    getVisitas(this.state.user).then((r) => {
      r.text().then((r) => {
        console.log(r);
        var json = JSON.parse(r);
        this.setState({ visitas: json, auth: true });
      });
    });
  }

  render() {
    if (this.state.auth === true) {
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
            <div className="form-group-auto m-2">
              <ReactList
                itemRenderer={this.renderItem}
                length={this.state.visitas.length}
                type="uniform"
              />
            </div>
          </form>
        </div>
      );
    } else {
      return <div>Acesso Negado</div>;
    }
  }
}

export default VisitHistory;
