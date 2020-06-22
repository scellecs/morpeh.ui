using System.Collections.Generic;
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
    private Filter denyShowFullScreenWindows;

    //ToDo: move to separate Entity ???
    private Queue<IEntity> openingQueue;
    
    public override void OnAwake() {
        this.windows = this.World.Filter.With<WindowComponent>().Without<ModalWindowComponent>();
        this.fullScreenWindows = this.World.Filter.With<WindowComponent>().With<ModalWindowComponent>();
        this.openedFullScreenWindow = this.World.Filter.With<WindowComponent>().With<ModalWindowComponent>()
            .With<OpenedModalWindowMarker>();
        this.denyShowFullScreenWindows = this.World.Filter.With<DenyShowModalWindowsComponent>();
        
        this.openingQueue = new Queue<IEntity>();
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

        if (this.denyShowFullScreenWindows.Length != 0)
        {
            foreach (var entity in this.fullScreenWindows)
            {
                if(!this.openingQueue.Contains(entity))
                    this.openingQueue.Enqueue(entity);
            }
            
            return;
        }

        foreach (var entity in this.fullScreenWindows)
        {
            ref var w = ref entity.GetComponent<WindowComponent>();
            if (w.openEvent) {
                if (entity.Has<ForceCloseOthersModalsComponent>())
                {
                    if (this.openedFullScreenWindow.Length != 0)
                    {
                        var opened = this.openedFullScreenWindow.First();
                        opened.RemoveComponent<OpenedModalWindowMarker>();
                        this.SetActive(ref opened.GetComponent<WindowComponent>(), false);
                    }

                    this.SetActiveFullScreen(entity, ref w, true);
                }
                else if (!entity.Has<ForceCloseOthersModalsComponent>() && this.openedFullScreenWindow.Length != 0)
                {
                    this.openingQueue.Enqueue(entity);
                }
                else if (!entity.Has<ForceCloseOthersModalsComponent>() && this.openedFullScreenWindow.Length == 0)
                {
                    this.SetActiveFullScreen(entity, ref w, true);
                }
            }
            else if (w.closeEvent)
            {
                this.SetActiveFullScreen(entity, ref w, false);

                if (this.openingQueue.Count != 0)
                {
                    var entityFromQueue = this.openingQueue.Dequeue();
                    ref var windowFromQueue = ref entityFromQueue.GetComponent<WindowComponent>();
                    this.SetActiveFullScreen(entityFromQueue, ref windowFromQueue, true);
                }
            }
        }
    }

    private void SetActiveFullScreen(IEntity entity, ref WindowComponent w, bool value) {
        if (value)
        {
            entity.SetComponent(new OpenedModalWindowMarker());
            if (entity.Has<EventsToPublishOnWindowShowComponent>())
            {
                ref var component = ref entity.GetComponent<EventsToPublishOnWindowShowComponent>();

                foreach (var @event in component.events)
                {
                    @event.NextFrame();
                }
            }
        }
        else
        {
            entity.RemoveComponent<OpenedModalWindowMarker>();
        }

        this.SetActive(ref w, value);
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