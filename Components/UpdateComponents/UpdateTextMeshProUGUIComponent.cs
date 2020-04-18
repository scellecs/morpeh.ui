namespace Morpeh.UI.Components {
    using Unity.IL2CPP.CompilerServices;
    using TMPro;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [System.Serializable]
    public struct UpdateTextMeshProUGUIComponent : IComponent {
        public TextMeshProUGUI tmp;
        public string value;
    }
}