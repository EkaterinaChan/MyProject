using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DT : MonoBehaviour
{
    public D dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DM>().StartDialogue(dialogue);
    }
}
