using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace StandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(BoxCollider))]
    public class Goal : MonoBehaviour
    {
        Collider col;
        GameMaster gm;
        // Start is called before the first frame update
        void Start()
        {
            col = GetComponent<BoxCollider>();
            gm = GameMaster.Instance;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                gm.EndGame(true);
            }
        }

        void OnDrawGizmos()
        {
            if (col!= null){
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(transform.position, col.bounds.extents * 2);
                Handles.Label(transform.position, "Goal");
            }
        }
    }
}