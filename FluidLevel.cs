using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// -1.9f to -0.68f

public class FluidLevel : MonoBehaviour
{
    public int cracksOpen = 0;

    [SerializeField] GameObject gameOver = null;
    [SerializeField] Image stomachImg;
    [SerializeField] float fluidChangeSpeed = 5f;

    private void Update()
    {
        if (cracksOpen == 0 && transform.position.y > -1.9f)
        {
            transform.Translate(0f, -0.1f * fluidChangeSpeed * Time.deltaTime, 0f, Space.World);
            if (AudioManager.instance.gushing.isPlaying)
            {
                AudioManager.instance.StopGushing();
            }
        }
        else if (transform.position.y < -0.68f)
        {
            transform.Translate(0f, 0.1f * cracksOpen * fluidChangeSpeed * Time.deltaTime, 0f, Space.World);
            if(!AudioManager.instance.gushing.isPlaying)
            {
                AudioManager.instance.PlayGushing();
            }
        }
        else if (gameOver)
        {
            gameOver.SetActive(true);
            AudioManager.instance.PlayLose();
            StartCoroutine(ReloadScene());
        }
        stomachImg.fillAmount = ((-1.9f + 0.68f)-(transform.position.y + 0.68f))/(-1.9f + 0.68f);
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(0);
    }
}
