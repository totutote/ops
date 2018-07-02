using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OPS.ViewModel;
using TMPro;

namespace OPS.View
{

	public class OptionListView : MonoBehaviour
	{
		public GameObject Culumn;

		private OptionListViewModel optionListViewModel;

		void Start()
		{
			optionListViewModel = new OptionListViewModel();
			foreach (var option in optionListViewModel.option) {
				var culumn = Instantiate(Culumn);
				culumn.SetActive(true);
				var culumnText = culumn.transform.Find("Text").GetComponent<TextMeshProUGUI>();
				culumnText.text = option.name;
				culumn.transform.SetParent(Culumn.transform.parent, false);
			}
		}
	}

}
