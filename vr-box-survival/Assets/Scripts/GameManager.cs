using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public enum BallPunchYLevel {

    Upper,
    Lower
}


public class GameManager : MonoBehaviour
{

    public GameObject BallInstance;

    public static GameManager Instance { get; private set; }

    public int TrainingScore { get; private set; }

    public float LocalPlayerCurrentPower { get; set; }

    public float TrainingWallCurrentPower { get; set; }

    public Vector3 BallMoveDirection { get; set; }

    public Vector3 BallRotation { get; set; }

    public bool FreezeBallMovement;

    public bool IsBallMovingTowardsLocalPlayer;

    public float CurrentAppliedBallPower => IsBallMovingTowardsLocalPlayer ? TrainingWallCurrentPower : LocalPlayerCurrentPower;

    public float BallUpperPunchYPos { get; private set; }

    public float BallLowerPunchYPos { get; private set; }

    public Vector3 LocalPlayerGoalWallPos => localPlayerGoalWall.transform.position;

    public Vector3 TrainingWallPos => trainingWall.transform.position;

    public BallPunchYLevel BallPunchYLevel { get; set; }

    public float TargetBallYPos => BallPunchYLevel == BallPunchYLevel.Upper? BallUpperPunchYPos : BallLowerPunchYPos;

    public Camera MainCamera => mainCamera;

    [SerializeField]
    GameObject ballPrefab;

    [SerializeField]
    GameObject ballParent;

    [SerializeField]
    TextMeshProUGUI scoreValueText;

    [SerializeField]
    TextMeshProUGUI maxScoreValueText;

    [SerializeField]
    TextMeshProUGUI mainTitleText;

    [SerializeField]
    UI_InteractionController uiInteractionController;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    GameObject xrOrigin;

    [SerializeField]
    GameObject localPlayerGoalWall;

    [SerializeField]
    GameObject trainingWall;

    [SerializeField]
    GameObject frameBallEffectPrefab;

    [SerializeField]
    GameObject skillAttackEffectPrefab;

    Vector3 ballInitialPosition = new Vector3(0.018f, 1.8f, 1.3f);

    GameObject frameBallEffect;

    GameObject skillAttackEffect;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
    }

    public void StartTrainingGame() {

        uiInteractionController.SetUiVisibility(false);
        StopAllCoroutines();

        StartCoroutine(StartTrainingGameRoutine());
    }

    public void SetTrainingScoreValue(int value) {

        TrainingScore = value;
        int maxScore = PlayerPrefs.GetInt("maxScore", 0);

        scoreValueText.text = TrainingScore.ToString();

        if(TrainingScore > maxScore) {
            maxScore = TrainingScore;
          //  PlayerPrefs.SetInt("maxScore", maxScore);
        }

        maxScoreValueText.text = maxScore.ToString();
    }

    public void EndTraining() {

        StartCoroutine(EndTrainingRoutine());
    }

    public void IncreaseTrainingScoreValueBy(int increament) {

        SetTrainingScoreValue(TrainingScore + increament);
    }


    private IEnumerator StartTrainingGameRoutine() {

        DestroyInstanceAndSetToNull(ref BallInstance);
        DestroyInstanceAndSetToNull(ref frameBallEffect);
        DestroyInstanceAndSetToNull(ref skillAttackEffect);

        mainTitleText.text = "Box Survival";

        BallUpperPunchYPos = mainCamera.transform.position.y * 0.95f;

        BallLowerPunchYPos = mainCamera.transform.position.y * 0.7f;

        FreezeBallMovement = false;

        SetTrainingScoreValue(0);
        LocalPlayerCurrentPower = 0;
        TrainingWallCurrentPower = 5;
        BallMoveDirection = Vector3.zero;
        IsBallMovingTowardsLocalPlayer = false;
        BallRotation = Vector3.zero;

        ballInitialPosition.y = BallUpperPunchYPos;
        ballParent.transform.position = ballInitialPosition;

        
        frameBallEffect = Instantiate(frameBallEffectPrefab, ballParent.transform);

        yield return new WaitForSeconds(2f);

        BallInstance = Instantiate(ballPrefab, ballParent.transform, false);

        yield return new WaitForSeconds(1f);

        DestroyInstanceAndSetToNull(ref frameBallEffect);
    }

    private IEnumerator EndTrainingRoutine() {

        FreezeBallMovement = true;

        yield return new WaitForSeconds(0.1f);

        skillAttackEffect = Instantiate(skillAttackEffectPrefab, ballParent.transform);

        DestroyInstanceAndSetToNull(ref BallInstance);

        yield return new WaitForSeconds(1f);

        DestroyInstanceAndSetToNull(ref skillAttackEffect);

        mainTitleText.text = "Game over";

        yield return null;

        uiInteractionController.SetUiVisibility(true);
    }

    private void DestroyInstanceAndSetToNull(ref GameObject objInstance) {
        if (objInstance != null) {
            Destroy(objInstance);
            objInstance = null;
        }
    }

}
