namespace Morpeh.UI.Components {
    using System;
    using UnityEngine.UI;

    [Serializable]
    public struct ButtonComponent : IMonoComponent<Button> {
        public Button Button;

        public Button monoComponent {
            get => this.Button;
            set => this.Button = value;
        }
    }
}