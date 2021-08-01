using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;


public class Trader : TradeSystem
{
    public PanelTradeScrollView panelTradeScrollView;
    public Player player;

    [Space(10)]
    public GameObject traderPopUp;
    public TextMeshProUGUI txtTraderPopUp;

    public void Start()
    {

    }

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

            if (traderItemValue >= playerItemValue - 1)
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

        Debug.Log(playerItemValue + "    " + traderItemValue);

        if (traderItemValue - 2 >= playerItemValue)
        {
            txtTraderPopUp.text = "Want More";
        }
        else
        {
            txtTraderPopUp.text = "Trade Successful";
        }
    }
    public void PlayerWantMoreCallBack()
    {
        int playerItemValue = player.GetItemValue();
        int traderItemValue = GetItemValue();

        int givenItem = givenTradeItems.Count;

        if (traderItemValue - 2 >= playerItemValue)
        {
            txtTraderPopUp.text = "Trade???";
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
    }

    private void GiveItem(int givenItem)
    {
        InstantiateItem(givenItem);
        panelTradeScrollView.SetAllItemInteractableStatus(true);
    }
}
