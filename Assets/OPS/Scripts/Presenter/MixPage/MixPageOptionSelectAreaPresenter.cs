using UnityEngine;
using OPS.Model;
using Zenject;
using TMPro;

namespace OPS.Presenter
{

    public class MixPageOptionSelectAreaPresenter : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _optionNameText = default;

        [SerializeField]
        TextMeshProUGUI _rateText = default;

        MasterOptionModel _masterOptionModel;

        double _rate;

        public void Setup(MasterOptionModel masterOptionModel, double rate)
        {
            _masterOptionModel = masterOptionModel;
            _rate = rate;

            _optionNameText.text = masterOptionModel.name.Value;
            _rateText.text = rate.ToString() + "%";
        }

        public class Factory : PlaceholderFactory<MixPageOptionSelectAreaPresenter>
        {
        }

    }

}
