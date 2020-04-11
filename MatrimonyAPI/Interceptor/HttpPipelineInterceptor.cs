﻿using Matrimony.Helper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MatrimonyAPI.Interceptor
{
    public class HttpPipelineInterceptor
    {
        private readonly RequestDelegate _next;

        public HttpPipelineInterceptor(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //First, get the incoming request
            if(context.Request.Method != "GET")
            {
                using (var bodyReader = new StreamReader(context.Request.Body))
                {
                    var bodyAsText = bodyReader.ReadToEnd();
                    string decodedString = AESEncryDecry.DecryptStringAES(bodyAsText);
                    dynamic json = JsonConvert.DeserializeObject(decodedString);
                    var obj = JsonConvert.DeserializeObject<dynamic>(decodedString);
                    var response= JsonConvert.SerializeObject(obj);
                    MemoryStream mStrm = new MemoryStream(Encoding.UTF8.GetBytes(response));
                    context.Request.Body = mStrm;
                    context.Request.ContentType = "application/json";
                }

                //Copy a pointer to the original response body stream
                var originalBodyStream = context.Response.Body;

                //Create a new memory stream...
                using (var responseBody = new MemoryStream())
                {
                    //...and use that for the temporary response body
                    context.Response.Body = responseBody;

                    //Continue down the Middleware pipeline, eventually returning to this class
                    await _next(context);

                    //Format the response from the server
                    var response = await FormatResponse(context.Response);

                    //TODO: Save log to chosen datastore

                    //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            else
            {
                //Copy a pointer to the original response body stream
                var originalBodyStream = context.Response.Body;

                //Create a new memory stream...
                using (var responseBody = new MemoryStream())
                {
                    //...and use that for the temporary response body
                    context.Response.Body = responseBody;

                    //Continue down the Middleware pipeline, eventually returning to this class
                    await _next(context);

                    //Format the response from the server
                    var response = await FormatResponse(context.Response);

                    //TODO: Save log to chosen datastore

                    //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            
        }

        private string FormatRequest(HttpRequest request)
        {
            var body = request.Body;

            //This line allows us to set the reader for the request back at the beginning of its stream.
            //request.EnableRewind();

            //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            string bodyMessage = new StreamReader(request.Body).ReadToEnd();
            string decodedString = AESEncryDecry.DecryptStringAES(bodyMessage);
            dynamic json = JsonConvert.DeserializeObject(decodedString);
            request.ContentType = "application/json";
            //request.Body = decodedString;
            //...Then we copy the entire request stream into the new buffer.
            request.Body.ReadAsync(buffer, 0, buffer.Length);

            //We convert the byte[] into a string using UTF8 encoding...
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            //..and finally, assign the read body back to the request body, which is allowed because of EnableRewind()
            request.Body = body;
            request.Body.Seek(0, SeekOrigin.Begin);
            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return $"{response.StatusCode}: {text}";
        }

    }
}