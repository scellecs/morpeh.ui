namespace Morpeh.UI.Components {
    using System;
    using UnityEngine.UI;

    [Serializable]
    public struct ScrollRectComponent : IMonoComponent<ScrollRect> {
        public ScrollRect ScrollRect;

        public ScrollRect monoComponent {
            get => this.ScrollRect;
            set => this.ScrollRect = value;
        }
    }
}