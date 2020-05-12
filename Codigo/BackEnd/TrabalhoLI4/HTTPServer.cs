using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Web.Script.Serialization;
using LI4;

class HTTPServer {

    private Thread serverThread;
    private string rootDirectory;
    private HttpListener listener;
    private int port;
    private VisitanteDAO visitanteDAO;
    private AdministradorDAO administradorDAO;
    private InstituicaoDAO instituicaoDAO;
    private VisitasDAO visitasDAO;
    private DepartamentoDAO departamentoDAO;
    private PedidoVisitaDAO pedidoVisitaDAO;
    private JWT jwt;



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
            case "createUser":
                ProcessCreateVisitante(context, JSON);
                break;
            case "createAdmin":
                ProcessCreateAdmin(context, JSON);
                break;
            case "createPedido":
                ProcessCreatePedido(context, JSON);
                break;
            case "createInst":
                ProcessCreateInst(context, JSON);
                break;
            case "createVaga":
                ProcessCreateVaga(context, JSON);
                break;

        }
       

    }


    void ProcessGetRequests(HttpListenerContext context) {

        switch (context.Request.QueryString["t"]) {
            case "validate":
                ProcessValidate(context);
                break;
            case "validateName":
                ProcessValidateName(context);
                break;
            case "login":
                ProcessLogin(context);
                break;
            case "insts":
                processGetInsts(context);
                break;
            case "visitas":
                processGetVisitas(context);
                break;
            case "depsByInst":
                processGetDepsByInst(context);
                break;
            case "pesByDep":
                processGetPesByDep(context);
                break;
            case "vagas":
                processGetVagas(context);
                break;
            case "pedidos":
                processGetPedidos(context);
                break;
            case "userName":
                processGetUserName(context);
                break;
            case "aceitePedido":
                processAceitePedido(context);
                break;
            case "visitasMarcadas":
                processGetVisitasMarcadas(context);
                break;
        }
    }


    void ProcessCreateVaga(HttpListenerContext context, Dictionary<String, String> JSON) {

        string start = JSON["start"];
        string end = JSON["end"];
        string user = JSON["user"];

        visitasDAO.putVaga(DateTime.Parse(start).ToUniversalTime(), DateTime.Parse(end).ToUniversalTime(), user);

        string reply = "ok";
        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);
        context.Response.OutputStream.Close();



    }


    void ProcessCreatePedido(HttpListenerContext context, Dictionary<String, String> JSON) {
        PedidoVisita pv = new PedidoVisita();

        string reply = "sucesso";

        pv.setHoraInicio(DateTime.Parse(JSON["hora_inicio"]).ToUniversalTime());
        pv.setHoraFim(DateTime.Parse(JSON["hora_fim"]).ToUniversalTime());
        pv.setComentario(JSON["comentario"]);
        pv.setVisitado(JSON["visitado"]);
        pv.setInstituicao(instituicaoDAO.getIdByName(JSON["isnt"]));
        pv.setDepartamento(departamentoDAO.getIdByName(JSON["dep"], instituicaoDAO.getIdByName(JSON["isnt"])));
        pv.setVisitante(int.Parse(JSON["visitante"]));

        try {
            pedidoVisitaDAO.Put(pv);
            visitasDAO.deleteVaga(JSON["visitado"], DateTime.Parse(JSON["hora_inicio"]).ToUniversalTime());
        }
        catch (Exception e) {
            Console.WriteLine(e.ToString());
            reply = "erro";
        }

        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);
        context.Response.OutputStream.Close();



    }

    void ProcessCreateInst(HttpListenerContext context, Dictionary<String, String> JSON) {

        string reply = "sucesso";

        bool x = instituicaoDAO.Put(new Instituicao(-1, JSON["nome"], JSON["email"], new ArrayList(), JSON["localizacao"]));

        if (!x) {
            reply = "erro";
        }
        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);
        context.Response.OutputStream.Close();



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

        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);
        context.Response.OutputStream.Close();
    }


    void ProcessCreateAdmin(HttpListenerContext context, Dictionary<String, String> JSON) {
        string reply;
        string email = JSON["email"];


        if (administradorDAO.emailExiste(email)) {
            reply = "emailRepetido";
        }
        else {
            string password = JSON["password"];
            string username = JSON["username"];
            string phone = JSON["phone"];
            int ad = instituicaoDAO.getIdByName(JSON["instituicao"]);
            Administrador a = new Administrador();
            a.SetEmail(email);
            a.SetPassword(password);
            a.SetNome(username);
            a.SetTelefone(phone.Trim('+'));

            if (administradorDAO.Put(a, ad, 1)) {
                reply = "sucesso";
            }
            else {
                reply = "falha";
            }
        }

        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);
        context.Response.OutputStream.Close();
    }


    void ProcessValidate(HttpListenerContext context) {
        string user = context.Request.QueryString["user"];
        string token = context.Request.QueryString["token"];

        bool reply = jwt.validateToken(token, int.Parse(user));

        int size = System.Text.Encoding.UTF8.GetBytes(reply.ToString()).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply.ToString()), 0, size);
    }

    void ProcessValidateName(HttpListenerContext context) {
        string user = context.Request.QueryString["user"];
        string token = context.Request.QueryString["token"];

        bool reply = jwt.validateToken(token, int.Parse(user));

        int size = System.Text.Encoding.UTF8.GetBytes(reply.ToString()).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply.ToString()), 0, size);
    }


    void processAceitePedido(HttpListenerContext context) {
        string aceite = context.Request.QueryString["accepted"];
        string id = context.Request.QueryString["id"];

        PedidoVisita pv = pedidoVisitaDAO.Get(int.Parse(id));

        if (aceite.CompareTo("true") == 0) {
            Visita v = new Visita(pv.getComentario(), 0, pv.getHoraInicio(), pv.getHoraFim(), pv.getVisitante(), pv.getVisitado(), "", pv.getInstituicao(), pv.getDepartamento());
            visitasDAO.Put(v);
        }

        pedidoVisitaDAO.deletePedido(pv.getIdVisita());

        string reply = "ok";

        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);
    }

    void processGetUserName(HttpListenerContext context) {
        string id = context.Request.QueryString["id"];

        string reply = visitanteDAO.getName(int.Parse(id));

        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);
    }

    void processGetVagas(HttpListenerContext context) {
        string name = context.Request.QueryString["name"];

        List<Vaga> lista = visitasDAO.getVagas(name);
        string reply = "[";
        
        for(int i = 0; i < lista.Count; i++) {
            reply += lista[i].getJson() + ",";
        }

        char[] chars = { ',' };
        reply = reply.Trim(chars);

        reply += "]";

        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);
    }

    void processGetPedidos(HttpListenerContext context) {
        string name = context.Request.QueryString["name"];

        List<PedidoVisita> lista = pedidoVisitaDAO.getAllPedidos(name);
        string reply = "[";

        for (int i = 0; i < lista.Count; i++) {
            reply += lista[i].getJson() + ",";
        }

        char[] chars = { ',' };
        reply = reply.Trim(chars);

        reply += "]";

        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);
    }

    void processGetPesByDep(HttpListenerContext context) {
        string inst = context.Request.QueryString["inst"];
        string dep = context.Request.QueryString["dep"];

        List<String> lista = departamentoDAO.getPessoasByDepartamento(inst, dep);

        String reply = new JavaScriptSerializer().Serialize(lista);

        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);

    }

    void processGetDepsByInst(HttpListenerContext context) {
        string inst = context.Request.QueryString["inst"];

        List<String> lista = departamentoDAO.DepByInst(inst);

        String reply = new JavaScriptSerializer().Serialize(lista);

        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);
    }


    void processGetVisitas(HttpListenerContext context) {
        string reply = "";

        string user = context.Request.QueryString["user"];

        List<Visita> lista = visitasDAO.GetAllByUser(int.Parse(user));

        string nameInst, nameDep;

        reply += "[";

        for(int i = 0; i < lista.Count; i++) {
            nameInst = instituicaoDAO.getNamebyId(lista[i].GetId_inst());
            nameDep = departamentoDAO.getNameById(lista[i].GetId_inst(), lista[i].GetDepartamentoID());
            reply += lista[i].getJson(nameInst, nameDep);
            reply += ",";
        }

        char[] chars = { ',' };
        reply = reply.Trim(chars);

        reply += "]";

        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);

    }


    void processGetVisitasMarcadas(HttpListenerContext context) {
        string reply = "";

        string visitado = context.Request.QueryString["visitado"];

        List<Visita> lista = visitasDAO.getFutureVisits(visitado);

        string nameInst, nameDep;

        reply += "[";

        for (int i = 0; i < lista.Count; i++) {
            nameInst = instituicaoDAO.getNamebyId(lista[i].GetId_inst());
            nameDep = departamentoDAO.getNameById(lista[i].GetId_inst(), lista[i].GetDepartamentoID());
            reply += lista[i].getJsonTimeStamp(nameInst, nameDep);
            reply += ",";
        }

        char[] chars = { ',' };
        reply = reply.Trim(chars);

        reply += "]";

        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);

    }

    void ProcessLogin(HttpListenerContext context) {

        string reply;

        string email = context.Request.QueryString["email"];
        string password = context.Request.QueryString["password"];


        Visitante v = visitanteDAO.Get(email);
        if(v.GetId_utilizador() < 0) {
            reply = "naoExiste";
        }
        if (password.CompareTo(v.GetPassword()) == 0) {
            string token = jwt.generateToken(v.GetId_utilizador());
            reply = "";
            reply += "{\"id\": \"" + v.GetId_utilizador() + "\", \"token\":\"" + token + "\"}";
        }
        else {
  
            reply = "false";
        }

        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);
    }

    private void processGetInsts(HttpListenerContext context) {
        List<String> lista = instituicaoDAO.getAllNames();

        String reply = new JavaScriptSerializer().Serialize(lista);

        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);

    }




    private void Initialize(string path, int port, String con) {
        this.rootDirectory = path;
        this.port = port;
        serverThread = new Thread(this.Listen);
        serverThread.Start();
        Console.WriteLine("Servidor Iniciado");

        visitanteDAO = new VisitanteDAO(con);
        administradorDAO = new AdministradorDAO(con);
        instituicaoDAO = new InstituicaoDAO(con);
        visitasDAO = new VisitasDAO(con);
        departamentoDAO = new DepartamentoDAO(con);
        pedidoVisitaDAO = new PedidoVisitaDAO(con);
        jwt = new JWT();

    }


}
