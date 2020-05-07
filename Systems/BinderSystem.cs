using System;
using System.Globalization;
using Morpeh;
using Morpeh.Globals;
using Morpeh.Globals.ECS;
using Morpeh.UI.Components;
using TMPro;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(BinderSystem))]
public sealed class BinderSystem : UpdateSystem {
    private Filter filterTMP;
    private Filter filterText;
    private Filter filterImage;
    private Filter filterImageFillAmount;
    private Filter filterSlider;
    
    private Filter filterInitialization;

    public override void OnAwake() {
        this.filterInitialization = this.World.Filter.With<BinderComponent>().Without<BinderInitializedMarker>();
        
        this.filterTMP = this.World.Filter.With<GlobalEventMarker>().With<TextMeshProComponent>().With<GlobalEventPublished>();
        this.filterText = this.World.Filter.With<GlobalEventMarker>().With<TextComponent>().With<GlobalEventPublished>();
        this.filterImage = this.World.Filter.With<GlobalEventMarker>().With<ImageComponent>().Without<BinderImageFillAmountMarker>().With<GlobalEventPublished>();
        this.filterImageFillAmount = this.World.Filter.With<GlobalEventMarker>().With<ImageComponent>().With<BinderImageFillAmountMarker>().With<GlobalEventPublished>();
        this.filterSlider = this.World.Filter.With<GlobalEventMarker>().With<SliderComponent>().With<GlobalEventPublished>();
    }

    public override void OnUpdate(float deltaTime) {
        Sprite GetLastSpriteValue(BaseGlobal bg) {
            switch (bg) {
                case GlobalVariableObject globalVariableObject:
                    return (Sprite)globalVariableObject.Value;
                default:
                    throw new ArgumentOutOfRangeException(nameof(bg));
            }
        }
        float GetLastFloatValue(BaseGlobal bg) {
            switch (bg) {
                case GlobalVariableFloat globalVariableFloat:
                    return globalVariableFloat.Value;
                case GlobalVariableInt globalVariableInt:
                    return globalVariableInt.Value;
                case GlobalVariableString globalVariableString:
                    return float.Parse(globalVariableString.Value, CultureInfo.InvariantCulture);
                default:
                    throw new ArgumentOutOfRangeException(nameof(bg));
            }
        }

        foreach (var entity in this.filterInitialization) {
            ref var binder = ref entity.GetComponent<BinderComponent>();
            var source = binder.source;
            var target = binder.target;
            
            if (source is IDataVariable dv) {
                switch (target) {
                    case TextMeshProUGUI textTMP:
                        entity.SetComponent(new UpdateTextMeshProUGUIComponent {
                                tmp   = textTMP,
                                value = dv.Wrapper.ToString()
                            });
                        break;
                    case Text simpleText:
                        entity.SetComponent(new UpdateTextComponent {
                            text  = simpleText,
                            value = dv.Wrapper.ToString()
                        });
                        break;
                    case Slider slider:
                        entity.SetComponent(new UpdateSliderComponent {
                            slider = slider,
                            value  = GetLastFloatValue(source)
                        });
                        break;
                    case Image image:
                        if (source is GlobalVariableObject) {
                            entity.SetComponent(new UpdateImageComponent {
                                image = image,
                                value = GetLastSpriteValue(source)
                            });
                        }
                        else {
                            entity.SetComponent(new UpdateImageFillAmountComponent {
                                image = image,
                                value = GetLastFloatValue(source)
                            });
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(target));
                }
            }

            var sourceEntity = binder.source.Entity;
            switch (target) {
                case TextMeshProUGUI textTMP:
                    sourceEntity.SetComponent(new TextMeshProComponent{ monoComponent = textTMP });
                    break;
                case Text simpleText:
                    sourceEntity.SetComponent(new TextComponent{ monoComponent = simpleText });
                    break;
                case Slider slider:
                    sourceEntity.SetComponent(new SliderComponent{ monoComponent = slider });
                    break;
                case Image image:
                    sourceEntity.SetComponent(new ImageComponent{ monoComponent = image });
                    if (!(source is GlobalVariableObject)) {
                        sourceEntity.SetComponent(new BinderImageFillAmountMarker());
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(target));
            }
            
            
            entity.AddComponent<BinderInitializedMarker>();
        }

        var toStringBag = this.filterTMP.Select<GlobalEventLastToString>();
        var tmpBag = this.filterTMP.Select<TextMeshProComponent>();

        for (int i = 0, length = this.filterTMP.Length; i < length; i++) {
            ref var ts = ref toStringBag.GetComponent(i);
            ref var tmp = ref tmpBag.GetComponent(i);

            tmp.monoComponent.text = ts.LastToString();
        }
        
        toStringBag = this.filterText.Select<GlobalEventLastToString>();
        var textBag = this.filterText.Select<TextComponent>();

        for (int i = 0, length = this.filterText.Length; i < length; i++) {
            ref var ts  = ref toStringBag.GetComponent(i);
            ref var tmp = ref textBag.GetComponent(i);

            tmp.monoComponent.text = ts.LastToString();
        }
        
        toStringBag = this.filterSlider.Select<GlobalEventLastToString>();
        var sliderBag      = this.filterSlider.Select<SliderComponent>();

        for (int i = 0, length = this.filterSlider.Length; i < length; i++) {
            ref var ts  = ref toStringBag.GetComponent(i);
            ref var tmp = ref sliderBag.GetComponent(i);

            tmp.monoComponent.value = float.Parse(ts.LastToString(), CultureInfo.InvariantCulture);
        }
        
        toStringBag = this.filterImageFillAmount.Select<GlobalEventLastToString>();
        var imageBag = this.filterImageFillAmount.Select<ImageComponent>();

        for (int i = 0, length = this.filterImageFillAmount.Length; i < length; i++) {
            ref var ts  = ref toStringBag.GetComponent(i);
            ref var tmp = ref imageBag.GetComponent(i);

            tmp.monoComponent.fillAmount = float.Parse(ts.LastToString(), CultureInfo.InvariantCulture);
        }
        
        var spriteBag = this.filterImage.Select<GlobalEventComponent<Object>>();
        imageBag = this.filterImage.Select<ImageComponent>();

        for (int i = 0, length = this.filterImage.Length; i < length; i++) {
            ref var sprite  = ref spriteBag.GetComponent(i);
            ref var tmp = ref imageBag.GetComponent(i);

            tmp.monoComponent.sprite = (Sprite)sprite.Data.Peek();
        }
    }
}