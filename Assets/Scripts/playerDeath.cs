using UnityEngine;
using UnityEngine.Audio;

public class playerDeath : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public GameObject player;
    void OnCollisionEnter(Collision collision)
    {
        player.transform.position = new Vector3(0f, 0f, 0f);
        print("death");

        //if (collision.gameObject.CompareTag("Player"))
        //{
            
        //    player.transform.position = new Vector3(0f, 0f, 0f);

        //}
        //if (player != null)
        //{
        //    player.transform.position = new Vector3(0f, 0f, 0f);
        //}
    }


    void playerDie()
    {

    }
}
