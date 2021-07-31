using System;
using Abstractions;
using UnityEngine;

namespace UserControlSystem.UI.Model
{
    [CreateAssetMenu(
        fileName = nameof(SelectableValue),
        menuName = "Strategy Game/" + nameof(SelectableValue))]
    public sealed class SelectableValue : ChangeableValueModel<ISelectable>
    {
        public override void ChangeValue(ISelectable value)
        {
            CurrentValue?.UnSelect();
            base.ChangeValue(value);
            CurrentValue?.Select();
        }
    }
}
