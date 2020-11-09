using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instatntiate_apples : MonoBehaviour
{
    public GameObject full1, full2, half, a1loc, a2loc, a3loc, collider, d, e, f, particles,particle_effect ;
    public Collider2D temp;
    public bool apples_falled;

    // Start is called before the first frame update
    public void Awake()
    {
       
        //full1.GetComponent<Rigidbody2D>().isKinematic = false;
    }
    public void instantiate_particles()
    {
        GameObject a = Instantiate(particle_effect);
        a.transform.position = particles.transform.position;
        Destroy(a, 3);
    }
    public void instantiate_apple()
    {if (apples_falled == false)
        {
            apples_falled = true;
            var q = GetComponents<Collider2D>();
            foreach (Collider2D q1 in q)
            {
                if (q1.isTrigger == false)
                {
                    q1.enabled = false;
                    temp = q1;

                }
            }

            collider.SetActive(true);   
            a1loc.SetActive(false);
            d = Instantiate(full1) as GameObject;
            d.transform.position = a1loc.transform.position;



            a2loc.SetActive(false);
            e = Instantiate(full2) as GameObject;
            e.transform.position = a2loc.transform.position;



            a3loc.SetActive(false);
            f = Instantiate(half) as GameObject;
            f.transform.position = a3loc.transform.position;



            StartCoroutine(disable_gravity_enable_trigger());
        }
    }

    public IEnumerator disable_gravity_enable_trigger()
    {
        yield return new WaitForSeconds(0.7f);
        GameObject.FindObjectOfType<IsometricPlayerMovementControllerGarden>().GetComponentInChildren<Collider2D>().enabled = true;
        temp.enabled = true;
        d.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        d.GetComponent<Rigidbody2D>().freezeRotation = true;
        d.GetComponent<Rigidbody2D>().gravityScale = 0;
        d.transform.rotation = Quaternion.Euler(Vector3.zero);
        d.GetComponent<Collider2D>().isTrigger = true;

        e.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        e.GetComponent<Rigidbody2D>().freezeRotation = true;
        e.GetComponent<Rigidbody2D>().gravityScale = 0;
        e.transform.rotation = Quaternion.Euler(Vector3.zero);
        e.GetComponent<Collider2D>().isTrigger = true;

        if (f != null)
        {
            f.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            f.GetComponent<Rigidbody2D>().freezeRotation = true;
            f.GetComponent<Rigidbody2D>().gravityScale = 0;
            f.transform.rotation = Quaternion.Euler(Vector3.zero);
            f.GetComponent<Collider2D>().isTrigger = true;
        }
            collider.SetActive(false);


    }


}
