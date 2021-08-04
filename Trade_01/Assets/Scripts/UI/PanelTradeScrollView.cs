using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelTradeScrollView : MonoBehaviour
{
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

    private List<GameObject> tradeItem = new List<GameObject>();
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
        }
    }

    public void SetAllItemInteractableStatus(bool status)
    {
        StartCoroutine(IenumSetAllItemInteractableStatus(status));
    }
    int count = 0;
    IEnumerator IenumSetAllItemInteractableStatus(bool status)
    {
        yield return new WaitForSeconds(2.0f);
        if (player.givenTradeItems.Count < player.tradeItemTransform.Count - 1)
        {
            count = 0;
            for (int i = 0; i < tradeItem.Count; i++)
            {
                if (player.tradeItems[i].numOfItem > 0)
                {
                    count++;
                    tradeItem[i].GetComponent<Button>().interactable = status;
                    tradeItem[i].GetComponent<TradeItem>().handAnim.SetActive(true);
                }
            }
        }
        if (count == 0)
            player.PlayerButtonInteractiveStatus(status);
    }
}
