namespace Morpeh {
    using UnityEngine;
    using TMPro;

    [System.Serializable]
    public struct TextMeshProComponent : IMonoComponent<TextMeshProUGUI> {
        public TextMeshProUGUI Text;

        public TextMeshProUGUI monoComponent {
            get { return this.Text; }
            set { this.Text = value; }
        }
    }
}