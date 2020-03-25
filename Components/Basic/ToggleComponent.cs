namespace Morpeh {
    using UnityEngine;
    using UnityEngine.UI;

    [System.Serializable]
    public struct ToggleComponent : IMonoComponent<Toggle> {
        public Toggle Toggle;

        public Toggle monoComponent {
            get { return this.Toggle; }
            set { this.Toggle = value; }
        }
    }
}