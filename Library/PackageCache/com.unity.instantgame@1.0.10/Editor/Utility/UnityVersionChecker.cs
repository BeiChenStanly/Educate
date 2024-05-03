using System;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
internal sealed class UnityVersionChecker
{
    public const string m_PackageName = "com.unity.instantgame";

    static UnityVersionChecker()
    {
        AssemblyReloadEvents.afterAssemblyReload += CheckUnityVersion;
#if !IG_C301 && !IG_C302 && !IG_C303 && !TUANJIE_2022_3_OR_NEWER
        RemovePackageInstantGame("com.unity.autostreaming");
        RemovePackageInstantGame("com.unity.autostreaming.UOS");
#endif
    }

    static void CheckUnityVersion()
    {
#if !IG_C301 && !IG_C302 && !IG_C303 && !TUANJIE_2022_3_OR_NEWER
        RemovePackageInstantGame(m_PackageName);
        EditorUtility.DisplayDialog("Package incompatible", "This versions of Editor and Instant Game package are incompatible.Please install Tuanjie Editor or Unity InstantGame Editor. Package Instant Game won't be installed", "OK");
#endif
    }

    static void RemovePackageInstantGame(string pkgName)
    {
        var dirDelim = Path.DirectorySeparatorChar;
        var packagesManifestPath = $"Packages{dirDelim}manifest.json";

        try
        {
            string json;
            using (var reader = new StreamReader(packagesManifestPath))
            {
                json = reader.ReadToEnd();
            }

            var jsonObj = JObject.Parse(json);
            var deps = (JObject)jsonObj.SelectToken("dependencies");
            deps.Property(pkgName)?.Remove();

            json = jsonObj.ToString();
            using (var writer = new StreamWriter(packagesManifestPath))
            {
                writer.Write(json);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Fail to remove package {pkgName}, error: {e}");
        }
    }
}
