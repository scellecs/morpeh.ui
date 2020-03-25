namespace Morpeh {
    using UnityEngine;
    using UnityEngine.UI;

    [System.Serializable]
    public struct DropdownComponent : IMonoComponent<Dropdown> {
        public Dropdown Dropdown;

        public Dropdown monoComponent {
            get { return this.Dropdown; }
            set { this.Dropdown = value; }
        }
    }
}
