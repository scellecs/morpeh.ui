namespace Morpeh.UI.Components {
    using System;
    using UnityEngine.UI;

    [Serializable]
    public struct MaskComponent : IMonoComponent<Mask> {
        public Mask Mask;

        public Mask monoComponent {
            get => this.Mask;
            set => this.Mask = value;
        }
    }
}