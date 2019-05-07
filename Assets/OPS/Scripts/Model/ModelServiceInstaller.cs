using System;
using Zenject;
using OPS.Model;
using OPS.Presenter;

public class ModelServiceInstaller : MonoInstaller<ModelServiceInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<MasterOptionDB>().AsSingle();
        Container.Bind<MasterOptionCategoryDB>().AsSingle();
        Container.Bind<MasterOptionParamDB>().AsSingle();
        Container.Bind<MasterOptionParamBaseDB>().AsSingle();
        Container.Bind<MasterMixChainDB>().AsSingle();
        Container.Bind<MasterMixBonusDB>().AsSingle();
        Container.Bind<UserMixCompleteMaterialDB>().AsSingle();
        Container.Bind<UserMixDB>().AsSingle();
        Container.Bind<UserMixCandidateMaterialDB>().AsSingle();
        Container.Bind<UserMixCandidateMaterialOptionDB>().AsSingle();
        Container.BindFactory<MasterOptionDB, MasterOptionDB.Factory>();
        Container.BindFactory<MasterOptionCategoryDB, MasterOptionCategoryDB.Factory>();
        Container.BindFactory<UserMixDB, UserMixDB.Factory>();
        Container.BindFactory<UserMixCandidateMaterialDB, UserMixCandidateMaterialDB.Factory>();
        Container.BindFactory<UserMixCandidateMaterialOptionDB, UserMixCandidateMaterialOptionDB.Factory>();
    }
}