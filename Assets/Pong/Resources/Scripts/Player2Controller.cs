using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player2Controller : MonoBehaviour{

    [SerializeField] private float velocity;
    [SerializeField] private float upLimit;
    [SerializeField] private float downLimit;
    [SerializeField] private GameObject model;

    private int playerPoints;

    private float width;
    private float height;

    public float Width { get => width; }
    public float Height { get => height; }

    private void Start(){
        width = model.transform.localScale.x;
        height = model.transform.localScale.y;
    }
    void Update(){

        if (Input.GetKey(KeyCode.UpArrow)){
            transform.Translate(Vector3.up * Time.deltaTime * velocity);
            if (transform.position.y > upLimit){
                transform.position = new Vector3(transform.position.x, upLimit, transform.position.z);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow)){

            transform.Translate(Vector3.down * Time.deltaTime * velocity);
            if (transform.position.y < downLimit){
                transform.position = new Vector3(transform.position.x, downLimit, transform.position.z);
            }
        }

    }
    public void IncreasePoints(){
        playerPoints += 1;
    }
}
