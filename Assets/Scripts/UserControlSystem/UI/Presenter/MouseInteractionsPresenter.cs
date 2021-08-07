using System.Linq;
using Abstractions;
using Abstractions.Items;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UserControlSystem.UI.Model;

namespace UserControlSystem.UI.Presenter
{
    public sealed class MouseInteractionsPresenter : MonoBehaviour
    {
        private const string LEFT_MOUSE_BTN = "lmb";
        private const string RIGHT_MOUSE_BTN = "rmb";

        [SerializeField] 
        private Camera _camera;
        [SerializeField] 
        private SelectableValue _selectedObject;
        [SerializeField] 
        private EventSystem _eventSystem;
        
        [SerializeField] 
        private Vector3Value _groundClicksRMB;
        [SerializeField] 
        private Transform _groundTransform;

        [SerializeField]
        private DamageableValue _damageableObject;
        
        private Plane _groundPlane;
        
        private void Start()
        {
            _groundPlane = new Plane(_groundTransform.up, 0);
            
            var clickStream = Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonUp(0) || Input.GetMouseButton(1))
                .Where(_ => !_eventSystem.IsPointerOverGameObject())
                .Select(_ => Input.GetMouseButtonUp(0) ? LEFT_MOUSE_BTN : RIGHT_MOUSE_BTN);

            clickStream.Subscribe(OnClick).AddTo(this);
        }
        
        private void OnClick(string buttonType)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            var hits = Physics.RaycastAll(ray);

            switch (buttonType)
            {
                case LEFT_MOUSE_BTN:
                {
                    GetHitOfType<ISelectable>(hits, out var selectable);
                    _selectedObject.ChangeValue(selectable);
                    break;
                }
                case RIGHT_MOUSE_BTN:
                {
                    if (GetHitOfType<IDamageable>(hits, out var damageable))
                    {
                        _damageableObject.ChangeValue(damageable);
                    } 
                    else if (_groundPlane.Raycast(ray, out var enter))
                    {
                        _groundClicksRMB.ChangeValue(ray.origin + ray.direction * enter);
                    }
                    break;
                }
                default:
                    break;
            }
        }

        private bool GetHitOfType<T>(RaycastHit[] hits, out T result) where T : class
        {
            result = default;
            if (hits.Length == 0)
                return false;

            result = hits
                .Select(hit => hit.collider.GetComponentInParent<T>())
                .FirstOrDefault(c => c != null);
            return result != default;
        }
    }
}
