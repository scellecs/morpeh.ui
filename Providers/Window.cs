using Morpeh;
using Morpeh.UI.Components;
using Sirenix.OdinInspector;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[RequireComponent(typeof(CanvasGroup))]
[HideMonoScript]
public sealed class Window : MonoProvider<WindowComponent> {
    private void OnValidate() {
        ref var data = ref this.GetData();
        data.canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
    }
}