using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if !UNITY_EDITOR
using Luna.Unity;
#endif
public enum UiCart
{
    old_ui,
    new_ui

}
public class PanelCTA_UI : MonoBehaviour
{
    [Header("------UI Components----")]
    [SerializeField] private RectTransform imgLogo;
    public Vector3 imgLogoPT;
    public Vector3 imgLogoLS;
    [SerializeField] private RectTransform imgPNFF;
    [SerializeField] private Vector3 imgPNFFPT;
    [SerializeField] private Vector3 imgPNFFLS;
    [SerializeField] private Button btnDownload;
    [SerializeField] private RectTransform imgDownload;
    [SerializeField] private Vector3 imgDownloadPT;
    [SerializeField] private Vector3 imgDownloadLS;
    [SerializeField] private Button btnRetry;

    public void Start()
    {
        CheckOrientation();

        btnDownload.onClick.AddListener(() => DownloadCallBack());
        btnRetry.onClick.AddListener(() => RetryCallBack());

        btnDownload.GetComponent<RectTransform>().DOScale(Vector3.one * .8f, .8f).SetLoops(-1, LoopType.Yoyo);
    }

    public void Update()
    {
        CheckOrientation();
    }

    private void CheckOrientation()
    {
        float screenRatio = (Screen.width / Screen.height);
        if (screenRatio >= 1)
        {
            // Landscape Layout
            //imgLogo.anchoredPosition = imgLogoLS;
            //imgPNFF.anchoredPosition = imgPNFFLS;
            imgDownload.anchoredPosition = imgDownloadLS;
        }
        else if (screenRatio < 1)
        {
            // Portrait Layout
            //imgLogo.anchoredPosition = imgLogoPT;
            //imgPNFF.anchoredPosition = imgPNFFPT;
            imgDownload.anchoredPosition = imgDownloadPT;
        }
    }

    private void RetryCallBack()
    {
        SceneManager.LoadScene(0);
    }

    private void DownloadCallBack()
    {
#if !UNITY_EDITOR
        Luna.Unity.Playable.InstallFullGame();
#endif
        Luna.Unity.LifeCycle.GameEnded();
    }
}
