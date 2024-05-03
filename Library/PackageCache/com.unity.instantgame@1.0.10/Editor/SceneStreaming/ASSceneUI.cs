#if  IG_C301 || IG_C302 || IG_C303 || TUANJIE_2022_3_OR_NEWER // Auto generated by AddMacroForInstantGameFiles.exe

using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.U2D;
using UnityEditor.U2D;

namespace Unity.AutoStreaming
{
    internal class ASSceneUI : TabBase<ASScenesTreeView, ASSceneTreeDataItem>
    {
        internal static readonly string k_AutoStreamingAbLutDir = AutoStreamingSettings.autoStreamingDirectory + "/ASABLut";

        // Used to compute hash128: 66b76bf45c65a57bcc05b59eedf85d96
        // Should be consistent with the one in DownloadAndLoadSceneOperation::PrepareDownload()
        const string k_PlaceholdersMagicName = "placeholders";

        bool m_SceneForceRebuildAssetBundle;
        private GUIStyle m_ToggleStyle;

        protected ASSharedSceneAssetsTreeView m_SharedSceneAssetTreeView;
        protected TreeViewState m_SharedSceneAssetTreeViewState;  // Serialized in the window layout file so it survives assembly reloading.
        protected MultiColumnHeaderState m_SharedSceneAssetMultiColumnHeaderState;

        bool dragging;
        float splitPos;

        public ASSceneUI()
        {
            m_ToggleStyle = GUI.skin.toggle;
            m_ToggleStyle.margin = new RectOffset(3, 3, 3, 2);
            splitPos = TreeViewRect.width / 2.0f;
        }

        protected override MultiColumnHeaderState CreateColumnHeaderState(float treeViewWidth)
        {
            m_SharedSceneAssetMultiColumnHeaderState = ASSharedSceneAssetsTreeView.CreateDefaultMultiColumnHeaderState(treeViewWidth * 0.5f);
            return ASScenesTreeView.CreateDefaultMultiColumnHeaderState(treeViewWidth * 0.5f);
        }

        protected override void InitTreeView(MultiColumnHeader multiColumnHeader)
        {
            var treeModel = new TreeModelT<ASSceneTreeDataItem>(ASMainWindow.Instance.SceneData);
            m_TreeView = new ASScenesTreeView(m_TreeViewState, multiColumnHeader, treeModel);

            if (m_SharedSceneAssetTreeViewState == null)
                m_SharedSceneAssetTreeViewState = new TreeViewState();

            var sharedSceneAssetTreeModel = new TreeModelT<ASSharedSceneAssetTreeDataItem>(ASMainWindow.Instance.SharedSceneAssetData);
            var sharedSceneAssetMultiColumnHeader = new ASMultiColumnHeader(m_SharedSceneAssetMultiColumnHeaderState);
            m_SharedSceneAssetTreeView = new ASSharedSceneAssetsTreeView(m_SharedSceneAssetTreeViewState, sharedSceneAssetMultiColumnHeader, sharedSceneAssetTreeModel);

            m_SearchField = new SearchField();
            m_SearchField.downOrUpArrowKeyPressed += m_SharedSceneAssetTreeView.SetFocusAndEnsureSelectedItem;
        }

