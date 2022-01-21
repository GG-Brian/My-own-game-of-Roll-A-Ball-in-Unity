using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasButtonHandler : MonoBehaviour {
    public void LoadLevel(string sceneName){
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }
}
