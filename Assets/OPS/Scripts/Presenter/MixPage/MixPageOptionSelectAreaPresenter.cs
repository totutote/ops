using UnityEngine;
using OPS.Model;
using Zenject;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using UniRx;

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

        UserMixModel _userMixModel;

        public void Setup(UserMixModel userMixModel, MasterOptionModel masterOptionModel, double rate)
        {
            _userMixModel = userMixModel;

            var userMixCompleteMaterial = _userMixCompleteMaterialDB.New();
            userMixCompleteMaterial.master_option_id.Value = masterOptionModel.id.Value;
            userMixCompleteMaterial.rate.Value = rate;
            userMixCompleteMaterial.select_agenda.Value = 0;
            userMixCompleteMaterial.user_mix_id.Value = userMixModel.id.Value;
            _userMixCompleteMaterialModel = _userMixCompleteMaterialDB.Save(userMixCompleteMaterial).First().Value;

            _optionNameText.text = _userMixCompleteMaterialModel.MasterOptionModel.name.Value;
            _rateText.text = _userMixCompleteMaterialModel.IncludePeriodBonusRate().ToString() + "%";
            _userMixCompleteMaterialModel.select_agenda.Subscribe(onoff => {_toggle.isOn = onoff == 0 ? false : true;}).AddTo(gameObject);
        }

        public void SelectOption(Toggle toggle)
        {
            if (toggle.isOn)
            {
                if (!_userMixCompleteMaterialModel.SelectAgenda())
                {
                    toggle.isOn = false;
                }
            }
            else
            {
                if (_userMixCompleteMaterialModel.select_agenda.Value == 0) return;
                _userMixCompleteMaterialModel.select_agenda.Value = 0;
                _userMixCompleteMaterialDB.Save(_userMixCompleteMaterialModel);
                _userMixModel.SortSelectAgenda();
            }
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
