using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OPS.Model;
using UnityEngine;
using Zenject;

namespace OPS.Presenter
{
    public class MaterialSelectMaterialListPresenter : MonoBehaviour
    {
        [Inject]
        MaterialSelectOptionListPresenter.Factory _optionListPresenterFactory = null;

        [Inject]
        UserMixCandidateMaterialDB _userMixCandidateMaterialDB = null;

        [SerializeField]
        MaterialSelectPagePresenter _materialSelectPagePresenter = null;

        [SerializeField]
        GameObject _addOptionListObject = default;

        public void Recovery()
        {
            var userMixCandidateMaterialModels = _materialSelectPagePresenter.UserMixModel.UserMixCandidateMaterialModel;
            foreach (var userMixCandidateMaterialModel in userMixCandidateMaterialModels)
            {
                var cpyOptionList = _optionListPresenterFactory.Create();
                cpyOptionList.Setup(userMixCandidateMaterialModel.Value);
                cpyOptionList.transform.SetParent(_addOptionListObject.transform, false);
                cpyOptionList.transform.SetAsLastSibling();
            }
        }

        public void OnAddList()
        {
            UserMixModel userMixModel = _materialSelectPagePresenter.UserMixModel;
            var newUserMixCandidateMaterialModel = _userMixCandidateMaterialDB.New();
            newUserMixCandidateMaterialModel.user_mix_id.Value = userMixModel.id.Value;
            newUserMixCandidateMaterialModel.sort_index.Value = _userMixCandidateMaterialDB.Where("user_mix_id", userMixModel.id.Value.ToString()).Count;
            var cpyOptionList = _optionListPresenterFactory.Create();
            cpyOptionList.Setup(_userMixCandidateMaterialDB.Save(newUserMixCandidateMaterialModel).First().Value);
            cpyOptionList.transform.SetParent(_addOptionListObject.transform, false);
        }

    }
}