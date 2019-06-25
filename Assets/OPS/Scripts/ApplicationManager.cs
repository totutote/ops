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

        void Awake()
        {
            disableAnylitics();
        }

        void Start()
        {
            var masterDb = new SqliteDatabase("master.sqlite3");
            masterDb.ForceUpdateDatabaseSchema();

            var userDb = new SqliteDatabase("user.sqlite3");
            userDb.UpdateDatabaseSchema();

            var cpyStartPage = _startPageFactory.Create();
            _pageManager.ChangePage(cpyStartPage);
        }

        void disableAnylitics()
        {
            UnityEngine.Analytics.PerformanceReporting.enabled = false;
        }

    }
}