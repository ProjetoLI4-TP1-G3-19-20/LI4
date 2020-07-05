import React from "react";
import { Component } from "react";
import { validateMe, getNextVisit } from "../HTTPRequests";
import WelcomeComponent from "./WelcomeComponent";
import "./helper.css";

class UserMain extends Component {
  constructor(props) {
    super(props);
    this.state = {
      auth: false,
      visitas: [],
      user: -1,
      customText: "olá",
    };

    const urlParams = new URLSearchParams(window.location.search);
    const u = urlParams.get("u");

    validateMe(u).then((r) => {
      r.text().then((r) => {
        if (String(r) === "True") {
          getNextVisit(u).then((r) => {
            r.json().then((r) => {
              if (r.visitado === "-1") {
                this.setState({
                  customText: "Neste momento, não tem qualquer visita marcada",
                  auth: true,
                  user: u,
                });
              } else {
                var date = new Date(parseInt(r.data));
                console.log(date);
                this.setState({
                  customText:
                    "A sua próxima visita está marcada com " +
                    r.visitado +
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
    } else {
      console.log(this.state);
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
          </form>
        </div>
      );
    }
  }
}

export default UserMain;
