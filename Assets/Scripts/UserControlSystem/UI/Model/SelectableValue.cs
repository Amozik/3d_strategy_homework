using System;
using Abstractions;
using UnityEngine;

namespace UserControlSystem.UI.Model
{
    [CreateAssetMenu(
        fileName = nameof(SelectableValue), 
        menuName = "Strategy Game/" + nameof(SelectableValue), 
        order = 0)]
    public sealed class SelectableValue : ScriptableObject
    {
        public ISelectable CurrentValue { get; private set; }
        public event Action<ISelectable> OnSelected;

        public void SetValue(ISelectable value)
        {
            CurrentValue?.UnSelect();
            CurrentValue = value;
            OnSelected?.Invoke(value);
            CurrentValue?.Select();
        }
    }
}
