import React from "react";
import { Component } from "react";
import { validateMeAdmin } from "../HTTPRequests";

class AdminMain extends Component {
  constructor(props) {
    super(props);
    this.state = {
      auth: false,
      visitas: [],
      user: -1,
    };

    const urlParams = new URLSearchParams(window.location.search);
    const u = urlParams.get("u");

    validateMeAdmin(u).then((r) => {
      r.text().then((r) => {
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
              href={"/newInst?u=" + this.state.user}
            >
              {" "}
              Registar nova instituição
            </a>
          </div>
          <div className="form-group-auto m-2">
            <a
              className="badge badge-primary"
              style={{ fontSize: "20px" }}
              href={"/newDep?u=" + this.state.user}
            >
              {" "}
              Registar novo departamento
            </a>
          </div>
        </form>
      </div>
    );
  }
}

export default AdminMain;
