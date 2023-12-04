using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class NPCManCtrl : MonoBehaviour
{
    private Transform npcTr;
    private Transform playerTr;
    private Animator anim;
    private NavMeshAgent agent;

    public Button man_dialog;

    public float traceDist = 2.5f;

    private readonly int hashIdle = Animator.StringToHash("Idle");

    void Start()
    {
        npcTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        StartCoroutine(CheckAnimState());
        StartCoroutine(CheckButtonState());

    }
    IEnumerator CheckAnimState()
    {
        while (true)
        {
            
            anim.SetBool(hashIdle, true);
           
            yield return new WaitForSeconds(10.0f);
    
            anim.SetBool(hashIdle, false);
            
            yield return new WaitForSeconds(10.0f);
        }
    }
    
    IEnumerator CheckButtonState()
    {
        while (true)
        {
            float distance = Vector3.Distance(playerTr.position, npcTr.position);

            if (distance <= traceDist)
            {
                man_dialog.gameObject.SetActive(true);
            }
            else
            {
                man_dialog.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    
    void Update()
    {
      
    }
}
