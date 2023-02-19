using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<FlappyBird>() != null)
        {
            GameManager.Instance.OnFlappyBirdDead();
        }
    }

}
