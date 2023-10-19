using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationHandler : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;

    public void EnableDeathScreen()
    {
        deathScreen.SetActive(true);
    }
}
