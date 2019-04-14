using UnityEngine;
using OPS.Model;
using Zenject;

namespace OPS.Presenter
{

    public class OptionSelectPagePresenter : MonoBehaviour
	{
        public delegate void DelegateSelectOption(MasterOptionModel masterOptionModel);

        event DelegateSelectOption _onSelectOption;

        public void OnActive(DelegateSelectOption delegateSelectOption)
        {
            _onSelectOption = delegateSelectOption;
        }

        public void OnSelectOption(MasterOptionModel masterOptionModel)
        {
            if (_onSelectOption == null) return;
            _onSelectOption(masterOptionModel);
            _onSelectOption = null;
        }


        public class Factory : PlaceholderFactory<OptionSelectPagePresenter>
        {
        }


	}

}
