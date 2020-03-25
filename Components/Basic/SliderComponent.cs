namespace Morpeh.UI.Components {
    using System;
    using UnityEngine.UI;

    [Serializable]
    public struct SliderComponent : IMonoComponent<Slider> {
        public Slider Slider;

        public Slider monoComponent {
            get => this.Slider;
            set => this.Slider = value;
        }
    }
}