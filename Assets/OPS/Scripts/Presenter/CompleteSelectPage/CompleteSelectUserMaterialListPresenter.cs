using UnityEngine;
using TMPro;
using Zenject;
using UniRx;
using OPS.Model;

namespace OPS.Presenter
{

    public class CompleteSelectUserMaterialListPresenter : MonoBehaviour
    {
        public GameObject Culumn;

        [Inject]
        UserMixCompleteMaterialDB userMaterialDB = null;

        void Start()
        {
            var options = userMaterialDB.All();
            userMaterialDB.cacheRecords.ObserveEveryValueChanged(x => x.Count).Subscribe(msg => Debug.Log("Subscribe1:" + msg));
            foreach (var option in options)
            {
                var culumn = Instantiate(Culumn);
                culumn.SetActive(true);
                culumn.transform.SetParent(Culumn.transform.parent, false);
            }
        }
    }

}
