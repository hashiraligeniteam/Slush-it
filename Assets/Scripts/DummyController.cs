using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DummyController : MonoBehaviour
{

    
    //Public variables
    public Transform Target;
    public bool isDead = false;
    public DummyType DummyType;
    public Vector2 animDelayLimit = new Vector2(1,7);
    
    
    //Private variable
    private Animator myAnimator;
    private float damping = 2.5f;
    

    private void Awake()
    {
//        if (GameObject.FindGameObjectWithTag("Player"))
//        {
//            var tempTarget = GameObject.FindGameObjectWithTag("Player").transform;
//            Target.position = new Vector3(tempTarget.position.x, 0, tempTarget.position.z);
//            Target.rotation = GameObject.FindGameObjectWithTag("Player").transform.rotation;
//        }
    }

    private void OnEnable()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        StartAnimation();
    }

    void StartAnimation()
    {
        myAnimator = this.GetComponent<Animator>();
        float animDelay = Random.Range(animDelayLimit.x, animDelayLimit.y);
        StartCoroutine(SwitchPosition(animDelay));

    }

    IEnumerator SwitchPosition(float delay)
    {
        yield return new WaitForSeconds(delay);
        myAnimator.SetTrigger("Idle");
    }

    void Update()
    {
        if (!isDead)
        {
            if (Target)
            {
                var lookPos = Target.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            }
        }
    }
    
    
    public void Death()
    {
        GameManager.instance.SoundController.KillSound();
        isDead = true;
        StartCoroutine(DestroyMe());

    }

    IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(0.0f);
        this.gameObject.SetActive(false);
    }
}

public enum DummyType
{
    A, 
    B,
    C
}
