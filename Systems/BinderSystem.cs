using System;
using System.Globalization;
using DG.Tweening;
using Morpeh;
using Morpeh.Globals;
using Morpeh.UI.Components;
using TMPro;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(BinderSystem))]
public sealed class BinderSystem : UpdateSystem {
    private Filter filter;
    private Filter filterInitialization;

    public override void OnAwake() {
        this.filter = this.World.Filter.With<BinderComponent>().With<BinderInitializedMarker>();
        this.filterInitialization = this.World.Filter.With<BinderComponent>().Without<BinderInitializedMarker>();
    }

    public override void OnUpdate(float deltaTime) {
        string GetLastString(BaseGlobal bg) {
            switch (bg) {
                case GlobalEvent globalEvent:
                    return globalEvent.BatchedChanges.Peek().ToString(CultureInfo.InvariantCulture);
                case GlobalEventBool globalEventBool:
                    return globalEventBool.BatchedChanges.Peek().ToString(CultureInfo.InvariantCulture);
                case GlobalEventFloat globalEventFloat:
                    return globalEventFloat.BatchedChanges.Peek().ToString(CultureInfo.InvariantCulture);
                case GlobalEventInt globalEventInt:
                    return globalEventInt.BatchedChanges.Peek().ToString(CultureInfo.InvariantCulture);
                case GlobalEventObject globalEventObject:
                    return globalEventObject.BatchedChanges.Peek().ToString();
                case GlobalEventSceneReference globalEventSceneReference:
                    return globalEventSceneReference.BatchedChanges.Peek().ToString();
                case GlobalEventString globalEventString:
                    return globalEventString.BatchedChanges.Peek();
                case GlobalVariableBool globalVariableBool:
                    return globalVariableBool.BatchedChanges.Peek().ToString();
                case GlobalVariableFloat globalVariableFloat:
                    return globalVariableFloat.BatchedChanges.Peek().ToString(CultureInfo.InvariantCulture);
                case GlobalVariableInt globalVariableInt:
                    return globalVariableInt.BatchedChanges.Peek().ToString(CultureInfo.InvariantCulture);
                case GlobalVariableString globalVariableString:
                    return globalVariableString.BatchedChanges.Peek();
                case GlobalVariableBigNumber globalVariableBigNumber:
                    return globalVariableBigNumber.BatchedChanges.Peek().ToString();
                default:
                    throw new ArgumentOutOfRangeException(nameof(bg));
            }
        }
        string GetLastStringValue(BaseGlobal bg) {
            switch (bg) {
                case GlobalVariableBool globalVariableBool:
                    return globalVariableBool.Value.ToString();
                case GlobalVariableFloat globalVariableFloat:
                    return globalVariableFloat.Value.ToString(CultureInfo.InvariantCulture);
                case GlobalVariableInt globalVariableInt:
                    return globalVariableInt.Value.ToString(CultureInfo.InvariantCulture);
                case GlobalVariableString globalVariableString:
                    return globalVariableString.Value;
                case GlobalVariableBigNumber globalVariableBigNumber:
                    return globalVariableBigNumber.Value.ToString();
                default:
                    throw new ArgumentOutOfRangeException(nameof(bg));
            }
        }
        
        Sprite GetLastSprite(BaseGlobal bg) {
            switch (bg) {
                case GlobalEventObject globalEventObject:
                    return (Sprite)globalEventObject.BatchedChanges.Peek();
                default:
                    throw new ArgumentOutOfRangeException(nameof(bg));
            }
        }

        Sprite GetLastSpriteValue(BaseGlobal bg) {
            switch (bg) {
                case GlobalVariableObject globalVariableObject:
                    return (Sprite)globalVariableObject.Value;
                default:
                    throw new ArgumentOutOfRangeException(nameof(bg));
            }
        }

        float GetLastFloat(BaseGlobal bg) {
            switch (bg) {
                case GlobalEvent globalEvent:
                    return globalEvent.BatchedChanges.Peek();
                case GlobalEventFloat globalEventFloat:
                    return globalEventFloat.BatchedChanges.Peek();
                case GlobalEventInt globalEventInt:
                    return globalEventInt.BatchedChanges.Peek();
                case GlobalEventString globalEventString:
                    return float.Parse(globalEventString.BatchedChanges.Peek(), CultureInfo.InvariantCulture);
                case GlobalVariableFloat globalVariableFloat:
                    return globalVariableFloat.BatchedChanges.Peek();
                case GlobalVariableInt globalVariableInt:
                    return globalVariableInt.BatchedChanges.Peek();
                case GlobalVariableString globalVariableString:
                    return float.Parse(globalVariableString.BatchedChanges.Peek(), CultureInfo.InvariantCulture);
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
            switch (binder.target) {
                case TextMeshProUGUI textTMP:
                    textTMP.text = GetLastStringValue(binder.source);
                    break;
                case Text simpleText:
                    simpleText.text = GetLastStringValue(binder.source);
                    break;
                case Slider slider:
                    slider.value = GetLastFloatValue(binder.source);
                    break;
                case Image image:
                    if (binder.source.GetType() == typeof(GlobalVariableObject)) {
                        image.sprite = GetLastSpriteValue(binder.source);
                    }
                    else {
                        var newValue = GetLastFloatValue(binder.source);
                        var currentValue = image.fillAmount;

                        if (currentValue > newValue)
                            image.DOFillAmount(1, 0.5f).OnComplete(() => image.fillAmount = 0);
                        else
                            image.DOFillAmount(newValue, 0.5f);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(binder.target));
            }
            entity.AddComponent<BinderInitializedMarker>();
        }
    
        foreach (var entity in this.filter) {
            ref var binder = ref entity.GetComponent<BinderComponent>();
            if (binder.source.IsPublished) {
                switch (binder.target) {
                    case TextMeshProUGUI textTMP:
                        textTMP.text = GetLastString(binder.source);
                        break;
                    case Text simpleText:
                        simpleText.text = GetLastString(binder.source);
                        break;
                    case Slider slider:
                        slider.value = GetLastFloat(binder.source);
                        break;
                    case Image image:
                        if (binder.source.GetType() == typeof(GlobalEventObject)) {
                            image.sprite = GetLastSprite(binder.source);
                        }
                        else
                        {
                            var newValue = GetLastFloat(binder.source);
                            var currentValue = image.fillAmount;

                            if (currentValue > newValue)
                                image.DOFillAmount(1, 0.5f).OnComplete(() => image.fillAmount = 0);
                            else
                                image.DOFillAmount(newValue, 0.5f);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(binder.target));
                }
            }
        }
    }
}