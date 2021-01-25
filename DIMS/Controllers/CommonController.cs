using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DIMS.Controllers
{
    public class CommonController : Controller
    {
        public JsonResult Upload()
        {
            var fileName = string.Empty;
            for (var i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i]; //Uploaded file
                //Use the following properties to get file's name, size and MIMEType
                var fileSize = file.ContentLength;
                fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_"+file.FileName;
                var mimeType = file.ContentType;
                var fileContent = file.InputStream;
                var url = Server.MapPath("~/uploads/") + fileName;
                //To save file, use SaveAs method
                file.SaveAs(url); //File will be saved in application root
            }

            return Json(new {Success = true, FileName = fileName });
        }
    }
}
