using UnityEngine;
using OPS.Model;
using TMPro;
using Zenject;

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

        void Start()
        {
            var newUserMixCandidate = _userMixCandidateMaterialDB.New();
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