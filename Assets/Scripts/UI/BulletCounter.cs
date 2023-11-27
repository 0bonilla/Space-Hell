using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCounter : MonoBehaviour
{
    [SerializeField] private Text BulletText;

    public void UpdateAmmo(int count)
    {
        BulletText.text = "Bullets: " + count;
    }
}
