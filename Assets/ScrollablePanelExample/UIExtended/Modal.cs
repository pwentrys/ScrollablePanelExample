#region Namespaces
#region Unity
using UnityEngine;
using UnityEngine.EventSystems;
#endregion
#region Project
using Project.UIExtended;
#endregion
#endregion

namespace Project.UIExtended
{
    /// <summary>
    /// Used to display popup for debugging.
    /// </summary>
    public class Modal : MonoBehaviour
    {
        public static Modal Instance;                           // For easy global calling.

        protected TextExtended m_TextExtended;                  // Text extended we're talking to. Located in child GO.

        /// <summary>
        /// Runs on Enable.
        /// </summary>
        void OnEnable()
        {
            if (!Instance) Instance = this;
            if (!FindObjectOfType<EventSystem>()) { var GO = new GameObject(); GO.name = "EventSystem"; GO.AddComponent<EventSystem>(); GO.AddComponent<StandaloneInputModule>();GO.AddComponent<TouchInputModule>(); }
        }

        /// <summary>
        /// Fades text away.
        /// </summary>
        /// <param name="_DisplayText">Text we want to show.</param>
        public void FadeAway(string _DisplayText)
        {
            if (!m_TextExtended) m_TextExtended = GetComponentInChildren<TextExtended>();
            m_TextExtended.enabled = true;
            m_TextExtended.FadeAway(_DisplayText);
        }
    }
}