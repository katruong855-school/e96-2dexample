using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script was made from the following tutorial: https://blog.yarsalabs.com/player-movement-with-new-input-system-in-unity/ 
//where NewPlayerControl is PlayerInput (the name of the input actions map)
public class PlayersMovement : MonoBehaviour
{

    public float PlayersMovementSpeed; //players movement speed
    private float _playersMovementDirection = 0; //Gives direction of player movement
    private PlayerInput _inputActionReference; //reference of generated C# script from the input
    private Rigidbody2D _playersRigidBody; //reference to players rigid body
    
    // Start is called before the first frame update
    private void Start()
    {
        _playersRigidBody ??= GetComponent<Rigidbody2D>();
        _inputActionReference = new PlayerInput();
        //enable input actions
        _inputActionReference.Enable();
        //read player's movement direction for movement direction

        _inputActionReference.Ground.Move.performed += moving =>
        {
            
            _playersMovementDirection = moving.ReadValue<float>();
        };
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //use the player's rigidbody to move it
        _playersRigidBody.velocity =
            new Vector2(_playersMovementDirection * PlayersMovementSpeed, _playersRigidBody.velocity.y);
        
    }
}


