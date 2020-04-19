export async function login(email, password) {
  let url = new URL("http://localhost:8080");
  url.search = new URLSearchParams({
    t: "login",
    email: email,
    password: password,
  });

  return fetch(url, {
    method: "GET",
    headers: new Headers(),
  });
}

export async function createUser(state) {
  const url = "http://localhost:8080";

  return fetch(url, {
    method: "POST",
    body: JSON.stringify({
      t: "createUser",
      email: state.email,
      password: state.password,
      username: state.username,
      phone: state.phone,
      morada: state.morada,
      postCode: state.postCode,
    }),
    headers: new Headers(),
  });
}

export async function createAdmin(state) {
  const url = "http://localhost:8080";

  return fetch(url, {
    method: "POST",
    body: JSON.stringify({
      t: "createAdmin",
      email: state.email,
      password: state.password,
      username: state.username,
      phone: state.phone,
      instituicao: state.selectedIns.label,
    }),
    headers: new Headers(),
  });
}

export async function createColab(state) {
  const url = "http://localhost:8080";

  return fetch(url, {
    method: "POST",
    body: JSON.stringify({
      t: "createColab",
      email: state.email,
      password: state.password,
      username: state.username,
      phone: state.phone,
      morada: state.morada,
      postCode: state.postCode,
    }),
    headers: new Headers(),
  });
}

export async function getAllInsts() {
  let url = new URL("http://localhost:8080");
  url.search = new URLSearchParams({
    t: "insts",
  });

  return fetch(url, {
    method: "GET",
    headers: new Headers(),
  });
}

export async function getVisitas(id) {
  let url = new URL("http://localhost:8080");
  url.search = new URLSearchParams({
    t: "visitas",
    user: id,
  });

  return fetch(url, {
    method: "GET",
    headers: new Headers(),
  });
}
