using System;
using UnityEngine;

namespace Abstractions
{
    public abstract class ChangeableValueModel<T> : ScriptableObject
    {
        public T CurrentValue { get; private set; }
        public event Action<T> OnChanged;

        public virtual void ChangeValue(T value)
        {
            CurrentValue = value;
            OnChanged?.Invoke(value);
        }
    }
}