using UnityEngine;
using System.Collections.Generic;
using UnityEditor.PackageManager;

public class gen_platform : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //genPlatform(-1);
        //genPlatform(1);
        //UnityEditor.EditorApplication.isPlaying = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public List<GameObject> platforms; //= new List<GameObject>();
    public Transform parent;

    public GameObject currLeftPlatform;
    public GameObject currRightPlatform;

    public void genSetPlatform(GameObject platform, int Side)
    {
        GameObject platformInst = Instantiate(randPlatform(), parent);

        platformInst.transform.localScale = new Vector3((float)Side, 1f, 1f);

        replacePlatformRef(platformInst, Side);
    }

    public void genRandPlatform(int Side)
    {
        GameObject platform = Instantiate(randPlatform(), parent);

        platform.transform.localScale = new Vector3((float)Side, 1f, 1f);

        replacePlatformRef(platform, Side);

    }


    //public void genRandPlatformRight()
    //{
    //    GameObject platform = Instantiate(randPlatform(), parent);

    //    platform.transform.localScale = new Vector3(1f, 1f, 1f);
    //}

    //public void genRandPlatformLeft()
    //{
    //    GameObject platform = Instantiate(randPlatform(), parent);

    //    platform.transform.localScale = new Vector3(-1f, 1f, 1f);
    //}

    //returns -1 (left facing platform) or 1 (right facing platform)

    void replacePlatformRef(GameObject platform, int Side)
    {
        if (Side == -1)
        {
            Destroy(currLeftPlatform);
            currLeftPlatform = platform;

        }
        else if (Side == 1)
        {
            Destroy(currRightPlatform);
            currRightPlatform = platform;
        }
        else
        {
            print("side must be -1 or 1");
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    int randDir()
    {
        int randInt = Random.value < 0.5f ? -1 : 1;
        return randInt;
    }

    GameObject randPlatform()
    {
        int randInt = Random.Range(0, platforms.Count);
        print("rand index = " + randInt);
        return platforms[randInt];
    }
}
