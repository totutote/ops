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
        UserMixCandidateMaterialDB userMixCandidateMaterialDB;

        [SerializeField]
        GameObject _addRowGameobject;

        void Start()
        {
            var newUserMixCandidate = userMixCandidateMaterialDB.New();
        }

        public void AddNewOption(MasterOptionModel masterOptionModel)
        {
            var rowCpy = GameObject.Instantiate(_row);
            rowCpy.SetOption(masterOptionModel);
            rowCpy.transform.SetParent(_addRowGameobject.transform, false);
        }

    }

}