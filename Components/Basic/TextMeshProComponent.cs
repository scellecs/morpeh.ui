namespace Morpeh.UI.Components {
    using System;
    using TMPro;

    [Serializable]
    public struct TextMeshProComponent : IMonoComponent<TextMeshProUGUI> {
        public TextMeshProUGUI Text;

        public TextMeshProUGUI monoComponent {
            get => this.Text;
            set => this.Text = value;
        }
    }
}