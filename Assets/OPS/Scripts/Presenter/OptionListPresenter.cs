using UnityEngine;
using OPS.Model;
using TMPro;
using Zenject;

namespace OPS.Presenter
{

    public class OptionListPresenter : MonoBehaviour
    {
        public GameObject Culumn;

        [Inject]
        MasterOptionDB masterOptionDB;

        void Start()
        {
            var options = masterOptionDB.All();
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
