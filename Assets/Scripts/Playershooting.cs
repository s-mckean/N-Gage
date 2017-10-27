using System.Collections;
using UnityEngine;

public class Playershooting : MonoBehaviour {
    public float damage = 10f;
    public float range = 100f;
    public float clipSize = 10;
    public float reloadTime = 1f;


    public Camera playerCam;


    public GameObject Canvas;
    public GameObject GunShots;

    private float currentClip;
    void Start () {
		
	}

    void Update()
    {

        if (currentClip <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            shotsFired();
        }
        
    }

    IEnumerator Reload()
    {
        //Debug.Log("reloading");
        yield return new WaitForSeconds(reloadTime);
        currentClip = clipSize;
    }

    void shotsFired()
    {

        currentClip--;
        Debug.Log("shooting");
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {
            EnemyControl enemy = hit.transform.GetComponent<EnemyControl>();
            Debug.Log(hit.transform.name);
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            GameObject shots = Instantiate(GunShots, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(shots, 1f);
        }
    }
}
