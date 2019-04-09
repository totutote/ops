using UnityEngine;
using OPS.Model;
using TMPro;
using Zenject;
using System.Linq;

namespace OPS.Presenter
{

    public class OptionSelectCategoryListPresenter : MonoBehaviour
    {
        [SerializeField]
        OptionSelectOptionGroupButtonPresenter baseOptionButton;

        [Inject]
        MasterOptionCategoryDB masterOptionCategoryDB;

        void Start()
        {
            Setup();
        }

        void Setup()
        {
            var masterOptionCategoryDic = masterOptionCategoryDB.All();
            foreach (var optionCategory in masterOptionCategoryDic)
            {
                var cpyBaseOptionButton = Instantiate(baseOptionButton);
                cpyBaseOptionButton.transform.SetParent(transform, false);
                cpyBaseOptionButton.SetModel(optionCategory.Value);
            }
        }

        void Setup(int userMixId)
        {

        }
    }



}
