using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpCount : MonoBehaviour
{
    private Transform main;
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.Find("MainPlayer").transform;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHpBar();
    }
    void UpdateHpBar() {
        if (main) {
            float hpCount = main.GetComponent<TankMain>().currentHp / main.GetComponent<TankMain>().hp;
            GetComponent<Image>().fillAmount = hpCount;
            if (hpCount > 0.5f)
            {
                GetComponent<Image>().color = Color.green;
            }
            else if (hpCount > 0.2f)
            {
                GetComponent<Image>().color = Color.yellow;
            }
            else GetComponent<Image>().color = Color.red;
        }
    }
}
