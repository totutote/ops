using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using OPS.Model;
using TMPro;

namespace OPS.Presenter
{
    public class CompleteSelectListButtonPresenter : MonoBehaviour
    {
        [Inject]
        PageManager _pageManager;

        [Inject]
        MaterialSelectPagePresenter.Factory _materialSelectPageFactory;

        [SerializeField]
        TextMeshProUGUI _nameText;

        UserMixModel _userMixModel;

        public void Setup(UserMixModel userMixModel)
        {
            _userMixModel = userMixModel;
            _nameText.text = _userMixModel.name.Value;
        }

        public void OnClick()
        {
            var _cpyMaterialSelectPage = _materialSelectPageFactory.Create();
            _cpyMaterialSelectPage.Setup(_userMixModel);
            _pageManager.ChangePage(_cpyMaterialSelectPage);
        }

        public class Factory : PlaceholderFactory<CompleteSelectListButtonPresenter>
        {
        }
    }
}