using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TradeItem : MonoBehaviour
{
    public Image imgIcon;
    public TextMeshProUGUI txtNumOfItem;

    public Button btnItem;

    private PanelTradeScrollView panelTradeScrollView;
    private TradeItemSO tradeItemSO;
    private Player player;

    public void Start()
    {
        btnItem.onClick.AddListener(ItemClickCallBack);
    }

    public void AssignValues(PanelTradeScrollView _panelTradeScrollView, int index)
    {
        panelTradeScrollView = _panelTradeScrollView;
        player = _panelTradeScrollView.player;
        tradeItemSO = player.tradeItems[index];

        imgIcon.sprite = tradeItemSO.imgSprite;
        txtNumOfItem.text = tradeItemSO.numOfItem.ToString();
    }

    private void ItemClickCallBack()
    {
        player.InstantiateItem(tradeItemSO, player.transform);
        panelTradeScrollView.trader.TraderTurn();

        panelTradeScrollView.SetAllItemInteractableStatus(false);
    }
}
