namespace Morpeh.UI.Components {
    using System;
    using UnityEngine.UI;

    [Serializable]
    public struct RawImageComponent : IMonoComponent<RawImage> {
        public RawImage RawImage;

        public RawImage monoComponent {
            get => this.RawImage;
            set => this.RawImage = value;
        }
    }
}