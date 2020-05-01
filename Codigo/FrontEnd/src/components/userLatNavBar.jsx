import React from "react";
import { Component } from "react";

class UserLatNavBar extends Component {
  render() {
    const urlParams = new URLSearchParams(window.location.search);
    const u = urlParams.get("u");
    return (
      <div className="position-relative m-4">
        <form>
          <div className="form-group-auto m-2">
            <span style={{ fontSize: "30px" }}>Bem vindo!</span>
          </div>

          <div className="form-group-auto m-2">
            <a
              className="badge badge-primary"
              style={{ fontSize: "20px" }}
              href={"/userHistory?u=" + u}
            >
              {" "}
              Histórico de Visitas
            </a>
          </div>
          <div className="form-group-auto m-2">
            <a
              className="badge badge-primary"
              style={{ fontSize: "20px" }}
              href={"/visitReq?u=" + u}
            >
              {" "}
              Pedir uma nova visita
            </a>
          </div>
        </form>
      </div>
    );
  }
}

export default UserLatNavBar;
