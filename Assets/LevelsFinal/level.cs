using UnityEngine;

public class level : MonoBehaviour
{

    int side;
    public GameObject endArea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initialize_values(int SIDESIDE)
    {
        side = SIDESIDE;
        if (side == 1)
        {
            endArea.layer = LayerMask.NameToLayer("winMask");
        }
        else if (side == -1)
        {
            endArea.layer = LayerMask.NameToLayer("loseMask");
        }
    }
}
