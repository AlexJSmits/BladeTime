using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int sceneNumber;

    public void SceneChanger()
    {
        SceneManager.LoadScene(sceneBuildIndex: sceneNumber);
    }

}
