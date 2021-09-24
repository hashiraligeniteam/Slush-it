using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LiftController : MonoBehaviour
{
    public float distance;
    public float delay;
    public Image liftFillBar;
    public bool InverseMovement;
    void Start()
    {
        if (!InverseMovement)
        {
            StartCoroutine(goingUpward());
        }
        else
        {
            this.transform.localPosition += new Vector3(0,distance,0);
            StartCoroutine(goingDownward());
        }
    }
    
    IEnumerator goingUpward()
    {
        liftFillBar.DOFillAmount(0, 3);
        yield return new WaitForSeconds(3.0f);
        transform.DOLocalMove(this.transform.localPosition + new Vector3(0, distance, 0), 0.5f).OnComplete(
            delegate
            {
                StartCoroutine(goingDownward());
            });
    }

    IEnumerator goingDownward()
    {
        liftFillBar.DOFillAmount(1, 3);
        yield return new WaitForSeconds(3.0f);
        transform.DOLocalMove(this.transform.localPosition - new Vector3(0, distance, 0), 0.5f).OnComplete(
            delegate
            {
                StartCoroutine(goingUpward());
            });
    }

}
