using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    [SerializeField]
    private Animator fadeAnimator;

    [SerializeField]
    private AudioSource audioSrc;

    public static SceneManage Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        fadeAnimator.Play("fade_out");
        StartCoroutine(StartAnimationFinished());
    }

    public void ChangeScene(string scene) 
    {
        fadeAnimator.transform.gameObject.SetActive(true);
        fadeAnimator.Play("fade_in");
        StartCoroutine(EndAnimationFinished(scene));
        if (audioSrc != null)
            StartCoroutine(StartMusicFade(1f, 1, 0));
    }

    IEnumerator EndAnimationFinished(string scene)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }

    IEnumerator StartAnimationFinished()
    {
        yield return new WaitForSeconds(1f);
        fadeAnimator.transform.gameObject.SetActive(false);
    }

    IEnumerator StartMusicFade(float time, float start, float target) 
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            audioSrc.volume = Mathf.Lerp(start, target, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
