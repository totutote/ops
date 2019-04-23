using UnityEngine;
using Zenject;
using OPS.Model;

namespace OPS.Presenter
{
    public class CompleteSelectPagePresenter : MonoBehaviour
    {
        [Inject]
        UserMixDB _userMixDB = null;

        [Inject]
        CompleteSelectListButtonPresenter.Factory _completeSelectListButtonFactory = null;

        [SerializeField]
        GameObject _addListObject = default;

        void Start()
        {
            var allUserMix = _userMixDB.All();
            foreach(var userMix in allUserMix)
            {
                var _cpyCompleteSelectListButton = _completeSelectListButtonFactory.Create();
                _cpyCompleteSelectListButton.Setup(userMix.Value);
                _cpyCompleteSelectListButton.transform.SetParent(_addListObject.transform, false);
            }
        }

        public class Factory : PlaceholderFactory<CompleteSelectPagePresenter>
        {
        }

    }
}