        public override void OnGUI()
        {
            OnToolbarGUI(TabToolbarRect);
            var sceneTreeViewRect = new Rect(TreeViewRect.x, TreeViewRect.y, splitPos, TreeViewRect.height);
            m_TreeView.OnGUI(sceneTreeViewRect);

            Rect splitRect = new Rect(splitPos - 2, TreeViewRect.y, 4, TreeViewRect.height);

            var sharedSceneAssetViewRect = new Rect(splitPos, TreeViewRect.y, TreeViewRect.width - splitPos, TreeViewRect.height);
            m_SharedSceneAssetTreeView.OnGUI(sharedSceneAssetViewRect);
            // Splitter events
            EditorGUIUtility.AddCursorRect(splitRect, MouseCursor.ResizeHorizontal);
            if (Event.current != null)
            {
                switch (Event.current.rawType)
                {
                    case EventType.MouseDown:
                        if (splitRect.Contains(Event.current.mousePosition))
                        {
                            dragging = true;
                        }
                        break;
                    case EventType.MouseDrag:
                        if (dragging)
                        {
                            splitPos += Event.current.delta.x;
                            ASMainWindow.Instance.Repaint();
                        }
                        break;
                    case EventType.MouseUp:
                        if (dragging)
                        {
                            dragging = false;
                        }
                        break;
                }
            }

        }
        protected override void OnToolbarGUI(Rect rect)
        {
            Rect leftRect = new Rect(rect.x, rect.y, splitPos, rect.height);
            GUILayout.BeginArea(leftRect);
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Space(5);

                if (GUILayout.Button("Sync Scenes", s_MiniButton, GUILayout.Width(90)))
                {
                    SyncScenes();
                }

                GUILayout.Space(20);
                m_SceneForceRebuildAssetBundle = GUILayout.Toggle(m_SceneForceRebuildAssetBundle, "Force Rebuild", m_ToggleStyle);

                if (GUILayout.Button("Generate ABs", s_MiniButton, GUILayout.Width(95)))
                {
                    GenerateSceneAssetBundles(m_SceneForceRebuildAssetBundle);
                    SyncScenes();
                    GUIUtility.ExitGUI();
                }

                GUILayout.FlexibleSpace();

                string statusReport = "";
                var allScenes = AutoStreamingSettings.scenes;
                var onDemandDownloadItems = allScenes.Where(x => x.onDemandDownload);
                statusReport = string.Format("Scene: {0}/{1}, AB: {2}",
                    onDemandDownloadItems.Count(),
                    allScenes.Length,
                    EditorUtility.FormatBytes(onDemandDownloadItems.Select(x => (long)x.assetBundleSize).Sum()));
                GUILayout.Label(statusReport);
            }
            GUILayout.EndArea();

            Rect rightRect = new Rect(leftRect.xMax, rect.y, rect.width - splitPos, rect.height);
            GUILayout.BeginArea(rightRect);
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Space(5);

                if (GUILayout.Button("Sync SharedAssets", s_MiniButton, GUILayout.Width(150)))
                {
                    SyncSharedAssets();
                }

                GUILayout.Space(5);
                m_SharedSceneAssetTreeView.searchString = m_SearchField.OnToolbarGUI(m_SharedSceneAssetTreeView.searchString, GUILayout.Width(200));

