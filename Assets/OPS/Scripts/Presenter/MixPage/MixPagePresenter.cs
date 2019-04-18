using UnityEngine;
using OPS.Model;
using Zenject;

namespace OPS.Presenter
{

    public class MixPagePresenter : MonoBehaviour
	{
        UserMixModel _userMixModel;

        public void Setup(UserMixModel userMixModel)
        {
            _userMixModel = userMixModel;
        }

        public class Factory : PlaceholderFactory<MixPagePresenter>
        {
        }

	}

}
