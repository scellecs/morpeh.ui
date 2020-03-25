namespace Morpeh.UI.Components {
    using System;
    using UnityEngine.UI;

    [Serializable]
    public struct ImageComponent : IMonoComponent<Image> {
        public Image Image;

        public Image monoComponent {
            get => this.Image;
            set => this.Image = value;
        }
    }
}