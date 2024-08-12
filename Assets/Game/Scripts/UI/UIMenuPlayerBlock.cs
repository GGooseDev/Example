using TMPro;
using UnityEngine;

public class UIMenuPlayerBlock : MonoBehaviour
{
    [SerializeField] private TMP_Text _nick;

    public void SetData(string nick)
    {
        _nick.text = nick;
    }
}