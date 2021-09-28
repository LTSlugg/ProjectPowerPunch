using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*TODO: This.Now
 * Handles User Input 
 * Communicates with Animator
 * Communicates with Other Game Systems
 * Communicates with Player Game Systems (HP, Daze Meter, Block Meter)
 * 
 * TODO: WASD Control Scheme two Buttons for attack***************
 */
public class PlayerController : MonoBehaviour
{

    [SerializeField] private Transform[] TransformPositionArray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        TrackMoveDirection();
    }


    private void TrackMoveDirection()
    {
        if(Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0) 
            { this.transform.position = TransformPositionArray[0].position; }

        switch (Input.GetAxisRaw("Vertical"))
        {
            case 1:
                transform.position = TransformPositionArray[2].position;
                break;

            case -1:
                break;
        }

        switch (Input.GetAxisRaw("Horizontal"))
        {
            case 1:
                transform.position = TransformPositionArray[1].position;
                break;

            case -1:
                transform.position = TransformPositionArray[3].position;
                break;
        }
    }


}
