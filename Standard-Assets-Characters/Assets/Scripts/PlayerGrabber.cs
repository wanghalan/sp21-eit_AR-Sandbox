using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabber : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (player!= null)
        {
            player.transform.position = this.transform.position;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
