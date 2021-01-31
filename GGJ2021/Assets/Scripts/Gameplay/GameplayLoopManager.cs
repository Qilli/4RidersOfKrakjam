using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayLoopManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameplayLoopEvent LoseEvent;
    [SerializeField]
    GameplayLoopEvent WinEvent;

    public bool checkEvent = false;
    void Start()
    {
        LoseEvent.Register(LoseGame);
        WinEvent.Register(WinGame);
    }  

    public void LoseGame()
    {
        //Debug.Log("You've lost! (baranie jebany)");
        SceneManager.LoadScene(2);
    }

    public void WinGame()
    {
        //ebug.Log("CTZGJ");
        SceneManager.LoadScene(3);
    }

    private void Update()
    {
        if(checkEvent == true)
        {
            WinEvent.Invoke();
        }
    }


}
