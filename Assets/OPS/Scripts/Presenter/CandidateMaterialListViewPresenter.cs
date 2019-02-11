using UnityEngine;
using OPS.Model;
using TMPro;
using Zenject;

namespace OPS.Presenter
{

    public class CandidateMaterialListViewPresenter : MonoBehaviour
    {
        public GameObject Culumn;

        [Inject]
        UserMixDB UserMixDB;

        void Start()
        {
            Setup();
        }

        void Setup()
        {
            var newUserMix = UserMixDB.New();
            Debug.Log(newUserMix);
            newUserMix.name.Value = "testModel2";
            var saveUserMix = UserMixDB.Save(newUserMix);
            Debug.Log("id" + saveUserMix[0].id.Value);
            Debug.Log("name" + saveUserMix[0].name.Value);
        }

        void Setup(int userMixId)
        {

        }
    }



}
