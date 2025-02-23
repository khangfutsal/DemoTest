using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool isRolling;
    public float initialSpeed = 10f;
    public float minSpeed = 0.5f;
    private float speedRolling;

    public float randomTimes;
    public float curTimes;

    private float finalReach = -1;


    private void Update()
    {
        if (!isRolling) return;

        float progress = curTimes / randomTimes;
        speedRolling = Mathf.Lerp(initialSpeed, minSpeed, progress);

        transform.Translate(Vector2.down * speedRolling * Time.deltaTime);

        if (curTimes < randomTimes - 1)
        {
            if (transform.position.y <= -12.5f)
            {
                curTimes++;
                transform.position = new Vector2(transform.position.x, -1.5f);
            }
        }
        else
        {
            if (finalReach == -1)
            {
                finalReach = Mathf.Floor(Random.Range(1, 7)) * -2.75f;
            }
            float distance = Mathf.Abs(transform.position.y - finalReach);
            if (distance < 0.1f)
            {
                speedRolling = 0;
                transform.position = new Vector2(transform.position.x, finalReach);
                isRolling = false;
            }
        }
    }

    public void Rolling()
    {
        isRolling = true;
        finalReach = -1;
        curTimes = 0;
        randomTimes = Random.Range(10, 15);
        speedRolling = initialSpeed;
    }
}
