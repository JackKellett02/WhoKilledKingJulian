using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

[CreateAssetMenu(fileName ="Murder",menuName ="ScriptableObjects/MurderScriptableObject")]
public class MurderSO : ScriptableObject
{
    [SerializeField]
    private Sprite m_bodySprite = null;

    [SerializeField]
    private List<Evidence> m_EvidenceList = new List<Evidence>();
}

[System.Serializable]
public class Evidence
{
    public Sprite evidenceImage;
    public string evidenceDescription;

    public Evidence(Sprite image, string description)
    {
        evidenceDescription = description;
        evidenceImage = image;
    }
}