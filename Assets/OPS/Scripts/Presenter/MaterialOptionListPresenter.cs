using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using UniRx;
using OPS.Model;

namespace OPS.Presenter
{

	public class MaterailOptionListPresenter : MonoBehaviour
	{
		public GameObject Culumn;

		[Inject]
		ModelService<UserMaterialOption> masterOptionService;

		void Start()
		{
			var options = masterOptionService.Regist(UserMaterialOption.AllOptions());
			UserMaterialOption.subject.Subscribe(msg => Debug.Log("Subscribe1:" + msg));
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
