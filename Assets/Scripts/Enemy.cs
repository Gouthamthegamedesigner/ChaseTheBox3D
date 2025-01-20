using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
   
   [SerializeField] private float _speed = 9;
   [SerializeField] private string playerTag = "Player"; // Tag to identify the player

   private Transform _target;

   void Awake()
   {
        _target = FindObjectOfType<PlayerMovement>().transform;
   }

    void Update()
    {
         Vector3 targetPosition = new Vector3(_target.position.x, transform.position.y, _target.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

        // Rotate towards the player
        Vector3 direction = targetPosition - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the player tag
        if (collision.gameObject.CompareTag(playerTag))
        {
            Application.Quit();
        }
    }

}
