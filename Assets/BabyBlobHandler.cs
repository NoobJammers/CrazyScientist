using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BabyBlobHandler : MonoBehaviour
{

    // 0 for sticky, 1 for fire, 2 for water
    public int id = 0;



    private void Start()
    {
        float temp = transform.position.y + 0.2f;
        transform.DOMoveY(temp, 1f).SetEase(Ease.OutSine).SetLoops(-1, LoopType.Yoyo);
    }


}
