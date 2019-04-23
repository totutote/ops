using System;
using UnityEngine;
using Zenject;
using OPS.Presenter;

public class PageManagerInstaller : MonoInstaller<PageManagerInstaller>
{
    [SerializeField]
    GameObject _mainCanvas = default;

    [SerializeField]
    GameObject _selectOptionList = default;

    [SerializeField]
    GameObject _optionArea = default;

    [SerializeField]
    GameObject _completeSelectPage = default;

    [SerializeField]
    GameObject _completeSelectListButton = default;

    [SerializeField]
    GameObject _materialSelectPage = default;

    [SerializeField]
    GameObject _optionSelectPage = default;

    [SerializeField]
    GameObject _mixPage = default;

    [SerializeField]
    GameObject _mixPageOptionSelectArea = default;

    public override void InstallBindings()
    {
        Container.Bind<PageManager>().FromComponentOn(_mainCanvas).AsSingle();
        Container.BindFactory<MaterialSelectOptionListPresenter, MaterialSelectOptionListPresenter.Factory>().FromComponentInNewPrefab(_selectOptionList);
        Container.BindFactory<MaterialSelectOptionAreaPresenter, MaterialSelectOptionAreaPresenter.Factory>().FromComponentInNewPrefab(_optionArea);
        Container.BindFactory<CompleteSelectPagePresenter, CompleteSelectPagePresenter.Factory>().FromComponentInNewPrefab(_completeSelectPage);
        Container.BindFactory<CompleteSelectListButtonPresenter, CompleteSelectListButtonPresenter.Factory>().FromComponentInNewPrefab(_completeSelectListButton);
        Container.BindFactory<MaterialSelectPagePresenter, MaterialSelectPagePresenter.Factory>().FromComponentInNewPrefab(_materialSelectPage);
        Container.BindFactory<OptionSelectPagePresenter, OptionSelectPagePresenter.Factory>().FromComponentInNewPrefab(_optionSelectPage);
        Container.BindFactory<MixPagePresenter, MixPagePresenter.Factory>().FromComponentInNewPrefab(_mixPage);
        Container.BindFactory<MixPageOptionSelectAreaPresenter, MixPageOptionSelectAreaPresenter.Factory>().FromComponentInNewPrefab(_mixPageOptionSelectArea);
    }
}