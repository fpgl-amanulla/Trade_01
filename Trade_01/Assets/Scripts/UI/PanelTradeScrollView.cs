using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelTradeScrollView : MonoBehaviour
{
    public GameObject productScrollView;

    public Player player;
    public Trader trader;

    public Transform content;
    public TradeItem tradeItemPrefab;

    [Space(10)]
    public Text txtStartInstruction;
    [Space(10)]
    public GameObject divider;

    [Space(10)]
    public GameObject firstTutorialHand;

    public List<GameObject> tradeItem = new List<GameObject>();
    public void Start()
    {
        txtStartInstruction.gameObject.SetActive(true);
        txtStartInstruction.rectTransform.DOScale(Vector3.one * 1.2f, 1.0f).SetLoops(-1, LoopType.Yoyo);

        GenerateItem();
    }

    private void GenerateItem()
    {
        for (int i = 0; i < player.tradeItems.Count; i++)
        {
            TradeItem item = Instantiate(tradeItemPrefab, content);
            tradeItem.Add(item.gameObject);
            item.AssignValues(this, i);
            if (i > 1)
                item.gameObject.SetActive(false);
        }
    }

    public void SetAllItemInteractableStatus(bool status, float waitTime = 0)
    {
        StartCoroutine(IenumSetAllItemInteractableStatus(status, waitTime));
    }
    IEnumerator IenumSetAllItemInteractableStatus(bool status, float waitTime = 0)
    {

        yield return new WaitForSeconds(waitTime);
        if (status)
        {
            firstTutorialHand.SetActive(true);
        }
        /*if (player.givenTradeItems.Count < player.tradeItemTransform.Count - 1)
        {
            for (int i = 0; i < tradeItem.Count; i++)
            {
                if (player.tradeItems[i].numOfItem > 0)
                {
                    tradeItem[i].GetComponent<Button>().interactable = status;
                }
            }
        }*/

        if (trader.givenTradeItems.Count > 1)
        {
            firstTutorialHand.SetActive(false);
            player.PlayerButtonInteractiveStatus(status);
            yield break;
        }
        ProductScrollViewAnim(true);
    }

    public void ProductScrollViewAnim(bool show)
    {
        if (show)
        {
            productScrollView.SetActive(true);
            productScrollView.transform.DOMoveY(15, 1.0f).SetEase(Ease.OutBounce);
        }
        else
        {
            productScrollView.transform.DOMoveY(-400, .5f).SetEase(Ease.OutFlash).OnComplete(() => { productScrollView.SetActive(false); });
        }
    }
}
