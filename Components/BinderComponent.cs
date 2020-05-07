namespace Morpeh.UI.Components {
    using Globals;
#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [System.Serializable]
    public struct BinderComponent : IComponent {
#if ODIN_INSPECTOR
        [Required]
#endif
        public Component  target;
#if ODIN_INSPECTOR
        [Required]
#endif
        public BaseGlobal source;
    }
}