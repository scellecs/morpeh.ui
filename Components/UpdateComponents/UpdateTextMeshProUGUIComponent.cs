namespace Morpeh.UI.Components.UpdateComponents {
    using System.Collections.Generic;
    using TMPro;
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [System.Serializable]
    public struct UpdateTextMeshProUGUIComponent : IComponent {
        public string value;
        public List<TextMeshProUGUI> tmps;
    }
}