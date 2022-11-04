using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using UnityEngine.SceneManagement;

namespace HCB.GridSystem.Samples
{
    public class SceneLoader : MonoBehaviour
    {
        private void Start()
        {
            LoadScene();
        }

        private void LoadScene() 
        {            
            SceneController.Instance.OnSceneLoaded.Invoke();
        }
    }
}
