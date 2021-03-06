using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Trader : TradeSystem
{
    private const string AnimKeyDance01 = "Dance01";
    private const string AnimKeyDenay01 = "Denay01";
    private const string AnimKeyNo = "No";
    private const string AnimKeyRaiseHand = "RaiseHand";

    public PanelTradeScrollView panelTradeScrollView;
    public Player player;

    [Space(10)]
    public GameObject traderPopUp;
    public TextMeshProUGUI txtTraderPopUp;

    public void TraderTurn()
    {
        StartCoroutine(TraderChoice());
    }

    private IEnumerator TraderChoice()
    {
        yield return new WaitForSeconds(1.0f);
        int givenItem = givenTradeItems.Count;

        if (givenItem == 0)
        {
            //  0 -> Give one item
            GiveItem(givenItem);
            traderPopUp.SetActive(true);
            txtTraderPopUp.text = "Trade???";
        }
        else
        {
            //  >0 -> Check for trade/Want more/denay
            int playerItemValue = player.GetItemValue();
            int traderItemValue = GetItemValue();

            if (traderItemValue - 2 >= playerItemValue)
            {
                //Want More

                int value = Random.Range(0, 10);
                if (value % 2 == 0)
                {
                    traderPopUp.SetActive(true);
                    txtTraderPopUp.text = "Trade??";
                }
                else
                {
                    traderPopUp.SetActive(true);
                    txtTraderPopUp.text = "Want More";

                    PlayAnimTrigger(AnimKeyRaiseHand);
                }
            }
            else
            {
                //Give More oR Denay
                GiveItem(givenItem);
                traderPopUp.SetActive(true);
                txtTraderPopUp.text = "Trade???";

            }

        }
        panelTradeScrollView.SetAllItemInteractableStatus(true);
    }

    public void PlayerTradeCallBack()
    {
        int playerItemValue = player.GetItemValue();
        int traderItemValue = GetItemValue();

        //Debug.Log(playerItemValue + "    " + traderItemValue);

        int givenItem = givenTradeItems.Count;
        if (givenItem > 1)
        {
            txtTraderPopUp.text = "Trade Successful";

            StartCoroutine(ReloadScene(true));
            return;
        }

        if (traderItemValue - 2 >= playerItemValue)
        {
            txtTraderPopUp.text = "Want More";
            PlayAnimTrigger(AnimKeyRaiseHand);
        }
        else
        {
            txtTraderPopUp.text = "Trade Successful";

            StartCoroutine(ReloadScene(true));
        }
    }
    public void PlayerWantMoreCallBack()
    {
        int playerItemValue = player.GetItemValue();
        int traderItemValue = GetItemValue();
        int givenItem = givenTradeItems.Count;
        //Debug.Log(playerItemValue + "    " + traderItemValue);

        if (traderItemValue - 2 >= playerItemValue)
        {
            txtTraderPopUp.text = "Not Interested!!!";
            PlayAnimTrigger(AnimKeyNo);
        }
        else
        {
            GiveItem(givenItem);
            traderPopUp.SetActive(true);
            txtTraderPopUp.text = "Trade???";
        }

    }
    public void PlayerDenayCallBack()
    {
        txtTraderPopUp.text = "Trade Cancelled";
        StartCoroutine(ReloadScene(false));
    }

    private void GiveItem(int givenItem)
    {
        InstantiateItem(givenItem, this.transform);
        panelTradeScrollView.SetAllItemInteractableStatus(true);
    }
    [Space(10)]
    public GameObject imgClickBlocker;
    public GameObject panelTradeComplete;
    public TextMeshProUGUI txtTradeStatus;
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
