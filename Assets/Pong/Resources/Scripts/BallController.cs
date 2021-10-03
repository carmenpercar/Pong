using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallController : MonoBehaviour{

    #region Variables
    [SerializeField] private float velocity;
    [SerializeField] private float upLimit;
    [SerializeField] private float downLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float leftLimit;
    [SerializeField] private float timeDelay;
    [SerializeField] private GameObject model;

    private float elapsedSeconds;
    private bool collisionAvailable;
    private bool stopCollisions;

    private Vector2 movementDirection = new Vector2();
    private Vector3 startPosition;
    private float height;
    private float width;

    public UnityEvent OnResetBall = new UnityEvent();
    public UnityEvent OnPlayerCollision = new UnityEvent();
    public UnityEvent OnFieldCollision = new UnityEvent();

    #endregion

    #region UnityMethods
    private void Start(){
        startPosition = transform.position;
        collisionAvailable = true;  
        stopCollisions = false;     
        SetRandomDirectionAndPosition();    // Set random position and velocity directon to the ball.
    }

    private void Update(){

        transform.Translate(velocity * movementDirection * Time.deltaTime); // Move the ball in the movement direction.
        if (transform.position.y > upLimit) {   // If the ball cross the upper limit, it changes its Y direction (simulating a bounce).
            changeYDirection();
        }
        else if(transform.position.y < downLimit) {     // If the ball cross the lower limit, it changes its Y direction (simulating a bounce).
            changeYDirection();
        }
        collisionAvailable = checkIfBallCollisionAvailable();   // If the time delay has been completed since the previous collision, the collision will be available. 
    }
    #endregion UnityMethods

    #region BallMethods
    private void SetRandomDirectionAndPosition(){

        float x = Random.Range(50, 100);
        float y = Random.Range(50, 100);
        float randomHigh = Random.Range(upLimit, downLimit);
        bool sign1 = (Random.Range(-10, 10) > 0);
        bool sign2 = (Random.Range(-10, 10) < 0);

        if(sign1){
            x *= (-1);
        }

        if(sign2){
            y *= (-1);
        }
        movementDirection = new Vector2(x, y).normalized;   // Set new movement direction without modifying the velocity.
        transform.position = new Vector2(transform.position.x, randomHigh); // Set new position in Y-axis to start new point.
    }

    public void changeXDirection(){ // This function is called when the ball collides with the player, its direction changes in X-Axis
        OnPlayerCollision.Invoke(); 
        if(collisionAvailable){
            movementDirection = new Vector2(-movementDirection.x, movementDirection.y);
        }
        stopCollisions = true;
        
    }
    private void changeYDirection(){ // This function is called when the ball cross the upper and lower limits, its direction changes in Y-Axis
        OnFieldCollision.Invoke();
        if (collisionAvailable){
            movementDirection = new Vector2(movementDirection.x, -movementDirection.y);
        }
        stopCollisions = true;
    }

    public void resetBall(){ // Set New position on the mid line and movement direction.
        OnResetBall.Invoke();
        transform.position = startPosition;
        SetRandomDirectionAndPosition();
    }

    private bool checkIfBallCollisionAvailable(){ // Timer to check if the collision is available after other collision.
        if(stopCollisions){
            elapsedSeconds += Time.deltaTime;
            if(elapsedSeconds >= timeDelay){
                elapsedSeconds -= elapsedSeconds;
                stopCollisions = false;
                return true;
            }
            else{
                return false;
            }
        }
        return true;
    }
    private void SetUpSize(){   //Set width and height and width checking the scale of the model.
        width = model.transform.localScale.x;
        height = model.transform.localScale.y;
    }

    #endregion

    #region Properties
    public float Width { get => width; }
    public float Height { get => height; }
    public float Velocity { get => velocity; set => velocity = value; }
    #endregion
}
