using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveSystem : MonoBehaviour
{
    public static bool createdalready;
    private void Awake()
    {
        if (!createdalready)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
        createdalready = true;
    }

    private void Start()
    {

    }
    //replace with the first position that player needs to spawn at
    public static Vector3 recentlyactivatedposition = new Vector3(-4, -14.7f, 0);





}

