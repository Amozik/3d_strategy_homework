using System;
using System.Collections.Generic;
using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandInterfaces;
using UnityEngine;
using UserControlSystem.CommandsRealization;
using UserControlSystem.UI.Model;
using UserControlSystem.UI.View;
using Utils.AssetsInjector;

namespace UserControlSystem.UI.Presenter
{
    public class CommandButtonsPresenter : MonoBehaviour
    {
        [SerializeField]
        private SelectableValue _selectable;
        [SerializeField]
        private CommandButtonsView _view;
        [SerializeField] 
        private AssetsContext _context;
        
        private ISelectable _currentSelectable;

        private void Start()
        {
            _selectable.OnSelected += OnSelected;
            OnSelected(_selectable.CurrentValue);

            _view.OnClick += OnButtonClick;
        }

        private void OnSelected(ISelectable selectable)
        {
            if (_currentSelectable == selectable)
            {
                return;
            }
            _currentSelectable = selectable;

            _view.Clear();
            if (selectable != null)
            {
                var commandExecutors = new List<ICommandExecutor>();
                commandExecutors.AddRange((selectable as Component).GetComponentsInParent<ICommandExecutor>());
                _view.MakeLayout(commandExecutors);
            }
        }

        private void OnButtonClick(ICommandExecutor commandExecutor)
        {
            
            switch (commandExecutor)
            {
                case CommandExecutorBase<IProduceUnitCommand> unitProducer:
                    unitProducer.ExecuteSpecificCommand(_context.Inject(new ProduceUnitCommandHeir()));
                    return;
                case CommandExecutorBase<IAttackCommand> attacker:
                    attacker.ExecuteSpecificCommand(new AttackCommand());
                    return;
                case CommandExecutorBase<IMoveCommand> mover:
                    mover.ExecuteSpecificCommand(new MoveCommand());
                    return;
                case CommandExecutorBase<IStopCommand> stopper:
                    stopper.ExecuteSpecificCommand(new StopCommand());
                    return;
                case CommandExecutorBase<IPatrolCommand> patroller:
                    patroller.ExecuteSpecificCommand(new PatrolCommand());
                    return;
                default:
                    throw new ApplicationException($"{nameof(CommandButtonsPresenter)}.{nameof(OnButtonClick)}: Unknown type of commands executor: {commandExecutor.GetType().FullName}!");
            }
        }

    }
}