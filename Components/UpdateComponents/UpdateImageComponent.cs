namespace Morpeh.UI.Components {
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine.UI;
    using UnityEngine;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [System.Serializable]
    public struct UpdateImageComponent : IComponent {
        public Image image;
        public Sprite value;
    }
}