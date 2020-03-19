using Morpeh;
using Morpeh.UI.Components;
using Sirenix.OdinInspector;
using TMPro;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[HideMonoScript]
public sealed class Binder : MonoProvider<BinderComponent> {
    private void OnValidate() {
        ref var data = ref this.GetData();
        if (data.target != null) {
            return;
        }
        data.target = this.GetComponent<Text>();
        if (data.target != null) {
            return;
        }
        data.target = this.GetComponent<TextMeshProUGUI>();
        if (data.target != null) {
            return;
        }
        data.target = this.GetComponent<Slider>();
    }
}