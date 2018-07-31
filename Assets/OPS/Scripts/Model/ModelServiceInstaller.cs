using System;
using Zenject;
using OPS.Model;

public class ModelServiceInstaller : MonoInstaller<ModelServiceInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ModelService<MasterOption>>().AsSingle();
        Container.Bind<ModelService<UserMaterialOption>>().AsSingle();
    }
}