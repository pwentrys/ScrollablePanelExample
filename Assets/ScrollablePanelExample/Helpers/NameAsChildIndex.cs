#region Namespaces
#region Project
using Project.UIExtended;
#endregion
#endregion

namespace Helpers
{
    /// <summary>
    /// Sets name to name + self index in parent child count.
    /// </summary>
    public class NameAsChildIndex : UnityEngine.UI.Button
    {
        /// <summary>
        /// Runs on start.
        /// </summary>
        protected override void Start() { if (name[name.Length-1] == ')') name = string.Format("{0}{1}", name.Replace("(Clone)", string.Empty), transform.GetSiblingIndex()); }

        /// <summary>
        /// OnClick listener hard code.
        /// </summary>
        /// <param name="pointData">Passed data from IPointerClick interface.</param>
        public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData pointData) { Modal.Instance.FadeAway(name); }
    }
}