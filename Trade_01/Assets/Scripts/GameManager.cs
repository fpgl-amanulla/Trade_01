using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform player;
    public Transform trader;

    public Animator playerAnim;
    public Animator traderAnim;


    public GameObject _canvas;


    public List<TradeItemSO> allTradeItemList = new List<TradeItemSO>();

    public void Start()
    {
        player.GetComponent<Player>().PlayerButtonInteractiveStatus(false);
        CanvasGroup canvasGroup = _canvas.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        playerAnim.SetBool("Walk", true);
        traderAnim.SetBool("Walk", true);

        player.DOMove(new Vector3(-4, 0, 1.5f), 2.0f).OnComplete(() =>
        {
            playerAnim.SetBool("Walk", false);
        });
        trader.DOMove(new Vector3(7, 0, -1.5f), 2.0f).OnComplete(() =>
        {
            traderAnim.SetBool("Walk", false);

            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, 1.0f);
        });
    }
}
