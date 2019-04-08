using UnityEngine;
using OPS.Model;
using TMPro;
using Zenject;
using System.Linq;

namespace OPS.Presenter
{

    public class OptionSelectParamListPresenter : MonoBehaviour
    {
        [SerializeField]
        OptionSelectParamButtonPresenter paramButton;

        [Inject]
        MasterOptionParamDB masterOptionParamDB;

        int _categoryId = 1;

        public int CategoryId
        {
            get {return _categoryId;}
            set {_categoryId = value;}
        }

        void Start()
        {
            Setup();
        }

        void Setup()
        {
            var masterOptionParamDic = masterOptionParamDB.Where("base_id", _categoryId.ToString());
            foreach (var param in masterOptionParamDic)
            {
                var cpyParamButton = Instantiate(paramButton);
                cpyParamButton.transform.SetParent(transform, false);
                cpyParamButton.SetModel(param.Value);
            }
        }

        void Setup(int userMixId)
        {

        }
    }

}
