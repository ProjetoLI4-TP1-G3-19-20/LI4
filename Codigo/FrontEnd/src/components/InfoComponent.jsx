import React from "react";
import { Component } from "react";
import "./helper.css";

class InfoComponente extends Component {
  constructor(props) {
    super(props);

    const urlParams = new URLSearchParams(window.location.search);
    const u = urlParams.get("u");

    this.state = {
      user: u,
    };
  }

  render() {
    return (
      <div>
        <br />
        <h4>Quem somos</h4>
        <text>
          Um grupo criado pela chance do destino, fomos unidos pelo projeto de
          uma cadeira do curso de Engenharia Informática.
          <br />O grupo final é o resultado da fusão de dois grupos que já
          haviam trabalhado separadamente antes, juntando as capacidades dos
          dois grupos, criando uma conbinação imparável.
          <br />
          Não há espaço para quaisquer interesses secundários. O nosso objetivo
          é único e simples. Passar à cadeira de LI4.
        </text>
      </div>
    );
  }
}

export default InfoComponente;
