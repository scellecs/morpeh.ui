namespace Morpeh.UI.Components {
    using System;
    using UnityEngine.UI;

    [Serializable]
    public struct RectMask2DComponent : IMonoComponent<RectMask2D> {
        public RectMask2D RectMask2D;

        public RectMask2D monoComponent {
            get => this.RectMask2D;
            set => this.RectMask2D = value;
        }
    }
}