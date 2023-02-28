using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Murder", menuName = "ScriptableObjects/MurderScriptableObject")]
public class MurderSO : ScriptableObject {
	//Variables.
	[SerializeField]
	private Sprite m_bodySprite = null;

    [SerializeField]
	private Vector3 m_bodyPos = Vector3.zero;

	public Vector3 bodyRot;

    [Header("Evidence")]
	[SerializeField]
	private bool randomiseEvidencePositions = false;

	[SerializeField]
	private List<Evidence> m_EvidenceList = new List<Evidence>();

	[SerializeField]
	private List<SuspectSO> m_suspects = new List<SuspectSO>();

	[SerializeField]
	private List<string> m_interviewQuestions = new List<string>();


	//Public functions.
	public Sprite GetBodySprite() {
		return m_bodySprite;
	}

	public Vector3 GetBodyPosition()
    {
		return m_bodyPos;
    }

	public List<Evidence> GetEvidenceList() {
		return m_EvidenceList;
	}

	public List<SuspectSO> GetSuspects() {
		return m_suspects;
	}

	public List<string> GetInterviewQuestions() {
		return m_interviewQuestions;
	}

	public bool RandomiseEvidencePositions() {
		return randomiseEvidencePositions;
	}
}

[System.Serializable]
public class Evidence {
	public Sprite evidenceImage;
	public string evidenceDescription;
	public Vector3 evidencePos;
	public Vector3 evRot;

	public Evidence(Sprite image, string description, Vector3 pos) {
		evidenceDescription = description;
		evidenceImage = image;
		evidencePos = pos;
	}
}