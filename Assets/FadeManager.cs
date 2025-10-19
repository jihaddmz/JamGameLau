using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public float fadeDuration = 1f;
    private Image fadeImage;

    void Awake()
    {
        fadeImage = GetComponent<Image>();
    }

    public void FadeAndRestart()
    {
        StartCoroutine(FadeOutAndRestart());
    }

    private IEnumerator FadeOutAndRestart()
    {
        float time = 0f;
        Color color = fadeImage.color;

        while (time < fadeDuration)
        {
            color.a = Mathf.Lerp(0, 1, time / fadeDuration);
            fadeImage.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        // Ensure fully opaque
        color.a = 1;
        fadeImage.color = color;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
