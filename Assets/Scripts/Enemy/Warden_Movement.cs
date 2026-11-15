using UnityEngine;

public class Warden_Movement : MonoBehaviour
{
    Animator animate;
    void Start()
    {
        animate = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //animate.SetBool("IsWalking", true); 
    }
}
