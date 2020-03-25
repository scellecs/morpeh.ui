namespace Morpeh
{
    using UnityEngine;
    using UnityEngine.UI;

    [System.Serializable]
    public struct RectMask2DComponent : IMonoComponent<RectMask2D>
    {
        public RectMask2D RectMask2D;

        public RectMask2D monoComponent
        {
            get { return this.RectMask2D; }
            set { this.RectMask2D = value; }
        }
    }
}
