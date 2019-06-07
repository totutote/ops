using UnityEngine;
using OPS.Model;
using TMPro;
using Zenject;
using System.Linq;

namespace OPS.Presenter
{

    public class MaterialSelectPagePresenter : MonoBehaviour
    {
        [SerializeField]
        TMP_InputField _nameInput = default;

        [Inject]
        UserMixDB UserMixDB = null;

        [Inject]
        MixPagePresenter.Factory _mixPageFactory = null;

        [Inject]
        PageManager _pageManager = null;

        [Inject]
        CompleteSelectPagePresenter.Factory _completeSelectPageFactory = null;

        [SerializeField]
        MaterialSelectMaterialListPresenter _materialSelectMaterialListPresenter = default;

        [SerializeField]
        MaterialSelectAdditionalItemPresenter _materialSelectAdditionalItemPresenter = default;

        [SerializeField]
        MaterialSelectSameNameBonusPresenter _materialSelectSameNameBonusPresenter = default;

        [SerializeField]
        MaterialSelectPeriodRateBonusPresenter _materialSelectPeriodRateBonusPresenter = default;

        UserMixModel _userMixModel;

        public UserMixModel UserMixModel
        {
            get { return _userMixModel; }
        }

        public void Setup()
        {
            var newUserMix = UserMixDB.New();
            Debug.Log(newUserMix);
            newUserMix.name.Value = "合成";
            _userMixModel = UserMixDB.Save(newUserMix).Values.First();
            _userMixModel.name.Value += _userMixModel.id.Value;
            UserMixDB.Save(_userMixModel);
            _nameInput.text = _userMixModel.name.Value;
            _materialSelectAdditionalItemPresenter.Setup(_userMixModel);
            _materialSelectSameNameBonusPresenter.Setup(_userMixModel);
            _materialSelectPeriodRateBonusPresenter.Setup(_userMixModel);
        }

        public void Setup(UserMixModel userMixModel)
        {
            _userMixModel = userMixModel;
            _nameInput.text = _userMixModel.name.Value;
            _materialSelectMaterialListPresenter.Recovery();
            _materialSelectAdditionalItemPresenter.Setup(_userMixModel);
            _materialSelectSameNameBonusPresenter.Setup(_userMixModel);
            _materialSelectPeriodRateBonusPresenter.Setup(_userMixModel);
        }

        public void OnClickMixButton()
        {
            var cpyMixPage = _mixPageFactory.Create();
            cpyMixPage.Setup(_userMixModel);
            _pageManager.ChangePage(cpyMixPage);
        }

        public void OnValueChangeName()
        {
            _userMixModel.name.Value = _nameInput.text;
            UserMixDB.Save(_userMixModel);
        }

        public void OnClickBackButton()
        {
            var cpyCompleteSelectPage = _completeSelectPageFactory.Create();
            _pageManager.ChangePage(cpyCompleteSelectPage);
            if (_userMixModel.UserMixCandidateMaterialModel.Count == 0)
            {
                _userMixModel.DestroyCompleteModel();
                UserMixDB.Delete(_userMixModel);
            }
        }

        public class Factory : PlaceholderFactory<MaterialSelectPagePresenter>
        {
        }
    }
}
