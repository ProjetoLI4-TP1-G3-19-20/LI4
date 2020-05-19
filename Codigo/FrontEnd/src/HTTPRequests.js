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

export async function loginAdmin(email, password) {
  let url = new URL("http://localhost:8080");
  url.search = new URLSearchParams({
    t: "loginAdmin",
    email: email,
    password: password,
  });

  return fetch(url, {
    method: "GET",
    headers: new Headers(),
  });
}

export async function loginInterno(email, password) {
  let url = new URL("http://localhost:8080");
  url.search = new URLSearchParams({
    t: "loginInterno",
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

export async function createPedido(state, u) {
  const url = "http://localhost:8080";

  return fetch(url, {
    method: "POST",
    body: JSON.stringify({
      t: "createPedido",
      hora_inicio: state.selectedEvent.start.toJSON(),
      hora_fim: state.selectedEvent.end.toJSON(),
      comentario: state.comentario,
      visitado: state.selectedPdi.label,
      dep: state.selectedDep.label,
      isnt: state.selectedInst.label,
      visitante: u,
    }),
    headers: new Headers(),
  });
}

export async function createInst(state) {
  const url = "http://localhost:8080";

  return fetch(url, {
    method: "POST",
    body: JSON.stringify({
      t: "createInst",
      nome: state.nome,
      email: state.email,
      localizacao: state.localizacao,
    }),
    headers: new Headers(),
  });
}

export async function createDep(state) {
  const url = "http://localhost:8080";

  return fetch(url, {
    method: "POST",
    body: JSON.stringify({
      t: "createDep",
      inst: state.selectedInst.label,
      dep: state.dep,
    }),
    headers: new Headers(),
  });
}

export async function createVaga(start, end, user) {
  const url = "http://localhost:8080";

  return fetch(url, {
    method: "POST",
    body: JSON.stringify({
      t: "createVaga",
      start: start,
      end: end,
      user: user,
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

export async function getDepartamentosByInst(inst) {
  let url = new URL("http://localhost:8080");
  url.search = new URLSearchParams({
    t: "depsByInst",
    inst: inst,
  });

  return fetch(url, {
    method: "GET",
    headers: new Headers(),
  });
}

export async function getPessoasByDepartamento(inst, dep) {
  let url = new URL("http://localhost:8080");
  url.search = new URLSearchParams({
    t: "pesByDep",
    inst: inst,
    dep: dep,
  });

  return fetch(url, {
    method: "GET",
    headers: new Headers(),
  });
}

export async function getVagas(name) {
  let url = new URL("http://localhost:8080");
  url.search = new URLSearchParams({
    t: "vagas",
    name: name,
  });

  return fetch(url, {
    method: "GET",
    headers: new Headers(),
  });
}

export async function getPedidos(name) {
  let url = new URL("http://localhost:8080");
  url.search = new URLSearchParams({
    t: "pedidos",
    name: name,
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

export async function validateMe(id) {
  let url = new URL("http://localhost:8080");
  url.search = new URLSearchParams({
    t: "validate",
    user: id,
    token: sessionStorage.getItem("token"),
  });

  return fetch(url, {
    method: "GET",
    headers: new Headers(),
  });
}

export async function validateMeAdmin(id) {
  let url = new URL("http://localhost:8080");
  url.search = new URLSearchParams({
    t: "validateAdmin",
    user: id,
    token: sessionStorage.getItem("token"),
  });

  return fetch(url, {
    method: "GET",
    headers: new Headers(),
  });
}

export async function validateMePI(name) {
  let url = new URL("http://localhost:8080");
  url.search = new URLSearchParams({
    t: "validateName",
    user: name,
    token: sessionStorage.getItem("token"),
  });

  return fetch(url, {
    method: "GET",
    headers: new Headers(),
  });
}

export async function sendSMS(message, phone) {
  let url = new URL("https://api.ez4uteam.com/ez4usms/API/sendSMS.php");
  url.search = new URLSearchParams({
    account: "a83870",
    licensekey: "94440b861bc9af37853ce91c",
    phoneNumber: phone,
    messageText: message,
    alfaSender: "UMVisitas",
  });

  return fetch(url, {
    method: "GET",
    mode: "no-cors",
    headers: new Headers(),
  });
}

export async function getUserName(id) {
  let url = new URL("http://localhost:8080");
  url.search = new URLSearchParams({
    t: "userName",
    id: id,
  });

  return fetch(url, {
    method: "GET",
    headers: new Headers(),
  });
}

export async function aceitePedido(accepted, idVisita) {
  let url = new URL("http://localhost:8080");
  url.search = new URLSearchParams({
    t: "aceitePedido",
    accepted: accepted,
    id: idVisita,
  });

  return fetch(url, {
    method: "GET",
    headers: new Headers(),
  });
}

export async function getVisitasMarcadas(visitado) {
  let url = new URL("http://localhost:8080");
  url.search = new URLSearchParams({
    t: "visitasMarcadas",
    visitado: visitado,
  });

  return fetch(url, {
    method: "GET",
    headers: new Headers(),
  });
}
