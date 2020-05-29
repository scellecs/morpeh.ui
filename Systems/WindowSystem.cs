using Morpeh;
using Morpeh.UI.Components;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;



[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(WindowSystem))]
public sealed class WindowSystem : UpdateSystem {
    private Filter filter;
    
    public override void OnAwake() {
        this.filter = this.World.Filter.With<WindowComponent>();
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var entity in this.filter) {
            ref var w = ref entity.GetComponent<WindowComponent>();
            if (w.openEvent) {
                w.canvasGroup.alpha        = 1f;
                w.canvasGroup.interactable = true;
                w.canvasGroup.blocksRaycasts = true;
                if (w.raycaster != null) {
                    w.raycaster.enabled = true;
                }
            }
            else if (w.closeEvent) {
                w.canvasGroup.alpha = 0f;
                w.canvasGroup.interactable = false;
                w.canvasGroup.blocksRaycasts = false;
                if (w.raycaster != null) {
                    w.raycaster.enabled = false;
                }
            }
        }
    }
}