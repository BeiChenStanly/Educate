#if  IG_C301 || IG_C302 || IG_C303 || TUANJIE_2022_3_OR_NEWER // Auto generated by AddMacroForInstantGameFiles.exe

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using Unity.AutoStreaming;
using Unity.AutoStreaming.CloudContentDelivery;
using UnityEngine;

public class ByteDanceVersions
{
    const string kByteDanceUrl = "http://developer.toutiao.com/api/apps/v3/meta?appid={0}&ttcode=HTPqioja5iYfNL8fRz5Imf68m-ymf9BHFV-Ism7M4YWsOxyfRczI-dLTGHFfydJ3iogU0q66k2Sfzhhpd-rUZxCdgLROGYuhssEGKclTPsV0lh_pVc-ToITMefqEUihAO1Y3FcCesNIBnGPJWwKLjgYInqZ-Wj4rsJVA-weK07I%3D&version={1}&abi_64=true";

    const string kLatestVersion = "latest";
    const string kAuditVersion = "audit";
    const string kCurrentVersion = "current";

    public struct Version
    {
        public string bucketUuid;
        public string badgeName;
    }

    public static Version LatestVersion { get { return _latestVersion; } } //测试版本
    public static Version AuditVersion { get { return _auditVersion; } }     //提审版本
    public static Version CurrentVersion { get { return _currentVersion; } }  // 线上版本


    private static Version _latestVersion; //测试版本
    private static Version _auditVersion;     //提审版本
    private static Version _currentVersion;   // 线上版本

    public static void FetchByteDanceVersions(string ttAppid)
    {
        if (string.IsNullOrEmpty(ttAppid))
            return;

        string url = FetchByteDanceConfigUrl(ttAppid, kLatestVersion);
        string resp = GetResponseStringFromUrl(url);
        ExtractVersionInfoFromJsonString(resp, ref _latestVersion);

        url = FetchByteDanceConfigUrl(ttAppid, kAuditVersion);
        resp = GetResponseStringFromUrl(url);
        ExtractVersionInfoFromJsonString(resp, ref _auditVersion);

        url = FetchByteDanceConfigUrl(ttAppid, kCurrentVersion);
        resp = GetResponseStringFromUrl(url);
        ExtractVersionInfoFromJsonString(resp, ref _currentVersion);
    }

    static string GetResponseStringFromUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
            return string.Empty;

        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Proxy = null;
            request.Method = "GET";
            using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
            {
                if (resp.StatusCode.Equals(HttpStatusCode.OK))
                {
                    string strJson;
                    using (Stream stream = resp.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            strJson = reader.ReadToEnd();
                        }
                    }
                    return strJson;
                }
                else
                {
                    Debug.LogWarning("Failed Fetching Response String from url:" + url);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Fetching Response String Error: " + e.Message);
        }
        return string.Empty;
    }

    static string FetchByteDanceConfigUrl(string ttAppid, string version)
    {
        string url = string.Format(kByteDanceUrl, ttAppid, version);
        string resp = GetResponseStringFromUrl(url);
        return ExtractConfigUrlFromJsonString(resp);
    }

    static string ExtractConfigUrlFromJsonString(string jsonStr)
    {
        if (string.IsNullOrEmpty(jsonStr))
            return string.Empty;

        string url = "";
        try
        {
            JObject root = (JObject)JsonConvert.DeserializeObject(jsonStr);
            JObject data = (JObject)root["data"];
            string versionStr = data["version_state_json"].ToString();
            JObject version_state_json = (JObject)JsonConvert.DeserializeObject(versionStr);
            JObject bundles = (JObject)version_state_json["bundles"];
            url = bundles["url"].ToString();
        }
        catch
        {
            Debug.LogWarning("InstantGame ByteDanceVersion: Failed to fetch bytedance version config url.");
        }
        return url;
    }

    static void ExtractVersionInfoFromJsonString(string jsonStr, ref Version version)
    {
        version.bucketUuid = string.Empty;
        version.badgeName = string.Empty;

        if (string.IsNullOrEmpty(jsonStr))
            return;

        string firstZipUrl = "";
        try
        {
            JObject root = (JObject)JsonConvert.DeserializeObject(jsonStr);
            JArray bundles = (JArray)root["bundles"];
            JObject firstZipBundle = (JObject)bundles.First;
            firstZipUrl = firstZipBundle["download_link"].ToString();
        }
        catch
        {
            Debug.LogWarning("InstantGame ByteDanceVersion: Failed to fetch bucketUuid and badgeName info from ig_config.json.");
        }

        string urlStart = string.Format("{0}/client_api/v1/buckets/", UOSInfo.DownloadApiHost);
        if (!firstZipUrl.StartsWith(urlStart))
        {
            Debug.LogWarning("InstantGame ByteDanceVersion: Invalid download link for first.zip in ig_config.json");
            return;
        }

        version.bucketUuid = firstZipUrl.Substring(urlStart.Length, 36);

        int startIndex = (urlStart + version.bucketUuid + "/release_by_badge/").Length;
        int endIndex = firstZipUrl.IndexOf("/content/");
        version.badgeName = firstZipUrl.Substring(startIndex, endIndex - startIndex);
    }
}

#endif  //  IG_C301 || IG_C302 || IG_C303 || TUANJIE_2022_3_OR_NEWER, Auto generated by AddMacroForInstantGameFiles.exe