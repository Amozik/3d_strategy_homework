using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandInterfaces;
using UnityEngine;
using Utils.Extensions;
using Utils.QuickOutline;

namespace Core
{
    public class Unit : MonoBehaviour, ISelectable
    {
        private const int MIN_HEALTH_VALUE = 50;
        private const int MAX_HEALTH_VALUE = 200;
        private const Outline.Mode OUTLINE_DEAFULT_MODE = Outline.Mode.OutlineAll;
        private const float OUTLINE_DEAFULT_WIDTH = 5f;
        private static readonly Color OUTLINE_DEAFULT_COLOR = Color.green;
        
        [Range(MIN_HEALTH_VALUE, MAX_HEALTH_VALUE)]
        [SerializeField]
        private float _maxHealth;

        [SerializeField] 
        private Sprite _icon;
        
        [SerializeField] 
        private Outline _outline;
        
        private float _health = MIN_HEALTH_VALUE;

        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;
        
        private void Awake()
        {
            if (_outline == null)
            {
                _outline = gameObject.GetOrAddComponent<Outline>();

                _outline.OutlineMode = OUTLINE_DEAFULT_MODE;
                _outline.OutlineColor = OUTLINE_DEAFULT_COLOR;
                _outline.OutlineWidth = OUTLINE_DEAFULT_WIDTH;
                
            }
            _outline.enabled = false;
        }

        public void Select()
        {
            _outline.enabled = true;
        }

        public void UnSelect()
        {
            _outline.enabled = false;
        }
    }
}