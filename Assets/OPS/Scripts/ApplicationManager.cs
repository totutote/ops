using UnityEngine;
using OPS.Presenter;
using Zenject;

namespace OPS
{
    public class ApplicationManager : MonoBehaviour
    {
        [Inject]
        PageManager _pageManager;

        [Inject]
        CompleteSelectPagePresenter.Factory _startPageFactory;

        void Start()
        {
            var cpyStartPage = _startPageFactory.Create();
            _pageManager.ChangePage(cpyStartPage);
        }

    }
}