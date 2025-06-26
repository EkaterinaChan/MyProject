using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QC1 : MonoBehaviour
{
    public Animator[] cloudss;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(Animator anim in cloudss)
            {
                anim.SetTrigger("isTriggered");
            }
        }
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(Animator anim in cloudss)
            {
                anim.SetTrigger("isTriggered");
            }
        }
    }
}
