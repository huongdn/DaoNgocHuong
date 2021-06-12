using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Helicopter;

    public float m_spawnerTime;


    // Start is called before the first frame update
    void Start()
    {
        float worldHeight = Camera.main.orthographicSize; // Camera Height
        float worldWidth = worldHeight * Screen.width / Screen.height; // Camera Width


        Quaternion leftIdentity = Quaternion.identity;
        Quaternion rightIdentity = leftIdentity;
        rightIdentity.y = 180;

        StartCoroutine(_SpawnHelicopter(worldHeight, worldWidth, leftIdentity, rightIdentity));        
    }

    IEnumerator _SpawnHelicopter(float worldHeight, float worldWidth, Quaternion leftIdentity, Quaternion rightIdentity)
    {
        yield return new WaitForSeconds(m_spawnerTime);

        Vector2 leftPos = new Vector2(-worldWidth, worldHeight);
        Vector2 rightPos = new Vector2(worldWidth, worldHeight);

        if (Random.Range(1, 5) % 2 == 0)
        {
            //Spawn left side helicopter
            leftPos.y -= Random.Range(0.5f, 1.5f);

            //Debug.Log("leftPos.y:" + leftPos.y);
            Instantiate(m_Helicopter, leftPos, leftIdentity);
        }
        else
        {
            //Spawn right side helicopter
            rightPos.y -= Random.Range(0.5f, 1.5f);
            //Debug.Log("rightPos.y:" + rightPos.y);
            Instantiate(m_Helicopter, rightPos, rightIdentity);
        }

        StartCoroutine(_SpawnHelicopter(worldHeight, worldWidth, leftIdentity, rightIdentity));
    }
}
