#region Namespaces
#region Unity
using UnityEngine;
using UnityEngine.UI;
#endregion
#endregion

namespace Project.UIExtended
{
    /// <summary>
    /// Extension to Text that auto sets text to parent's name and to fade out color.
    /// </summary>
    public class TextExtended : Text
    {
        [SerializeField]
        public bool InheritParentName = false;              // You can view and change this in the Debug inspector.
        [HideInInspector]
        bool isFading = false;                              // Keeps track of whether fade text is running.

        /// <summary>
        /// Runs on Start.
        /// </summary>
        override protected void Start() { base.Start(); if (InheritParentName) SetText(transform.parent.name); }

        /// <summary>
        /// Cleaner way to do Text.text = string.
        /// </summary>
        /// <param name="_Text">String to set text to.</param>
        public void SetText(string _Text) { text = _Text; }

        /// <summary>
        /// Sets material color alpha.
        /// </summary>
        /// <param name="Alpha">Desired alpha.</param>
        private void SetMaterialColorAlpha(float Alpha) { material.color = new Color(color.r, color.g, color.b, Alpha); }

        /// <summary>
        /// Start fade.
        /// </summary>
        /// <param name="_Text">Desired text.</param>
        public void FadeAway(string _Text) {
            if (isFading) { CancelInvoke(); }

            SetText(_Text);
            SetMaterialColorAlpha(1);
            isFading = true;
            InvokeRepeating("FadeInvoke", .1f, .1f); }

        /// <summary>
        /// Invoke for the actual fade.
        /// </summary>
        void FadeInvoke()
        {
            SetMaterialColorAlpha(material.color.a - .05f);
            if (material.color.a <= 0) {
                isFading = false;
                CancelInvoke();
                enabled = false;
            }
        }
    }
}