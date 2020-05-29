using System;
using System.Collections.Generic;
using System.Globalization;
using Morpeh;
using Morpeh.Globals;
using Morpeh.Globals.ECS;
using Morpeh.UI.Components;
using Morpeh.UI.Components.UpdateComponents;
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
        
        this.filterTMP = this.World.Filter.With<GlobalEventMarker>().With<UpdateTextMeshProUGUIComponent>().With<GlobalEventPublished>();
        this.filterText = this.World.Filter.With<GlobalEventMarker>().With<UpdateTextComponent>().With<GlobalEventPublished>();
        this.filterImage = this.World.Filter.With<GlobalEventMarker>().With<UpdateImageComponent>().With<GlobalEventPublished>();
        this.filterImageFillAmount = this.World.Filter.With<GlobalEventMarker>().With<UpdateImageFillAmountComponent>().With<GlobalEventPublished>();
        this.filterSlider = this.World.Filter.With<GlobalEventMarker>().With<UpdateSliderComponent>().With<GlobalEventPublished>();
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
            
            var sourceEntity = binder.source.Entity;
            switch (target) {
                case TextMeshProUGUI textTMP:
                    ref var tmpc = ref sourceEntity.GetComponent<UpdateTextMeshProUGUIComponent>(out var hasTMP);
                    if (hasTMP) {
                        tmpc.tmps.Add(textTMP);
                    }
                    else {
                        sourceEntity.SetComponent(new UpdateTextMeshProUGUIComponent{ tmps = new List<TextMeshProUGUI>{textTMP} });
                    }
                    break;
                case Text simpleText:
                    ref var tc = ref sourceEntity.GetComponent<UpdateTextComponent>(out var hasT);
                    if (hasT) {
                        tc.texts.Add(simpleText);
                    }
                    else {
                        sourceEntity.SetComponent(new UpdateTextComponent{ texts = new List<Text>{simpleText} });
                    }
                    break;
                case Slider slider:
                    ref var sc = ref sourceEntity.GetComponent<UpdateSliderComponent>(out var hasS);
                    if (hasS) {
                        sc.sliders.Add(slider);
                    }
                    else {
                        sourceEntity.SetComponent(new UpdateSliderComponent{ sliders = new List<Slider>{slider} });
                    }
                    break;
                case Image image:
                    if (source is GlobalVariableObject) {
                        ref var ic = ref sourceEntity.GetComponent<UpdateImageComponent>(out var hasI);
                        if (hasI) {
                            ic.images.Add(image);
                        }
                        else {
                            sourceEntity.SetComponent(new UpdateImageComponent{ images = new List<Image>{image} });
                        }
                    }
                    else {
                        ref var ic = ref sourceEntity.GetComponent<UpdateImageFillAmountComponent>(out var hasI);
                        if (hasI) {
                            ic.images.Add(image);
                        }
                        else {
                            sourceEntity.SetComponent(new UpdateImageFillAmountComponent{ images = new List<Image>{image} });
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(target));
            }
            
            if (source is IDataVariable dv) {
                switch (target) {
                    case TextMeshProUGUI _:
                        ref var tmpc = ref sourceEntity.GetComponent<UpdateTextMeshProUGUIComponent>();
                        tmpc.value = dv.Wrapper.ToString();
                        sourceEntity.SetComponent(new UpdateMarker());
                        break;
                    case Text _:
                        ref var tc = ref sourceEntity.GetComponent<UpdateTextComponent>();
                        tc.value = dv.Wrapper.ToString();
                        sourceEntity.SetComponent(new UpdateMarker());
                        break;
                    case Slider _:
                        ref var sc = ref sourceEntity.GetComponent<UpdateSliderComponent>();
                        sc.value = GetLastFloatValue(source);
                        sourceEntity.SetComponent(new UpdateMarker());
                        break;
                    case Image _:
                        if (source is GlobalVariableObject) {
                            ref var ic = ref sourceEntity.GetComponent<UpdateImageComponent>();
                            ic.value = GetLastSpriteValue(source);
                            sourceEntity.SetComponent(new UpdateMarker());
                        }
                        else {
                            ref var ifac = ref sourceEntity.GetComponent<UpdateImageFillAmountComponent>();
                            ifac.value = GetLastFloatValue(source);
                            sourceEntity.SetComponent(new UpdateMarker());
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(target));
                }
            }
            
            entity.AddComponent<BinderInitializedMarker>();
        }

        var toStringBag = this.filterTMP.Select<GlobalEventLastToString>();
        var tmpBag = this.filterTMP.Select<UpdateTextMeshProUGUIComponent>();

        for (int i = 0, length = this.filterTMP.Length; i < length; i++) {
            ref var ts = ref toStringBag.GetComponent(i);
            ref var tmp = ref tmpBag.GetComponent(i);

            tmp.value = ts.LastToString();
            this.filterTMP.GetEntity(i).SetComponent(new UpdateMarker());
        }
        
        toStringBag = this.filterText.Select<GlobalEventLastToString>();
        var textBag = this.filterText.Select<UpdateTextComponent>();

        for (int i = 0, length = this.filterText.Length; i < length; i++) {
            ref var ts  = ref toStringBag.GetComponent(i);
            ref var tmp = ref textBag.GetComponent(i);

            tmp.value = ts.LastToString();
            this.filterText.GetEntity(i).SetComponent(new UpdateMarker());
        }
        
        toStringBag = this.filterSlider.Select<GlobalEventLastToString>();
        var sliderBag      = this.filterSlider.Select<UpdateSliderComponent>();

        for (int i = 0, length = this.filterSlider.Length; i < length; i++) {
            ref var ts  = ref toStringBag.GetComponent(i);
            ref var tmp = ref sliderBag.GetComponent(i);

            tmp.value = float.Parse(ts.LastToString(), CultureInfo.InvariantCulture);
            this.filterSlider.GetEntity(i).SetComponent(new UpdateMarker());
        }
        
        toStringBag = this.filterImageFillAmount.Select<GlobalEventLastToString>();
        var imageBag = this.filterImageFillAmount.Select<UpdateImageFillAmountComponent>();

        for (int i = 0, length = this.filterImageFillAmount.Length; i < length; i++) {
            ref var ts  = ref toStringBag.GetComponent(i);
            ref var tmp = ref imageBag.GetComponent(i);

            tmp.value = float.Parse(ts.LastToString(), CultureInfo.InvariantCulture);
            
            this.filterImageFillAmount.GetEntity(i).SetComponent(new UpdateMarker());
        }
        
        var spriteBag = this.filterImage.Select<GlobalEventComponent<Object>>();
        var imageSpriteBag = this.filterImage.Select<UpdateImageComponent>();

        for (int i = 0, length = this.filterImage.Length; i < length; i++) {
            ref var sprite  = ref spriteBag.GetComponent(i);
            ref var tmp = ref imageSpriteBag.GetComponent(i);

            tmp.value = (Sprite)sprite.Data.Peek();
            this.filterImage.GetEntity(i).SetComponent(new UpdateMarker());
        }
    }
}