using System.Linq;
using OPS.Model;
using UnityEngine;
using Zenject;

namespace OPS.Presenter
{
    public class MixPageAgendaOptionListPresenter : MonoBehaviour
    {
        [Inject]
        MixPageOptionAgendaAreaPresenter.Factory _mixPageOptionAgendaAreaFactory = default;

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
            AddAgendaOptionListObject.transform.DetachChildren();
            foreach (var userMixCompleteMaterialModel in _userMixModel.UserMixCompleteMaterialSelectAgendaModels.OrderBy(x => x.Value.select_agenda.Value))
            {
                var _cpyMixPageOptionAgendaArea = _mixPageOptionAgendaAreaFactory.Create();
                _cpyMixPageOptionAgendaArea.Setup(userMixCompleteMaterialModel.Value);
                _cpyMixPageOptionAgendaArea.transform.SetParent(AddAgendaOptionListObject.transform, false);
                _cpyMixPageOptionAgendaArea.transform.SetSiblingIndex(userMixCompleteMaterialModel.Value.select_agenda.Value - 1);
            }
        }
    }
}