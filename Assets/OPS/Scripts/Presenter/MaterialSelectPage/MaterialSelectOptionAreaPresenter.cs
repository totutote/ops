using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using OPS.Model;
using Zenject;
using System.Linq;

namespace OPS.Presenter
{

	public class MaterialSelectOptionAreaPresenter : MonoBehaviour
	{
		[Inject]
		UserMixCandidateMaterialOptionDB _userMixCandidateMaterialOptionDB;

		public Text culumnText;

		int MaterialOptionId {get;set;}

		UserMixCandidateMaterialOptionModel _userMixCandidateMaterialOptionModel;

		public void SetText(string text)
		{
			culumnText.text = text;
		}

		public void SetOption(MasterOptionModel masterOptionModel)
		{
			var newUserMixCandidateMaterialOptionModel = _userMixCandidateMaterialOptionDB.New();
			newUserMixCandidateMaterialOptionModel.master_option_id.Value = masterOptionModel.id.Value;
			var _userMixCandidateMaterialOptionModel = _userMixCandidateMaterialOptionDB.Save(newUserMixCandidateMaterialOptionModel).First().Value;
			culumnText.text = _userMixCandidateMaterialOptionModel.MasterOptionModel.name.Value;
		}

		public void OnSelectOption()
		{			
		}

        public class Factory : PlaceholderFactory<MaterialSelectOptionAreaPresenter>
		{
		}
	}

}
