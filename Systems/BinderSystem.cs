using System;
using System.Collections.Generic;
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
        
        this.filterTMP = this.World.Filter.With<GlobalEventMarker>().With<BinderTextMeshProComponent>().With<GlobalEventPublished>();
        this.filterText = this.World.Filter.With<GlobalEventMarker>().With<BinderTextComponent>().With<GlobalEventPublished>();
        this.filterImage = this.World.Filter.With<GlobalEventMarker>().With<BinderImageComponent>().With<GlobalEventPublished>();
        this.filterImageFillAmount = this.World.Filter.With<GlobalEventMarker>().With<BinderImageFillAmountComponent>().With<GlobalEventPublished>();
        this.filterSlider = this.World.Filter.With<GlobalEventMarker>().With<BinderSliderComponent>().With<GlobalEventPublished>();
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
                        textTMP.text = dv.Wrapper.ToString();
                        break;
                    case Text simpleText:
                        simpleText.text = dv.Wrapper.ToString();
                        break;
                    case Slider slider:
                        slider.value = GetLastFloatValue(source);
                        break;
                    case Image image:
                        if (source is GlobalVariableObject) {
                            image.sprite = GetLastSpriteValue(source);
                        }
                        else {
                            image.fillAmount = GetLastFloatValue(source);
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(target));
                }
            }

            var sourceEntity = binder.source.Entity;
            switch (target) {
                case TextMeshProUGUI textTMP:
                    ref var tmpc = ref sourceEntity.GetComponent<BinderTextMeshProComponent>(out var hasTMP);
                    if (hasTMP) {
                        tmpc.values.Add(textTMP);
                    }
                    else {
                        sourceEntity.SetComponent(new BinderTextMeshProComponent{ values = new List<TextMeshProUGUI>{textTMP} });
                    }
                    break;
                case Text simpleText:
                    ref var tc = ref sourceEntity.GetComponent<BinderTextComponent>(out var hasT);
                    if (hasT) {
                        tc.values.Add(simpleText);
                    }
                    else {
                        sourceEntity.SetComponent(new BinderTextComponent{ values = new List<Text>{simpleText} });
                    }
                    break;
                case Slider slider:
                    ref var sc = ref sourceEntity.GetComponent<BinderSliderComponent>(out var hasS);
                    if (hasS) {
                        sc.values.Add(slider);
                    }
                    else {
                        sourceEntity.SetComponent(new BinderSliderComponent{ values = new List<Slider>{slider} });
                    }
                    break;
                case Image image:
                    if (source is GlobalVariableObject) {
                        ref var ic = ref sourceEntity.GetComponent<BinderImageComponent>(out var hasI);
                        if (hasI) {
                            ic.values.Add(image);
                        }
                        else {
                            sourceEntity.SetComponent(new BinderImageComponent{ values = new List<Image>{image} });
                        }
                    }
                    else {
                        ref var ic = ref sourceEntity.GetComponent<BinderImageFillAmountComponent>(out var hasI);
                        if (hasI) {
                            ic.values.Add(image);
                        }
                        else {
                            sourceEntity.SetComponent(new BinderImageFillAmountComponent{ values = new List<Image>{image} });
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(target));
            }
            
            
            entity.AddComponent<BinderInitializedMarker>();
        }

        var toStringBag = this.filterTMP.Select<GlobalEventLastToString>();
        var tmpBag = this.filterTMP.Select<BinderTextMeshProComponent>();

        for (int i = 0, length = this.filterTMP.Length; i < length; i++) {
            ref var ts = ref toStringBag.GetComponent(i);
            ref var tmp = ref tmpBag.GetComponent(i);

            var str = ts.LastToString();
            foreach (var t in tmp.values) {
                t.text = str;
            }
        }
        
        toStringBag = this.filterText.Select<GlobalEventLastToString>();
        var textBag = this.filterText.Select<BinderTextComponent>();

        for (int i = 0, length = this.filterText.Length; i < length; i++) {
            ref var ts  = ref toStringBag.GetComponent(i);
            ref var tmp = ref textBag.GetComponent(i);

            var str = ts.LastToString();
            foreach (var t in tmp.values) {
                t.text = str;
            }
        }
        
        toStringBag = this.filterSlider.Select<GlobalEventLastToString>();
        var sliderBag      = this.filterSlider.Select<BinderSliderComponent>();

        for (int i = 0, length = this.filterSlider.Length; i < length; i++) {
            ref var ts  = ref toStringBag.GetComponent(i);
            ref var tmp = ref sliderBag.GetComponent(i);

            var str = float.Parse(ts.LastToString(), CultureInfo.InvariantCulture);
            foreach (var t in tmp.values) {
                t.value = str;
            }
        }
        
        toStringBag = this.filterImageFillAmount.Select<GlobalEventLastToString>();
        var imageBag = this.filterImageFillAmount.Select<BinderImageFillAmountComponent>();

        for (int i = 0, length = this.filterImageFillAmount.Length; i < length; i++) {
            ref var ts  = ref toStringBag.GetComponent(i);
            ref var tmp = ref imageBag.GetComponent(i);

            var str = float.Parse(ts.LastToString(), CultureInfo.InvariantCulture);
            foreach (var t in tmp.values) {
                t.fillAmount = str;
            }
        }
        
        var spriteBag = this.filterImage.Select<GlobalEventComponent<Object>>();
        var imageSpriteBag = this.filterImage.Select<BinderImageComponent>();

        for (int i = 0, length = this.filterImage.Length; i < length; i++) {
            ref var sprite  = ref spriteBag.GetComponent(i);
            ref var tmp = ref imageSpriteBag.GetComponent(i);

            var sprt = (Sprite)sprite.Data.Peek();
            foreach (var t in tmp.values) {
                t.sprite = sprt;
            }
        }
    }
}