using UnityEngine;
using TMPro;
using OPS.Model;

namespace OPS.Presenter
{
    public class OptionSelectOptionGroupButtonPresenter : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _optionGroupText;

        MasterOptionParamBaseModel _model;

        public void SetModel(MasterOptionParamBaseModel model)
        {
            _model = model;
            _optionGroupText.text = model.name.Value;
        }
    }
}