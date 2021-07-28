using Abstractions.Commands.CommandInterfaces;
using UnityEngine;
using Utils.AssetsInjector;

namespace UserControlSystem.CommandsRealization
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        [InjectAsset("Chomper")] 
        protected GameObject _unitPrefab;
        
        public GameObject UnitPrefab  => _unitPrefab;
    }
}