#region Namespaces
#region Unity
using UnityEngine;
#endregion
#endregion

namespace Project.UIExtended.Tester
{
    /// <summary>
    /// Used for easy for creating ScrollRect content.
    /// </summary>
    [ExecuteInEditMode, RequireComponent(typeof(UnityEngine.UI.ScrollRect))]
    public class ScrollRectTester : MonoBehaviour
    {
        [SerializeField]
        public GameObject Prefab;               // Our test scrollable piece of content to add.
    }
}