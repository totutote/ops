using OPS.Model;
using UnityEngine;
using Zenject;

namespace OPS.Presenter
{
    public class MixPageAgendaOptionListPresenter : MonoBehaviour
    {
        [Inject]
        MixPageOptionAgendaAreaPresenter.Factory _mixPageOptionAgendaAreaFactory = default;

        [Inject]
        UserMixCompleteMaterialDB _userMixCompleteMaterialDB = default;

        [SerializeField]
        GameObject AddAgendaOptionListObject = default;

        UserMixModel _userMixModel;

        public void Reload(UserMixModel userMixModel)
        {
            _userMixModel = userMixModel;
            foreach (Transform child in AddAgendaOptionListObject.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            foreach (var userMixCompleteMaterialModel in _userMixModel.UserMixCompleteMaterialModels)
            {
                if (userMixCompleteMaterialModel.Value.select_agenda.Value == 0) continue;
                var _cpyMixPageOptionAgendaArea = _mixPageOptionAgendaAreaFactory.Create();
                _cpyMixPageOptionAgendaArea.Setup(userMixCompleteMaterialModel.Value);
                _cpyMixPageOptionAgendaArea.transform.SetParent(AddAgendaOptionListObject.transform, false);
            }
        }

    }
}