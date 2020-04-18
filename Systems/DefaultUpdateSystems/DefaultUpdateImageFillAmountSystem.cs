namespace Morpeh.UI {
    using Morpeh.UI.Components;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DefaultUpdateImageFillAmountSystem))]
    public sealed class DefaultUpdateImageFillAmountSystem : UpdateSystem {
        private Filter filter;

        public override void OnAwake() {
            this.filter = this.World.Filter.With<UpdateImageFillAmountComponent>();
        }

        public override void OnUpdate(float deltaTime) {
            foreach (var entity in this.filter)
            {
                ref var component = ref entity.GetComponent<UpdateImageFillAmountComponent>();
                component.image.fillAmount = component.value;
                entity.RemoveComponent<UpdateImageFillAmountComponent>();
            }
        }
    }
}