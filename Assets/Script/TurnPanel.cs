using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnPanel : MonoBehaviour
{
    public TMP_Text turnText; // 用于显示当前回合的文本
    public float fadeDuration = 0.5f; // 淡入淡出的持续时间

    public static TurnPanel instance;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if(instance!=null){
            Destroy(instance);

        }

        instance = this;
        turnText.text="/";

    }

    private void Start() {
        HidePanel();
    }
    public void ShowPanel(string text)
    {
        turnText.text = text;
        StartCoroutine(FadeIn());
    }

    public void HidePanel()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {   
        Debug.Log("调用回合面板");
        gameObject.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
    
}
