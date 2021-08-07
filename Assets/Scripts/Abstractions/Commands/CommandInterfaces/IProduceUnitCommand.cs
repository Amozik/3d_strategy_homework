using Abstractions.Items;
using UnityEngine;

namespace Abstractions.Commands.CommandInterfaces
{
    public interface IProduceUnitCommand : ICommand, IIconHolder
    {
        float ProductionTime { get; }
        GameObject UnitPrefab { get; }
        string UnitName { get; }
    }

}