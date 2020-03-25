namespace Morpeh
{
    using UnityEngine;
    using UnityEngine.UI;

    [System.Serializable]
    public struct RawImageComponent : IMonoComponent<RawImage>
    {
        public RawImage RawImage;

        public RawImage monoComponent
        {
            get { return this.RawImage; }
            set { this.RawImage = value; }
        }
    }
}
