namespace Morpeh.UI.Components {
    using System;
    using UnityEngine.UI;

    [Serializable]
    public struct SelectableComponent : IMonoComponent<Selectable> {
        public Selectable Selectable;

        public Selectable monoComponent {
            get => this.Selectable;
            set => this.Selectable = value;
        }
    }
}