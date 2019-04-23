using UnityEngine;
using TMPro;
using OPS.Model;

namespace OPS.Presenter
{
    public class OptionSelectParamButtonPresenter : MonoBehaviour
    {
        OptionSelectPagePresenter _pagePresenter;

        [SerializeField]
        TextMeshProUGUI _optionText = default;

        MasterOptionModel _model;

        public void SetPagePresenter(OptionSelectPagePresenter pagePresenter)
        {
            _pagePresenter = pagePresenter;
        }

        public void SetModel(MasterOptionModel model)
        {
            _model = model;
            _optionText.text = _model.name.Value;
        }

        public void OnSelectOption()
        {
            _pagePresenter.OnSelectOption(_model);            
        }
    }
}