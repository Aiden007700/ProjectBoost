using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    public Vector3 movmentVector;
    [SerializeField] [Range(0,1)] float movmentFactor;
    public float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / (period <= Mathf.Epsilon ? 1 : period);
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movmentFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movmentVector * movmentFactor;
        transform.position = startingPosition + offset;
    }
}
