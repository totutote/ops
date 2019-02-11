using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace OPS.Presenter
{

	public class MaterailOptionListButtonPresenter : MonoBehaviour
	{
		public Text culumnText;

		int MaterialOptionId {get;set;}

		public void SetText(string text)
		{
			culumnText.text = text;
		}

		public void OnSelectOption()
		{
			
		}
	}

}
