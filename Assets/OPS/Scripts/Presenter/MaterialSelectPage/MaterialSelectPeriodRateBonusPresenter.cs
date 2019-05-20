using UnityEngine;
using UnityEngine.UI;
using OPS.Model;
using Zenject;
using System.Collections.Generic;
using TMPro;
using System.Linq;

namespace OPS.Presenter
{
    public class MaterialSelectPeriodRateBonusPresenter : MonoBehaviour
    {
        [SerializeField]
        TMP_Dropdown _dropDown = default;

        UserMixModel _userMixModel;

        public void Setup(UserMixModel userMixModel)
        {
            _userMixModel = userMixModel;

            _dropDown.ClearOptions();
            List<string> listOptions = new List<string>();
            listOptions.Add("報酬期間等の確率合計");
            foreach (var rateKey in Enumerable.Range(1, 20))
            {
                listOptions.Add((rateKey * 5).ToString() + "%");
            }
            _dropDown.AddOptions(listOptions);
            UserMixKeyValueModel userPeriodRateBonus = _userMixModel.UserMixPeriodRateBonusKeyValue;
            if (userPeriodRateBonus == null)
            {
                _dropDown.value = 0;
            }
            else
            {
                _dropDown.value = int.Parse(userPeriodRateBonus.value.Value);
            }
            _dropDown.RefreshShownValue();
        }

        public void OnValueChanged(TMP_Dropdown result)
        {
            _userMixModel.SaveOrCreatePeriodRateBonus(result.value);
        }
    }
}
