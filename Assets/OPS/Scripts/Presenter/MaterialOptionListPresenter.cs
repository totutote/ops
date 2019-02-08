using UnityEngine;
using TMPro;
using Zenject;
using UniRx;
using OPS.Model;

namespace OPS.Presenter
{

    public class MaterailOptionListPresenter : MonoBehaviour
    {
        public GameObject Culumn;

        [Inject]
        UserMaterialOptionDB userMaterialOptionDB;

        void Start()
        {
            var options = userMaterialOptionDB.All();
            userMaterialOptionDB.cacheRecords.ObserveEveryValueChanged(x => x.Count).Subscribe(msg => Debug.Log("Subscribe1:" + msg));
            foreach (var option in options)
            {
                var culumn = Instantiate(Culumn);
                culumn.SetActive(true);
                var culumnText = culumn.transform.Find("Text").GetComponent<TextMeshProUGUI>();
                culumnText.text = option.Value.name.Value;
                culumn.transform.SetParent(Culumn.transform.parent, false);
            }
        }
    }

}
