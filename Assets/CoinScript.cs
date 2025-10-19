using UnityEngine;

public class CoinScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Coin collected!");
            GameManager.Instance.AddTime(GameManager.Instance.timeBonus); // add time bonus
            Destroy(gameObject); // remove the coin

        }
    }
}
