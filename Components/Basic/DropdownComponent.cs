namespace Morpeh.UI.Components {
    using System;
    using UnityEngine.UI;

    [Serializable]
    public struct DropdownComponent : IMonoComponent<Dropdown> {
        public Dropdown Dropdown;

        public Dropdown monoComponent {
            get => this.Dropdown;
            set => this.Dropdown = value;
        }
    }
}