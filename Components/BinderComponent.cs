namespace Morpeh.UI.Components {
    using Globals;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [System.Serializable]
    public struct BinderComponent : IComponent {
        public Component  target;
        public BaseGlobal source;
    }
}