using UnityEngine;
using TMPro;
using OPS.Model;

namespace OPS.Presenter
{
    public class OptionSelectParamButtonPresenter : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _optionText;

        MasterOptionModel _model;

        public void SetModel(MasterOptionModel model)
        {
            _model = model;
            _optionText.text = _model.name.Value;
        }
    }
}