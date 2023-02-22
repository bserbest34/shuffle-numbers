using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dlite.Games.Managers;
public class Obstacle : MonoBehaviour
{
    private float y;
    private float rotationSpeed;
    void Start()
    {
        y = 0;
        rotationSpeed = 150;
    }
    void Update()
    {
        Rotate();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("3") || other.CompareTag("2")|| other.CompareTag("1"))
        {
            //FindObjectOfType<NumberManager>().AddNumber(other.gameObject, 0);
            //FindObjectOfType<NumberManager>().SetNumber(gameObject, FindObjectOfType<NumberManager>().ReadNumber(other.transform.parent.gameObject).ToString());

            HapticManager.Haptic(Dlite.Games.HapticType.HeavyImpact);
            int rightDigitsCount = FindObjectOfType<NumberManager>().ReadNumber(other.transform.parent.gameObject).ToString().Length;
            if (rightDigitsCount == 1)
                return;
            this.GetComponent<BoxCollider>().enabled = false;
            GameObject temp = other.gameObject;
            temp = Instantiate(other.gameObject);
            temp.transform.position = other.gameObject.transform.position;
            temp.GetComponent<Rigidbody>().useGravity = true;
            temp.GetComponent<Rigidbody>().isKinematic = false;
            temp.GetComponent<Rigidbody>().mass = 5f;
            //other.gameObject.SetActive(false);
            for (int i = 0; i < other.transform.childCount; i++)
            {
                other.transform.GetChild(i).gameObject.SetActive(false);
            }
            if (other.CompareTag("2"))
            {
                FindObjectOfType<NumberManager>().FallMiddleNumber(other.transform.parent.gameObject);
            }
            if (other.CompareTag("1"))
            {
                FindObjectOfType<NumberManager>().FallFirstNumber(other.transform.parent.gameObject);
            }
            FindObjectOfType<NumberManager>().SetNumber(other.transform.parent.gameObject,FindObjectOfType<NumberManager>().ReadNumber(other.transform.parent.gameObject).ToString());
            other.transform.parent.Find("CuteDeath1").GetComponent<ParticleSystem>().Play();
        }
    }
    void Rotate()
    {
        y += Time.deltaTime * rotationSpeed;
        transform.localRotation = Quaternion.Euler(0, y, 0);
    }
}
