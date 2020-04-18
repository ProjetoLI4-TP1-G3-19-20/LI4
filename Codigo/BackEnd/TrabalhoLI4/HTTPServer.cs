using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Web.Script.Serialization;
using LI4;


class HTTPServer {

    private Thread serverThread;
    private string rootDirectory;
    private HttpListener listener;
    private int port;
    private VisitanteDAO visitanteDAO;
    


    public int Port {
        get { return port; }
        private set { }
    }

    /// <summary>
    /// Construct server with given port.
    /// </summary>
    /// <param name="path">Directory path to serve.</param>
    /// <param name="port">Port of the server.</param>
    public HTTPServer(string path, int port, String con) {
        this.Initialize(path, port, con);
    }

    public void Stop() {
        serverThread.Abort();
        listener.Stop();
    }

    private void Listen() {
        listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:" + port.ToString() + "/");
        listener.Start();

        while (true) {
            try {
                HttpListenerContext context = listener.GetContext();
                Process(context);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    private void Process(HttpListenerContext context) {

        if (context.Request.HttpMethod == "GET") {
            ProcessGetRequests(context);
        }else if(context.Request.HttpMethod == "POST") {
            ProcessPostRequests(context);
        }

    }


    void ProcessPostRequests(HttpListenerContext context) {

        var body = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding).ReadToEnd();
        var JSON = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(body);

        switch (JSON["t"]) {
            case "create":
                ProcessCreateVisitante(context, JSON);
                break;
        }

    }

    void ProcessGetRequests(HttpListenerContext context) {

        switch (context.Request.QueryString["t"]) {
            case "login":
                ProcessLogin(context);
            break;
        }
    }

    void ProcessCreateVisitante(HttpListenerContext context, Dictionary<String, String> JSON) {
        string reply;
        string email = JSON["email"];
        

        if (visitanteDAO.emailExiste(email)) {
            reply = "emailRepetido";
        }
        else {
            string password = JSON["password"];
            string username = JSON["username"];
            string phone = JSON["phone"];
            string postcode = JSON["postCode"];
            string morada = JSON["morada"];
            Visitante v = new Visitante();
            v.SetEmail(email);
            v.SetPassword(password);
            v.SetNome(username);
            v.SetTelefone(phone.Trim('+'));
            v.SetCod_postal(postcode);
            v.SetMorada(morada);

            if (visitanteDAO.Put(v)) {
                reply = "sucesso";
            }
            else {
                reply = "falha";
            }
        }

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = reply.Length;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, reply.Length);
        context.Response.OutputStream.Close();
    }

    void ProcessLogin(HttpListenerContext context) {

        string reply;

        string email = context.Request.QueryString["email"];
        string password = context.Request.QueryString["password"];


        Visitante v = visitanteDAO.Get(email);
        if(v.GetId_utilizador() < 0) {
            reply = "naoExiste";
        }
        else {

        }
        if (password.CompareTo(v.GetPassword()) == 0) {
            reply = "true";
        }
        else {
            reply = "false";
        }

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = reply.Length;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, reply.Length);
    }


    

    private void Initialize(string path, int port, String con) {
        this.rootDirectory = path;
        this.port = port;
        serverThread = new Thread(this.Listen);
        serverThread.Start();
        Console.WriteLine("Servidor Iniciado");

        visitanteDAO = new VisitanteDAO(con);
    }


}
