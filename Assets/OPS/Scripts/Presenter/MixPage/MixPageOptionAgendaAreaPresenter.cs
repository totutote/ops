using UnityEngine;
using OPS.Model;
using Zenject;
using TMPro;
using UnityEngine.UI;

namespace OPS.Presenter
{
    public class MixPageOptionAgendaAreaPresenter : MonoBehaviour
    {
        [SerializeField]
        Image OptionElementImage = default;

        [SerializeField]
        TextMeshProUGUI _optionNameText = default;

        [SerializeField]
        TextMeshProUGUI _rateText = default;

        UserMixCompleteMaterialModel _userMixCompleteMaterialModel;

        public void Setup(UserMixCompleteMaterialModel userMixCompleteMaterialModel)
        {
            _userMixCompleteMaterialModel = userMixCompleteMaterialModel;

            _optionNameText.text = _userMixCompleteMaterialModel.MasterOptionModel.name.Value;
            _rateText.text = _userMixCompleteMaterialModel.IncludeExtraRate().ToString() + "%";
            if (_userMixCompleteMaterialModel.IsExtraSlot()) OptionElementImage.color = new Color(1f, 0.6f, 0.6f);
        }

        public class Factory : PlaceholderFactory<MixPageOptionAgendaAreaPresenter>
        {
        }

    }
}
