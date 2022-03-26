﻿using System.Collections;
using System.Collections.Generic;
using RestClient.Core.Models;
using RestClient.Core.Singletons;
using UnityEngine;
using UnityEngine.Networking;
 
namespace RestClient.Core
{
    public class RestWebClient : Singleton<RestWebClient>
    {
        //private const string defaultContentType = "application/json";
        private const string defaultContentType = "application/octet-stream";
        public IEnumerator HttpGet(string url, string body, System.Action<Response> callback, IEnumerable<RequestHeader> headers = null)
        {
            yield return new WaitForSeconds(1.0f); //delay for azure read processing
            using(UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                if (headers != null)
                {
                    foreach (RequestHeader header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }
                //webRequest.uploadHandler.contentType = defaultContentType;
                //webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(body));
                

                yield return webRequest.SendWebRequest();
                
                if(webRequest.isNetworkError){
                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                    });
                }
                
                if(webRequest.isDone)
                {
                    string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                    //var data = webRequest.GetResponseHeaders();
                    Debug.Log("GET Output: " + data);
                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                        Data = data
                    });
                }
            }
        }

        public IEnumerator HttpDelete(string url, System.Action<Response> callback)
        {
            using(UnityWebRequest webRequest = UnityWebRequest.Delete(url))
            {
                yield return webRequest.SendWebRequest();

                if(webRequest.isNetworkError){
                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error
                    });
                }
                
                if(webRequest.isDone)
                {
                    callback(new Response {
                        StatusCode = webRequest.responseCode
                    });
                }
            }
        }

        public IEnumerator HttpPost(string url, byte[] bytes, System.Action<Response> callback, IEnumerable<RequestHeader> headers = null)
        {
            //WWWForm form = new WWWForm();
            //form.AddBinaryData("file", bytes, "imageFile", "application/octet-stream");

            WWWForm webForm = new WWWForm();
            using (UnityWebRequest webRequest = UnityWebRequest.Post(url, webForm))
            {
                if (headers != null)
                {
                    foreach (RequestHeader header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }

                //webRequest.uploadHandler.contentType = "application/octet-stream";
                //webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(body));
                //webRequest.uploadHandler = new UploadHandlerRaw(bytes);

                webRequest.downloadHandler = new DownloadHandlerBuffer();
                webRequest.uploadHandler = new UploadHandlerRaw(bytes);
                webRequest.uploadHandler.contentType = "application/octet-stream";

                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    callback(new Response
                    {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error
                    });
                }

                if (webRequest.isDone)
                {
                    var data = webRequest.GetResponseHeaders();
                    //string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                    callback(new Response
                    {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                        Headers = data
                    });
                }
            }
        }

        public IEnumerator HttpPut(string url, string body, System.Action<Response> callback, IEnumerable<RequestHeader> headers = null)
        {
            using(UnityWebRequest webRequest = UnityWebRequest.Put(url, body))
            {
                if(headers != null)
                {
                    foreach (RequestHeader header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }

                webRequest.uploadHandler.contentType = defaultContentType;
                webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(body));

                yield return webRequest.SendWebRequest();

                if(webRequest.isNetworkError)
                {
                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                    });
                }
                
                if(webRequest.isDone)
                {
                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                    });
                }
            }
        }

        public IEnumerator HttpHead(string url, System.Action<Response> callback)
        {
            using(UnityWebRequest webRequest = UnityWebRequest.Head(url))
            {
                yield return webRequest.SendWebRequest();
                
                if(webRequest.isNetworkError){
                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                    });
                }
                
                if(webRequest.isDone)
                {
                    var responseHeaders = webRequest.GetResponseHeaders();
                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                        Headers = responseHeaders
                    });
                }
            }
        }
    }
}