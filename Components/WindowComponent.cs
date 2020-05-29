namespace Morpeh.UI.Components {
    using Globals;
    using Morpeh;
    using Sirenix.OdinInspector;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [System.Serializable]
    public struct WindowInitializedMarker : IComponent {
    }

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [System.Serializable]
    public struct WindowComponent : IComponent {
        [Header("Components")]
        [Required]
        public CanvasGroup canvasGroup;
        [Space]
        [Header("Events")]
        public GlobalEvent openEvent;
        public GlobalEvent closeEvent;

        [HideInInspector]
        public GraphicRaycaster raycaster;
    }
}