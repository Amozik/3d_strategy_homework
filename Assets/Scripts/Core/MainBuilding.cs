using Abstractions;
using QuickOutline;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    public sealed class MainBuilding : MonoBehaviour, IUnitProducer, ISelectable
    {
        private const int MIN_HEALTH_VALUE = 100;
        private const int MAX_HEALTH_VALUE = 1000;
        private const int MIN_INCLUSIVE = -10;
        private const int MAX_INCLUSIVE = 10;
        private const int ZERO = 0;
        private const Outline.Mode OUTLINE_DEAFULT_MODE = Outline.Mode.OutlineAll;
        private const float OUTLINE_DEAFULT_WIDTH = 5f;
        private static readonly Color OUTLINE_DEAFULT_COLOR = Color.yellow;
        
        [SerializeField] private GameObject _unitPrefab;
        [SerializeField] private Transform _unitsParent;
        [SerializeField] private Outline _outline;

        [Range(MIN_HEALTH_VALUE, MAX_HEALTH_VALUE)]
        [SerializeField]
        private float _maxHealth;

        [SerializeField] 
        private Sprite _icon;

        private float _health = MAX_HEALTH_VALUE;

        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;

        private void Awake()
        {
            if (_outline != null) return;
            _outline = GetComponent<Outline>();
            if (_outline == null)
                _outline = gameObject.AddComponent<Outline>();
                
            _outline.OutlineMode = OUTLINE_DEAFULT_MODE;
            _outline.OutlineColor = OUTLINE_DEAFULT_COLOR;
            _outline.OutlineWidth = OUTLINE_DEAFULT_WIDTH;
            _outline.enabled = false;
        }

        public void ProduceUnit()
        {
            var xRandom = Random.Range(MIN_INCLUSIVE, MAX_INCLUSIVE);
            var zRandom = Random.Range(MIN_INCLUSIVE, MAX_INCLUSIVE);
            Instantiate(_unitPrefab, new Vector3(xRandom, ZERO, zRandom), Quaternion.identity,
                _unitsParent);
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