using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneDelay : MonoBehaviour
{
    [SerializeField] float waitSeconds = 5f;

    private void Start()
    {
        StartCoroutine(WaitThenSwitch());
    }

    private IEnumerator WaitThenSwitch()
    {
        yield return new WaitForSeconds(waitSeconds);
        SceneManager.LoadScene(1);
    }
}
