using System;
using Abstractions;
using Abstractions.Items;
using UnityEngine;

namespace Core
{
    public class DestroyableItem : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private int _maxHealth;
        
        public Health Health { get; private set; }

        private void Awake()
        {
            Health = new Health(_maxHealth, _maxHealth);
        }
    }
}