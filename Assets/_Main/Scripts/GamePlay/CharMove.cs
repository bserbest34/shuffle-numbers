using UnityEngine;
using DG.Tweening;

public class CharMove : MonoBehaviour
{
    public float speedInZAxis;
    public float speedInYAxis;

    private void Update()
    {
        transform.Translate(new Vector3(0, 0, speedInZAxis * Time.deltaTime));
        transform.Translate(new Vector3(0, speedInYAxis, 0 * Time.deltaTime));
    }
}