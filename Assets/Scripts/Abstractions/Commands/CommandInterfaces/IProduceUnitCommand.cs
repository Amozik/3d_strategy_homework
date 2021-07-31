using UnityEngine;

namespace Abstractions.Commands.CommandInterfaces
{
    public interface IProduceUnitCommand : ICommand
    {
        GameObject UnitPrefab { get; }
    }

}