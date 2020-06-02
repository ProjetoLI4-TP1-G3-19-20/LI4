import React from "react";
import { Component } from "react";
import { validateMePI } from "../HTTPRequests";

class InternoMain extends Component {
  constructor(props) {
    super(props);
    this.state = {
      auth: false,
      visitas: [],
      user: "",
    };

    const urlParams = new URLSearchParams(window.location.search);
    const u = urlParams.get("u");

    validateMePI(u).then((r) => {
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
              href={"/createVaga?u=" + this.state.user}
            >
              {" "}
              Gerir Vagas
            </a>
          </div>
          <div className="form-group-auto m-2">
            <a
              className="badge badge-primary"
              style={{ fontSize: "20px" }}
              href={"/AcceptVisit?u=" + this.state.user}
            >
              {" "}
              Gerir visitas
            </a>
          </div>
        </form>
      </div>
    );
  }
}

export default InternoMain;
