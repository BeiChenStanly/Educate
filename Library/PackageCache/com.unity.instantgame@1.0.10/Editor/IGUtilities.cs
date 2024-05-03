#if  IG_C301 || IG_C302 || IG_C303 || TUANJIE_2022_3_OR_NEWER // Auto generated by AddMacroForInstantGameFiles.exe

/*======================================================================
Copyright © 2021 Unity Technologies ApS
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
    http://www.apache.org/licenses/LICENSE-2.0
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
======================================================================*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

namespace Unity.InstantGame
{
    enum IGBundleType
    {
        Game,
        GameRes,
        GameLib,
        Engine
    }

    [System.Serializable]
    class IGUnity
    {
        public string jsonversion = "";
        public string uniqueName = "";
        public string displayName = "";
        public string iconUrl = "";
        public IGBundle[] bundles;
    }

    [System.Serializable]
    class IGKuaiShou : IGUnity
    {
        internal IGKuaiShou(IGUnity igUnity)
        {
            this.jsonversion = igUnity.jsonversion;
            this.uniqueName = igUnity.uniqueName;
            this.displayName = igUnity.displayName;
            this.iconUrl = igUnity.iconUrl;
            this.bundles = igUnity.bundles;
        }
    }

    [System.Serializable]
    class IGAlipay : IGUnity
    {
        internal IGAlipay(IGUnity igUnity)
        {
            this.jsonversion = igUnity.jsonversion;
            this.uniqueName = igUnity.uniqueName;
            this.displayName = igUnity.displayName;
            this.iconUrl = igUnity.iconUrl;
            this.bundles = igUnity.bundles;
        }
    }

    [System.Serializable]
    class IGShouQ : IGUnity
    {
        internal IGShouQ(IGUnity igUnity)
        {
            this.jsonversion = igUnity.jsonversion;
            this.uniqueName = igUnity.uniqueName;
            this.displayName = igUnity.displayName;
            this.iconUrl = igUnity.iconUrl;
            this.bundles = igUnity.bundles;
        }
    }

    [System.Serializable]
    class IGByteDance
    {
        public string min_sdk_version = "";
        public IGBundle[] bundles;
    }

    [System.Serializable]
    class IGBundle
    {
        public string abi = "";
        public string filename = "";
        public string download_link = "";
        public bool keep_after_unzip;
        public bool is_main_resource;
        public string parent_dir_type = "";
        public long size;
        public string md5 = "";
        public string engine_folder = "";
        public string sub_dir = "";
        public IGBundleItem[] unzip_files;
    }

    [System.Serializable]
    class IGBundleItem
    {
        public string filename = "";
        public string md5 = "";
        public string parent_dir_type = "";
        public string sub_dir = "";
    }

    internal class ProcessUtilities
    {
        // lower level version function to begin process ... allows external access to process.ErrorOutput
        internal static System.Diagnostics.Process BeginProcess(string processFileName, string inputArguments, string workingDir)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = processFileName;
            process.StartInfo.Arguments = inputArguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            if (workingDir.Trim() != "")
                process.StartInfo.WorkingDirectory = workingDir;
            process.Start();

            return process;
        }

        internal static StreamReader StartProcess(string processFileName, string inputArguments, string outputFile, bool waitForExit, string workingDir)
        {
            System.Diagnostics.Process process = BeginProcess(processFileName, inputArguments, workingDir);

            if (waitForExit)
            {
                // Avoid deadlock when output stream contains lots of text, see http://msdn.microsoft.com/en-us/library/system.diagnostics.processstartinfo.redirectstandardoutput.aspx
                string output = process.StandardOutput.ReadToEnd();
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(output);
                Stream s = new MemoryStream(bytes);
                StreamReader sr = new StreamReader(s);

                process.WaitForExit();
                if ((process.ExitCode != 0) || ((outputFile.Trim() != "") && !File.Exists(outputFile)))
                {
                    UnityEngine.Debug.LogWarning(processFileName + " " + inputArguments
                        + "\nOutput: " + outputFile + ":" + process.StandardError.ReadToEnd()
                        + "This error occurs only in development build, and is harmless.");
                    //throw new System.Exception(processFileName + " -> " + outputFile + " : " + process.StandardError.ReadToEnd());
                }

                return sr;
            }

            return process.StandardOutput;
        }

        internal const string k_MinBytedanceSdkVersionString = "5.6.0";
        private static readonly Version k_MinBytedanceSdkVersion = new Version(k_MinBytedanceSdkVersionString);

        internal static bool CheckBytedanceSdkVersion(out string currentBytedanceSdkVersion)
        {
#if UNITY_ANDROID
            if (IGConfigUIExtension.m_SelectedPlatfomName != "Bytedance")
            {
                currentBytedanceSdkVersion = "";
                return true;
            }

            const string k_BytedanceSdkPackageJsonPath = "Assets/Plugins/ByteGame/com.bytedance.starksdk/package.json";
            if (!File.Exists(k_BytedanceSdkPackageJsonPath))
            {
                currentBytedanceSdkVersion = "";
                return true;
            }

            using (StreamReader r = new StreamReader(k_BytedanceSdkPackageJsonPath))
            {
                string json = r.ReadToEnd();
                dynamic jsonObj = JsonConvert.DeserializeObject(json);
                currentBytedanceSdkVersion = jsonObj.version;
                var bytedanceSdkVersion = new Version(currentBytedanceSdkVersion);
                return bytedanceSdkVersion >= k_MinBytedanceSdkVersion;
            }
#else
            currentBytedanceSdkVersion = "";
            return true;
#endif //UNITY_ANDROID
        }
    }

    internal class FileUtilities
    {
#if UNITY_EDITOR_WIN
        static string s_ZipRelativePath = "/Tools/7z.exe";
#else
        static string s_ZipRelativePath = "/Tools/7za";
#endif

        public static bool ContainChinese(string input)
        {
            const string k_ChineseCharPattern = "[\u4e00-\u9fbb]";
            return Regex.IsMatch(input, k_ChineseCharPattern);
        }

        internal static bool ZipFile(string sourcePath, string zipFilePath)
        {
            string zipper = EditorApplication.applicationContentsPath + s_ZipRelativePath;
            if (File.Exists(zipper))
            {
                zipFilePath = Path.GetFullPath(zipFilePath);
                File.Delete(zipFilePath);
                string currDirectory = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory(sourcePath);
                string inputArguments = "a -tzip -mx5 " + "\"" + zipFilePath + "\"";
                ProcessUtilities.StartProcess(zipper, inputArguments, "", true, "");
                Directory.SetCurrentDirectory(currDirectory);

                return true;
            }
            else
            {
                Debug.Log("7z not found at: " + zipper);
            }

            return false;
        }

        internal static bool UnZipFile(string zipFilePath, string destPath)
        {
            string zipper = EditorApplication.applicationContentsPath + s_ZipRelativePath;

            if (File.Exists(zipper))
            {
                string inputArguments = "x -o" + "\"" + destPath + "\"" + " \"" + zipFilePath + "\" -y";
                ProcessUtilities.StartProcess(zipper, inputArguments, "", true, "");
            }
            else
            {
                Debug.LogError("7z not found at: " + zipper);
            }

            return true;
        }

        internal static string GenerateMD5FromFile(string file)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(file))
                {
                    byte[] retVal = md5.ComputeHash(stream);

                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < retVal.Length; i++)
                    {
                        sb.Append(retVal[i].ToString("x2"));
                    }
                    return sb.ToString();
                }
            }
        }

        internal static void NewCleanDirectory(string dirPath, string outputPath, HashSet<string> keepSubdir = null)
        {
            if (Directory.Exists(dirPath))
            {
                DirectoryInfo di = new DirectoryInfo(dirPath);
                foreach (FileInfo file in di.GetFiles())
                {
                    if (!file.FullName.Equals(Path.GetFullPath(outputPath)))
                        file.Delete();
                }

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    if (keepSubdir == null || !keepSubdir.Contains(dir.Name))
                    {
                        dir.Delete(true);
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(dirPath);
            }
        }
    }

    internal class IGQRCodeGenerator
    {
        public static Texture2D GenerateQRCodeTexture(string url)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = 256,
                    Width = 256,
                    Margin = 2,
                    PureBarcode = true
                }
            };

            Color32[] colors = writer.Write(url);
            Texture2D qrtexture = new Texture2D(256, 256, TextureFormat.RGBA32, false);
            qrtexture.SetPixels32(colors);
            qrtexture.Apply();

            return qrtexture;
        }
    }

#if UNITY_ANDROID
    internal class IGStartupFiles
    {
        internal enum SupportedABI
        {
            None = 0,
            ARMV7 = 1,
            ARM64 = 2
        }

        internal static long startup_size;
        internal static long first_size;
        internal static long gameres_size;
        internal static long gamelib_size;
        internal static long engine_size;

        internal static SupportedABI supportedABI;

        internal static void RefreshInfo()
        {
            supportedABI = SupportedABI.None;
            startup_size = 0;
            string firstPath = IGBuildConstants.k_OutputDir + "/first.zip";
            string resPath = IGBuildConstants.k_OutputDir + "/game_res.zip";
            if (!File.Exists(firstPath) || !File.Exists(resPath))
                return;

            string gamelibPath = IGBuildConstants.k_OutputDir + "/gamelibs_arm64-v8a.zip";
            string enginePath = IGBuildConstants.k_OutputDir + "/engine_arm64-v8a.zip";
            if (File.Exists(gamelibPath))
            {
                gamelib_size = new FileInfo(gamelibPath).Length;
                startup_size += gamelib_size;

                if (File.Exists(enginePath))
                    engine_size = new FileInfo(enginePath).Length;
                else
                    engine_size = ReadEngineSizeFromInstantGameMetaFile("engine_arm64-v8a.zip");
                startup_size += engine_size;

                supportedABI |= SupportedABI.ARM64;
            }

            gamelibPath = IGBuildConstants.k_OutputDir + "/gamelibs_armeabi-v7a.zip";
            enginePath = IGBuildConstants.k_OutputDir + "/engine_armeabi-v7a.zip";
            if (File.Exists(gamelibPath))
            {
                //use armeabi-v7a size if arm64-v8a not supported
                if (supportedABI == SupportedABI.None)
                {
                    gamelib_size = new FileInfo(gamelibPath).Length;
                    startup_size += gamelib_size;

                    if (File.Exists(enginePath))
                        engine_size = new FileInfo(enginePath).Length;
                    else
                        engine_size = ReadEngineSizeFromInstantGameMetaFile("engine_armeabi-v7a.zip");
                    startup_size += engine_size;
                }
                supportedABI |= SupportedABI.ARMV7;
            }

            if (supportedABI != SupportedABI.None)
            {
                first_size = new FileInfo(firstPath).Length;
                startup_size += first_size;

                gameres_size = new FileInfo(resPath).Length;
                startup_size += gameres_size;
            }

        }

        internal static long ReadEngineSizeFromInstantGameMetaFile(string engineFileName)
        {
            if (File.Exists(IGBuildConstants.k_OutputDir + "/" + IGBuildConstants.k_IGJsonFileName))
            {
                string iGUnityStr = File.ReadAllText(IGBuildConstants.k_OutputDir + "/" + IGBuildConstants.k_IGJsonFileName);
                var iGUnity = JsonUtility.FromJson<IGUnity>(iGUnityStr);

                foreach (var bundle in iGUnity.bundles)
                {
                    if (bundle.filename == engineFileName)
                        return bundle.size;
                }
            }
            return 0;
        }
    }
#endif //UNITY_ANDROID
}

#endif  //  IG_C301 || IG_C302 || IG_C303 || TUANJIE_2022_3_OR_NEWER, Auto generated by AddMacroForInstantGameFiles.exe