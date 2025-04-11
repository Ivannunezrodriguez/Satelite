<%@ WebHandler Language="C#" Class="FileUploadHandler" %>

using System;
using System.Web;
using System.Web.SessionState; 


public class FileUploadHandler : IHttpHandler, IRequiresSessionState 
{

    public void ProcessRequest(HttpContext context)
    {
        HttpPostedFile file = context.Request.Files[0];

        string fname = context.Server.MapPath(context.Session["Upload"].ToString() + file.FileName);

        //string fname = context.Server.MapPath("uploads/" + file.FileName);
        file.SaveAs(fname);
        context.Response.ContentType = "text/plain";
        context.Response.Write("Ficheros subidos. Pulsa 'Actualizar'.");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


}