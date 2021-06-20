using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets m_sIstance;

    public AudioClip m_FireBulletSfx;
    public static GameAssets instance
    {
        get
        {
            if(m_sIstance == null)
            {
                m_sIstance = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            }
            return m_sIstance;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
