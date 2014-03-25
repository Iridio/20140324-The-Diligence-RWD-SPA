using System.IO;
using System.Linq;
using System.Web;

namespace FlexibleImages
{
  class FlexibleImagesHandler : IHttpHandler
  {
    public void ProcessRequest(HttpContext context)
    {
      var sourceFile = context.Server.MapPath(context.Request.RawUrl).ToLower();
      int screenWidth = 0;
      int devicePixelRatio = 0;
      //rielaboro l'immagine
      var fileName = Path.GetFileName(sourceFile);
      var pathName = Path.GetDirectoryName(sourceFile);
      var cookie = context.Request.Cookies[Constants.CookieName];
      if (cookie != null)
      {
        int.TryParse(cookie[Constants.DevicePixelRatioName], out devicePixelRatio);
        int.TryParse(cookie[Constants.ScreenWidthName], out screenWidth);
      }
      fileName = GetImageName(screenWidth, devicePixelRatio);
      sourceFile = Path.Combine(pathName, fileName);
      if (!File.Exists(sourceFile))
      {
        //fare una gestione degli errori. Magari torna una "no-image.jpg"
        return;
      }
      try
      {
        SendImage(context, sourceFile);
      }
      catch
      {
        context.Response.TrySkipIisCustomErrors = true;
        context.Response.StatusCode = (int)(int)System.Net.HttpStatusCode.NotFound;
      }
    }

    static string GetImageName(int width, decimal devicePixelRatio)
    {
      //Fatto così per semplicità, ma qui si deve inserire un controllo e comporre le immagini con suffissi/prefissi diversi
      //Da un ipotetico management vengono create le immagini in tutti i formati e qui viene caricata quella corretta.
      var result = "star-wars-google_320.jpg";
      if (width > 767)
        result = "star-wars-google_1024.jpg";
      if (width > 1025)
        result = "star-wars-google_full.jpg";

      //Gestione dei device retina
      //if (devicePixelRatio > 1)
      //{
      //  result = "star-wars-google_MobileRetina.jpg";
      //  if (width > 1515)
      //    result = "star-wars-google_TabletRetina.jpg";
      //  if (width > 2050)
      //    result = "star-wars-google_DesktopRetina.jpg";
      //}
      return result;
    }

    readonly static string[] imageExts = { ".png", ".gif", ".jpeg" };
    static void SendImage(HttpContext context, string filename)
    {
      string extension = Path.GetExtension(filename).ToLower();
      context.Response.ContentType = "image/" + (imageExts.Contains(extension) ? extension.TrimStart('.') : "jpg");
      context.Response.TransmitFile(filename);
    }

    public bool IsReusable
    {
      get { return false; }
    }
  }
}
