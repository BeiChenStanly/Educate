#if  IG_C301 || IG_C302 || IG_C303 || TUANJIE_2022_3_OR_NEWER // Auto generated by AddMacroForInstantGameFiles.exe

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace Unity.AutoStreaming
{
    internal class ASSharedSceneAssetTreeDataItem : TreeDataItem
    {
        public AutoStreamingSettingsSharedSceneAsset sharedSceneAssetSettings { get; set; }

        public ASSharedSceneAssetTreeDataItem(AutoStreamingSettingsSharedSceneAsset inSettings, int depth, int id)
            : base(inSettings.assetPath, depth, id)
        {
            sharedSceneAssetSettings = inSettings;
        }

        public ASSharedSceneAssetTreeDataItem(string name, int depth, int id)
            : base(name, depth, id)
        {
        }
    }

    internal class ASSharedSceneAssetsTreeView : TreeViewBaseT<ASSharedSceneAssetTreeDataItem>
    {
        enum MyColumns
        {
            AssetPath,
            RuntimeSize,
            IsSharedAsset,
            References,
            Warnings,
        }
        enum SortOption
        {
            AssetPath,
            RuntimeSize,
            IsSharedAsset,
            References,
        }

        SortOption[] m_SortOptions =
        {
            SortOption.AssetPath,
            SortOption.RuntimeSize,
            SortOption.IsSharedAsset,
            SortOption.References,
        };

        private readonly GUIContent warningIcon = new GUIContent(EditorGUIUtility.IconContent("console.warnicon.sml").image, "Select a fbx file will include all its assets into the shared Assets AB");

        public ASSharedSceneAssetsTreeView(TreeViewState state, MultiColumnHeader multicolumnHeader, TreeModelT<ASSharedSceneAssetTreeDataItem> model) : base(state, multicolumnHeader, model)
        {
            // Custom setup
            rowHeight = k_RowHeights;

            columnIndexForTreeFoldouts = 0;
            showAlternatingRowBackgrounds = true;
            showBorder = true;
            customFoldoutYOffset = (k_RowHeights - EditorGUIUtility.singleLineHeight) * 0.5f; // center foldout in the row since we also center content. See RowGUI

            multicolumnHeader.sortingChanged += OnSortingChanged;
            Reload();
        }

        protected override void SortByMultipleColumns()
        {
            var sortedColumns = multiColumnHeader.state.sortedColumns;

            if (sortedColumns.Length == 0)
                return;

            var myTypes = rootItem.children.Cast<TreeViewItemBaseT<ASSharedSceneAssetTreeDataItem>>();
            var orderedQuery = InitialOrder(myTypes, sortedColumns);
            for (int i = 1; i < sortedColumns.Length; i++)
            {
                SortOption sortOption = m_SortOptions[sortedColumns[i]];
                bool ascending = multiColumnHeader.IsSortedAscending(sortedColumns[i]);

                switch (sortOption)
                {
                    case SortOption.AssetPath:
                        orderedQuery = orderedQuery.ThenBy(l => l.Data.Name, ascending);
                        break;
                    case SortOption.RuntimeSize:
                        orderedQuery = orderedQuery.ThenBy(l => l.Data.sharedSceneAssetSettings.runtimeMemory, ascending);
                        break;
                    case SortOption.IsSharedAsset:
                        orderedQuery = orderedQuery.ThenBy(l => l.Data.sharedSceneAssetSettings.includedInSharedSceneAssetsAB, ascending);
                        break;
                    case SortOption.References:
                        orderedQuery = orderedQuery.ThenBy(l => l.Data.sharedSceneAssetSettings.refs.Length, ascending);
                        break;
                }
            }

            rootItem.children = orderedQuery.Cast<TreeViewItem>().ToList();
        }

        IOrderedEnumerable<TreeViewItemBaseT<ASSharedSceneAssetTreeDataItem>> InitialOrder(IEnumerable<TreeViewItemBaseT<ASSharedSceneAssetTreeDataItem>> myTypes, int[] history)
        {
            SortOption sortOption = m_SortOptions[history[0]];
            bool ascending = multiColumnHeader.IsSortedAscending(history[0]);
            switch (sortOption)
            {
                case SortOption.AssetPath:
                    return myTypes.Order(l => l.Data.Name, ascending);
                case SortOption.RuntimeSize:
                    return myTypes.Order(l => l.Data.sharedSceneAssetSettings.runtimeMemory, ascending);
                case SortOption.IsSharedAsset:
                    return myTypes.Order(l => l.Data.sharedSceneAssetSettings.includedInSharedSceneAssetsAB, ascending);
                case SortOption.References:
                    return myTypes.Order(l => l.Data.sharedSceneAssetSettings.refs.Length, ascending);
                default:
                    Assert.IsTrue(false, "Unhandled enum");
                    break;
            }

            // default
            return myTypes.Order(l => l.Data.Name, ascending);
        }

        public static MultiColumnHeaderState CreateDefaultMultiColumnHeaderState(float treeViewWidth)
        {
            var columns = new[]
            {
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("AssetPath"),
                    headerTextAlignment = TextAlignment.Left,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Center,
                    width = 300,
                    minWidth = 100,
                    autoResize = false,
                    allowToggleVisibility = false
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("RT Mem"),
                    headerTextAlignment = TextAlignment.Left,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Center,
                    width = 70,
                    minWidth = 50,
                    autoResize = false,
                    allowToggleVisibility = false
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("IsSharedAsset"),
                    headerTextAlignment = TextAlignment.Left,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Center,
                    width = 70,
                    minWidth = 50,
                    autoResize = false,
                    allowToggleVisibility = false
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("References"),
                    headerTextAlignment = TextAlignment.Left,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Center,
                    width = 100,
                    minWidth = 100,
                    autoResize = false,
                    allowToggleVisibility = false
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Warning"),
                    headerTextAlignment = TextAlignment.Left,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Center,
                    canSort = false,
                    width = 30,
                    minWidth = 20,
                    autoResize = false,
                    allowToggleVisibility = false
                },
            };

            // Assert.AreEqual(columns.Length, Enum.GetValues(typeof(MyColumns)).Length, "Number of columns should match number of enum values: You probably forgot to update one of them.");

            var state = new MultiColumnHeaderState(columns);
            return state;
        }

        override protected void SingleClickedItem(int id)
        {
            var sharedSceneAssetItems = ASMainWindow.Instance.SharedSceneAssetData;

            if (id < sharedSceneAssetItems.Count)
            {
                var sharedSceneAssetItem = sharedSceneAssetItems[id];
                Selection.activeObject = sharedSceneAssetItem.sharedSceneAssetSettings.obj;
            }
        }

        protected override void SelectionChanged(IList<int> selectedIds)
        {
            if (selectedIds.Count == 0)
                return;
            int id = selectedIds[0];
            var sharedSceneAssetItems = ASMainWindow.Instance.SharedSceneAssetData;

            if (id < sharedSceneAssetItems.Count)
            {
                var sharedSceneAssetItem = sharedSceneAssetItems[id];
                Selection.activeObject = sharedSceneAssetItem.sharedSceneAssetSettings.obj;
            }
        }


        protected override void RowGUI(RowGUIArgs args)
        {
            var item = (TreeViewItemBaseT<ASSharedSceneAssetTreeDataItem>)args.item;

            for (int i = 0; i < args.GetNumVisibleColumns(); ++i)
            {
                CellGUI(args.GetCellRect(i), item, (MyColumns)args.GetColumn(i), ref args);
            }
        }

        void CellGUI(Rect cellRect, TreeViewItemBaseT<ASSharedSceneAssetTreeDataItem> item, MyColumns column, ref RowGUIArgs args)
        {
            // Center cell rect vertically (makes it easier to place controls, icons etc in the cells)
            CenterRectUsingSingleLineHeight(ref cellRect);

            switch (column)
            {
                case MyColumns.AssetPath:
                {
                    string value = item.Data.sharedSceneAssetSettings.assetPath;
                    DefaultGUI.Label(cellRect, value, args.selected, args.focused);
                }
                break;

                case MyColumns.RuntimeSize:
                {
                    string value = EditorUtility.FormatBytes(item.Data.sharedSceneAssetSettings.runtimeMemory);
                    DefaultGUI.Label(cellRect, value, args.selected, args.focused);
                }
                break;

                case MyColumns.IsSharedAsset:
                {
                    // Do toggle
                    Rect toggleRect = cellRect;
                    toggleRect.width = k_ToggleWidth;
                    if (toggleRect.xMax < cellRect.xMax)
                    {
                        bool isEnabled = EditorGUI.Toggle(toggleRect, item.Data.sharedSceneAssetSettings.includedInSharedSceneAssetsAB);
                        if (isEnabled != item.Data.sharedSceneAssetSettings.includedInSharedSceneAssetsAB)
                        {
                            IList<int> ids = GetSelection();
                            if (!ids.Contains(item.id))
                            {
                                var tmpSettings = item.Data.sharedSceneAssetSettings;
                                tmpSettings.includedInSharedSceneAssetsAB = isEnabled;
                            }
                            else
                            {
                                List<ASSharedSceneAssetTreeDataItem> elems = ASMainWindow.Instance.SharedSceneAssetData;
                                foreach (int id in ids)
                                {
                                    var tmpSettings = elems[id].sharedSceneAssetSettings;
                                    tmpSettings.includedInSharedSceneAssetsAB = isEnabled;
                                }
                            }
                        }
                    }
                }
                break;

                case MyColumns.References:
                {
                    string value = string.Join(",", item.Data.sharedSceneAssetSettings.refs.ToArray());
                    DefaultGUI.Label(cellRect, value, args.selected, args.focused);
                }
                break;

                case MyColumns.Warnings:
                    {
                        if (item.Data.sharedSceneAssetSettings.assetPath.ToLower().EndsWith(".fbx"))
                            EditorGUI.LabelField(cellRect, warningIcon);
                    }
                    break;
            }
        }
    }
}

#endif  //  IG_C301 || IG_C302 || IG_C303 || TUANJIE_2022_3_OR_NEWER, Auto generated by AddMacroForInstantGameFiles.exe