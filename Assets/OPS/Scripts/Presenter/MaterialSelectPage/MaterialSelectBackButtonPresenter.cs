using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace OPS.Presenter
{
    public class MaterialSelectBackButtonPresenter : MonoBehaviour
    {

        [Inject]
        PageManager _pageManager;

        [Inject]
        CompleteSelectPagePresenter.Factory _completeSelectPageFactory;

        public void OnClick()
        {
            var cpyCompleteSelectPage = _completeSelectPageFactory.Create();
            _pageManager.ChangePage(cpyCompleteSelectPage);
            
        }

    }
}