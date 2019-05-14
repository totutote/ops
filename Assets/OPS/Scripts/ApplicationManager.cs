using UnityEngine;
using OPS.Presenter;
using Zenject;

namespace OPS
{
    public class ApplicationManager : MonoBehaviour
    {
        [Inject]
        PageManager _pageManager = null;

        [Inject]
        CompleteSelectPagePresenter.Factory _startPageFactory = null;

        void Start()
        {
            var db = new SqliteDatabase("master.sqlite3");
            db.ForceUpdateDatabaseSchema();

            var cpyStartPage = _startPageFactory.Create();
            _pageManager.ChangePage(cpyStartPage);
        }

    }
}