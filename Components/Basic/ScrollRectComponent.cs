namespace Morpeh {
    using UnityEngine;
    using UnityEngine.UI;
    

    [System.Serializable]
    public struct ScrollRectComponent : IMonoComponent<ScrollRect> {
        public ScrollRect ScrollRect;

        public ScrollRect monoComponent {
            get { return this.ScrollRect; }
            set { this.ScrollRect = value; }
        }
    }
}