using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using OPS.Model;
using Zenject;
using System.Linq;
using UniRx;
using System;

namespace OPS.Presenter
{

    public class MaterialSelectOptionAreaPresenter : MonoBehaviour
    {
        [Inject]
        UserMixCandidateMaterialOptionDB _userMixCandidateMaterialOptionDB = null;

        public Text culumnText;

        UserMixCandidateMaterialOptionModel _userMixCandidateMaterialOptionModel;

        IDisposable _textDisposable;

        public void Recovery(UserMixCandidateMaterialOptionModel userMixCandidateMaterialOptionModel)
        {
            _userMixCandidateMaterialOptionModel = userMixCandidateMaterialOptionModel;
            _textDisposable = _userMixCandidateMaterialOptionModel.master_option_id.Subscribe(id => { culumnText.text = _userMixCandidateMaterialOptionModel.MasterOptionModel.name.Value; });
        }

        public void SetOption(MasterOptionModel masterOptionModel, UserMixCandidateMaterialModel userMixCandidateMaterialModel)
        {
            var newUserMixCandidateMaterialOptionModel = _userMixCandidateMaterialOptionDB.New();
            newUserMixCandidateMaterialOptionModel.user_mix_candidate_material_id.Value = userMixCandidateMaterialModel.id.Value;
            newUserMixCandidateMaterialOptionModel.master_option_id.Value = masterOptionModel.id.Value;
            newUserMixCandidateMaterialOptionModel.sort_index.Value = _userMixCandidateMaterialOptionDB.Where("user_mix_candidate_material_id", userMixCandidateMaterialModel.id.Value.ToString()).Count;
            _userMixCandidateMaterialOptionModel = _userMixCandidateMaterialOptionDB.Save(newUserMixCandidateMaterialOptionModel).First().Value;
            _textDisposable = _userMixCandidateMaterialOptionModel.master_option_id.Subscribe(id => { culumnText.text = _userMixCandidateMaterialOptionModel.MasterOptionModel.name.Value; });
        }

        public void OnSelectOption()
        {
        }

        void OnDestroy()
        {
            _textDisposable.Dispose();
        }

        public class Factory : PlaceholderFactory<MaterialSelectOptionAreaPresenter>
        {
        }
    }

}
