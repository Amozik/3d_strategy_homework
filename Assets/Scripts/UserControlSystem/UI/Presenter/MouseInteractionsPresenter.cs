using System.Linq;
using Abstractions;
using UnityEngine;
using UnityEngine.EventSystems;
using UserControlSystem.UI.Model;

namespace UserControlSystem.UI.Presenter
{
    public sealed class MouseInteractionsPresenter : MonoBehaviour
    {
        [SerializeField] 
        private Camera _camera;
        [SerializeField] 
        private SelectableValue _selectedObject;
        [SerializeField] 
        private EventSystem _eventSystem;
        
        private void Update()
        {
            if (!Input.GetMouseButtonUp(0))
            {
                return;
            }
            
            if (_eventSystem.IsPointerOverGameObject())
            {
                return;
            }
        
            var hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
            if (hits.Length == 0)
            {
                return;
            }
        
            var selectable = hits
                .Select(hit => hit.collider.GetComponentInParent<ISelectable>())
                .FirstOrDefault(component => component != null);
        
            _selectedObject.SetValue(selectable);
        }
    }
}
