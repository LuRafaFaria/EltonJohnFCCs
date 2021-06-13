using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReptileController : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("speed", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
