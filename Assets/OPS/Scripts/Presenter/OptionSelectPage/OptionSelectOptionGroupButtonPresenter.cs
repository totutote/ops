using UnityEngine;
using TMPro;
using OPS.Model;

namespace OPS.Presenter
{
    public class OptionSelectOptionGroupButtonPresenter : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _optionGroupText;

        MasterOptionCategoryModel _model;

        public void SetModel(MasterOptionCategoryModel model)
        {
            _model = model;
            _optionGroupText.text = model.name.Value;
        }
    }
}