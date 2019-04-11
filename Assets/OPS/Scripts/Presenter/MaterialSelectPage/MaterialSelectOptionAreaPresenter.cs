using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using OPS.Model;

namespace OPS.Presenter
{

	public class MaterialSelectOptionAreaPresenter : MonoBehaviour
	{
		public Text culumnText;

		int MaterialOptionId {get;set;}

		MasterOptionModel _masterOptionModel;

		public void SetText(string text)
		{
			culumnText.text = text;
		}

		public void SetOption(MasterOptionModel masterOptionModel)
		{
			_masterOptionModel = masterOptionModel;
			culumnText.text = _masterOptionModel.name.Value;
		}

		public void OnSelectOption()
		{
			
		}
	}

}
