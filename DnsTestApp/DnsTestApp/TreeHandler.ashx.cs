using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DnsTestApp.Model;
using Newtonsoft.Json;

namespace DnsTestApp
{
    public class TreeHandler : IHttpHandler
    {
        private const string IdParameter = "id";
        private const string ParentParameter = "parent";
        private const string SortParameter = "position";
        private const string CommandParameter = "operation";

        public void ProcessRequest(HttpContext httpContext)
        {
            try
            {
                httpContext.Response.ContentType = "text/plain";

                string param = httpContext.Request[IdParameter] == "#" ? "0" : httpContext.Request[IdParameter];
                int nodeId;
                if (int.TryParse(param, out nodeId))
                {
                    string command = httpContext.Request[CommandParameter];
                    switch (command)
                    {
                        case "get_node":
                        {
                            GetNode(httpContext, nodeId);
                            break;
                        }

                        case "move_node":
                        {
                            int targetParent;
                            int sort;
                            if (int.TryParse(httpContext.Request[ParentParameter], out targetParent) &&
                                int.TryParse(httpContext.Request[SortParameter], out sort))
                            {
                                MoveNode(nodeId, targetParent, sort);
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                httpContext.Response.TrySkipIisCustomErrors = true;
                httpContext.Response.Clear();
                httpContext.Response.Write(ex.Message);
                throw;
            }

        }

        private void GetNode(HttpContext httpContext, int parentId)
        {
            using (var context = new DnsTestContext())
            {
                var query = from node in context.Nodes
                    where node.Parent == parentId
                    orderby node.Sort 
                    select new
                    {
                        id = node.Id,
                        text = node.Text,
                        children = node.Folder,
                        type = node.Folder ? "default" : "file",
                        icon = node.Folder ? "./Content/folder.png" : "./Content/leaf.png",
                    };
                var json = JsonConvert.SerializeObject(query.ToList());
                httpContext.Response.Write(json);
            }
        }

        private void MoveNode(int node, int parent, int sort)
        {
            using (var context = new DnsTestContext())
            {
                var currentNode = context.Nodes.FirstOrDefault(x => x.Id == node);
                var parentNode = context.Nodes.FirstOrDefault(x => x.Id == parent);
                if(currentNode != null && parentNode != null)
                {
                    currentNode.Parent = parentNode.Id;
                    currentNode.Sort = sort;
                    context.Entry(currentNode).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}