namespace Morpeh {
    using UnityEngine;
    using UnityEngine.UI;

    [System.Serializable]
    public struct SelectableComponent : IMonoComponent<Selectable> {
        public Selectable Selectable;

        public Selectable monoComponent {
            get { return this.Selectable; }
            set { this.Selectable = value; }
        }
    }
}
