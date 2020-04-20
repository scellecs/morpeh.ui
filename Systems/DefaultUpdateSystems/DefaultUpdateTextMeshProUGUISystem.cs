namespace Morpeh.UI {
    using Morpeh.UI.Components;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DefaultUpdateTextMeshProUGUISystem))]
    public sealed class DefaultUpdateTextMeshProUGUISystem : UpdateSystem {
        private Filter filter;

        public override void OnAwake() {
            this.filter = this.World.Filter.With<UpdateTextMeshProUGUIComponent>();
        }

        public override void OnUpdate(float deltaTime) {
            foreach (var entity in this.filter)
            {
                ref var component = ref entity.GetComponent<UpdateTextMeshProUGUIComponent>();
                component.tmp.text = component.value;
                entity.RemoveComponent<UpdateTextMeshProUGUIComponent>();
            }
        }
    }
}