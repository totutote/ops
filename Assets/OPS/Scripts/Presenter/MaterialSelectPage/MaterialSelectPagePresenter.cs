using UnityEngine;
using OPS.Model;
using TMPro;
using Zenject;
using System.Linq;

namespace OPS.Presenter
{

    public class MaterialSelectPagePresenter : MonoBehaviour
    {
        public TMP_InputField nameInput;

        [Inject]
        UserMixDB UserMixDB;

        void Start()
        {
            Setup();
        }

        void Setup()
        {
            var newUserMix = UserMixDB.New();
            Debug.Log(newUserMix);
            newUserMix.name.Value = "合成名";
            var saveUserMix = UserMixDB.Save(newUserMix);
            nameInput.text =  saveUserMix.Values.First().name.Value;
        }

        void Setup(int userMixId)
        {

        }
    }

}
