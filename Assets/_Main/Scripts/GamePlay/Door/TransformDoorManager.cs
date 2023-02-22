using TMPro;
using UnityEngine;

public class TransformDoorManager : MonoBehaviour
{
    [SerializeField] public enum TransformType { Positive, Negative, Shuffle , Multiple, Div }
    [SerializeField] public TransformType type;

    [SerializeField] private GameObject[] models;

    private void OnValidate()
    {
        for (int i = 0; i < models.Length; i++)
        {
            if (models[i].name.Contains(type.ToString()))
            {
                models[i].SetActive(true);

                if (models[i].name == "Shuffle")
                {
                    foreach (Transform child in this.gameObject.transform)
                    {
                        if (child.name != "Shuffle")
                        {
                            child.gameObject.SetActive(false);
                        }
                    }
                }
            }
            else
            {
                models[i].SetActive(false);
            }

        }
        switch (type)
        {
            case TransformType.Positive:
                if (transform.Find("Positive").Find("Positive").GetComponent<TextMeshPro>().text[0] == '+')
                    return;
                transform.Find("Positive").Find("Positive").GetComponent<TextMeshPro>().text = "+" +    transform.Find("Positive").Find("Positive").GetComponent<TextMeshPro>().text;
                break;
            case TransformType.Negative:
                if (transform.Find("Negative").Find("Negative").GetComponent<TextMeshPro>().text[0] == '-')
                    return;
                transform.Find("Negative").Find("Negative").GetComponent<TextMeshPro>().text = "-" + transform.Find("Negative").Find("Negative").GetComponent<TextMeshPro>().text;
                break;
            case TransformType.Shuffle:
                break;
            case TransformType.Multiple:
                if (transform.Find("Multiple").Find("Multiple").GetComponent<TextMeshPro>().text[0] == 'x')
                    return;
                transform.Find("Multiple").Find("Multiple").GetComponent<TextMeshPro>().text = "x" + transform.Find("Multiple").Find("Multiple").GetComponent<TextMeshPro>().text;
                break;
            case TransformType.Div:
                //if (transform.Find("Div").Find("Div").GetComponent<TextMeshPro>().text[0] == '?')
                //    return;
                //transform.Find("Div").Find("Div").GetComponent<TextMeshPro>().text = "?" + transform.Find("Div").Find("Div").GetComponent<TextMeshPro>().text;
                break;
            default:
                break;
        }
        
    }
}