namespace Morpeh.UI.Components {
    using System;
    using UnityEngine.UI;

    [Serializable]
    public struct InputFieldComponent : IMonoComponent<InputField> {
        public InputField InputField;

        public InputField monoComponent {
            get => this.InputField;
            set => this.InputField = value;
        }
    }
}