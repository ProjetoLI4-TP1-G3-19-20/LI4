import React from "react";
import { Component } from "react";
import {
  validateMePI,
  getNextVisitInterno,
  getUserName,
  getNumberOfRequests,
} from "../HTTPRequests";
import WelcomeComponent from "./WelcomeComponent";

class InternoMain extends Component {
  constructor(props) {
    super(props);
    this.state = {
      auth: false,
      visitas: [],
      user: "",
      customText: "",
      customText2: "",
    };

    const urlParams = new URLSearchParams(window.location.search);
    const u = urlParams.get("u");

    validateMePI(u).then((r) => {
      r.text().then((r) => {
        if (String(r) === "True") {
          getNextVisitInterno(u).then((r) => {
            r.json().then((r) => {
              if (r.visitante === "-1") {
                getNumberOfRequests(u).then((r2) => {
                  r2.text().then((r2) => {
                    this.setState({
                      customText:
                        "Neste momento não tem qualquer visita marcada",
                      auth: true,
                      user: u,
                      customText2:
                        "Neste momento tem " +
                        r2 +
                        " pedidos de visita para rever.",
                    });
                  });
                });
              } else {
                var date = new Date(parseInt(r.data));
                getUserName(r.visitante).then((r) => {
                  r.text().then((r) => {
                    getNumberOfRequests(u).then((r2) => {
                      r2.text().then((r2) => {
                        this.setState({
                          customText:
                            "A sua próxima visita está marcada com " +
                            r +
                            ", dia " +
                            ("0" + date.getDate()).slice(-2) +
                            "/" +
                            ("0" + date.getMonth()).slice(-2) +
                            ", pelas " +
                            ("0" + date.getHours()).slice(-2) +
                            ":" +
                            ("0" + date.getMinutes()).slice(-2),
                          auth: true,
                          user: u,
                          customText2:
                            "Neste momento tem " +
                            r2 +
                            " pedidos de visita para rever.",
                        });
                      });
                    });
                  });
                });
              }
            });
          });
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
          <br />
          <div>
            <WelcomeComponent
              text1={"A Sua Próxima Visita Marcada"}
              text2={this.state.customText}
            />
          </div>
          <br />
          <div>
            <WelcomeComponent
              text1={"Pedidos de Visita"}
              text2={this.state.customText2}
            />
          </div>
        </form>
      </div>
    );
  }
}

export default InternoMain;
