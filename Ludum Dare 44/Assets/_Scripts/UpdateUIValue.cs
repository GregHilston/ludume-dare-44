using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateUIValue : MonoBehaviour {
    
    private TextMeshProUGUI tmpro;

    [SerializeField]
    [TextArea]
    private string preText;
    [SerializeField]
    [TextArea]
    private string postText;

    void Start() {
        tmpro = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateUI(object value) {
        if (tmpro != null) {
            tmpro.text = preText + value + postText;
        } else {
            Debug.LogError(gameObject.name + " does not have a reference to TextMeshPro");
        }
    }

}
