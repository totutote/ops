using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace OPS.Presenter
{
    public class MaterialSelectMaterialListPresenter : MonoBehaviour
    {
        [Inject]
        MaterialSelectOptionListPresenter.Factory _optionListPresenterFactory;

        [SerializeField]
        GameObject _addOptionListObject;

        public void OnAddList()
        {
            var cpyOptionList = _optionListPresenterFactory.Create();
            cpyOptionList.transform.SetParent(_addOptionListObject.transform, false);
        }

    }
}