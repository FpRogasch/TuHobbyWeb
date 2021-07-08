using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TuHobbyWeb.Controllers
{
    public class DefaultBaseController : Controller
    {
        public string UploadFile(HttpPostedFileBase file, string directoryName)
        {
            // nintendo.jpg || asdf.nintendo.jpg
            string relativePath = "/Content/uploads/" + directoryName;
            string folder = Server.MapPath(relativePath);
            string name = $"{Guid.NewGuid()}.{file.FileName.Split('.').Last()}";
            string path = Path.Combine(folder, name);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(folder);
            }

            file.SaveAs(path);

            return $"{relativePath}/{name}";
        }
    }
}