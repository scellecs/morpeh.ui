namespace Morpeh {
    using UnityEngine;
    using UnityEngine.UI;

    [System.Serializable]
    public struct ToggleGroupComponent : IMonoComponent<ToggleGroup> {
        public ToggleGroup ToggleGroup;

        public ToggleGroup monoComponent {
            get { return this.ToggleGroup; }
            set { this.ToggleGroup = value; }
        }
    }
}