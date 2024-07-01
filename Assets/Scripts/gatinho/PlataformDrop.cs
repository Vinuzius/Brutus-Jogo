using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformDrop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(FallTimer() );
        }
    }

    IEnumerator FallTimer()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        GetComponent<CapsuleCollider2D>().enabled = true;
    }
}
