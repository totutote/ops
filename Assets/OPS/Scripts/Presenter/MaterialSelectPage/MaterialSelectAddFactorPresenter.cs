using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using OPS.Model;
using Zenject;

namespace OPS.Presenter
{

    public class MaterialSelectAddFactorPresenter : MonoBehaviour
    {
        [Inject]
        PageManager _pageManager = null;

        [Inject]
        OptionSelectPagePresenter.Factory _optionSelectPageFactory = null;

        [SerializeField]
        MaterialSelectOptionListPresenter _optionListPresenter = default;

        [SerializeField]
        TextMeshProUGUI _factorName = default;

        [SerializeField]
        GameObject _removeButton = default;

        OptionSelectPagePresenter _cpyOptionSelectPage;

        public void Setup(UserMixCandidateMaterialOptionModel userMixCandidateMaterialOptionModel)
        {
            if (userMixCandidateMaterialOptionModel == null)
            {
                _factorName.text = "＋特殊能力因子";
                _removeButton.SetActive(false);
            }
            else
            {
                _factorName.text = userMixCandidateMaterialOptionModel.MasterOptionModel.name.Value;
                _removeButton.SetActive(true);
            }
        }

        public void OnClick()
        {
            _cpyOptionSelectPage = _pageManager.AddPage(_optionSelectPageFactory.Create());
            _cpyOptionSelectPage.OnActive(OnAddSelectOption);
        }

        void OnAddSelectOption(MasterOptionModel masterOptionModel)
        {
			_optionListPresenter.AddNewFactor(masterOptionModel);
            _pageManager.DestroyPage<OptionSelectPagePresenter>();
        }

        public void OnRemoveClick()
        {
            _optionListPresenter.RemoveFactor();
        }

    }

}
