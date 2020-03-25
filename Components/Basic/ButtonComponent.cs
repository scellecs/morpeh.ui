namespace Morpeh {
    using UnityEngine;
    using UnityEngine.UI;

    [System.Serializable]
    public struct ButtonComponent : IMonoComponent<Button> {
        public Button Button;

        public Button monoComponent {
            get { return this.Button; }
            set { this.Button = value; }
        }
    }
}
