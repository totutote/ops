using UnityEngine;
using OPS.Model;
using TMPro;
using Zenject;
using System.Linq;

namespace OPS.Presenter
{
    public class MaterialSelectOptionListPresenter : MonoBehaviour
    {
        [Inject]
        UserMixCandidateMaterialDB _userMixCandidateMaterialDB = null;

        [Inject]
        UserMixCandidateMaterialOptionDB _userMixCandidateMaterialOptionDB = null;

        [Inject]
        MaterialSelectOptionAreaPresenter.Factory _materialSelectOptionAreaFactory = null;

        [SerializeField]
        TextMeshProUGUI _materialNameText;

        [SerializeField]
        GameObject _addRowGameobject = default;

        UserMixCandidateMaterialModel _userMixCandidateMaterialModel;

        public void Recovery(UserMixCandidateMaterialModel userMixCandidateMaterialModel)
        {
            _userMixCandidateMaterialModel = userMixCandidateMaterialModel;
            var userMixCandidateMaterialOptionModels = _userMixCandidateMaterialModel.UserMixCandidateMaterialOptionModel;
            foreach(var userMixCandidateMaterialOptionModel in userMixCandidateMaterialOptionModels)
            {
                var rowCpy = _materialSelectOptionAreaFactory.Create();
                rowCpy.Recovery(userMixCandidateMaterialOptionModel.Value);
                rowCpy.transform.SetParent(_addRowGameobject.transform, false);
                rowCpy.transform.SetSiblingIndex(userMixCandidateMaterialOptionModel.Value.sort_index.Value + 1);
            }
            SetMaterialNameText(_userMixCandidateMaterialModel.sort_index.Value);
        }

        public void AddSetup(UserMixModel userMixModel)
        {
            var newUserMixCandidateMaterialModel = _userMixCandidateMaterialDB.New();
            newUserMixCandidateMaterialModel.user_mix_id.Value = userMixModel.id.Value;
            newUserMixCandidateMaterialModel.sort_index.Value = _userMixCandidateMaterialDB.Where("user_mix_id", userMixModel.id.Value.ToString()).Count;
            _userMixCandidateMaterialModel = _userMixCandidateMaterialDB.Save(newUserMixCandidateMaterialModel).First().Value;
            SetMaterialNameText(_userMixCandidateMaterialModel.sort_index.Value);
        }

        void SetMaterialNameText(int sortIndex)
        {
            string nameText = sortIndex == 0 ? "本体" : "素材" + sortIndex;
            _materialNameText.text = nameText;
        }

        public void AddNewOption(MasterOptionModel masterOptionModel)
        {
            var sameCategoryIncludeModel = _userMixCandidateMaterialModel.SameCategoryIncludeModel(masterOptionModel);
            if (sameCategoryIncludeModel != null)
            {
                sameCategoryIncludeModel.master_option_id.Value = masterOptionModel.id.Value;
                _userMixCandidateMaterialOptionDB.Save(sameCategoryIncludeModel);
                return;
            }
            var rowCpy = _materialSelectOptionAreaFactory.Create();
            rowCpy.SetOption(masterOptionModel, _userMixCandidateMaterialModel);
            rowCpy.transform.SetParent(_addRowGameobject.transform, false);
        }
        
        public class Factory : PlaceholderFactory<MaterialSelectOptionListPresenter>
        {
        }

    }

}