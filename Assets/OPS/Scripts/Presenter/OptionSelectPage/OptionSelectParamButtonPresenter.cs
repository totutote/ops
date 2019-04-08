using UnityEngine;
using TMPro;
using OPS.Model;

namespace OPS.Presenter
{
    public class OptionSelectParamButtonPresenter : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _optionText;

        MasterOptionParamModel _model;

        public void SetModel(MasterOptionParamModel model)
        {
            _model = model;
            _optionText.text = model.MasterOptionModel.name.Value;
        }
    }
}