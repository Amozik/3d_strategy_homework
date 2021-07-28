using Abstractions.Commands;
using Abstractions.Commands.CommandInterfaces;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class CommandExecutorProduceUnit : CommandExecutorBase<IProduceUnitCommand>
    {
        private const int MIN_INCLUSIVE = -10;
        private const int MAX_INCLUSIVE = 10;
        private const int ZERO = 0;
        
        [SerializeField] 
        private Transform _unitsParent;
        
        public override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            var xRandom = Random.Range(MIN_INCLUSIVE, MAX_INCLUSIVE);
            var zRandom = Random.Range(MIN_INCLUSIVE, MAX_INCLUSIVE);
            Instantiate(command.UnitPrefab, new Vector3(xRandom, ZERO, zRandom), Quaternion.identity,
                _unitsParent);
        }
    }
}