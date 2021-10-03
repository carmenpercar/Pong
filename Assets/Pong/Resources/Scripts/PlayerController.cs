using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayerController : MonoBehaviour{

    #region Variables
    [SerializeField] private float velocity;
    [SerializeField] private float upLimit;
    [SerializeField] private float downLimit;
    [SerializeField] private GameObject model;
    [SerializeField] private TextMeshProUGUI playerPointsText;

    private char side;
    private int playerPoints;
    private float width;
    private float height;

    #endregion

    #region UnityMethods
    private void Start(){

        SetUpSize();
        SetUpSide();
        playerPoints = 0;
    }
    void Update(){
        switch (side){ // Decide the movement control
            case 'R':
                if (Input.GetKey(KeyCode.UpArrow)){
                    MoveUp();
                }else if(Input.GetKey(KeyCode.DownArrow)){
                    MoveDown();
                }
                break;

            case 'L':
                if (Input.GetKey(KeyCode.W)){
                    MoveUp();
                }
                else if (Input.GetKey(KeyCode.S)){
                    MoveDown();
                }
                break;
        }
    }
    #endregion
    #region MovementControlMethods
    private void MoveUp(){
        transform.Translate(Vector3.up * Time.deltaTime * velocity);
        if (transform.position.y > upLimit){
            transform.position = new Vector3(transform.position.x, upLimit, transform.position.z);
        }
    }

    private void MoveDown(){
        transform.Translate(Vector3.down * Time.deltaTime * velocity);
        if (transform.position.y < downLimit){
            transform.position = new Vector3(transform.position.x, downLimit, transform.position.z);
        }
    }
    #endregion
    #region PointsControlMethods
    public void IncreasePoints(){
        playerPoints += 1;
        UpdatePointsDisplay();
    }
    private void UpdatePointsDisplay(){
        playerPointsText.text = playerPoints.ToString(); // Set the new score. 
    }
    public void ResetPoints(){
        playerPoints = 0;
        UpdatePointsDisplay();
    }
    #endregion
    #region SetUpMethods
    private void SetUpSide(){
        if (transform.position.x >= 0){ // Check the position of the player, and decide if it is on the right side or on the left side.
            side = 'R';
        }
        else{
            side = 'L';
        }

    }
    private void SetUpSize(){
        height = model.transform.localScale.y;
        width = model.transform.localScale.x;
    }
    #endregion
    #region Properties
    public float Width { get => width; }
    public float Height { get => height; }
    public float Velocity { get => velocity; set => velocity = value; }
    #endregion
}
