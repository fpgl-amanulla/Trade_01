using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Player : TradeSystem
{
    public Trader trader;

    [Space(10)]
    public Button btnTrade;
    public Button btnWantMore;
    public Button btnDenay;
    public GameObject playerButtons;

    [Space(10)]
    public GameObject wandMoreHandTutorial;
    public GameObject firstHandTutorial;
    //---------Private Fields---------

    bool iswantMoreClicked = false;

    public void Start()
    {
        btnTrade.onClick.AddListener(TradeCallBack);
        btnWantMore.onClick.AddListener(WantMoreCallBack);
        btnDenay.onClick.AddListener(DenayCallBack);
    }

    private void DenayCallBack()
    {
        firstHandTutorial.SetActive(false);
        PlayerButtonsAnim(false);
        trader.PlayerDenayCallBack();
    }

    private void WantMoreCallBack()
    {
        iswantMoreClicked = true;
        firstHandTutorial.SetActive(false);
        PlayerButtonsAnim(false);
        //StartCoroutine(IenumWantmore());
        StartCoroutine(trader.PlayerWantMoreCallBack());
    }

    public IEnumerator IenumWantmore()
    {
        yield return new WaitForSeconds(1.0f);
        PlayerButtonsAnim(true);
        firstHandTutorial.SetActive(true);
        btnWantMore.gameObject.SetActive(false);
        btnDenay.gameObject.SetActive(true);
        btnTrade.gameObject.SetActive(true);

    }

    private void TradeCallBack()
    {
        PlayerButtonsAnim(false);
        trader.PlayerTradeCallBack();
        btnTrade.interactable = false;
        firstHandTutorial.SetActive(false);
    }

    public void PlayerButtonInteractiveStatus(bool action, float waitTime = 0)
    {
        PlayerButtonsAnim(true);

        btnWantMore.gameObject.SetActive(true);
        btnTrade.gameObject.SetActive(true);
        firstHandTutorial.SetActive(true);
        //StartCoroutine(BtnInteraction(action, waitTime));
    }

    public void PlayerButtonsAnim(bool show)
    {
        if (show)
        {
            playerButtons.SetActive(true);
            playerButtons.transform.DOMoveY(130, 1.0f);
        }
        else
        {
            playerButtons.transform.DOMoveY(-130, .5f).OnComplete(() => { playerButtons.SetActive(false); });
        }
    }

    private IEnumerator BtnInteraction(bool action, float waitTime = 0)
    {
        yield return new WaitForSeconds(waitTime);
        if (action)
        {
            if (!iswantMoreClicked)
            {
                wandMoreHandTutorial.SetActive(true);
                btnTrade.interactable = false;
                btnWantMore.interactable = true;
            }
            else
            {
                wandMoreHandTutorial.SetActive(false);
                btnTrade.interactable = true;
                btnWantMore.interactable = false;
            }
        }
    }


}
