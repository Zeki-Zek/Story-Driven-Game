using System.Collections;
using UnityEngine;



public class TouchGrass : MonoBehaviour
{

    public Animator grassAnim1;
    public Animator grassAnim2;
    public Animator grassAnim3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            
            grassAnim1.SetTrigger("Touched");
            grassAnim2.SetTrigger("Touched");
            grassAnim3.SetTrigger("Touched");
            


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
