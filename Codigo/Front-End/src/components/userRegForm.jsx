import React, { Component } from "react";

const userRegForm = () => {
  return (
    <border>
      <form>
        <div class="form-group">
          <label for="exampleInputUsername">Nome de Utilizador</label>
          <input
            type="username"
            class="form-control"
            id="exampleInputUsername"
          />
        </div>
        <div class="form-group">
          <label for="exampleInputEmail1">E-mail</label>
          <input
            type="email"
            class="form-control"
            id="exampleInputEmail1"
            aria-describedby="emailHelp"
          />
          <small id="emailHelp" class="form-text text-muted">
            Nunca partilharemos o seu e-mail com ninguém.
          </small>
        </div>
        <div class="form-group">
          <label for="exampleInputPassword1">Password</label>
          <input
            type="password"
            class="form-control"
            id="exampleInputPassword1"
          />
        </div>
        <button type="Registar" class="btn btn-primary">
          Submit
        </button>
      </form>
    </border>
  );
};

export default userRegForm;
