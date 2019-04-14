using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace OPS.Presenter
{
    public class CompleteSelectCreateButtonPresenter : MonoBehaviour
    {
        [Inject]
        PageManager _pageManager;

        [Inject]
        MaterialSelectPagePresenter.Factory _materialSelectPageFactory;

        public void OnClick()
        {
            MaterialSelectPagePresenter cpyMaterialSelectPagePresenter = _materialSelectPageFactory.Create();
            cpyMaterialSelectPagePresenter.Setup();
            _pageManager.ChangePage(cpyMaterialSelectPagePresenter);
        }

    }
}