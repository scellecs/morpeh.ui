namespace Morpeh {
    using UnityEngine;
    using UnityEngine.UI;
    
    [System.Serializable]
    public struct ScrollbarComponent : IMonoComponent<Scrollbar> {
        public Scrollbar Scrollbar;

        public Scrollbar monoComponent {
            get { return this.Scrollbar; }
            set { this.Scrollbar = value; }
        }
    }
}