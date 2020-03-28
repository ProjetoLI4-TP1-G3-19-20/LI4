import axios from "axios";

export async function login(email, password) {
  const url = "http://localhost:8080";

  console.log("Sending request");
  const response = await axios.get(url, {
    params: {
      t: "login",
      email: email,
      pass: password
    }
  });
  if (String(response.data) === "true") return true;
  else return false;
}
