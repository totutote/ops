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
        UserMixCandidateMaterialDB _userMixCandidateMaterialDB;

        [Inject]
        MaterialSelectOptionAreaPresenter.Factory _materialSelectOptionAreaFactory;

        [SerializeField]
        GameObject _addRowGameobject;

        UserMixCandidateMaterialModel _userMixCandidateMaterialModel;

        void Start()
        {
        }

        public void AddSetup(UserMixModel userMixModel)
        {
            var newUserMixCandidateMaterialModel = _userMixCandidateMaterialDB.New();
            newUserMixCandidateMaterialModel.user_mix_id.Value = userMixModel.id.Value;
            _userMixCandidateMaterialModel = _userMixCandidateMaterialDB.Save(newUserMixCandidateMaterialModel).First().Value;
        }

        public void AddNewOption(MasterOptionModel masterOptionModel)
        {
            var rowCpy = _materialSelectOptionAreaFactory.Create();
            rowCpy.SetOption(masterOptionModel);
            rowCpy.transform.SetParent(_addRowGameobject.transform, false);
        }
        
        public class Factory : PlaceholderFactory<MaterialSelectOptionListPresenter>
        {
        }

    }

}