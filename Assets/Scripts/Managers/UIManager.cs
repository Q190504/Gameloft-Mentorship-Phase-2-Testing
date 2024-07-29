using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text healthText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthText(float health) 
    {
        healthText.text = health.ToString();
        if (health >= 7) 
            healthText.color = Color.green;
        else if(health >= 2 && health <= 6)
            healthText.color = Color.yellow;
        else if (health == 1)
            healthText.color = Color.red;
    }
}
