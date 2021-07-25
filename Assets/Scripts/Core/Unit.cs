using Abstractions;
using UnityEngine;

namespace Core
{
    public class Unit : ISelectable
    {
        public float Health { get; }
        public float MaxHealth { get; }
        public Sprite Icon { get; }
        
        public void Select()
        {
        }

        public void UnSelect()
        {
        }
    }
}