namespace Morpeh.UI.Components {
    using System;
    using UnityEngine.UI;

    [Serializable]
    public struct ToggleComponent : IMonoComponent<Toggle> {
        public Toggle Toggle;

        public Toggle monoComponent {
            get => this.Toggle;
            set => this.Toggle = value;
        }
    }
}