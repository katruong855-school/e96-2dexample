using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.Serialization;

//This script was made from the following tutorial: https://blog.yarsalabs.com/player-movement-with-new-input-system-in-unity/ 
//where NewPlayerControl is PlayerInput (the name of the input actions map)
public class PlayersMovement : MonoBehaviour
{

    public float PlayersMovementSpeed; //players movement speed
    public float PlayerJumpingForce;
    private float _playersMovementDirection = 0; //Gives direction of player movement
    private PlayerInput _inputActionReference; //reference of generated C# script from the input
    private Rigidbody2D _playersRigidBody; //reference to players rigid body
    private bool isGrounded = true;
    private bool facingLeft = true;

    
    // Start is called before the first frame update
    private void Start()
    {
        _playersRigidBody = GetComponent<Rigidbody2D>();
        _inputActionReference = new PlayerInput();
        //enable input actions
        _inputActionReference.Enable();
        //read player's movement direction for movement direction

        _inputActionReference.Ground.Move.performed += moving =>
        {
            
            _playersMovementDirection = moving.ReadValue<float>();
        };

        //Jumping the player
        _inputActionReference.Ground.Jump.performed += jumping => { JumpThePlayer();};
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(_playersRigidBody.velocity.y == 0){
            isGrounded = true;
        }

        //use the player's rigidbody to move it
        _playersRigidBody.velocity =
            new Vector2(_playersMovementDirection * PlayersMovementSpeed, _playersRigidBody.velocity.y);

        if(_playersMovementDirection > 0 && facingLeft){
            Flip();
        }else if(_playersMovementDirection < 0 && !facingLeft){
            Flip();
        }
        
        
    }

    private void JumpThePlayer()
    {
        //Moving player using player rigid body.
        if(isGrounded == true){
            
            _playersRigidBody.velocity = Vector2.up * PlayerJumpingForce;
            isGrounded = false;
        }
        
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingLeft = !facingLeft;
    }
}


