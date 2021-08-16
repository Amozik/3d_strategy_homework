using System.Threading.Tasks;
using Abstractions.Commands;
using Abstractions.Commands.CommandInterfaces;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class CommandExecutorAttack : CommandExecutorBase<IAttackCommand>
    {
        public override Task ExecuteSpecificCommand(IAttackCommand command)
        {
            Debug.Log("Attack");
            return Task.CompletedTask;
        }
    }
}