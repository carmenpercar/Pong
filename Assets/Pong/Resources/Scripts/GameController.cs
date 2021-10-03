using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    #region Variables
    [SerializeField] private float rightLimit;
    [SerializeField] private float lefttLimit;
    [SerializeField] private float center;
    [SerializeField] private BallController ball;
    [SerializeField] private PlayerController player1;
    [SerializeField] private PlayerController player2;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button PauseButton;

    private bool gameInPause;
    private float ballVelocity;
    private float player1Velocity;
    private float player2Velocity;

    #endregion

    #region UnityMethods
    private void Start(){
        restartButton.onClick.AddListener(RestartGame);
        PauseButton.onClick.AddListener(PauseGame);
        player1Velocity = player1.Velocity;
        player2Velocity = player2.Velocity;
        ballVelocity = ball.Velocity;
    }
    private void Update(){        
        if(PlayerCollisionDetected(player1) || PlayerCollisionDetected(player2)){
            OnPlayerCollisionEnter();
        }
        if (ball.gameObject.transform.position.x >= rightLimit) {    // If the ball cross the right limmit without collides with the player, it will be reset and its one point to player1
            ball.resetBall();
            player1.IncreasePoints();
        }
        else if (ball.gameObject.transform.position.x <= lefttLimit) { // If the ball cross the left limmit without collides with the player, it will be reset and its one point to player2
            ball.resetBall();
            player2.IncreasePoints();
        }
    }
    #endregion

    #region CollisionsMethods
    private bool PlayerCollisionDetected(PlayerController player){

        float XBall = ball.transform.position.x;
        float YBall = ball.transform.position.y;
        float XPlayer = player.gameObject.transform.position.x;
        float YPlayer = player.gameObject.transform.position.y;
        if(XBall/XPlayer > 0){ //check if the player is in the same field than the ball.
            if (((YBall - ball.Height/2) <= (YPlayer + player.Height/2)) && ((YBall + ball.Height/2) >= YPlayer - player.Height/2)){ // Check if the ball is in the same Y position than player.
                if (XBall > 0){ //Check if the ball is in the right field.
                    if (((XBall + ball.Width/2) >= (XPlayer - player.Width/2)) && ((XBall - ball.Width / 2) <= (XPlayer + player.Width / 2))){ //Check if the ball is in the same X position than player.
                        return true;
                    }
                }
                else{   //Check if the ball is in the left field.
                    if (((XBall - ball.Width/2) <= (XPlayer + player.Width/2)) && ((XBall + ball.Width / 2) >= (XPlayer - player.Width / 2))){
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private void OnPlayerCollisionEnter(){
        ball.changeXDirection();
    }
    #endregion

    #region GameControllerMethods
    public void RestartGame(){  // Associated to Restart Button is the function to start again the game.

        ResumeGame();
        player1.ResetPoints();
        player2.ResetPoints();
        ball.resetBall();
    }

    private void PauseGame(){   //Associated to Pause Button is the function to pause the game.
        if (gameInPause){
            return;
        }
        player1Velocity = player1.Velocity;  //  Player1 velocity is stored.
        player2Velocity = player2.Velocity;  // Player2 velocity is stored.
        ballVelocity = ball.Velocity;   //  Ball velocity is stored.

        player1.Velocity = 0;
        player2.Velocity = 0;
        ball.Velocity = 0;

        PauseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Resume"; //Change the textButton from 'Pause' to 'Resume'.

        PauseButton.onClick.RemoveAllListeners();
        PauseButton.onClick.AddListener(ResumeGame);
        gameInPause = true;

    }
    private void ResumeGame(){ // Resume the game after Pause

        player1.Velocity = player1Velocity; // Set player velocity again.   
        player2.Velocity = player2Velocity; // Set player velocity again.
        ball.Velocity = ballVelocity;   // Set ball velocity again.

        PauseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Pause";   //Change the textButton from 'Resume' to 'Pause'.

        PauseButton.onClick.RemoveAllListeners();
        PauseButton.onClick.AddListener(PauseGame);
        gameInPause = false;
    }
    #endregion
}
