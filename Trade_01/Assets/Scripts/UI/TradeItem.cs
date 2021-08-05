using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeItem : MonoBehaviour
{
    public Image imgIcon;
    public Text txtNumOfItem;

    public Button btnItem;

    private PanelTradeScrollView panelTradeScrollView;
    private TradeItemSO tradeItemSO;
    private Player player;

    int clickCount = 0;

    public void Start()
    {
        btnItem.onClick.AddListener(ItemClickCallBack);
    }
    public void AssignValues(PanelTradeScrollView _panelTradeScrollView, int index)
    {
        panelTradeScrollView = _panelTradeScrollView;
        player = _panelTradeScrollView.player;
        tradeItemSO = player.tradeItems[index];
        tradeItemSO.SetNumOfItem(1);

        imgIcon.sprite = tradeItemSO.imgSprite;
        txtNumOfItem.text = tradeItemSO.numOfItem.ToString();
    }

    private void ItemClickCallBack()
    {
        panelTradeScrollView.SetAllItemInteractableStatus(false);

        if (panelTradeScrollView.txtStartInstruction.gameObject.activeSelf)
        {
            panelTradeScrollView.txtStartInstruction.gameObject.SetActive(false);
            panelTradeScrollView.divider.gameObject.SetActive(true);
        }
        panelTradeScrollView.firstTutorialHand.SetActive(false);

        player.InstantiateItem(tradeItemSO, player.transform);
        panelTradeScrollView.trader.TraderTurn();
        tradeItemSO.numOfItem--;
        txtNumOfItem.text = tradeItemSO.numOfItem.ToString();
        //if (tradeItemSO.numOfItem < 1) btnItem.interactable = false;
        this.gameObject.SetActive(false);
        panelTradeScrollView.tradeItem[2].gameObject.SetActive(true);



        if (player.givenTradeItems.Count > 1)
        {
            panelTradeScrollView.productScrollView.SetActive(false);
            return;
        }

    }
}
