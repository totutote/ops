using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OPS.Presenter
{
    public class PageManager : MonoBehaviour
    {
        Dictionary<Type, GameObject> _activePages = new Dictionary<Type, GameObject>();

        public T ChangePage<T>(T pagePrefab) where T : MonoBehaviour
        {
            if (pagePrefab == null) return null;
            DestroyActivePages();
            SetPrantPage(pagePrefab.gameObject);
            _activePages.Add(typeof(T), (GameObject)pagePrefab.gameObject);
            return pagePrefab;
        }

        public T AddPage<T>(T pagePrefab) where T : MonoBehaviour
        {
            if (pagePrefab == null) return null;
            SetPrantPage(pagePrefab.gameObject);
            _activePages.Add(typeof(T), pagePrefab.gameObject);
            return pagePrefab;
        }

        public GameObject GetPage<T>()
        {
            return _activePages[typeof(T)];
        }

        public void DestroyPage<T>()
        {
            GameObject.Destroy(_activePages[typeof(T)]);
            _activePages.Remove(typeof(T));
        }

        void SetPrantPage(GameObject pageObject)
        {
            pageObject.transform.SetParent(transform, false);
            pageObject.transform.SetAsLastSibling();
        }

        void DestroyActivePages()
        {
            foreach (var page in _activePages)
            {
                GameObject.Destroy(page.Value);
            }
            _activePages.Clear();
        }
    }
}