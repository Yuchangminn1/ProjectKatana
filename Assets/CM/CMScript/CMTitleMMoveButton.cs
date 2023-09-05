using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMTitleMMoveButton : MonoBehaviour
{
    [SerializeField] public Button[] buttons;
    [SerializeField] int currentNum = 0;
    [SerializeField] public RectTransform currentTr;
    [SerializeField] public Text currentText;

    // Start is called before the first frame update
    void Start()
    {

        currentTr = buttons[currentNum].GetComponentInChildren<RectTransform>();
        currentText = buttons[currentNum].GetComponentInChildren<Text>();
        StartCoroutine("TextDance");
        buttons[currentNum].image.color = new Color(0.4f, 0.4f, 0.4f, 0.3f);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            ChangButton(-1);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            ChangButton(1);
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ClickButton();
            Debug.Log($"{currentNum}번 버튼 눌렀음");
        }
    }

    private void ChangButton(int num)
    {
        if (currentNum + num < 0 || currentNum + num > 4)
            return;
        else
        {
            buttons[currentNum].image.color = new Color(0,0,0,0.3f);
            currentText.color = new Color(1, 1, 1, 0.4f);
            currentTr.localScale = Vector3.one;
            currentNum += num;
            buttons[currentNum].image.color = new Color(0.4f, 0.4f, 0.4f, 0.3f);
            currentTr = buttons[currentNum].GetComponentInChildren<RectTransform>();
            currentText = buttons[currentNum].GetComponentInChildren<Text>();

        }
    }
    IEnumerator TextDance()
    {
        while(true)
        {
            
            currentTr.localScale = Vector3.one + Vector3.up * 0.05f;
            currentText.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.25f);
            currentText.color = new Color(1, 1, 1, 0.4f);
            currentTr.localScale = Vector3.one;
            yield return new WaitForSeconds(0.25f);

        }

    }
    void ClickButton()
    {
        buttons[currentNum].GetComponent<Button>().onClick.Invoke();
    }
}
