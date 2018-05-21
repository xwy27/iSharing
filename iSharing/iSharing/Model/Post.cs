/**************************************************
 *  Author      ：xwy (xuwy27@mail2.sysu.edu.cn)
 *  Time        ：2018/5/18 10:56:51
 *  Filename    ：Post
 *  Version     : V1.0.1  
 *  Description : 
 **************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace iSharing.Models {

  public class Post {
    /**
     * 向服务器发送带有不同 json 的不同请求
     * @param {string} postUrl 请求Url
     * @param {string} json 发送 json 数据
     * @return {bool} 服务器返回状态为 success 则为 true，否则为 false
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
     * 加密函数
     * @param {string} input 需加密信息
     * @return {string} 加密后的 16 进制串
     */
    public static string Encode(string input) {
      var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
      IBuffer buff = CryptographicBuffer.ConvertStringToBinary(input, BinaryStringEncoding.Utf8);
      var hashed = alg.HashData(buff);
      return CryptographicBuffer.EncodeToHexString(hashed);
    }
  }
}
