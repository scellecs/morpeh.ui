namespace Morpeh.UI.Components {
    using System;
    using UnityEngine.UI;

    [Serializable]
    public struct TextComponent : IMonoComponent<Text> {
        public Text Text;

        public Text monoComponent {
            get => this.Text;
            set => this.Text = value;
        }
    }
}