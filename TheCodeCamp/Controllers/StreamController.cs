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

      public HttpResponseMessage Get()
		{
         var result = new HttpResponseMessage(HttpStatusCode.OK)
         {
            Content = new PushStreamContent(streamInit())
         };
         result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/text");
         //result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "MyZipfile.zip" };
         return result;
      }


      private Func<Stream, HttpContent, TransportContext, Task> streamInit()
		{
			async Task onStreamAvailable(Stream outputStream, HttpContent httpContext, TransportContext transportContext)
			{
            using (var streamWriter = new StreamWriter(outputStream))
				{
               var i = 0;

               var obj1 = @"{""name"":""John"",
                             ""age"":30}";
               var obj2 = @"{""name"":""Patrick"",
                             ""age"":25}";
               var obj3 = @"{""name"":""Tom"",
                             ""age"":22}";
               var obj4 = @"{""name"":""Arci"",
                             ""age"":12}";
               foreach (var kvp in (new List<string> { obj1, obj2, obj3, obj4 }))
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