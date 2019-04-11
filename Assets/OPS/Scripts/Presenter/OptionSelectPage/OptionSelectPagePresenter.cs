using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using OPS.Model;

namespace OPS.Presenter
{

	public class OptionSelectPagePresenter : MonoBehaviour
	{
        MaterialSelectOptionListPresenter _materialSelectOptionListPresenter;

        public void OnActive(MaterialSelectOptionListPresenter materialSelectOptionListPresenter)
        {
            _materialSelectOptionListPresenter = materialSelectOptionListPresenter;
        }

        public void OnSelectOption(MasterOptionModel masterOptionModel)
        {
            _materialSelectOptionListPresenter.AddNewOption(masterOptionModel);
        }
	}

}
