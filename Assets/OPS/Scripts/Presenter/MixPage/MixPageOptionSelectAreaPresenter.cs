using UnityEngine;
using OPS.Model;
using Zenject;
using TMPro;
using System.Linq;
using UnityEngine.UI;

namespace OPS.Presenter
{

    public class MixPageOptionSelectAreaPresenter : MonoBehaviour
    {
        [SerializeField]
        Toggle _toggle = default;

        [SerializeField]
        TextMeshProUGUI _optionNameText = default;

        [SerializeField]
        TextMeshProUGUI _rateText = default;

        [Inject]
        UserMixCompleteMaterialDB _userMixCompleteMaterialDB = default;

        public delegate void DelegateSelectOption();

        public event DelegateSelectOption _onSelectOption;

        UserMixCompleteMaterialModel _userMixCompleteMaterialModel;

        public void Setup(UserMixModel userMixModel, MasterOptionModel masterOptionModel, double rate)
        {
            var userMixCompleteMaterial = _userMixCompleteMaterialDB.New();
            userMixCompleteMaterial.master_option_id.Value = masterOptionModel.id.Value;
            userMixCompleteMaterial.rate.Value = rate;
            userMixCompleteMaterial.select_agenda.Value = 0;
            userMixCompleteMaterial.user_mix_id.Value = userMixModel.id.Value;
            _userMixCompleteMaterialModel = _userMixCompleteMaterialDB.Save(userMixCompleteMaterial).First().Value;

            _optionNameText.text = _userMixCompleteMaterialModel.MasterOptionModel.name.Value;
            _rateText.text = _userMixCompleteMaterialModel.rate.ToString() + "%";
            _toggle.isOn = _userMixCompleteMaterialModel.select_agenda.Value == 0 ? false : true;
        }

        public void SelectOption()
        {
            if (_toggle.isOn)
            {
                _userMixCompleteMaterialModel.SelectAgenda();
            }
            else
            {
                _userMixCompleteMaterialModel.select_agenda.Value = 0;
            }
            _userMixCompleteMaterialDB.Save(_userMixCompleteMaterialModel);
            if (_onSelectOption != null)
            {
                _onSelectOption();
            }
        }

        public class Factory : PlaceholderFactory<MixPageOptionSelectAreaPresenter>
        {
        }

    }

}
