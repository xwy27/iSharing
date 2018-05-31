/**************************************************
 *  Author      ：xwy (xuwy27@mail2.sysu.edu.cn)
 *  Time        ：2018/5/18 10:56:51
 *  Filename    ：Post
 *  Version     : V1.0.1  
 *  Description : 
 **************************************************/

using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Streams;

namespace iSharing.Models {

  public class Post {
    private static IBuffer buff;
    /**
     * 向服务器发送带有不同 json 的不同请求
     * @param {string} postUrl 请求Url
     * @param {string} json 发送 json 数据
     * @return {string} 服务器返回 json 数据
     */
    public static async Task<string> PostHttp(string url, string json) {
      var httpClient = new HttpClient();
      string resourceAddress = "http://localhost:8000" + url;

      httpClient.DefaultRequestHeaders.Accept.Add(
        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
      HttpResponseMessage response = await httpClient.PostAsync(
        resourceAddress, new StringContent(json, Encoding.UTF8, "application/json"));

      if (response.IsSuccessStatusCode) {
        // Set encoding to 'UTF-8'
        Byte[] getByte1 = await response.Content.ReadAsByteArrayAsync();
        Encoding code1 = Encoding.GetEncoding("UTF-8");
        return code1.GetString(getByte1, 0, getByte1.Length);
      }
      return "";
    }

    /**
     * 向服务器发送图片
     * @param {StorageFile} file 发送图片
     * @return {string} 服务器返回图片 url
     */
    public static async Task<string> PostPhoto(StorageFile file) {
      HttpClient client = new HttpClient();
      var content = new MultipartFormDataContent();
      if (file != null) {
        var streamData = await file.OpenReadAsync();
        var bytes = new byte[streamData.Size];
        using (var dataReader = new DataReader(streamData)) {
          await dataReader.LoadAsync((uint)streamData.Size);
          dataReader.ReadBytes(bytes);
        }
        var streamContent = new StreamContent(new MemoryStream(bytes));
        content.Add(streamContent, "file", "icon.jpg");
      }

      var response = await client.PostAsync(new Uri("http://localhost:8000/image_upload", UriKind.Absolute), content);
      if (response.IsSuccessStatusCode) {
        // Set encoding to 'UTF-8'
        Byte[] getByte1 = await response.Content.ReadAsByteArrayAsync();
        Encoding code1 = Encoding.GetEncoding("UTF-8");
        return code1.GetString(getByte1, 0, getByte1.Length);
      }
      return "";
    }

    /**
     * 加密 password 函数
     * @param {string} input 需加密信息
     * @return {string} 加密后的 16 进制串
     */
    public static string EncodePsd(string input) {
      var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
      buff = CryptographicBuffer.ConvertStringToBinary(input, BinaryStringEncoding.Utf8);
      var hashed = alg.HashData(buff);
      return CryptographicBuffer.EncodeToHexString(hashed);
    }

    /**
     * 解密 password 函数
     * @param {string} input 需解密信息
     * @return {string} 解密后的信息
     */
    public static string DecodePsd(string input) {
      return CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, buff);
    }
  }
}
