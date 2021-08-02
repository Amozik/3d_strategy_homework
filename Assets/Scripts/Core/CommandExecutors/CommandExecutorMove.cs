using System.Collections;
using Abstractions.Commands;
using Abstractions.Commands.CommandInterfaces;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class CommandExecutorMove : CommandExecutorBase<IMoveCommand>
    {
        private const float SPEED = 2.1f; 
        
        public override void ExecuteSpecificCommand(IMoveCommand command)
        {
            //Debug.Log($"{name} is moving to {command.Target}!");

            StartCoroutine(Move(command.Target));
        }

        private IEnumerator Move(Vector3 targetPosition)
        {
            while (Vector3.SqrMagnitude(transform.position - targetPosition) > 0.01f)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * SPEED);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}