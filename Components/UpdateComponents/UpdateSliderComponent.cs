namespace Morpeh.UI.Components {
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine.UI;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [System.Serializable]
    public struct UpdateSliderComponent : IComponent {
        public Slider slider;
        public float value;
    }
}