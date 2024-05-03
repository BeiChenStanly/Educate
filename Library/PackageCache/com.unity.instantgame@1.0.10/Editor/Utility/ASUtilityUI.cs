﻿#if  IG_C301 || IG_C302 || IG_C303 || TUANJIE_2022_3_OR_NEWER // Auto generated by AddMacroForInstantGameFiles.exe

using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

namespace Unity.AutoStreaming
{
    internal class ASUtilityUI
    {

        internal static readonly GUIContent modelImport = EditorGUIUtility.TrTextContent("Model Importing", "This allow you to set a default material for all imported models and pack it into a AssetBundle to avoid duplicated Unity Standard Shader.");
        internal static readonly GUIContent fontTool = EditorGUIUtility.TrTextContent("Font Tool", "Replace all other font reference to the specified font.");
        internal static readonly GUIContent predownload = EditorGUIUtility.TrTextContent("Predownload", "Pack and Predownload bundles based on scene and custom tags to improve runtime experiences.");


        internal static readonly GUIContent packBundles = EditorGUIUtility.TrTextContent("Pack Pre-download Bundles", "Pack pre-download bundles according to pre-download list/log files.");
        internal static readonly GUIContent generateList = EditorGUIUtility.TrTextContent("Generate Pre-download List for Scenes", "Current only work for active scenes in BuildSettings.");
        internal static readonly GUIContent modelImportDefaultMaterial = EditorGUIUtility.TrTextContent("Default Material", "When ModelImporter.materialImportMode is None, this default material will be used.");
        GUIContent fontGUIContent = new GUIContent("Font to Use", "Replace all fonts in scenes and prefabs with this font.");

        GUIContent reimportModel = new GUIContent("Reimport", "Reimport all model files.");


        private Font m_Font;
        private Material m_mat;

        public ASUtilityUI () 
        {
            m_mat = EditorGraphicsSettings.GetModelImportDefaultMaterial();
        }

        public void OnGUI() 
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.BeginVertical();

            EditorGUILayout.Space();
            OnReimpotModelGUI();

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            OnReplaceFontGUI();
#if IG_C109 || IG_C110
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            OnPredownloadGUI();
#endif   // IG_C109 || IG_C110

            EditorGUILayout.EndVertical();

        }

        void OnReimpotModelGUI()
        {
            GUILayout.Label(modelImport, EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            m_mat = EditorGUILayout.ObjectField(modelImportDefaultMaterial, m_mat, typeof(Material), false) as Material;

            if (GUILayout.Button(reimportModel, GUILayout.Width(80)))
            {
                ModelTools.ReimportWithMaterial(m_mat);
            }
            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();
        }

        void OnReplaceFontGUI()
        {
            GUILayout.Label(fontTool, EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            m_Font = EditorGUILayout.ObjectField(fontGUIContent, m_Font, typeof(Font), false) as Font;
            if (GUILayout.Button("Replace All", GUILayout.Width(80)))
            {
                ReplaceFonts.ReplaceTextFont(m_Font);
            }
            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();
        }
#if IG_C109 || IG_C110
        void OnPredownloadGUI()
        {
            GUILayout.Label(predownload, EditorStyles.boldLabel);
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical();

            if (GUILayout.Button(generateList, GUILayout.Width(300)))
            {
                AutoStreamingSettings.GeneratePredownloadForScenes();
            }
            EditorGUILayout.Space();
            if (GUILayout.Button(packBundles, GUILayout.Width(300)))
            {
                var recordFolder = EditorUtility.OpenFolderPanel("Select Predownload folder", Application.dataPath, string.Empty);
                if (!string.IsNullOrEmpty(recordFolder))
                {
                    UnityEngine.AutoStreaming.PackPredownloadBundles(recordFolder);
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
        }
#endif   // IG_C109 || IG_C110
    }
}

#endif  //  IG_C301 || IG_C302 || IG_C303 || TUANJIE_2022_3_OR_NEWER, Auto generated by AddMacroForInstantGameFiles.exe