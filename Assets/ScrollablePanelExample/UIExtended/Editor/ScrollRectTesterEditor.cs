#region Namespaces
#region Unity
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
#endregion
#region Project
using Project.UIExtended.Tester;
#endregion
#endregion


namespace Project.UIExtended.Editors
{
    /// <summary>
    /// Helper strings for button names.
    /// </summary>
    static class Strings
    {
        public static string Label = "Test Prefabs";
        public static string GO = "Game Object";
        public static string Create = "Create";
        public static string CreateMore = "Create More";
        public static string Remove = "Remove";
        public static string Reset = "Reset";
        public static string First = "First";
        public static string Last = "Last";
        public static string All = "All";
    }

    /// <summary>
    /// Editor Only portion of ScrollRectTester.
    /// </summary>
    [CustomEditor(typeof(ScrollRectTester))]
    public class ScrollRectTesterEditor : Editor
    {
        ScrollRectTester _ScrollRectTester;   // Our target for this script.
        ScrollRect _ScrollRect;             // Scroll Rect we're using for this test.
        GameObject _Prefab;                 // Helpers not reset Prefab GO when going into play mode.

        /// <summary>
        /// Runs OnEnable.
        /// </summary>
        void OnEnable()
        {
            _ScrollRectTester = (ScrollRectTester)target;
            _ScrollRect = _ScrollRectTester.GetComponent<ScrollRect>();
        }

        /// <summary>
        /// Runs OnInspectorGUI.
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical(); EditorGUILayout.Space(); EditorGUILayout.Space();  EditorGUILayout.LabelField(Strings.Label);

            // Draw conditional select prefab section.
            SelectPrefab();

            EditorGUILayout.BeginHorizontal();

            // If Prefab GO Specified, Draw a Create (or Create More) Prefab button
            if (_ScrollRectTester.Prefab) if (GUILayout.Button(((_ScrollRect.content.childCount > 0))? Strings.CreateMore: Strings.Create)) { CreatePrefabs(); if (_ScrollRect.verticalScrollbar) _ScrollRect.verticalScrollbar.value = 0; }

            // Conditional destroy buttons if any children exist in the hierarchy under Scroll Rect content.
            CreateDestroyButtons();

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// Select prefab to be used for creating test objects.
        /// </summary>
        private void SelectPrefab()
        {
            EditorGUILayout.BeginHorizontal();

            // Prefab GO Label
            EditorGUILayout.PrefixLabel(Strings.GO);
            // Prefab GO Selection popup / defining Prefab in MonoBehaviour.
            _Prefab = (GameObject)EditorGUILayout.ObjectField(_ScrollRectTester.Prefab, typeof(GameObject), allowSceneObjects: true);
            if (_Prefab != null && _Prefab != _ScrollRectTester.Prefab) _ScrollRectTester.Prefab = _Prefab;
            // Reset Prefab GO Button
            if (_ScrollRectTester.Prefab) if (GUILayout.Button(Strings.Reset)) _ScrollRectTester.Prefab = null;

            EditorGUILayout.EndHorizontal(); EditorGUILayout.Space();
        }

        /// <summary>
        /// Creates case-dependant buttons to destroy children in ScrollRect Content.
        /// </summary>
        private void CreateDestroyButtons()
        {
            if (_ScrollRect.content.childCount > 0)
            {
                // Layout formatting to keep things nice.
                EditorGUILayout.EndHorizontal(); EditorGUILayout.Space(); EditorGUILayout.LabelField(Strings.Remove); EditorGUILayout.BeginHorizontal();

                // Destroy First, Last (if > 1 child), All, or Reset Everything buttons.
                if (GUILayout.Button(Strings.First)) DestroyPrefab(0);
                if (_ScrollRect.content.transform.childCount > 1) { if (GUILayout.Button(Strings.Last)) { DestroyPrefab(_ScrollRect.content.transform.childCount - 1); } }
                if (GUILayout.Button(Strings.All)) { DestroyPrefabs(); }
                if (GUILayout.Button(Strings.Reset)) { DestroyPrefabs(); _ScrollRectTester.Prefab = null; }
            }
        }

        /// <summary>
        /// Create prefab from prefab GO specified in MonoBehaviour.
        /// </summary>
        private void CreatePrefabs()
        {
            for (int i = 0; i < Random.Range(3, 50); i++)
            {
                GameObject GO = (GameObject)Instantiate(_ScrollRectTester.Prefab);
                GO.transform.SetParent(_ScrollRect.content.transform);
                GO.SetActive(true);
            }
        }

        /// <summary>
        /// Destroy all prefabs attached to "Content" rect transform, as specified in ScrollRect.
        /// </summary>
        private void DestroyPrefabs() { for (int i = _ScrollRect.content.transform.childCount; i > 0; i--) DestroyPrefab(i - 1); }

        /// <summary>
        /// Destroy prefab of childcount index number attached to "Content" rect transform, as specified in ScrollRect.
        /// </summary>
        /// <param name="Idx">Prefab Index Number</param>
        private void DestroyPrefab(int Idx) { if (_ScrollRect.content.GetChild(Idx)) DestroyImmediate(_ScrollRect.content.GetChild(Idx).gameObject); }
    }
}