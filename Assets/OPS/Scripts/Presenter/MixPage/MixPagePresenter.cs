using UnityEngine;
using OPS.Model;
using Zenject;

namespace OPS.Presenter
{

    public class MixPagePresenter : MonoBehaviour
	{
        [Inject]
        MaterialSelectPagePresenter.Factory _materialSelectPageFactory;

        [Inject]
        PageManager _pageManager;

        UserMixModel _userMixModel;

        public void Setup(UserMixModel userMixModel)
        {
            _userMixModel = userMixModel;
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
