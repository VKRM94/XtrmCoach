using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using XtrmCoachRESTServer.Models;
using XtrmCoachRESTServer.RepositoryInterface;

namespace XtrmCoachRESTServer.Controller
{
	public class PlayerImageController : ApiController
	{

		// GET: api/PlayerImage
		public HttpResponseMessage Get()
		{
			string fileName = HttpContext.Current.Request.QueryString.GetValues("fileName")[0];

			if (!string.IsNullOrWhiteSpace(fileName) && !fileName.Equals("{{player.imageId}}"))
			{
				var filePath = HttpContext.Current.Server.MapPath("~/PlayerImage/" + fileName);

				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
				response.Content = new StreamContent(new FileStream(filePath, FileMode.Open, FileAccess.Read));
				response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
				//response.Content.Headers.ContentDisposition.FileName = fileName;
				return response;
			}

			return null;
			/*
			byte[] fileData = File.ReadAllBytes(filePath);

			if (fileData == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			response.Content = new ByteArrayContent(fileData);
			response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

			return response;
			*/

			/*
			var result = new HttpResponseMessage(HttpStatusCode.OK);
			//String filePath = HostingEnvironment.MapPath("~/Images/HT.jpg");
			FileStream fileStream = new FileStream(filePath, FileMode.Open);
			Image image = Image.FromStream(fileStream);
			MemoryStream memoryStream = new MemoryStream();
			image.Save(memoryStream, ImageFormat.Jpeg);
			result.Content = new ByteArrayContent(memoryStream.ToArray());
			result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

			return result;
			*/
			/*
			var stream = new FileStream(filePath, FileMode.Open);
			var content = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StreamContent(stream)
			};

			//content.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
			return content;
			*/
		}

		// POST: api/PlayerImage
		public HttpResponseMessage Post()
		{
			var httpRequest = HttpContext.Current.Request;

			if (httpRequest.Files.Count < 1)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "No image sent.");
			}

			var postedFile = httpRequest.Files["image"];
			var filePath = HttpContext.Current.Server.MapPath("~/PlayerImage/" + postedFile.FileName);
			postedFile.SaveAs(filePath);

			return Request.CreateResponse(HttpStatusCode.OK, "PlayerImage added successfully.");
		}

		/*
		// PUT: api/PlayerImage/5
		public HttpResponseMessage Put()
		{
			HttpResponseMessage response;



			if (isUpdated)
			{
				response = Request.CreateResponse(HttpStatusCode.OK, "Updated PlayerImage successfully.");
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.Conflict, "Invalid PlayerImage id passed.");
			}

			return response;
		}

		// DELETE: api/PlayerImage/5
		public HttpResponseMessage Delete(int id)
		{
			HttpResponseMessage response;

			if (id <= 0)
			{
				response = Request.CreateResponse(HttpStatusCode.NoContent, "Invalid Id Passed.");
				return response;
			}

			bool isDeleted = _iPlayerImageRepository.DeletePlayerImage(id);

			if (isDeleted)
			{
				response = Request.CreateResponse(HttpStatusCode.OK, "PlayerImage successfully deleted.");
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.Conflict, "Invalid PlayerImage Id.");
			}

			return response;
			
		}*/
	}
}