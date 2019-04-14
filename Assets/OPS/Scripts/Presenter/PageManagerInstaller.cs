using System;
using UnityEngine;
using Zenject;
using OPS.Presenter;

public class PageManagerInstaller : MonoInstaller<PageManagerInstaller>
{
    [SerializeField]
    GameObject _mainCanvas;

    [SerializeField]
    GameObject _selectOptionList;

    [SerializeField]
    GameObject _optionSelectPage;

    public override void InstallBindings()
    {
        Container.Bind<PageManager>().FromComponentOn(_mainCanvas).AsSingle();
        Container.BindFactory<MaterialSelectOptionListPresenter, MaterialSelectOptionListPresenter.Factory>().FromComponentInNewPrefab(_selectOptionList);
        Container.BindFactory<OptionSelectPagePresenter, OptionSelectPagePresenter.Factory>().FromComponentInNewPrefab(_optionSelectPage);
    }
}