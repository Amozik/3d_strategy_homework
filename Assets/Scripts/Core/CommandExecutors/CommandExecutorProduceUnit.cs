using Abstractions.Commands;
using Abstractions.Commands.CommandInterfaces;
using Abstractions.Items.Production;
using Core.UnitTasks;
using UniRx;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class CommandExecutorProduceUnit : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
    {
        private const int MIN_INCLUSIVE = -10;
        private const int MAX_INCLUSIVE = 10;
        private const int ZERO = 0;
        
        [SerializeField] 
        private Transform _unitsParent;
        [SerializeField] 
        private int _maximumUnitsInQueue = 5;

        private ReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();

        public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;

        private void InstantiateUnit(GameObject unitPrefab)
        {
            var xRandom = Random.Range(MIN_INCLUSIVE, MAX_INCLUSIVE);
            var zRandom = Random.Range(MIN_INCLUSIVE, MAX_INCLUSIVE);
            Instantiate(unitPrefab, new Vector3(xRandom, ZERO, zRandom), Quaternion.identity,
                _unitsParent);
        }
        
        private void Update()
        {
            if (_queue.Count == 0)
            {
                return;
            }

            var innerTask = (UnitProductionTask)_queue[0];
            innerTask.TimeLeft -= Time.deltaTime;
            
            if (innerTask.TimeLeft <= 0)
            {
                RemoveTaskAtIndex(0);
                InstantiateUnit(innerTask.UnitPrefab);
            }
        }
        
        private void RemoveTaskAtIndex(int index)
        {
            for (var i = index; i < _queue.Count - 1; i++)
            {
                _queue[i] = _queue[i + 1];
            }
            
            _queue.RemoveAt(_queue.Count - 1);
        }

        public void Cancel(int index) => RemoveTaskAtIndex(index);
        
        public override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            if (_queue.Count >= _maximumUnitsInQueue)
                return;
            
            _queue.Add(new UnitProductionTask(command.ProductionTime, command.Icon, command.UnitPrefab, command.UnitName));
        }
    }
}