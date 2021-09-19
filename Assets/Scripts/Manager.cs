using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class MasterManager
{
    private static Manager instance;

    public static Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = UnityEngine.Object.FindObjectOfType<Manager>();
            }
            return instance;
        }
    }
}
public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
