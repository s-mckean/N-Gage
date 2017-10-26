using System.Collections;
using UnityEngine;

public class Playershooting : MonoBehaviour {
    public float damage = 10f;
    public float range = 100f;
    public Camera playerCam;
    
    void Start () {
		
	}

    void Update()
    {
     
        if (Input.GetButtonDown("Fire1"))
        {
            shotsFired();
        }
        
    }

    void shotsFired()
    {
       
        Debug.Log("shooting");
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {
            EnemyControl enemy = hit.transform.GetComponent<EnemyControl>();
            Debug.Log(hit.transform.name);
            if (enemy != null)
            {
                
            }

        
        }
    }
}
