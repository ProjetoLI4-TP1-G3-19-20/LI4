using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;



class HTTPServer
{
    
    private Thread serverThread;
    private string rootDirectory;
    private HttpListener listener;
    private int port;

    public int Port
    {
        get { return port; }
        private set { }
    }

    /// <summary>
    /// Construct server with given port.
    /// </summary>
    /// <param name="path">Directory path to serve.</param>
    /// <param name="port">Port of the server.</param>
    public HTTPServer(string path, int port)
    {
        this.Initialize(path, port);
    }

    /// <summary>
    /// Construct server with suitable port.
    /// </summary>
    /// <param name="path">Directory path to serve.</param>
    public HTTPServer(string path)
    {
        //get an empty port
        TcpListener l = new TcpListener(IPAddress.Loopback, 0);
        l.Start();
        int port = ((IPEndPoint)l.LocalEndpoint).Port;
        l.Stop();
        this.Initialize(path, port);
    }

    /// <summary>
    /// Stop server and dispose all functions.
    /// </summary>
    public void Stop()
    {
        serverThread.Abort();
        listener.Stop();
    }

    private void Listen()
    {
        listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:" + port.ToString() + "/");
        listener.Start();

        while (true)
        {
            try
            {
                HttpListenerContext context = listener.GetContext();
                Process(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    private void Process(HttpListenerContext context)
    {
        if (context.Request.QueryString["t"].CompareTo("login") == 0)
        {
            ProcessLogin(context);
        }
    }

    void ProcessLogin(HttpListenerContext context)
    {
        string email = context.Request.QueryString["email"];
        string password = context.Request.QueryString["password"];

        Console.WriteLine(email + " - " + password);

        string response;
        if(email.CompareTo("pedro") == 0 && password.CompareTo("1234") == 0)
        {
            response = "true";
        }
        else
        {
            response = "false";
        }

        context.Response.ContentType = "text/simple";
        context.Response.ContentLength64 = response.Length;
        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
        context.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(response), 0, response.Length);
    }

    private void Initialize(string path, int port)
    {
        this.rootDirectory = path;
        this.port = port;
        serverThread = new Thread(this.Listen);
        serverThread.Start();
    }


}