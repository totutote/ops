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
        MixPageOptionSelectAreaPresenter.Factory _mixPageOptionSelectAreaFactory = null;

        [Inject]
        PageManager _pageManager = null;

        [SerializeField]
        GameObject _addOptionSelectAreaObject = default;

        [SerializeField]
        MixPageAgendaOptionListPresenter _mixPageAgendaOptionList = default;

        UserMixModel _userMixModel;

        public void Setup(UserMixModel userMixModel)
        {
            userMixModel.DestroyCompleteModel();
            _userMixModel = userMixModel;
            var mixOptionRates = _userMixModel.MixOptionRate;
            foreach(var mixOptionRate in mixOptionRates)
            {
                var cpyMixPageOptionSelectArea = _mixPageOptionSelectAreaFactory.Create();
                cpyMixPageOptionSelectArea.Setup(_userMixModel, mixOptionRate.Key, mixOptionRate.Value);
                cpyMixPageOptionSelectArea._onSelectOption += SelectOption;
                cpyMixPageOptionSelectArea.transform.SetParent(_addOptionSelectAreaObject.transform, false);
            }
        }

        public void OnBackClick()
        {
            var cpyMaterialSelectPage = _materialSelectPageFactory.Create();
            cpyMaterialSelectPage.Setup(_userMixModel);
            _pageManager.ChangePage(cpyMaterialSelectPage);
        }

        public void SelectOption()
        {
            _mixPageAgendaOptionList.Reload(_userMixModel);
        }

        public class Factory : PlaceholderFactory<MixPagePresenter>
        {
        }

	}

}
