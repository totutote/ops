using UnityEngine;
using OPS.Model;
using Zenject;

namespace OPS.Presenter
{

    public class MixPagePresenter : MonoBehaviour
	{
        [Inject]
        MaterialSelectPagePresenter.Factory _materialSelectPageFactory = null;

        [Inject]
        MixPageOptionSelectAreaPresenter.Factory _mixPageOptionSelectAreaFactory;

        [Inject]
        PageManager _pageManager = null;

        [SerializeField]
        GameObject _addOptionSelectAreaObject;

        UserMixModel _userMixModel;

        public void Setup(UserMixModel userMixModel)
        {
            _userMixModel = userMixModel;
            var mixOptionRates = _userMixModel.MixOptionRate;
            foreach(var mixOptionRate in mixOptionRates)
            {
                var cpyMixPageOptionSelectArea = _mixPageOptionSelectAreaFactory.Create();
                cpyMixPageOptionSelectArea.Setup(mixOptionRate.Key, mixOptionRate.Value);
                cpyMixPageOptionSelectArea.transform.SetParent(_addOptionSelectAreaObject.transform, false);
            }
        }

        public void OnBackClick()
        {
            var cpyMaterialSelectPage = _materialSelectPageFactory.Create();
            cpyMaterialSelectPage.Setup(_userMixModel);
            _pageManager.ChangePage(cpyMaterialSelectPage);
        }

        public class Factory : PlaceholderFactory<MixPagePresenter>
        {
        }

	}

}
