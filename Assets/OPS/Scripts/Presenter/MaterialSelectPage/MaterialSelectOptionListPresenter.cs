using UnityEngine;
using OPS.Model;
using TMPro;
using Zenject;

namespace OPS.Presenter
{
    public class MaterialSelectOptionListPresenter : MonoBehaviour
    {
        public MaterialSelectOptionAreaPresenter row;

        [Inject]
        UserMixCandidateMaterialDB userMixCandidateMaterialDB;

        void Start()
        {
            var newUserMixCandidate = userMixCandidateMaterialDB.New();
        }

    }

}