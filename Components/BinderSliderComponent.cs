using System.Collections.Generic;
using Morpeh;
using UnityEngine.UI;

[System.Serializable]
public struct BinderSliderComponent : IComponent {
    public List<Slider> values;
}