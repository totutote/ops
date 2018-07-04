using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OPS.ViewModel;
using TMPro;

namespace OPS.View
{

	public class MaterailOptionListView : MonoBehaviour
	{
		public GameObject Culumn;

		private MaterialOptionListViewModel materialOptionListViewModel;

		void Start()
		{
			materialOptionListViewModel = new MaterialOptionListViewModel();
			foreach (var option in materialOptionListViewModel.option) {
				var culumn = Instantiate(Culumn);
				culumn.SetActive(true);
				var culumnText = culumn.transform.Find("Text").GetComponent<TextMeshProUGUI>();
				culumnText.text = (string)option.Record["name"];
				culumn.transform.SetParent(Culumn.transform.parent, false);
			}
		}
	}

}
