using Abstractions.Commands.CommandInterfaces;
using UnityEngine;
using Utils.AssetsInjector;
using Zenject;

namespace UserControlSystem.CommandsRealization
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        [InjectAsset("Chomper")] 
        protected GameObject _unitPrefab;
        
        [Inject(Id = "Chomper")] 
        public string UnitName { get; }
        [Inject(Id = "Chomper")] 
        public Sprite Icon { get; }
        [Inject(Id = "Chomper")] 
        public float ProductionTime { get; }
        
        public GameObject UnitPrefab  => _unitPrefab;
    }
}