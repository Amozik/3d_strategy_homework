using System;
using UnityEngine;
using Utils;

namespace UserControlSystem.UI.Model
{
    public abstract class ChangeableValueModel<T> : ScriptableObject, IAwaitable<T>
    {
        private class NewValueNotifier<TAwaited> : IAwaiter<TAwaited>
        {
            private readonly ChangeableValueModel<TAwaited> _changeableValueModel;
            private TAwaited _result;
            private Action _continuation;
            private bool _isCompleted;

            public NewValueNotifier(ChangeableValueModel<TAwaited> changeableValueModel)
            {
                _changeableValueModel = changeableValueModel;
                _changeableValueModel.OnChanged += OnNewValue;
            }

            private void OnNewValue(TAwaited obj)
            {
                _changeableValueModel.OnChanged -= OnNewValue;
                _result = obj;
                _isCompleted = true;
                _continuation?.Invoke();
            }

            public void OnCompleted(Action continuation)
            {
                if (_isCompleted)
                {
                    continuation?.Invoke();
                }
                else
                {
                    _continuation = continuation;
                }
            }
            public bool IsCompleted => _isCompleted;
            public TAwaited GetResult() => _result;
        }
        
        public T CurrentValue { get; private set; }
        public event Action<T> OnChanged;

        public virtual void ChangeValue(T value)
        {
            CurrentValue = value;
            OnChanged?.Invoke(value);
        }

        public IAwaiter<T> GetAwaiter()
        {
            return new NewValueNotifier<T>(this);
        }
    }
}