using System;
using Abstractions.Commands;
using Abstractions.Commands.CommandInterfaces;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class CommandExecutorPatrol : CommandExecutorBase<IPatrolCommand>
    {
        private const int WAYPONTS_COUNT = 4;
        
        [SerializeField] 
        private float radius = 1;
        
        private Vector3[] _waypoints = new Vector3[WAYPONTS_COUNT];
        
        public override void ExecuteSpecificCommand(IPatrolCommand command)
        {
            var centerPoint = command.CenterPoint;
            
            var vectorR = new Vector3(1, 0, 1);
            var vectorL = new Vector3(1, 0, -1);
            
            for (var i = 0; i < 4; i++)
            {
                var radius1 = radius * (i < WAYPONTS_COUNT / 2 ? 1 : -1);
                
                _waypoints[i] = centerPoint + (i % 2 == 0 ? vectorR : vectorL) * radius1;
            }

            Debug.Log("Patrol");
        }
    }
}