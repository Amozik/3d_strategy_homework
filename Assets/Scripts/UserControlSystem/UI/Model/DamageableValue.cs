using System;
using Abstractions;
using Abstractions.Items;
using UnityEngine;

namespace UserControlSystem.UI.Model
{
    [CreateAssetMenu(
        fileName = nameof(DamageableValue), 
        menuName = "Strategy Game/" + nameof(DamageableValue))]
    public sealed class DamageableValue : ChangeableValueModel<IDamageable>
    {
    }
}
