using UnityEngine;
using OPS.Model;
using Zenject;
using TMPro;

namespace OPS.Presenter
{
    public class MixPageOptionAgendaAreaPresenter : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _optionNameText = default;

        [SerializeField]
        TextMeshProUGUI _rateText = default;

        UserMixCompleteMaterialModel _userMixCompleteMaterialModel;

        public void Setup(UserMixCompleteMaterialModel userMixCompleteMaterialModel)
        {
            _userMixCompleteMaterialModel = userMixCompleteMaterialModel;

            _optionNameText.text = _userMixCompleteMaterialModel.MasterOptionModel.name.Value;
            _rateText.text = _userMixCompleteMaterialModel.rate.Value.ToString();
        }

        public class Factory : PlaceholderFactory<MixPageOptionAgendaAreaPresenter>
        {
        }

    }
}
