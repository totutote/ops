using UnityEngine;
using UnityEngine.UI;
using OPS.Model;
using Zenject;
using System.Linq;
using UniRx;

namespace OPS.Presenter
{

    public class MaterialSelectOptionAreaPresenter : MonoBehaviour
    {
        [Inject]
        UserMixCandidateMaterialOptionDB _userMixCandidateMaterialOptionDB = null;

        public Text culumnText;

        UserMixCandidateMaterialOptionModel _userMixCandidateMaterialOptionModel;

        public void Recovery(UserMixCandidateMaterialOptionModel userMixCandidateMaterialOptionModel)
        {
            _userMixCandidateMaterialOptionModel = userMixCandidateMaterialOptionModel;
            _userMixCandidateMaterialOptionModel.master_option_id.Subscribe(id => { culumnText.text = _userMixCandidateMaterialOptionModel.MasterOptionModel.name.Value; }).AddTo(gameObject);;
        }

        public void SetOption(MasterOptionModel masterOptionModel, UserMixCandidateMaterialModel userMixCandidateMaterialModel)
        {
            var newUserMixCandidateMaterialOptionModel = _userMixCandidateMaterialOptionDB.New();
            newUserMixCandidateMaterialOptionModel.user_mix_candidate_material_id.Value = userMixCandidateMaterialModel.id.Value;
            newUserMixCandidateMaterialOptionModel.master_option_id.Value = masterOptionModel.id.Value;
            newUserMixCandidateMaterialOptionModel.sort_index.Value = _userMixCandidateMaterialOptionDB.Where("user_mix_candidate_material_id", userMixCandidateMaterialModel.id.Value.ToString()).Count;
            newUserMixCandidateMaterialOptionModel.option_type.Value = (int)UserMixCandidateMaterialOptionDB.OptionType.Normal;
            _userMixCandidateMaterialOptionModel = _userMixCandidateMaterialOptionDB.Save(newUserMixCandidateMaterialOptionModel).First().Value;
            _userMixCandidateMaterialOptionModel.master_option_id.Subscribe(id => { culumnText.text = _userMixCandidateMaterialOptionModel.MasterOptionModel.name.Value; }).AddTo(gameObject);;
        }

        public void OnSelectOption()
        {
        }
        
        public void OnClickRemoveButton()
        {
            _userMixCandidateMaterialOptionDB.Delete(_userMixCandidateMaterialOptionModel);
            Destroy(gameObject);
            RestoreSortIndex();
        }

        private void RestoreSortIndex()
        {
            var materialOptions = _userMixCandidateMaterialOptionDB.Where("user_mix_candidate_material_id", _userMixCandidateMaterialOptionModel.user_mix_candidate_material_id.Value.ToString());
            int sortIndex = 0;
            foreach (var materialOption in materialOptions)
            {
                materialOption.Value.sort_index.Value = sortIndex;
                _userMixCandidateMaterialOptionDB.Save(materialOption.Value);
                sortIndex++;
            }
        }

        public class Factory : PlaceholderFactory<MaterialSelectOptionAreaPresenter>
        {
        }
    }

}
