using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace TheCodeCamp.Controllers
{
	public class StreamController: ApiController
	{
      public delegate Task<string> SendData(Task<string> data);

      Func<SendData,Task<int>> getData = async (sendData) =>
      {
         var i = 0;
         foreach (var kvp in (new List<string> { "1", "2", "3", "4" }))
         {
            i++;
            await Task.Delay(5000);
            await sendData(Task.Run(()=> kvp));
         }
         return i;
      };

      public HttpResponseMessage Get()
		{
         var result = new HttpResponseMessage(HttpStatusCode.OK)
         {
            Content = new PushStreamContent(streamInit(getData))
         };
         result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
         result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "MyZipfile.zip" };
         return result;
      }


      private Func<Stream, HttpContent, TransportContext, Task> streamInit(Object objGetData)
		{
			async Task onStreamAvailable(Stream outputStream, HttpContent httpContext, TransportContext transportContext)
			{
            Func<Task<string>,bool> getDataPrimary = objGetData as Func<Task<string>, bool>;
            using (var streamWriter = new StreamWriter(outputStream))
				{

               var i = 0;
               foreach (var kvp in (new List<string> { "1", "2", "3", "4" }))
               {
                  i++;
                  await Task.Delay(5000);
                  await streamWriter.WriteAsync(JsonConvert.SerializeObject(kvp));
                  await streamWriter.FlushAsync();
               }
            }
         }
			return onStreamAvailable;
		}
	}
}