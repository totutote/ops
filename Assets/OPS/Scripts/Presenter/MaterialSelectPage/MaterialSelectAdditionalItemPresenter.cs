﻿using UnityEngine;
using UnityEngine.UI;
using OPS.Model;
using Zenject;
using System.Collections.Generic;
using TMPro;

namespace OPS.Presenter
{
    public class MaterialSelectAdditionalItemPresenter : MonoBehaviour
    {
        [Inject]
        MasterAdditionalItemDB _masterAdditionalItemDB = null;

        [SerializeField]
        TMP_Dropdown _dropDown = default;

        UserMixModel _userMixModel;

        public void Setup(UserMixModel userMixModel)
        {
            _userMixModel = userMixModel;

            _dropDown.ClearOptions();
            List<string> listOptions = new List<string>();
            listOptions.Add("追加オプションなし");
            foreach (var additionalItem in _masterAdditionalItemDB.All())
            {
                listOptions.Add(additionalItem.Value.name.Value);
            }
            _dropDown.AddOptions(listOptions);
            UserMixKeyValueModel userAdditionalItem = _userMixModel.UserMixAdditionalItem;
            if (userAdditionalItem == null)
            {
                _dropDown.value = 0;
            }
            else
            {
                _dropDown.value = int.Parse(userAdditionalItem.value.Value);
            }
            _dropDown.RefreshShownValue();
        }

        public void OnValueChanged(TMP_Dropdown result)
        {
            _userMixModel.SaveOrCreateAdditionalItem(result.value);
        }
    }
}
