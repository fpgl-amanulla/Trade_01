using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 traderFoucsPos;
    public Vector3 normalPos;

    public void FocusTrader()
    {
        transform.DOMove(traderFoucsPos, 1.0f);
    }
    public void ResetFocusTrader()
    {
        transform.DOMove(normalPos, 1.0f);
    }
}
