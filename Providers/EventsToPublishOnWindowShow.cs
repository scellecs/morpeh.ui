namespace Morpeh.UI {
    using Morpeh;
    using Sirenix.OdinInspector;
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [HideMonoScript]
    public sealed class EventsToPublishOnWindowShow : MonoProvider<EventsToPublishOnWindowShowComponent> {
    }
}