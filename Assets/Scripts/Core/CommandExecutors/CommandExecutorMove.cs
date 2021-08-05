using System;
using System.Collections;
using System.Threading;
using Abstractions.Commands;
using Abstractions.Commands.CommandInterfaces;
using UnityEngine;
using UnityEngine.AI;
using Utils.Extensions;

namespace Core.CommandExecutors
{
    public class CommandExecutorMove : CommandExecutorBase<IMoveCommand>
    {
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        
        [SerializeField] 
        private NavMeshAgent _navMeshAgent;
        [SerializeField]
        private UnitMovementStop _stop;
        [SerializeField]
        private Animator _animator;

        [SerializeField] 
        private CommandExecutorStop _stopCommandExecutor;
        
        public override async void ExecuteSpecificCommand(IMoveCommand command)
        {
            _navMeshAgent.SetDestination(command.Target);
            _animator.SetBool(IsWalking, true);
            
            _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
            try
            {
                await _stop.WithCancellation(_stopCommandExecutor.CancellationTokenSource.Token);
            }
            catch
            {
                _navMeshAgent.isStopped = true;
                _navMeshAgent.ResetPath();
            }
            _stopCommandExecutor.CancellationTokenSource.Dispose();
            _stopCommandExecutor.CancellationTokenSource = null;

            _animator.SetBool(IsWalking, false);
        }

        public void PlayStep()
        {
            //pay step sound
        }

        public void Grunt()
        {
            //play grunt sound
        }
    }
}