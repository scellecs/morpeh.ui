namespace Morpeh {
    using UnityEngine;
    using UnityEngine.UI;

    [System.Serializable]
    public struct SliderComponent : IMonoComponent<Slider> {
        public Slider Slider;

        public Slider monoComponent {
            get { return this.Slider; }
            set { this.Slider = value; }
        }
    }
}
