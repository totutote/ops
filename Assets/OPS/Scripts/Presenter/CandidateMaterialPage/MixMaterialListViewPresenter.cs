using UnityEngine;
using OPS.Model;
using TMPro;
using Zenject;

namespace OPS.Presenter
{
    public class MixMaterialListViewPresenter : MonoBehaviour
    {
        public MaterialOptionListPresenter row;

        [Inject]
        UserMixCandidateMaterialDB userMixCandidateMaterialDB;

        void Start()
        {
            var newUserMixCandidate = userMixCandidateMaterialDB.New();
        }

    }

}