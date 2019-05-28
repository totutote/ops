using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using OPS.Model;
using Zenject;

namespace OPS.Presenter
{

	public class MaterialSelectAddOptionPresenter : MonoBehaviour
	{
		[Inject]
        PageManager _pageManager = null;

        [Inject]
        OptionSelectPagePresenter.Factory _optionSelectPageFactory = null;

        [SerializeField]
        MaterialSelectOptionListPresenter _optionListPresenter = default;

		[SerializeField]
		Button _addOptionButton = default;

		OptionSelectPagePresenter _cpyOptionSelectPage;

		public void OnClick()
		{
			_cpyOptionSelectPage = _pageManager.AddPage(_optionSelectPageFactory.Create());
            _cpyOptionSelectPage.OnActive(OnAddSelectOption);
		}

		void OnAddSelectOption(MasterOptionModel masterOptionModel)
		{
			_optionListPresenter.AddNewOption(masterOptionModel);
			this.transform.SetAsLastSibling();
			_pageManager.DestroyPage<OptionSelectPagePresenter>();
		}

		public void EnableButton(bool isEnable)
		{
			_addOptionButton.interactable = isEnable;
		}

	}

}
