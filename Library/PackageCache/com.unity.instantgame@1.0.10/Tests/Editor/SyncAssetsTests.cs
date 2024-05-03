using UnityEngine;
using NUnit.Framework;
using UnityEditor.SceneManagement;

namespace UnityEditor.InstantGame.Tests
{

    [Description("Tests suite related to sync all supported autostreaming assets in projects")]
    class SyncAssetsTests
    {
        const string k_RootFolder = "Assets";
        const string k_TestFolder = "SyncAssetsTests";
        const string k_TestFolderPath = k_RootFolder + "/" + k_TestFolder;
        const string k_TestScenePath = k_TestFolderPath + "/SyncAssetsTests.scene";
        const string k_TestPrefabPath = "Packages/com.unity.instantgame/Tests/Editor/TestAssets/TestSyncAssets.prefab";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            if (!AssetDatabase.IsValidFolder(k_TestFolderPath))
                AssetDatabase.CreateFolder(k_RootFolder, k_TestFolder);
            Assume.That(AssetDatabase.IsValidFolder(k_TestFolderPath));

            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            EditorSceneManager.SaveScene(scene, k_TestScenePath);


            var prefab = AssetDatabase.LoadAssetAtPath(k_TestPrefabPath, typeof(GameObject));
            Assume.That(prefab != null);

            PrefabUtility.InstantiatePrefab(prefab, scene);

            EditorSceneManager.SaveScene(scene, k_TestScenePath);
        }

        [Test]
        public void ShouldBeEmptyBeforeSceneAddToBuild()
        {
            Assume.That(EditorBuildSettings.scenes.Length == 0);

            AutoStreamingSettings.SyncScenes();
            Assert.That(AutoStreamingSettings.scenes.Length == 0, "Scenes should be empty!");

            AutoStreamingSettings.SyncTextures(false);
            Assert.That(AutoStreamingSettings.textures.Length == 0, "Textures should be empty!");

            AutoStreamingSettings.SyncAudios();
            Assert.That(AutoStreamingSettings.audios.Length == 0, "Audios should be empty!");

            AutoStreamingSettings.SyncMeshes();
            Assert.That(AutoStreamingSettings.meshes.Length == 0, "Meshes should be empty!");

            AutoStreamingSettings.SyncAnimations();
            Assert.That(AutoStreamingSettings.animations.Length == 0, "Animations should be empty!");
        }

        [Test]
        public void ShouldBeNonEmptyAfterSceneAddToBuild()
        {

            EditorBuildSettings.scenes = new EditorBuildSettingsScene[]{new EditorBuildSettingsScene(k_TestScenePath, true) };

            Assume.That(EditorBuildSettings.scenes.Length == 1);

            AutoStreamingSettings.SyncScenes();
            Assert.That(AutoStreamingSettings.scenes.Length == 1, "Scenes is empty!");

            AutoStreamingSettings.SyncTextures(false);
            Assert.That(AutoStreamingSettings.textures.Length == 1, "Textures is empty!");

            AutoStreamingSettings.SyncAudios();
            Assert.That(AutoStreamingSettings.audios.Length == 1, "Audios is empty!");

            AutoStreamingSettings.SyncMeshes();
            Assert.That(AutoStreamingSettings.meshes.Length == 1, "Meshes is empty!");

            AutoStreamingSettings.SyncAnimations();
            Assert.That(AutoStreamingSettings.animations.Length == 1, "Animations is empty!");
        }


        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            EditorBuildSettings.scenes = new EditorBuildSettingsScene[] { };

            if (AssetDatabase.IsValidFolder(k_TestFolderPath))
                AssetDatabase.DeleteAsset(k_TestFolderPath);
        }
    }

}