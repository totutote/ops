using UnityEngine;
using OPS.Model;
using TMPro;
using Zenject;
using System.Linq;

namespace OPS.Presenter
{

    public class MaterialSelectPagePresenter : MonoBehaviour
    {
        public TMP_InputField nameInput;

        [Inject]
        UserMixDB UserMixDB;

        UserMixModel _userMixModel;

        public void Setup()
        {
            var newUserMix = UserMixDB.New();
            Debug.Log(newUserMix);
            newUserMix.name.Value = "合成名";
            var _userMixModel = UserMixDB.Save(newUserMix).Values.First();
            nameInput.text = _userMixModel.name.Value;
        }

        public void Setup(UserMixModel userMixModel)
        {
            _userMixModel = userMixModel;
            Debug.Log(_userMixModel);
            nameInput.text = _userMixModel.name.Value;
        }

        public class Factory : PlaceholderFactory<MaterialSelectPagePresenter>
        {
        }
    }
}
