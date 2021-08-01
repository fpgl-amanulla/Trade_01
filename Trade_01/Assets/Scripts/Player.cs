using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : TradeSystem
{
    public Trader trader;

    [Space(10)]
    public Button btnTrade;
    public Button btnWantMore;
    public Button btnDenay;


    //---------Private Fields---------

    public void Start()
    {
        btnTrade.onClick.AddListener(TradeCallBack);
        btnWantMore.onClick.AddListener(WantMoreCallBack);
        btnDenay.onClick.AddListener(DenayCallBack);
    }

    private void DenayCallBack()
    {
        trader.PlayerDenayCallBack();
    }

    private void WantMoreCallBack()
    {
        trader.PlayerWantMoreCallBack();
    }

    private void TradeCallBack()
    {
        trader.PlayerTradeCallBack();
    }
}
