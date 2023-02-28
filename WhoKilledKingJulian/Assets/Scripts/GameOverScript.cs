using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    #region Variables to assign via the unity inspector (SerializeFields).
    [SerializeField]
    private GameObject gameWonObject = null;

    [SerializeField]
    private GameObject gameLostObject = null;

    public GameObject win;
    public GameObject lose;

    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float gameOverTime = 2.0f;
    #endregion

    #region Private Variable Declarations.
    private static bool gameWon = false;
    #endregion

    #region Private Functions.
    // Start is called before the first frame update
    void Start()
    {
        if (gameWon)
        {
            //Show game won frame.
            gameWonObject.SetActive(true);
            gameLostObject.SetActive(false);
            win.SetActive(true);
            lose.SetActive(false);
        }
        else
        {
            //Show game lost frame.
            gameWonObject.SetActive(false);
            gameLostObject.SetActive(true);
            lose.SetActive(true);
            win.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);
        }
    }

    #endregion

    #region Public Access Functions.
    public static void SetGameWonState(bool a_state)
    {
        gameWon = a_state;
    }
    #endregion
}
