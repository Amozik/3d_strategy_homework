using Abstractions.Items;
using UnityEngine;
using UserControlSystem.UI.Model;
using Utils;
using Utils.AssetsInjector;
using Zenject;

[CreateAssetMenu(fileName = nameof(GlobalModelInstaller), menuName = "Installers/" + nameof(GlobalModelInstaller))]
public class GlobalModelInstaller : ScriptableObjectInstaller<GlobalModelInstaller>
{
    [SerializeField]
    private AssetsContext _legacyContext;
    [SerializeField]
    private Vector3Value _groundClick;
    [SerializeField]
    private DamageableValue _damageableObject;
    
    public override void InstallBindings()
    {
        Container.Bind<AssetsContext>().FromInstance(_legacyContext);
        Container.Bind<Vector3Value>().FromInstance(_groundClick);
        Container.Bind<DamageableValue>().FromInstance(_damageableObject);
        
        Container.Bind<IAwaitable<IDamageable>>().FromInstance(_damageableObject);
        Container.Bind<IAwaitable<Vector3>>().FromInstance(_groundClick);
    }
}