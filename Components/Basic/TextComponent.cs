namespace Morpeh {
    using UnityEngine;
    using UnityEngine.UI;

    [System.Serializable]
    public struct TextComponent : IMonoComponent<Text> {
        public Text Text;

        public Text monoComponent {
            get { return this.Text; }
            set { this.Text = value; }
        }
    }
}