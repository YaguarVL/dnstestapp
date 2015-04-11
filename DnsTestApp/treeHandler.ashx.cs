using System.Web;

namespace DnsTestApp
{
    /// <summary>
    /// Summary description for treeHandler
    /// </summary>
    public class TreeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string param = context.Request["id"];
            string id = param == "#" ? "1" : (int.Parse(param) + 1).ToString();
            string parent = param == "#" ? "" : string.Format("\"parent\":\"{0}\"", int.Parse(param));
            context.Response.ContentType =   "text/plain";
            string json = string.Format("[{{\"id\":{0},\"text\":\"{0}\",\"children\":true}}]", id);

            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }


    public struct Node
    {
        public Node(string id, string parentId, string text)
            : this()
        {
            Id = id;
            Parent = parentId;
            Text = text;
        }

        public string Id { get; private set; }
        public string Parent { get; private set; }
        public string Text { get; private set; }
    }
}