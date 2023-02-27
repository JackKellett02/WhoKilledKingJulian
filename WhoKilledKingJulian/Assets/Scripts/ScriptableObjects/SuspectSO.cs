using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Suspect", menuName = "ScriptableObjects/SuspectScriptableObject")]
public class SuspectSO : ScriptableObject {
	//Variables.
	[SerializeField]
	private Sprite m_suspectImage = null;

	[SerializeField]
	private List<string> m_suspectInterviewText = new List<string>();

	[SerializeField]
	private bool m_isMurderer = false;

	//Functions.
	public Sprite GetSuspectImage() {
		return m_suspectImage;
	}

	public List<string> GetSuspectInterviewText() {
		return m_suspectInterviewText;
	}

	public bool GetIsMurderer() {
		return m_isMurderer;
	}
}