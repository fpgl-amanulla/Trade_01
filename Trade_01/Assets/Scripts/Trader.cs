using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using Random = UnityEngine.Random;

public class Trader : TradeSystem
{
    private const string AnimKeyDance01 = "Dance01";
    private const string AnimKeyDenay01 = "Denay01";
    private const string AnimKeyNo = "No";
    private const string AnimKeyRaiseHand = "RaiseHand";

    public PanelTradeScrollView panelTradeScrollView;
    public CameraController cameraController;
    public Player player;

    [Space(10)]
    public GameObject traderPopUp;
    public Text txtTraderPopUp;

    public void Start()
    {
        var rand = new System.Random();
        tradeItems = tradeItems.OrderBy(item => rand.Next()).ToList();
    }

    public void TraderTurn()
    {
        StartCoroutine(TraderChoice());
    }

    private IEnumerator TraderChoice()
    {
        cameraController.FocusTrader();

        yield return new WaitForSeconds(1.0f);
        int givenItem = givenTradeItems.Count;

        if (givenItem < 2)
        {
            //  0 -> Give one item
            GiveItem(givenItem);
            traderPopUp.SetActive(true);

            givenItem = givenTradeItems.Count;
            txtTraderPopUp.text = givenItem == 1 ? "Want More" : "Trade???";

            if (givenItem != 1) animator.SetBool(AnimKeyDance01, true);
  
        }
        else
        {
            //  >0 -> Check for trade/Want more/denay
            //int playerItemValue = player.GetItemValue();
            //int traderItemValue = GetItemValue();

            int value = UnityEngine.Random.Range(0, 10);
            if (value % 2 == 0)
            {
                traderPopUp.SetActive(true);
                txtTraderPopUp.text = "Want More";
                PlayAnimTrigger(AnimKeyRaiseHand);

            }
            else
            {
                //Give More oR Denay
                GiveItem(givenItem);
                traderPopUp.SetActive(true);
                txtTraderPopUp.text = "Trade???";

            }

        }
    }

    public void PlayerTradeCallBack()
    {
        int playerItemValue = player.GetItemValue();
        int traderItemValue = GetItemValue();

        //Debug.Log(playerItemValue + "    " + traderItemValue);

        int givenItem = givenTradeItems.Count;
        int value = 2;//Random.Range(0, 10);
        if (value % 2 == 0 || givenItem == tradeItems.Count - 1)
        {
            txtTraderPopUp.text = "Trade Successful";
            CTACallBack();
            StartCoroutine(ReloadScene(true));
        }
        else
        {
            if (traderItemValue >= playerItemValue)
            {
                txtTraderPopUp.text = "Want More";
                PlayAnimTrigger(AnimKeyRaiseHand);
            }
        }
    }

    public IEnumerator PlayerWantMoreCallBack()
    {
        int playerItemValue = player.GetItemValue();
        int traderItemValue = GetItemValue();
        int givenItem = givenTradeItems.Count;
        //Debug.Log(playerItemValue + "    " + traderItemValue);

        int value = 3;// Random.Range(0, 10);
        if (value % 2 == 0)
        {
            GiveItem(givenItem);
            traderPopUp.SetActive(true);
            txtTraderPopUp.text = "Trade???";
        }
        else
        {
            cameraController.FocusTrader();
            yield return new WaitForSeconds(1.0f);
            txtTraderPopUp.text = "Not Interested!!!\n Trade???";
            PlayAnimTrigger(AnimKeyNo);
            yield return new WaitForSeconds(2.0f);

            cameraController.ResetFocusTrader();
            //yield return new WaitForSeconds(1.5f);
            StartCoroutine(player.IenumWantmore());
        }

    }
    public void PlayerDenayCallBack()
    {
        txtTraderPopUp.text = "Trade Cancelled";
        CTACallBack();
        StartCoroutine(ReloadScene(false));
    }

    private void CTACallBack()
    {
        //Luna.Unity.Playable.InstallFullGame();
    }

    private void GiveItem(int givenItem)
    {
        if (givenItem < tradeItemTransform.Count - 1)
        {
            InstantiateItem(givenItem, this.transform);
            PlayAnimTrigger(AnimKeyRaiseHand);
        }
        else
        {
            txtTraderPopUp.text = "Want More";
            PlayAnimTrigger(AnimKeyRaiseHand);
        }

        StartCoroutine(ResetTraderFocus());
    }

    IEnumerator ResetTraderFocus()
    {
        yield return new WaitForSeconds(1.5f);
        cameraController.ResetFocusTrader();
        panelTradeScrollView.SetAllItemInteractableStatus(true, 1.0f);
    }

    [Space(10)]
    public GameObject imgClickBlocker;
    public GameObject panelTradeComplete;
    public Text txtTradeStatus;
    IEnumerator ReloadScene(bool tradeStatus)
    {
        imgClickBlocker.SetActive(true);
        if (tradeStatus)
        {

            OnTradeComplate(player.transform);
            player.OnTradeComplate(this.transform);

            PlayAnimBool(AnimKeyDance01, true);
            player.PlayAnimBool(AnimKeyDance01, true);
        }
        else
        {
            OnTradeComplate(this.transform);
            player.OnTradeComplate(player.transform);

            PlayAnimBool(AnimKeyDenay01, true);
            player.PlayAnimBool(AnimKeyDenay01, true);
        }

        yield return new WaitForSeconds(3.0f);
        panelTradeComplete.SetActive(true);
        txtTradeStatus.text = tradeStatus ? "Trade Successful" : "Trade Cancelled";
        CanvasGroup canvasGroup = panelTradeComplete.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, 1.0f);

        yield return new WaitForSeconds(2.0f);

    }
    public void ReloadSceneCallBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
