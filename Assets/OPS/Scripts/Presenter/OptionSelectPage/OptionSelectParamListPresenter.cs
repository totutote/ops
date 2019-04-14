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
        OptionSelectPagePresenter _pagePresenter;

        [SerializeField]
        OptionSelectParamButtonPresenter paramButton;

        [Inject]
        MasterOptionDB masterOptionDB;

        int _categoryId = 1;

        void Start()
        {
            Setup();
        }

        void Setup()
        {
            var masterOptionParamDic = masterOptionDB.Where("category_id", _categoryId.ToString());
            foreach (var param in masterOptionParamDic)
            {
                var cpyParamButton = Instantiate(paramButton);
                cpyParamButton.transform.SetParent(transform, false);
                cpyParamButton.SetPagePresenter(_pagePresenter);
                cpyParamButton.SetModel(param.Value);
            }
        }

        public void Replace(int categoryId)
        {
            ListClear();
            _categoryId = categoryId;
            Setup();
        }

        void ListClear()
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

}
