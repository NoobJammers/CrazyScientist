using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    private float cooldown;
    public void StartGame()
    {   

        //TODO: REPLACE WITH STARTING POSITION
        SaveSystem.recentlyactivatedposition = Vector3.zero;
        SceneManager.LoadScene("LvlDesignShayne");
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape) && Time.time-cooldown>0.5f)
        {
            cooldown = Time.time;
            GameObject.FindGameObjectWithTag("MainMenu").transform.GetChild(0).gameObject.SetActive(!(GameObject.FindGameObjectWithTag("MainMenu").transform.GetChild(0).gameObject.activeInHierarchy));
        }
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
