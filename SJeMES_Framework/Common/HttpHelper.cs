using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Framework.Common
{
    public class HttpHelper
    {

        public static long GetUploadFileMaxLength(string httpUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var requestUri = httpUrl + $"/GetUploadFileMaxLength";

                    var result = client.GetAsync(requestUri);
                    if (result.Result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string resultStr = result.Result.Content.ReadAsStringAsync().Result;
                        if (resultStr == "-1")
                        {
                            throw new Exception("Failed to get uploaded file size");
                        }
                        return Convert.ToInt64(resultStr);
                    }
                    else
                    {
                        throw new Exception("Failed to get uploaded file size");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static UploadFileResultDto UpLoadCommon(string httpUrl, string path, string usertoken)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string saveName = path.Substring(path.LastIndexOf(@"\") + 1);
                    var content = new MultipartFormDataContent();
                    //string path = Path.Combine(filePath);

                    long fileMaxLength = GetUploadFileMaxLength(httpUrl);
                    var fileBytes = new ByteArrayContent(System.IO.File.ReadAllBytes(path));
                    if (fileBytes.Headers.ContentLength > fileMaxLength)
                    {
                        throw new Exception($@"file size exceeds{(fileMaxLength / 1024 / 1024)}m，upload failed！");
                    }
                    content.Add(fileBytes, "file", saveName);
                    var requestUri = httpUrl + $"/UploadCommon?usertoken={usertoken}";

                    var result = client.PostAsync(requestUri, content);
                    if (result.Result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string resultStr = result.Result.Content.ReadAsStringAsync().Result;
                        UploadFileResultDto dic = Newtonsoft.Json.JsonConvert.DeserializeObject<UploadFileResultDto>(resultStr);
                        return dic;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static UploadFileResultDto MoveNfsFile(string httpUrl, string usertoken, string companyCode, string nfsFilePath)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var content = new MultipartFormDataContent();
                    var fileBytes = new ByteArrayContent(new byte[1]);
                    content.Add(fileBytes, "file", "666");

                    var requestUri = httpUrl + $"/MoveNfsFile?userToken={usertoken}&nfsFilePath={nfsFilePath}&companyCode={companyCode}";

                    var result = client.PostAsync(requestUri, content);
                    if (result.Result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string resultStr = result.Result.Content.ReadAsStringAsync().Result;
                        UploadFileResultDto dic = Newtonsoft.Json.JsonConvert.DeserializeObject<UploadFileResultDto>(resultStr);
                        return dic;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 删除文件接口（图片或文件）
        /// </summary>
        /// <param name="httpUrl"></param>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <param name="usertoken"></param>
        /// <returns></returns>
        public static UploadFileResultDto UpLoad(string httpUrl, string path, int type, string usertoken)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string saveName = path.Substring(path.LastIndexOf(@"\")+1);
                    var content = new MultipartFormDataContent();
                    //string path = Path.Combine(filePath);

                    content.Add(new ByteArrayContent(System.IO.File.ReadAllBytes(path)), "file", saveName);
                    var requestUri = httpUrl + $"/Upload_PDA?usertoken={usertoken}&type={type}";

                    var result = client.PostAsync(requestUri, content);
                    if (result.Result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string resultStr = result.Result.Content.ReadAsStringAsync().Result;
                        UploadFileResultDto dic = Newtonsoft.Json.JsonConvert.DeserializeObject<UploadFileResultDto>(resultStr);
                        return dic;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 删除文件接口（图片或文件）
        /// </summary>
        /// <param name="httpUrl"></param>
        /// <param name="url"></param>
        /// <param name="type"></param>
        /// <param name="usertoken"></param>
        /// <returns></returns>
        public static bool DeleteFile(string httpUrl, string url, int type, string usertoken)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var requestUri = httpUrl + $"/DeleteFile_PDA?usertoken={usertoken}&type={type}&url={url}";
                    var result = client.PostAsync(requestUri,null);
                    if (result.Result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
