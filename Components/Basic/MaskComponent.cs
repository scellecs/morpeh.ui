namespace Morpeh
{
    using UnityEngine;
    using UnityEngine.UI;

    [System.Serializable]
    public struct MaskComponent : IMonoComponent<Mask>
    {
        public Mask Mask;

        public Mask monoComponent
        {
            get { return this.Mask; }
            set { this.Mask = value; }
        }
    }
}
