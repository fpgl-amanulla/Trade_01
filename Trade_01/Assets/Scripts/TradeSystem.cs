using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class TradeSystem : MonoBehaviour
{
    public Animator animator;

    [Space(10)]
    public List<TradeItemSO> tradeItems = new List<TradeItemSO>();

    [Space(10)]
    public List<Transform> tradeItemTransform = new List<Transform>();

    [Space(10)]
    public float itemScaleFactor = .75f;

    [HideInInspector]
    public List<TradeItemSO> givenTradeItems = new List<TradeItemSO>();
    public List<GameObject> givenTradeItemsGameobject = new List<GameObject>();

    public void InstantiateItem(int tradeItemIndex, Transform _transform)
    {
        GameObject tradeItemPrefab = tradeItems[tradeItemIndex].itemPrefab;
        Transform insTransform = tradeItemTransform[givenTradeItems.Count];
        GameObject item = Instantiate(tradeItemPrefab, _transform.position, insTransform.rotation, insTransform);
        AnimSeq(insTransform, item);
        givenTradeItemsGameobject.Add(item);
        givenTradeItems.Add(tradeItems[tradeItemIndex]);
    }

    private void AnimSeq(Transform insTransform, GameObject item)
    {
        item.transform.localScale = Vector3.one * .2f;
        item.transform.DOScale(Vector3.one * itemScaleFactor, 1.25f);
        item.transform.DOJump(insTransform.position, 1, 1, 1.25f).SetEase(Ease.OutExpo);
    }

    public void InstantiateItem(TradeItemSO tradeItem, Transform _transform)
    {
        Transform insTransform = tradeItemTransform[givenTradeItems.Count];
        GameObject item = Instantiate(tradeItem.itemPrefab, _transform.position, insTransform.rotation, insTransform);
        AnimSeq(insTransform, item);
        givenTradeItemsGameobject.Add(item);
        givenTradeItems.Add(tradeItem);
    }

    public int GetItemValue()
    {
        int value = 0;
        for (int i = 0; i < givenTradeItems.Count; i++)
        {
            value += givenTradeItems[i].starValue;
        }
        return value;
    }

    public void OnTradeComplate(Transform _transform)
    {
        StartCoroutine(OnTradeComplateAnim(_transform));
    }

    private IEnumerator OnTradeComplateAnim(Transform _transform)
    {
        for (int i = 0; i < givenTradeItems.Count; i++)
        {
            yield return new WaitForSeconds(.5f);
            givenTradeItemsGameobject[i].transform.DOScale(Vector3.zero, 1.25f);
            givenTradeItemsGameobject[i].transform.DOJump(_transform.position, 1, 1, 1.25f).SetEase(Ease.OutExpo);
        }
    }

    public void PlayAnimBool(string key, bool action)
    {
        animator.SetBool(key, action);
    }
    public void PlayAnimTrigger(string key)
    {
        animator.SetTrigger(key);
    }
}
