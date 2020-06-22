namespace Morpeh.UI {
    using Morpeh;
    using Unity.IL2CPP.CompilerServices;
    using System.Collections.Generic;
    using Morpeh.Globals;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [System.Serializable]
    public struct EventsToPublishOnWindowShowComponent : IComponent {
        public List<GlobalEvent> events;
    }
}