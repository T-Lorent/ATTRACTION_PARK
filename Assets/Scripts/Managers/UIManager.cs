using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI visitors_text;
    [SerializeField] private TextMeshProUGUI walkers_text;

    void Awake() => Instance = this;

    public void update_nb_agents(int nb_visitors, int nb_walkers)
    {
        visitors_text.text = "Nb visitors : " + nb_visitors;
        walkers_text.text = "Nb walkers : " + nb_walkers;
    }
}
