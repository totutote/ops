using UnityEngine;
using OPS.Model;
using TMPro;
using Zenject;
using System.Linq;
using UniRx;

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
        TextMeshProUGUI _materialNameText = default;

        [SerializeField]
        GameObject _addRowGameobject = default;

        UserMixCandidateMaterialModel _userMixCandidateMaterialModel;

        public void Recovery(UserMixCandidateMaterialModel userMixCandidateMaterialModel)
        {
            _userMixCandidateMaterialModel = userMixCandidateMaterialModel;
            var userMixCandidateMaterialOptionModels = _userMixCandidateMaterialModel.UserMixCandidateMaterialOptionModel;
            foreach (var userMixCandidateMaterialOptionModel in userMixCandidateMaterialOptionModels)
            {
                var rowCpy = _materialSelectOptionAreaFactory.Create();
                rowCpy.Recovery(userMixCandidateMaterialOptionModel.Value);
                rowCpy.transform.SetParent(_addRowGameobject.transform, false);
                rowCpy.transform.SetSiblingIndex(userMixCandidateMaterialOptionModel.Value.sort_index.Value + 1);
            }
            _userMixCandidateMaterialModel.sort_index.Subscribe(sort_index => { SetMaterialNameText(sort_index); }).AddTo(gameObject);

        }

        public void AddSetup(UserMixModel userMixModel)
        {
            var newUserMixCandidateMaterialModel = _userMixCandidateMaterialDB.New();
            newUserMixCandidateMaterialModel.user_mix_id.Value = userMixModel.id.Value;
            newUserMixCandidateMaterialModel.sort_index.Value = _userMixCandidateMaterialDB.Where("user_mix_id", userMixModel.id.Value.ToString()).Count;
            _userMixCandidateMaterialModel = _userMixCandidateMaterialDB.Save(newUserMixCandidateMaterialModel).First().Value;
            _userMixCandidateMaterialModel.sort_index.Subscribe(sort_index => { SetMaterialNameText(sort_index); }).AddTo(gameObject);
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

        public void OnClickRemoveMaterial()
        {
            foreach (var userMixCandidateMaterialOptionModel in _userMixCandidateMaterialModel.UserMixCandidateMaterialOptionModel)
            {
                _userMixCandidateMaterialOptionDB.Delete(userMixCandidateMaterialOptionModel.Value);
            }
            _userMixCandidateMaterialDB.Delete(_userMixCandidateMaterialModel);
            Destroy(gameObject);
            RestoreSortIndex();
        }

        private void RestoreSortIndex()
        {
            var materials = _userMixCandidateMaterialDB.Where("user_mix_id", _userMixCandidateMaterialModel.user_mix_id.Value.ToString());
            int sortIndex = 0;
            foreach (var material in materials)
            {
                material.Value.sort_index.Value = sortIndex;
                _userMixCandidateMaterialDB.Save(material.Value);
                sortIndex++;
            }
        }

        public class Factory : PlaceholderFactory<MaterialSelectOptionListPresenter>
        {
        }

    }

}