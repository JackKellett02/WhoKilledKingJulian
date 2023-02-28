using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    #region Variables to assign via the unity inspector (SerializeFields).
    [Header("Murder Info")]
    [SerializeField]
    private MurderSO startingMurderScene = null;

    [Header("Murder Stage references")]
    [SerializeField]
    private GameObject worldSpaceMurderStage = null;

    [SerializeField]
    private Image evidenceCloseupImage = null;

    [SerializeField]
    private TextMeshProUGUI evidenceDescriptionBox = null;

    public GameObject backgroundForCloseup;

    [Header("Interview Stage References")]
    [SerializeField]
    private List<GameObject> suspectScrolls = new List<GameObject>();


    [Header("Accuse Stage References")]

    [Header("Gameover Stage references")]

    [Header("Utility")]
    [SerializeField]
    [Range(0.0f, 2.0f)]
    private float evidenceHoverRange = 0.1f;

    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float evidenceScale = 3.0f;

    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float bodyScale = 3.5f;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject spriteRendererPrefab = null;
    #endregion

    #region Private Variables.
    private List<EvidenceStruct> currentEvidenceList = new List<EvidenceStruct>();
    private SpriteRenderer bodySprite = null;
    private MurderSO currentMurderScene = null;

    private Stages currentStage = Stages.murderStage;
    private Evidence currentHoverEvidence = null;
    #endregion

    #region Private Functions.
    // Start is called before the first frame update
    void Start()
    {
        LoadMurderScene(startingMurderScene);
        SetStage("murder");
    }

    // Update is called once per frame
    void Update()
    {
        //Ensure everything is valid.
        if (worldSpaceMurderStage == null)
        {
            Debug.LogError("World space holder for murder stage was not assigned");
            return; //early out.
        }
        if (evidenceCloseupImage == null)
        {
            Debug.LogError("Evidence closeup image was not assigned");
            return;//Early out.
        }
        if (evidenceDescriptionBox == null)
        {
            Debug.LogError("Evidence description box was not assigned");
            return;//Early out.
        }

        //Check current stage.
        if (currentStage == Stages.murderStage)
        {
            worldSpaceMurderStage.SetActive(true);
            Evidence hoverEvidence = CheckIfMouseIsOverEvidence();
            if (hoverEvidence == null || currentHoverEvidence == hoverEvidence)
            {
                evidenceCloseupImage.gameObject.SetActive(false);
                evidenceDescriptionBox.gameObject.SetActive(false);
                backgroundForCloseup.gameObject.SetActive(false);
                return; //Early out.
            }

            //Set the closeup image.
            evidenceCloseupImage.gameObject.SetActive(true);
            evidenceCloseupImage.sprite = hoverEvidence.evidenceImage;

            //Set the text.
            evidenceDescriptionBox.gameObject.SetActive(true);
            evidenceDescriptionBox.text = hoverEvidence.evidenceDescription;

            backgroundForCloseup.gameObject.SetActive(true);
        }
        else if (currentStage == Stages.interviewStage)
        {

        }
        else if (currentStage == Stages.gameOver)
        {

        }
    }

    private Evidence CheckIfMouseIsOverEvidence()
    {
        int index = -1;
        float shortestDistanceSqr = float.PositiveInfinity;
        for (int i = 0; i < currentEvidenceList.Count; i++)
        {
            //Get the mouse position.
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0.0f;

            //Get the position of the evidence.
            Vector3 evidencePosition = currentEvidenceList[i].spriteRenderer.transform.position;

            //Check if it's close enough.
            float distanceSqr = (evidencePosition - mousePos).sqrMagnitude;
            if (distanceSqr > evidenceHoverRange * evidenceHoverRange * evidenceScale)
            {
                continue;
            }

            //If it is check if it's closer than the current shortest distance.
            if (distanceSqr > shortestDistanceSqr)
            {
                continue;
            }

            //If it is set the index to the current i value.
            index = i;
            shortestDistanceSqr = distanceSqr;
        }

        if (index == -1)
        {
            //Debug.Log("Wasn't hovering over evidence");
            return null;//If the mouse isn't hovering over any of them.
        }

        return currentEvidenceList[index].evidence;
    }
    #endregion

    #region Public Access Functions.
    public void SetStage(string a_sStage)
    {
        Stages a_stage = Stages.murderStage;
        if (a_sStage == "murder")
        {
            a_stage = Stages.murderStage;
        }
        else if (a_sStage == "interview")
        {
            a_stage = Stages.interviewStage;
        }
        else if (a_sStage == "gameOver")
        {
            a_stage = Stages.gameOver;
        }
        currentStage = a_stage;

        //Deactivate stage references.
        if (a_sStage != "murder")
        {
            worldSpaceMurderStage.gameObject.SetActive(false);
            evidenceCloseupImage.gameObject.SetActive(false);
            evidenceDescriptionBox.gameObject.SetActive(false);
            backgroundForCloseup.gameObject.SetActive(false);
        }
        if (a_sStage != "interview")
        {

        }
        if (a_sStage != "gameOver")
        {

        }
    }

    public void LoadMurderScene(MurderSO a_murderScene)
    {
        //Cleanup.
        CleanupOldMurderScene();

        //Set the new scene.
        currentMurderScene = a_murderScene;

        //Instantiate the body sprite.
        GameObject body = Instantiate(spriteRendererPrefab, worldSpaceMurderStage.transform);
        body.transform.position = currentMurderScene.GetBodyPosition();
        body.transform.localScale = Vector3.one * bodyScale;
        body.transform.rotation = Quaternion.Euler(currentMurderScene.bodyRot);
        bodySprite = body.GetComponent<SpriteRenderer>();
        Sprite sprite = currentMurderScene.GetBodySprite();
        if (sprite != null)
        {
            bodySprite.sprite = sprite;
        }

        //Instantiate enough sprite renderers for all the Evidence.
        List<Evidence> evidenceList = currentMurderScene.GetEvidenceList();
        for (int i = 0; i < evidenceList.Count; i++)
        {
            //Instantiate the evidence.
            GameObject currentEvidence = Instantiate(spriteRendererPrefab, worldSpaceMurderStage.transform.position, Quaternion.Euler(evidenceList[i].evRot));
            currentEvidence.transform.localScale = Vector3.one * evidenceScale;

            if (currentMurderScene.RandomiseEvidencePositions())
            {
                //Move it to a random position around the body.
                float randX = UnityEngine.Random.Range(-5f, 5f);
                float randY = UnityEngine.Random.Range(-2.5f, 2.5f);
                Vector3 randPos = new Vector3(randX, randY, 0.0f);
                currentEvidence.transform.position = randPos;
            }
            else
            {
                Vector3 pos = evidenceList[i].evidencePos;
                currentEvidence.transform.position = pos;
            }

            //Add it to the list.
            EvidenceStruct evidenceStruct;
            evidenceStruct.spriteRenderer = currentEvidence.GetComponent<SpriteRenderer>();
            evidenceStruct.spriteRenderer.sprite = evidenceList[i].evidenceImage;
            evidenceStruct.evidence = evidenceList[i];
            currentEvidenceList.Add(evidenceStruct);
        }

        //Load the correct suspect info into the scrolls.
        List<SuspectSO> suspects = currentMurderScene.GetSuspects();
        List<string> questions = currentMurderScene.GetInterviewQuestions();
        for (int i = 0; i < suspects.Count && i < suspectScrolls.Count; i++)
        {
            //Get the suspect.
            SuspectSO suspect = suspects[i];

            //Get the scroll.
            ScrollAssignerScript scroll = suspectScrolls[i].GetComponent<ScrollAssignerScript>();

            //Assign if they're the murderer.
            scroll.SetSuspectIsMurderer(suspect.GetIsMurderer());

            //Assign the picture.
            scroll.AssignSuspectPicture(suspect.GetSuspectImage());

            //Assign the name.
            scroll.SetSuspectName(suspect.GetSuspectName());

            //Construct the interview text.
            List<string> interview = suspect.GetSuspectInterviewText();
            string text = "";
            for (int j = 0; j < interview.Count && j < questions.Count; j++)
            {
                text += ("Q - " + questions[j] + "\n" + "A - " + interview[j] + "\n");
            }

            //Assign it.
            scroll.SetSuspectText(text);
        }
    }
    #endregion

    #region Utility.

    private struct EvidenceStruct
    {
        public Evidence evidence;
        public SpriteRenderer spriteRenderer;
    }

    private void CleanupOldMurderScene()
    {

    }
    #endregion
}

public enum Stages
{
    murderStage,
    interviewStage,
    accuseStage,
    gameOver
}