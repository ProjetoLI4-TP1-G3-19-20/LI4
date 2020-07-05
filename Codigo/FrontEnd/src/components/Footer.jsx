import React from "react";
import { Component } from "react";
import "./helper.css";
import styled from "styled-components";

class Footer extends Component {
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
      <FooterContainer className="main-footer">
        <div className="footer-middle">
          <div className="container">
            <div className="row">
              <div className="col-md-3 col-sm-6">
                <h4>EzVisits</h4>
                <ul className="list-unstyled">
                  <li>
                    <a href={"infoI"}> Quem somos</a>
                  </li>
                </ul>
              </div>
              <div className="col-md-3 col-sm-6">
                <h4>Contactos</h4>
                <ul className="list-unstyled">
                  <li>ezvisits@esteemailnaoexiste.pt</li>
                  <li>253999999</li>
                </ul>
              </div>
            </div>
            <div className="footer-bottom">
              <p className="text-xs-center">
                &copy;{new Date().getFullYear()} GrupoLI4
              </p>
            </div>
          </div>
        </div>
      </FooterContainer>
    );
  }
}

export default Footer;

const FooterContainer = styled.footer`
  position: relative;
  width: 100%;

  .footer-middle {
    background: var(--mainDark);
    color: var(--mainWhite);
    padding-top: 2rem;
  }

  .footer-bottom {
    padding-bottom: 0.5rem;
  }

  ul li a {
    color: var(--mainGrey);
  }

  ul li a:hover {
    color: var(--mainLightGrey);
  }
`;
