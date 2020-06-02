import React from "react";
import { Component } from "react";
import { validateMe } from "../HTTPRequests";

class UserLatNavBar extends Component {
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
        }
      });
    });
  }

  render() {
    if (this.state.auth === false) {
      return <div>Acesso Negado</div>;
    }
    return (
      <div className="position-relative m-4">
        <form>
          <div className="form-group-auto m-2">
            <span style={{ fontSize: "30px" }}>Bem vindo!</span>
          </div>
          <div
            className="form-group-auto m-2"
            onClick={() => sessionStorage.removeItem("token")}
          >
            <a
              className="badge badge-primary"
              style={{ fontSize: "20px" }}
              href={"/"}
            >
              {" "}
              Log out
            </a>
          </div>
          <div className="form-group-auto m-2">
            <a
              className="badge badge-primary"
              style={{ fontSize: "20px" }}
              href={"/userHistory?u=" + this.state.user}
            >
              {" "}
              Histórico de Visitas
            </a>
          </div>
          <div className="form-group-auto m-2">
            <a
              className="badge badge-primary"
              style={{ fontSize: "20px" }}
              href={"/visitReq?u=" + this.state.user}
            >
              {" "}
              Pedir uma nova visita
            </a>
          </div>
          <div className="form-group-auto m-2">
            <a
              className="badge badge-primary"
              style={{ fontSize: "20px" }}
              href={"/userDataUpdate?u=" + this.state.user}
            >
              {" "}
              Alterar dados pessoais
            </a>
          </div>
        </form>
      </div>
    );
  }
}

export default UserLatNavBar;
