using UnityEngine;
using OPS.Model;
using TMPro;
using Zenject;

namespace OPS.Presenter
{
    public class MaterialSelectOptionListPresenter : MonoBehaviour
    {
        [SerializeField]
        MaterialSelectOptionAreaPresenter _row;

        [Inject]
        UserMixCandidateMaterialDB _userMixCandidateMaterialDB;

        [SerializeField]
        GameObject _addRowGameobject;

        void Start()
        {
            var newUserMixCandidate = _userMixCandidateMaterialDB.New();
        }

        public void AddNewOption(MasterOptionModel masterOptionModel)
        {
            var rowCpy = GameObject.Instantiate(_row);
            rowCpy.SetOption(masterOptionModel);
            rowCpy.transform.SetParent(_addRowGameobject.transform, false);
        }

        
        public class Factory : PlaceholderFactory<MaterialSelectOptionListPresenter>
        {
        }

    }

}