                GUILayout.FlexibleSpace();
                string statusReport = "";
                var allShareAssets = AutoStreamingSettings.sharedSceneAssets;
                var includeInAB = allShareAssets.Where(x => x.includedInSharedSceneAssetsAB);
                statusReport = string.Format("Assets Selected: {0}/{1}",
                    includeInAB.Count(),
                    allShareAssets.Length);
                GUILayout.Label(statusReport);
            }
            GUILayout.EndArea();
        }

        void SyncScenes()
        {
            AutoStreamingSettings.SyncScenes();
            ASMainWindow.Instance.SceneData = null;
            m_TreeViewInitialized = false;
        }

        void SyncSharedAssets()
        {
            AutoStreamingSettings.SyncSharedSceneAssets();
            ASMainWindow.Instance.SharedSceneAssetData = null;
            m_TreeViewInitialized = false;
        }

        internal static void GenerateSceneAssetBundles(bool forceRebuild)
        {
            if(File.Exists(Path.Combine(k_AutoStreamingAbLutDir, "scenes")))
                File.Delete(Path.Combine(k_AutoStreamingAbLutDir, "scenes"));
            // back up
            bool originalAutoStreaming = PlayerSettings.autoStreaming;

            // Enable AutoStreaming when building AssetBundles for the scene AB.
            PlayerSettings.autoStreaming = true;

            List<AssetBundleBuild> abs = new List<AssetBundleBuild>();
            Directory.CreateDirectory(k_AutoStreamingAbLutDir);

            //search share assets
            {
                List<string> shareAssetsPaths = (from r in AutoStreamingSettings.sharedSceneAssets where r.includedInSharedSceneAssetsAB select r.assetPath).ToList();

                var texItems = AutoStreamingSettings.textures;

                if (shareAssetsPaths.Count == 0)
                    shareAssetsPaths.Add("Packages/com.unity.instantgame/dummy.png");

                AssetBundleBuild ab = new AssetBundleBuild();
                ab.assetNames = shareAssetsPaths.ToArray();
                ab.assetBundleName = Hash128.Compute(k_PlaceholdersMagicName).ToString() + ".abas";

                abs.Add(ab);
            }

            // scenes ABs
            List<string> sceneList = new List<string>();
            List<string> sceneListAll = new List<string>();
            Dictionary<string, string> sceneMap = new Dictionary<string, string>();
            var allScenes = AutoStreamingSettings.scenes;
            foreach (var scene in allScenes)
            {
                sceneListAll.Add(scene.assetPath);
                if (scene.onDemandDownload)
                {
                    sceneList.Add(scene.assetPath);
                    sceneMap.Add(AssetDatabase.AssetPathToGUID(scene.assetPath), scene.assetPath);
                }
            }

            // delete scene
            List<string> existingABPaths = ASUtilities.GetExistingAssetBundles(ASBuildConstants.k_SceneABPath);
            foreach (var abPath in existingABPaths)
            {
                string abGuid = Path.GetFileNameWithoutExtension(abPath);
                if (sceneMap.ContainsKey(abGuid))
                    continue;

                File.Delete(abPath);

                string manifestPath = abPath + ".manifest";
                if (File.Exists(manifestPath))
                {
                    File.Delete(manifestPath);
                }
            }

            List<string> sceneABLines = new List<string>();
            sceneABLines.Add("scenes");
            sceneABLines.Add((sceneList.Count * 2).ToString());
            foreach (var scenePath in sceneList)
            {
                AssetBundleBuild ab = new AssetBundleBuild();
                ab.assetBundleName = AssetDatabase.AssetPathToGUID(scenePath) + ".abas";
                ab.assetNames = new string[] { scenePath };

                sceneABLines.Add(scenePath);
                sceneABLines.Add(AssetDatabase.AssetPathToGUID(scenePath));

                abs.Add(ab);
            }
            sceneABLines.Add((sceneListAll.Count * 2).ToString());
            foreach (var scenePath in sceneListAll)
            {
                sceneABLines.Add(scenePath);
                sceneABLines.Add(AssetDatabase.AssetPathToGUID(scenePath));
            }
            File.WriteAllText(Path.Combine(k_AutoStreamingAbLutDir, "scenes"), string.Join("\n", sceneABLines.ToArray()));

            string sceneABDir = ASUtilities.GetPlatformSpecificResourcePath(ASBuildConstants.k_SceneABPath);
            Directory.CreateDirectory(sceneABDir);

            var buildABOption = forceRebuild ? BuildAssetBundleOptions.ForceRebuildAssetBundle : BuildAssetBundleOptions.None;

            if (forceRebuild)
            {
                if (Directory.Exists(sceneABDir))
                {
                    FileUtil.DeleteFileOrDirectory(sceneABDir);
                }
            }
            Directory.CreateDirectory(sceneABDir);
            // TODO: LZ:
            //      for debug
            buildABOption = (buildABOption | BuildAssetBundleOptions.DisableWriteTypeTree);
            BuildPipeline.BuildAssetBundles(sceneABDir, abs.ToArray(), buildABOption, EditorUserBuildSettings.activeBuildTarget);

            // restore
            PlayerSettings.autoStreaming = originalAutoStreaming;

            AssetDatabase.Refresh();
            GUIUtility.ExitGUI();
        }
    }
}

#endif  //  IG_C301 || IG_C302 || IG_C303 || TUANJIE_2022_3_OR_NEWER, Auto generated by AddMacroForInstantGameFiles.exe