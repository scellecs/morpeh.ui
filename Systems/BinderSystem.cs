using System;
using System.Globalization;
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
            if (binder.source is IDataVariable dv) {
                switch (binder.target) {
                    case TextMeshProUGUI textTMP:
                            this.World.CreateEntity().SetComponent(new UpdateTextMeshProUGUIComponent {
                                tmp   = textTMP,
                                value = dv.Wrapper.ToString()
                            });
                        break;
                    case Text simpleText:
                        this.World.CreateEntity().SetComponent(new UpdateTextComponent {
                            text  = simpleText,
                            value = dv.Wrapper.ToString()
                        });
                        break;
                    case Slider slider:
                        this.World.CreateEntity().SetComponent(new UpdateSliderComponent {
                            slider = slider,
                            value  = GetLastFloatValue(binder.source)
                        });
                        break;
                    case Image image:
                        if (binder.source is GlobalVariableObject) {
                            this.World.CreateEntity().SetComponent(new UpdateImageComponent {
                                image = image,
                                value = GetLastSpriteValue(binder.source)
                            });
                        }
                        else {
                            this.World.CreateEntity().SetComponent(new UpdateImageFillAmountComponent {
                                image = image,
                                value = GetLastFloatValue(binder.source)
                            });
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(binder.target));
                }
            }

            entity.AddComponent<BinderInitializedMarker>();
        }
    
        foreach (var entity in this.filter) {
            ref var binder = ref entity.GetComponent<BinderComponent>();
            if (binder.source.IsPublished) {
                switch (binder.target) {
                    case TextMeshProUGUI textTMP:
                        this.World.CreateEntity().SetComponent( new UpdateTextMeshProUGUIComponent {
                            tmp = textTMP,
                            value = binder.source.LastToString()
                        });
                        break;
                    case Text simpleText:
                        this.World.CreateEntity().SetComponent( new UpdateTextComponent {
                            text = simpleText,
                            value = binder.source.LastToString()
                        });
                        break;
                    case Slider slider:
                        this.World.CreateEntity().SetComponent( new UpdateSliderComponent {
                            slider = slider,
                            value = GetLastFloat(binder.source)
                        });
                        break;
                    case Image image:
                        if (binder.source is GlobalEventObject) {
                            this.World.CreateEntity().SetComponent( new UpdateImageComponent {
                                image = image,
                                value = GetLastSprite(binder.source)
                            });
                        }
                        else {
                            this.World.CreateEntity().SetComponent( new UpdateImageFillAmountComponent {
                                image = image,
                                value = GetLastFloat(binder.source)
                            });
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(binder.target));
                }
            }
        }
    }
}