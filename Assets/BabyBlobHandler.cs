using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BabyBlobHandler : MonoBehaviour
{

    // 0 for sticky, 1 for fire, 2 for water
    public int id = 0;
    float temp;


    private void Start()
    {
        temp = transform.position.y + 0.2f;
        StartCoroutine(Hover());

    }

    IEnumerator Hover()
    {
        yield return new WaitForSeconds(Random.Range(0f, 1f));
        transform.DOMoveY(temp, 1f).SetEase(Ease.OutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
