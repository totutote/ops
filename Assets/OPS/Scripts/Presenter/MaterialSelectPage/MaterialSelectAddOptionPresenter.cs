using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace OPS.Presenter
{

	public class MaterialSelectAddOptionPresenter : MonoBehaviour
	{
        [SerializeField]
        OptionSelectPagePresenter _optionSelectPage;

        [SerializeField]
        MaterialSelectOptionListPresenter _optionListPresenter;

		public void OnClick()
		{
            _optionSelectPage.OnActive(_optionListPresenter);
		}
	}

}
