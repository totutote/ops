using UnityEngine;
using TMPro;
using OPS.Model;

namespace OPS.Presenter
{
    public class OptionSelectOptionGroupButtonPresenter : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _optionGroupText = default;

        [SerializeField]
        OptionSelectParamListPresenter _paramListPresenter = default;

        MasterOptionCategoryModel _model;

        public void SetModel(MasterOptionCategoryModel model)
        {
            gameObject.SetActive(true);
            _model = model;
            _optionGroupText.text = model.name.Value;
        }

        public void OnClick()
        {
            _paramListPresenter.Replace(_model.id.Value);
        }
    }
}