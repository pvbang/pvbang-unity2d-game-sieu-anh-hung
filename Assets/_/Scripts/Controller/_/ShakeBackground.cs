using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBackground : MonoBehaviour
{
    private GameObject mainCamera;
    private GameObject background;

    private Animator animatorMainCamera;
    private Animator animatorBackground;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        background = GameObject.FindGameObjectWithTag("Background");
        animatorMainCamera = mainCamera.GetComponent<Animator>();
        animatorBackground = background.GetComponent<Animator>();
    }

    public void shakeBackground()
    {
        if (animatorMainCamera != null)
        {
            animatorMainCamera.SetTrigger("Shake");
        }
        if (animatorBackground != null)
        {
            animatorBackground.SetTrigger("Shake");
        }
    }

    public void shakeBackgroundTime()
    {
        if (animatorMainCamera != null)
        {
            animatorMainCamera.SetBool("Shake2", true);
        }
        if (animatorBackground != null)
        {
            animatorBackground.SetBool("Shake2", true);
        }
    }

    public void endShakeBackgroundTime()
    {
        if (animatorMainCamera != null)
        {
            animatorMainCamera.SetBool("Shake2", false);
        }
        if (animatorBackground != null)
        {
            animatorBackground.SetBool("Shake2", false);
        }
    }
}
