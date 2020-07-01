using System;
using System.Collections;
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
    private AdministradorDAO administradorDAO;
    private InstituicaoDAO instituicaoDAO;
    private VisitasDAO visitasDAO;
    private DepartamentoDAO departamentoDAO;
    private PedidoVisitaDAO pedidoVisitaDAO;
    private PessoaDeInteresseDAO pessoaDeInteresseDAO;
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
        listener.Prefixes.Add("http://+:" + port.ToString() + "/");

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

        Console.WriteLine("Request");

        if (context.Request.HttpMethod == "GET") {
            ProcessGetRequests(context);
        }
        else if (context.Request.HttpMethod == "POST") {
            ProcessPostRequests(context);
        }
        else if (context.Request.HttpMethod == "PUT") {
            ProcessPutRequests(context);
        }
        else if (context.Request.HttpMethod == "OPTIONS") {
            ProcessAllow(context);
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
            case "createColab":
                ProcessCreateColab(context, JSON);
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
            case "createDep":
                ProcessCreateDep(context, JSON);
                break;

        }


    }

    void ProcessPutRequests(HttpListenerContext context) {

        var body = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding).ReadToEnd();
        var JSON = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(body);


        switch (JSON["t"]) {
            case "updateUser":
                ProcessUpdateVisitante(context, JSON);
                break;
            case "updateInterno":
                ProcessUpdateInterno(context, JSON);
                break;
            case "finishVisita":
                ProcessFinishVisita(context, JSON);
                break;
            case "initVisita":
                ProcessInitVisita(context, JSON);
                break;
        }


    }


    void ProcessGetRequests(HttpListenerContext context) {

        Console.WriteLine(context.Request.QueryString["t"]);

        switch (context.Request.QueryString["t"]) {
            case "validate":
                ProcessValidate(context);
                break;
            case "validateAdmin":
                ProcessValidateAdmin(context);
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
            case "userInfo":
                processUserInfo(context);
                break;
            case "internoInfo":
                processInternoInfo(context);
                break;
            case "deleteAllVagas":
                ProcessDeleteAllVagas(context);
                break;
            default:
                processDefault(context);
                break;
        }
    }

    void ProcessAllow(HttpListenerContext context) {

        string reply = "ok";
        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS, PUT");
        context.Response.AddHeader("Access-Control-Allow-Headers", "X-Requested-With, Accept, Access-Control-Allow-Origin, Content-Type");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply.ToString()), 0, size);
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

    void ProcessDeleteAllVagas(HttpListenerContext context) {

        string user = context.Request.QueryString["user"];

        visitasDAO.deleteAllVagas(user);

        string reply = "ok";
        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);
        context.Response.OutputStream.Close();

    }


    void ProcessFinishVisita(HttpListenerContext context, Dictionary<String, String> JSON) {


        visitasDAO.finishVisita(JSON["user"], DateTime.Parse(JSON["date"]).ToUniversalTime(), JSON["aval"]);
        Console.WriteLine(JSON["user"]);
        Console.WriteLine(DateTime.Parse(JSON["date"]));
        Console.WriteLine(JSON["aval"]);

        string reply = "ok";
        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);
        context.Response.OutputStream.Close();

    }

    void ProcessInitVisita(HttpListenerContext context, Dictionary<String, String> JSON) {


        visitasDAO.initVisita(JSON["user"], DateTime.Parse(JSON["date"]).ToUniversalTime());


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

    void ProcessCreateDep(HttpListenerContext context, Dictionary<String, String> JSON) {

        string reply = "sucesso";

        int inst = instituicaoDAO.getIdByName(JSON["inst"]);
        bool x = false;

        if (!departamentoDAO.existeNome("", inst)) {
            x = departamentoDAO.Put(new Departamento(JSON["dep"], -1), inst);
        }


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
            string password = SecurePasswordHasher.Hash(JSON["password"]);
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

    void ProcessCreateColab(HttpListenerContext context, Dictionary<String, String> JSON) {
        string reply = "";
        string email = JSON["email"];
        string password = SecurePasswordHasher.Hash(JSON["password"]);
        string name = JSON["username"];
        string phone = JSON["phone"];
        string inst = JSON["inst"];
        string dep = JSON["dep"];

        if (pessoaDeInteresseDAO.emailExiste(email)) {
            reply = "emailRepetido";
        }
        else {
            PessoaDeInteresse pdi = new PessoaDeInteresse(name, email, password, phone, instituicaoDAO.getIdByName(inst), departamentoDAO.getIdByName(dep, instituicaoDAO.getIdByName(inst)));

            if (pessoaDeInteresseDAO.Put(pdi)) {
                reply = "ok";
            }
            else {
                reply = "erro";
            }

        }

        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);
        context.Response.OutputStream.Close();
    }


    void ProcessUpdateVisitante(HttpListenerContext context, Dictionary<String, String> JSON) {
        string reply;
        string email = JSON["email"];

        string password;
        if(JSON["password"].CompareTo("") != 0) {
            password = SecurePasswordHasher.Hash(JSON["password"]);
        }
        else {
            password = "";
        }
        
        string username = JSON["username"];
        string phone = JSON["phone"];
        string postcode = JSON["postCode"];
        string morada = JSON["morada"];
        int id = int.Parse(JSON["id"]);

        Visitante v = new Visitante();
        v.SetEmail(email);
        v.SetPassword(password);
        v.SetNome(username);
        v.SetTelefone(phone.Trim('+'));
        v.SetCod_postal(postcode);
        v.SetMorada(morada);
        v.SetId_utilizador(id);

        if (visitanteDAO.Update(v)) {
            reply = "sucesso";
        }
        else {
            reply = "falha";
        }


        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);
        context.Response.OutputStream.Close();
    }

    void ProcessUpdateInterno(HttpListenerContext context, Dictionary<String, String> JSON) {
        string reply;
        string email = JSON["email"];

        string password;
        if (JSON["password"].CompareTo("") != 0) {
            password = SecurePasswordHasher.Hash(JSON["password"]);
        }
        else {
            password = "";
        }

        string ogname = JSON["ogname"];
        string username = JSON["username"];
        string phone = JSON["phone"];
        int inst = instituicaoDAO.getIdByName(JSON["inst"]);
        int dep = departamentoDAO.getIdByName(JSON["dep"], inst);
        Console.WriteLine("inst:" + inst + "\ndep:" + dep);


        PessoaDeInteresse pdi = new PessoaDeInteresse();
        pdi.setEmail(email);
        pdi.setPassword(password);
        pdi.setNome(username);
        pdi.setPhone(phone.Trim('+'));
        pdi.setDep(dep);
        pdi.setInst(inst);


        if (pessoaDeInteresseDAO.Update(pdi, ogname)) {
            reply = "sucesso";
        }
        else {
            reply = "falha";
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
            string password = SecurePasswordHasher.Hash(JSON["password"]);
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

        bool reply = jwt.validateToken(token, int.Parse(user), false);

        int size = System.Text.Encoding.UTF8.GetBytes(reply.ToString()).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply.ToString()), 0, size);
    }
    void processDefault(HttpListenerContext context) {

        string reply = "unrecognized request";

        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply.ToString()), 0, size);
    }

    void ProcessValidateAdmin(HttpListenerContext context) {
        string user = context.Request.QueryString["user"];
        string token = context.Request.QueryString["token"];

        bool reply = jwt.validateToken(token, int.Parse(user), true);

        int size = System.Text.Encoding.UTF8.GetBytes(reply.ToString()).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply.ToString()), 0, size);
    }

    void ProcessValidateName(HttpListenerContext context) {
        string user = context.Request.QueryString["user"];
        string token = context.Request.QueryString["token"];

        bool reply = jwt.validateToken(token, user);

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

    void processUserInfo(HttpListenerContext context) {
        string user = context.Request.QueryString["user"];
        int id = int.Parse(user);

        Visitante v = visitanteDAO.Get(id);

        string reply = v.getJson();

        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = size;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(reply), 0, size);
    }

    void processInternoInfo(HttpListenerContext context) {
        string user = context.Request.QueryString["user"];

        PessoaDeInteresse pdi = pessoaDeInteresseDAO.Get(user);

        string reply = pdi.getJson(instituicaoDAO.getNamebyId(pdi.getInst()), departamentoDAO.getNameById(pdi.getInst(), pdi.getDep()));

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

        Console.WriteLine(int.Parse(user));

        string nameInst, nameDep;

        reply += "[";

        for (int i = 0; i < lista.Count; i++) {
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
        bool found = false;


        Visitante v = visitanteDAO.Get(email);
        if (v.GetId_utilizador() < 0) {
            reply = "naoExiste";
        }
        else{
            if (SecurePasswordHasher.Verify(password, v.GetPassword())) {
                found = true;
                string token = jwt.generateToken(v.GetId_utilizador(), false);
                reply = "";
                reply += "{\"t\": \"v\", \"id\": \"" + v.GetId_utilizador() + "\", \"token\":\"" + token + "\"}";
            }
            else {
                reply = "false";
            }
        }

        if (!found) {
            Administrador a = administradorDAO.Get(email);
            if (a.GetId_utilizador() < 0) {
                reply = "naoExiste";
            }
            else {
                if (SecurePasswordHasher.Verify(password, a.GetPassword())) {
                    found = true;
                    string token = jwt.generateToken(a.GetId_utilizador(), true);
                    reply = "";
                    reply += "{\"t\": \"a\", \"id\": \"" + a.GetId_utilizador() + "\", \"token\":\"" + token + "\"}";
                }
                else {
                    reply = "false";
                }
            }
            
        }

        if (!found) {
            PessoaDeInteresse p = pessoaDeInteresseDAO.GetByEmail(email);
            if (p.getEmail().CompareTo("") == 0) {
                reply = "naoExiste";
            }
            else {
                if (SecurePasswordHasher.Verify(password, p.getPassword())) {
                    string token = jwt.generateToken(p.getNome());
                    reply = "";
                    reply += "{\"t\": \"i\", \"nome\": \"" + p.getNome() + "\", \"token\":\"" + token + "\"}";
                }
                else {
                    reply = "false";
                }
            }
            
        }


        int size = System.Text.Encoding.UTF8.GetBytes(reply).Length;

        context.Response.ContentType = "text/simple";
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.ContentLength64 = size;
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
        pessoaDeInteresseDAO = new PessoaDeInteresseDAO(con);
        jwt = new JWT();

    }


}
