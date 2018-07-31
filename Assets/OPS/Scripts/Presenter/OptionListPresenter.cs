using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OPS.Model;
using TMPro;
using Zenject;

namespace OPS.Presenter
{

	public class OptionListPresenter : MonoBehaviour
	{
		public GameObject Culumn;

		[Inject]
		ModelService<MasterOption> masterOptionService;

		void Start()
		{
			var options = masterOptionService.Regist(MasterOption.AllOptions());
			foreach (var option in options) {
				var culumn = Instantiate(Culumn);
				culumn.SetActive(true);
				var culumnText = culumn.transform.Find("Text").GetComponent<TextMeshProUGUI>();
				culumnText.text = (string)option.Value.Record["name"].Value;
				culumn.transform.SetParent(Culumn.transform.parent, false);
			}
		}
	}

}
