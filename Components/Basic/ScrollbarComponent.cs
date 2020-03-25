namespace Morpeh.UI.Components {
    using System;
    using UnityEngine.UI;

    [Serializable]
    public struct ScrollbarComponent : IMonoComponent<Scrollbar> {
        public Scrollbar Scrollbar;

        public Scrollbar monoComponent {
            get => this.Scrollbar;
            set => this.Scrollbar = value;
        }
    }
}