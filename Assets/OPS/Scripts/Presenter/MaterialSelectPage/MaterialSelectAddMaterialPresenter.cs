using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OPS.Presenter
{
    public class MaterialSelectAddMaterialPresenter : MonoBehaviour
    {

        public void OnClick()
        {
            transform.parent.SetAsLastSibling();
        }

    }
}