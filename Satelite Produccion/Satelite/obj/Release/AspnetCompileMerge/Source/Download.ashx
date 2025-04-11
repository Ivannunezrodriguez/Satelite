    <%@ WebHandler Language="C#" Class="Download" %>

using System;
using System.Web;
using System.IO;

public class Download : IHttpHandler {

    public void ProcessRequest (HttpContext context) {

        string file = context.Request.QueryString["file"];

        if (!string.IsNullOrEmpty(file) && File.Exists(context.Server.MapPath(file)))
        {
            context.Response.Clear();
            context.Response.ContentType = "application/octet-stream";
            context.Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(file));
            context.Response.WriteFile(context.Server.MapPath(file));
            // This would be the ideal spot to collect some download statistics and / or tracking  
            // also, you could implement other requests, such as delete the file after download  
             // Este sería el lugar ideal para recopilar algunas estadísticas de descarga y / o seguimiento  
            // también, puede implementar otras solicitudes, como eliminar el archivo después de la descarga  
            context.Response.End();
        }
        else
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("File not be found!");
        }

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}
