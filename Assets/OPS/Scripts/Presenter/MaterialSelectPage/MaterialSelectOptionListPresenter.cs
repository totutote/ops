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
        UserMixCompleteMaterialDB _userMixCompleteMaterialDB = null;

        [Inject]
        MaterialSelectOptionAreaPresenter.Factory _materialSelectOptionAreaFactory = null;

        [SerializeField]
        TextMeshProUGUI _materialNameText = default;

        [SerializeField]
        GameObject _addRowGameobject = default;

        [SerializeField]
        MaterialSelectAddFactorPresenter _materialSelectAddFactorPresenter = default;

        UserMixCandidateMaterialModel _userMixCandidateMaterialModel;

        public void Setup(UserMixCandidateMaterialModel userMixCandidateMaterialModel)
        {
            _userMixCandidateMaterialModel = userMixCandidateMaterialModel;
            var userMixCandidateMaterialOptionModels = _userMixCandidateMaterialModel.UserMixCandidateMaterialOptionTypeNormalModel;
            foreach (var userMixCandidateMaterialOptionModel in userMixCandidateMaterialOptionModels)
            {
                var rowCpy = _materialSelectOptionAreaFactory.Create();
                rowCpy.Recovery(userMixCandidateMaterialOptionModel.Value);
                rowCpy.transform.SetParent(_addRowGameobject.transform, false);
                rowCpy.transform.SetSiblingIndex(userMixCandidateMaterialOptionModel.Value.sort_index.Value);
            }
            _userMixCandidateMaterialModel.sort_index.Subscribe(sort_index => { SetMaterialNameText(sort_index); }).AddTo(gameObject);
            _materialSelectAddFactorPresenter.Setup(_userMixCandidateMaterialModel.UserMixCandidateMaterialOptionTypeFartorModel);
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
            if (_userMixCandidateMaterialModel.UserMixCandidateMaterialOptionTypeNormalModel.Count >= _userMixCompleteMaterialDB.ExtraRateTable.Count) return;
            var rowCpy = _materialSelectOptionAreaFactory.Create();
            rowCpy.SetOption(masterOptionModel, _userMixCandidateMaterialModel);
            rowCpy.transform.SetParent(_addRowGameobject.transform, false);
        }

        public void AddNewFactor(MasterOptionModel masterOptionModel)
        {
            var factorModel = _userMixCandidateMaterialModel.UserMixCandidateMaterialOptionTypeFartorModel;
            if (factorModel != null)
            {
                factorModel.master_option_id.Value = masterOptionModel.id.Value;
                _userMixCandidateMaterialOptionDB.Save(factorModel);
                _materialSelectAddFactorPresenter.Setup(factorModel);
                return;
            }
            var newFactorModel = _userMixCandidateMaterialOptionDB.New();
            newFactorModel.master_option_id.Value = masterOptionModel.id.Value;
            newFactorModel.option_type.Value = (int)UserMixCandidateMaterialOptionDB.OptionType.Factor;
            newFactorModel.user_mix_candidate_material_id.Value = _userMixCandidateMaterialModel.id.Value;
            var newModel = _userMixCandidateMaterialOptionDB.Save(newFactorModel);
            _materialSelectAddFactorPresenter.Setup(newModel.First().Value);
        }

        public void RemoveFactor()
        {
            var factorModel = _userMixCandidateMaterialModel.UserMixCandidateMaterialOptionTypeFartorModel;
            if (factorModel == null) return;
            _userMixCandidateMaterialOptionDB.Delete(factorModel);
            _materialSelectAddFactorPresenter.Setup(null);
        }

        public void OnClickRemoveMaterial()
        {
            foreach (var userMixCandidateMaterialOptionModel in _userMixCandidateMaterialModel.UserMixCandidateMaterialOptionTypeNormalModel)
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