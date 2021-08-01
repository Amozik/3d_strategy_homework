using System;
using Abstractions.Commands.CommandInterfaces;
using Abstractions.Items;
using UserControlSystem.CommandsRealization;
using Utils.AssetsInjector;
using Zenject;

namespace UserControlSystem.UI.Model.CommandCreators
{
    public class AttackCommandCommandCreator : CommandCreatorBase<IAttackCommand>
    {
        [Inject] 
        private AssetsContext _context;
        
        private Action<IAttackCommand> _creationCallback;

        [Inject]
        private void Init(DamageableValue damageableObject)
        {
            damageableObject.OnChanged += OnNewValue;
        }

        private void OnNewValue(IDamageable item)
        {
            _creationCallback?.Invoke(_context.Inject(new AttackCommand(item)));
            _creationCallback = null;
        }
        
        protected override void ClassSpecificCommandCreation(Action<IAttackCommand> creationCallback)
        {
            _creationCallback = creationCallback;
        }
        
        public override void ProcessCancel()
        {
            base.ProcessCancel();

            _creationCallback = null;
        }
    }
}