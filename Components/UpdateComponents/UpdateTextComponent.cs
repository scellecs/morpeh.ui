namespace Morpeh.UI.Components.UpdateComponents {
    using System.Collections.Generic;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine.UI;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [System.Serializable]
    public struct UpdateTextComponent : IComponent {
        public string value;
        public List<Text> texts;
    }
}