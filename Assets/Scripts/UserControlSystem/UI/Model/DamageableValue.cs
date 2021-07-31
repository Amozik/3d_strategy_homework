using System;
using Abstractions;
using Abstractions.Items;
using UnityEngine;

namespace UserControlSystem.UI.Model
{
    [CreateAssetMenu(
        fileName = nameof(DamageableValue), 
        menuName = "Strategy Game/" + nameof(DamageableValue))]
    public sealed class DamageableValue : ScriptableObject
    {
        public IDamageable CurrentValue { get; private set; }
        public event Action<IDamageable> OnSelected;

        public void SetValue(IDamageable value)
        {
            CurrentValue = value;
            OnSelected?.Invoke(value);
        }
    }
}
