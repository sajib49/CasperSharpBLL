using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;


namespace CS.Web.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult CustomerRoiPage()
        {
            return View();
        }

        public ActionResult CustomerRoi()
        {
            ReportDocument rd = new ReportDocument();
            //rd.Load(Path.Combine(Server.MapPath("~/Reports"), "TestRepot.rpt")); //For Same Project
            string path = Path.GetFullPath(Path.Combine(Server.MapPath("~"), @"..\CS.Report/t.rpt"));  //For Diffrent Project
            rd.Load(path);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            //return File(stream, "application/pdf","file_name"); // For Direct download
            return new FileStreamResult(stream, "application/pdf"); //For Direct View
        }
	}
}