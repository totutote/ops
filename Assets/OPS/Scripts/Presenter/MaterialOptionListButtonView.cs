using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OPS.ViewModel;
using TMPro;

namespace OPS.View
{

	public class MaterailOptionListButtonView : MonoBehaviour
	{
		public MaterailOptionListView materailOptionListView;

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
