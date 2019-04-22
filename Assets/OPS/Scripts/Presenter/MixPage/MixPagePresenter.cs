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
        PageManager _pageManager = null;

        UserMixModel _userMixModel;

        public void Setup(UserMixModel userMixModel)
        {
            _userMixModel = userMixModel;
            var mixOptionRates = _userMixModel.MixOptionRate;
            foreach(var mixOptionRate in mixOptionRates)
            {
                Debug.Log(mixOptionRate.Key.name.Value);
                Debug.Log(mixOptionRate.Value);
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
