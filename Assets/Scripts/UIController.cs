using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public float time { get; set; }
    public bool timerActive { get; set; }
    public bool solved { get; set; }

    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text solveText;
    [SerializeField] TMP_Text instructionText;

    // Start is called before the first frame update
    void Start()
    {

        timeText.text = $"Time: {time}";
        StartCoroutine(Flush());
    }

    IEnumerator Flush()
    {
        yield return Utility.wait(15);
        UpdateInstructions("Q to quit");
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            time += Time.deltaTime;
            timeText.text = $"Time: {time.ToString("N")}";
        }

        solveText.text = $"Solved: {solved}";
    }

    public void UpdateInstructions(string instruction)
    {
        instructionText.text = instruction;
    }
}
