using Morpeh;
using Morpeh.UI;
using Morpeh.UI.Components;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;



[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(WindowSystem))]
public sealed class WindowSystem : UpdateSystem {
    private Filter windows;
    private Filter fullScreenWindows;
    private Filter openedFullScreenWindow;
    
    public override void OnAwake() {
        this.windows = this.World.Filter.With<WindowComponent>().Without<FullScreenWindowComponent>();
        this.fullScreenWindows = this.World.Filter.With<WindowComponent>().With<FullScreenWindowComponent>();
        this.openedFullScreenWindow = this.World.Filter.With<WindowComponent>().With<FullScreenWindowComponent>()
            .With<OpenedFullScreenWindowMarker>();
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var entity in this.windows) {
            ref var w = ref entity.GetComponent<WindowComponent>();
            if (w.openEvent) {
                this.SetActive(ref w, true);
            }
            else if (w.closeEvent)
            {
                this.SetActive(ref w, false);
            }
        }

        foreach (var entity in this.fullScreenWindows)
        {
            ref var w = ref entity.GetComponent<WindowComponent>();
            if (w.openEvent) {
                if (entity.Has<ForceCloseOthersFullScreenComponent>() && this.openedFullScreenWindow.Length != 0)
                {
                    var opened = this.openedFullScreenWindow.First();
                    opened.RemoveComponent<OpenedFullScreenWindowMarker>();
                    this.SetActive(ref opened.GetComponent<WindowComponent>(), false);
                    
                    entity.SetComponent(new OpenedFullScreenWindowMarker());
                    this.SetActive(ref w, true);
                }
                else if (!entity.Has<ForceCloseOthersFullScreenComponent>() && this.openedFullScreenWindow.Length != 0)
                {
                    //ToDo: add to queue
                }
                else if (!entity.Has<ForceCloseOthersFullScreenComponent>() && this.openedFullScreenWindow.Length == 0)
                {
                    entity.SetComponent(new OpenedFullScreenWindowMarker());
                    this.SetActive(ref w, true);
                }
            }
            else if (w.closeEvent)
            {
                entity.RemoveComponent<OpenedFullScreenWindowMarker>();
                this.SetActive(ref w, false);
                
                //ToDo: check queue and show first
            }
        }
    }

    private void SetActive(ref WindowComponent w, bool value) {
        w.canvasGroup.alpha = value ? 1f : 0f;
        w.canvasGroup.interactable = value;
        w.canvasGroup.blocksRaycasts = value;
        if (w.raycaster != null) {
            w.raycaster.enabled = value;
        }
    }
}