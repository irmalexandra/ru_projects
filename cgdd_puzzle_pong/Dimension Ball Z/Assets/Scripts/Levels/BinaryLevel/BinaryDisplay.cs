using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BinaryDisplay : MonoBehaviour
{

    public static BinaryDisplay Instance;

    public TMPro.TextMeshProUGUI currentBinary;
    public TMPro.TextMeshProUGUI currentInt;
    public TMPro.TextMeshProUGUI requiredInt;
    public GameObject winPortal;

    private List<int> _binaryList;
    // Start is called before the first frame update
    void Start()
    {
        requiredInt.text = Random.Range(3, 16).ToString();
        Instance = this;
        _binaryList = new List<int>() {0,0,0,0};
        UpdateCurrentBinary();
    }

    private void UpdateCurrentBinary()
    {
        currentBinary.text = String.Join("", _binaryList.ToArray());
    }
    public void Signal(int input)
    {
        _binaryList.Insert(4, input);
        _binaryList.RemoveAt(0);
        UpdateCurrentBinary();
        int required = int.Parse(requiredInt.text);
        int binaryToInt = Convert.ToInt32(currentBinary.text, 2);
        currentInt.text = binaryToInt.ToString();

        if (binaryToInt == required)
        {
            winPortal.SetActive(true);
        }
    }
    
}
