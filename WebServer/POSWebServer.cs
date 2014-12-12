using System;
using System.Net;
using System.Threading;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;


using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebServer
{
    class POSWebServer
    {
        private readonly HttpListener _listener = new HttpListener();
        private readonly Func<HttpListenerRequest, string> _responderMethod;
        private string mIp;
        private int mPort;
        private string mUrl;
        private ProcessReport mProcessReport;
        public POSWebServer(string ip, int port)
        {
            mProcessReport = new ProcessReport();
            mIp = ip;
            mPort = port;
            if (!HttpListener.IsSupported)
                throw new NotSupportedException(
                    "Needs Windows XP SP2, Server 2003 or later.");

            // URI prefixes are required, for example 
            // "http://localhost:8080/index/".            

            // A responder method is required
            Func<HttpListenerRequest, string> method = SendResponse;
            if (method == null)
                throw new ArgumentException("method");

            mUrl = String.Format("http://{0}:{1}", mIp, mPort);
            string url = String.Format("{0}/", mUrl);
            _listener.Prefixes.Add(url);
            _responderMethod = method;
            _listener.Start();
        }

        public void Run()
        {
            //int count = 0;
            ThreadPool.QueueUserWorkItem((o) =>
            {
                Console.WriteLine("Webserver running...");
                try
                {
                    while (_listener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem((c) =>
                        {
                            var ctx = c as HttpListenerContext;
                            Console.WriteLine(ctx.Request.RawUrl);
                            try
                            {
                                string rstr = _responderMethod(ctx.Request);
                                byte[] buf = Encoding.UTF8.GetBytes(rstr);                                
                                switch (ctx.Request.RawUrl)
                                {
                                    case "/home/test.abc":
                                        string s = "{\"khoa\":" +
                                                        "khoa"+
                                                    "\"}";
                                        buf = Encoding.UTF8.GetBytes(s);
                                        break;
                                    case "/readreporticon":
                                        string path = System.Windows.Forms.Application.StartupPath + "\\Web\\img\\Product sales report.png";
                                        byte[] img = Utilities.ImageHandler.GetByteFromUrl(path);                                        
                                        buf = Encoding.UTF8.GetBytes(Convert.ToBase64String(img));
                                        break;
                                    case "/readinfo":
                                        buf = Encoding.UTF8.GetBytes(mProcessReport.LoadInformation());                                        
                                        break;
                                    case "/readreport":
                                        buf = Encoding.UTF8.GetBytes(mProcessReport.LoadReportNow());
                                        break;
                                    default:
                                        break;
                                }
                                ctx.Response.ContentType = "text/html";
                                ctx.Response.StatusCode = (int)HttpStatusCode.OK;
                                ctx.Response.ContentLength64 = buf.Length;
                                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                            }
                            catch { } // suppress any exceptions
                            finally
                            {
                                // always close the stream
                                ctx.Response.OutputStream.Close();
                            }
                        }, _listener.GetContext());
                    }
                }
                catch { } // suppress any exceptions
            });
        }

        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }

        private string SendResponse(HttpListenerRequest request)
        {
            StringBuilder html = new StringBuilder();
            string[] strHtml = System.IO.File.ReadAllLines(System.Windows.Forms.Application.StartupPath + "/Web/html/index.html");
            foreach (string item in strHtml)
            {
                html.AppendLine(item);
                if (item.Contains("<head>"))
                {
                    string[] jsFile = System.IO.Directory.GetFiles(System.Windows.Forms.Application.StartupPath + "\\Web\\js\\");
                    foreach (string jsFileName in jsFile)
                    {
                        html.Append("<script type=\"text/javascript\">");
                        html.Append(System.IO.File.ReadAllText(jsFileName));
                        html.Append("</script>");
                    }
                    html.Append("<script type=\"text/javascript\">");
                    string mainIPPort = String.Format("var mainIPPort='{0}';", mUrl);
                    html.Append(mainIPPort);
                    html.Append("</script>");


                    string[] cssFile = System.IO.Directory.GetFiles(System.Windows.Forms.Application.StartupPath + "\\Web\\css\\");
                    foreach (string jsFileName in cssFile)
                    {
                        html.Append("<style type=\"text/css\">");
                        html.Append(System.IO.File.ReadAllText(jsFileName));
                        html.Append("</style>");                                            
                    }
                }
            }
            return html.ToString();
        }

        
    }
}
