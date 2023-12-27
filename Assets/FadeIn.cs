using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float secondsToFade;
    private bool isFading = false;

    private void FixedUpdate()
    {
        if (isFading)
        {
            if (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= (Time.deltaTime / secondsToFade);
            }
            else
            {
                isFading = false;
                canvasGroup.alpha = 0;
                gameObject.SetActive(false);
            }
        }
    }

    public IEnumerator Fade()
    {
        canvasGroup.alpha = 1;
        yield return new WaitForSeconds(3.5f);
        FindObjectOfType<SoundController>().PlayGameOver(true);
        isFading = true;
    }
}
