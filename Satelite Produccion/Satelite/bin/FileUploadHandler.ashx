<%@ WebHandler Language="C#" Class="FileUploadHandler" %>

using System;
using System.Web;
using System.Web.SessionState;
//using Satelite;

public class FileUploadHandler : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        HttpPostedFile file = context.Request.Files[0];

        string fname = context.Server.MapPath(context.Session["Upload"].ToString() + file.FileName);
        context.Session["FilesUp"] += context.Session["Upload"].ToString() + file.FileName + ";";
        //string fname = context.Server.MapPath("uploads/" + file.FileName);
        file.SaveAs(fname);
        context.Response.ContentType = "text/plain";
        context.Response.Write("Ficheros subidos.");
    }

    //public void ProcessRequest(HttpContext context)
    //{
    //    HttpPostedFile file = context.Request.Files[0];

    //    string fname = "";
    //    if(context.Session["Volumen"].ToString() != "")
    //    {
    //        fname = context.Server.MapPath(context.Session["Volumen"].ToString() + file.FileName);
    //        context.Session["FilesUp"] += context.Session["Volumen"].ToString() + file.FileName + ";";
    //    }
    //    else
    //    {
    //        fname = context.Server.MapPath(context.Session["Upload"].ToString() + file.FileName);
    //        context.Session["FilesUp"] += context.Session["Upload"].ToString() + file.FileName + ";";
    //    }
    //    //Variables.mPATHa += context.Session["Upload"].ToString() + file.FileName + ";";
    //    //string fname = context.Server.MapPath("uploads/" + file.FileName);
    //    file.SaveAs(fname);
    //    context.Response.ContentType = "text/plain";
    //    context.Response.Write("Ficheros subidos.");
    //}

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


}