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

        [Inject]
        MixPagePresenter.Factory _mixPageFactory;

        [Inject]
        PageManager _pageManager;

        [SerializeField]
        MaterialSelectMaterialListPresenter _materialSelectMaterialListPresenter;

        UserMixModel _userMixModel;

        public UserMixModel UserMixModel
        {
            get { return _userMixModel; }
        }

        public void Setup()
        {
            var newUserMix = UserMixDB.New();
            Debug.Log(newUserMix);
            newUserMix.name.Value = "合成名";
            _userMixModel = UserMixDB.Save(newUserMix).Values.First();
            _userMixModel.name.Value += _userMixModel.id.Value;
            UserMixDB.Save(_userMixModel);
            nameInput.text = _userMixModel.name.Value;
        }

        public void Setup(UserMixModel userMixModel)
        {
            _userMixModel = userMixModel;
            nameInput.text = _userMixModel.name.Value;
            _materialSelectMaterialListPresenter.Recovery();
        }

        public void OnClickMixButton()
        {
            var cpyMixPage = _mixPageFactory.Create();
            cpyMixPage.Setup(_userMixModel);
            _pageManager.ChangePage(cpyMixPage);
        }

        public class Factory : PlaceholderFactory<MaterialSelectPagePresenter>
        {
        }
    }
}
