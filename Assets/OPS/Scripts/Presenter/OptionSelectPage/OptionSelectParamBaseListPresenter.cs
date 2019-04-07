using UnityEngine;
using OPS.Model;
using TMPro;
using Zenject;
using System.Linq;

namespace OPS.Presenter
{

    public class OptionSelectParamBaseListPresenter : MonoBehaviour
    {
        [SerializeField]
        OptionSelectOptionGroupButtonPresenter baseOptionButton;

        [Inject]
        MasterOptionParamBaseDB masterOptionParamBaseDB;

        void Start()
        {
            Setup();
        }

        void Setup()
        {
            var masterOptionParamBaseDic = masterOptionParamBaseDB.All();
            foreach (var paramBase in masterOptionParamBaseDic)
            {
                var cpyBaseOptionButton = Instantiate(baseOptionButton);
                cpyBaseOptionButton.transform.SetParent(transform, false);
                cpyBaseOptionButton.SetModel(paramBase.Value);
            }
        }

        void Setup(int userMixId)
        {

        }
    }



}
