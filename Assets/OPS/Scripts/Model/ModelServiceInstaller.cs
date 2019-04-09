using System;
using Zenject;
using OPS.Model;

public class ModelServiceInstaller : MonoInstaller<ModelServiceInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<MasterOptionDB>().AsSingle();
        Container.Bind<MasterOptionCategoryDB>().AsSingle();
        Container.Bind<MasterOptionParamDB>().AsSingle();
        Container.Bind<MasterOptionParamBaseDB>().AsSingle();
        Container.Bind<UserMaterialDB>().AsSingle();
        Container.Bind<UserMixDB>().AsSingle();
        Container.Bind<UserMixCandidateMaterialDB>().AsSingle();
        Container.BindFactory<MasterOptionDB, MasterOptionDB.Factory>();
        Container.BindFactory<MasterOptionCategoryDB, MasterOptionCategoryDB.Factory>();
    }
}