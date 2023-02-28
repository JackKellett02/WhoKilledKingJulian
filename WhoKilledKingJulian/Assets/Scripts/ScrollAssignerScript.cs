using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScrollAssignerScript : MonoBehaviour
{
    #region Variables to assign via the unity inspector (SerializeFields).
    [SerializeField]
    private Image m_suspectPicture = null;

    [SerializeField]
    private TextMeshProUGUI m_suspectText = null;

    [SerializeField]
    private TextMeshProUGUI m_suspectName = null;
    #endregion

    #region Private Variables.
    private bool suspectIsMurderer = false;
    #endregion

    #region Private Functions.
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Public Access Functions.
    public void Accuse()
    {
        //Set whether or not the player has won based on whether or not they are accusing the murderer.
        GameOverScript.SetGameWonState(suspectIsMurderer);

        //Load the game over scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AssignSuspectPicture(Sprite a_sprite)
    {
        if (m_suspectPicture == null)
        {
            Debug.LogError("Suspect picture on scroll prefab not assigned.");
            return;
        }
        m_suspectPicture.sprite = a_sprite;
    }

    public void SetSuspectText(string a_text)
    {
        if (m_suspectText == null)
        {
            Debug.LogError("Suspect text on scroll prefab not assigned.");
            return;
        }
        m_suspectText.text = a_text;
    }

    public void SetSuspectName(string a_name)
    {
        if (m_suspectName == null)
        {
            Debug.LogError("Suspect name on scroll prefab was note assigned.");
            return;
        }
        m_suspectName.text = a_name;
    }

    public void SetSuspectIsMurderer(bool value)
    {
        suspectIsMurderer = value;
    }
    #endregion
}
