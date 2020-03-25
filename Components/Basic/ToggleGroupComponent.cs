namespace Morpeh.UI.Components {
    using System;
    using UnityEngine.UI;

    [Serializable]
    public struct ToggleGroupComponent : IMonoComponent<ToggleGroup> {
        public ToggleGroup ToggleGroup;

        public ToggleGroup monoComponent {
            get => this.ToggleGroup;
            set => this.ToggleGroup = value;
        }
    }
}