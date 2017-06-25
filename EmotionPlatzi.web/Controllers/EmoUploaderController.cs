using EmotionPlatzi.web.Models;
using EmotionPlatzi.web.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace EmotionPlatzi.web.Controllers
{
    public class EmoUploaderController : Controller
    {
        EmotionPlatziwebContext db = new EmotionPlatziwebContext();
        EmotionHelper emoHelper;
        string key;
        string ServerFolderPath;

        public EmoUploaderController()
        {
            key = ConfigurationManager.AppSettings["Emotion_Key"];
            emoHelper = new EmotionHelper(key);
            ServerFolderPath = ConfigurationManager.AppSettings["UPLOAD_DIR"];
        }


        // GET: EmoUploader
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase file)
        {
            try
            {
                if (file?.ContentLength > 0)
                {
                    var PictureName = Guid.NewGuid().ToString();
                    PictureName += Path.GetExtension(file.FileName);

                    var route = Server.MapPath(ServerFolderPath);
                    route += $"{"/"}{PictureName}";
                    file.SaveAs(route);
                    var emoPicture = await emoHelper.DetectAndExtractFacesAsync(file.InputStream);
                    emoPicture.Name = file.FileName;
                    emoPicture.Path = $"{ServerFolderPath}/{PictureName}";
                    db.EmoPictures.Add(emoPicture);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Details", "EmoPictures", new { id = emoPicture.Id });
                }
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }            
            return View();
        }
    }
}