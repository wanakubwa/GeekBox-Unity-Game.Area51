using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LvlMenager : MonoBehaviour
{
    GameMenager gameMenager;
    TextMeshProUGUI lvlTime;

    float currentLvlTime = 0f;
    float currentEditedTime = 0f;
    bool isCountingEnable = false;

    // Start is called before the first frame update
    void Start()
    {
        gameMenager = FindObjectOfType<GameMenager>();
        FindLvlTimeText();
        lvlTime.text = "00:00s";
        StartCoroutine(CheckSceneFullyLoaded());
        InGameEvents.startCountingTimeEvent += this.StartCountingTime_Handler;
        InGameEvents.playerWinLvlEvent += this.PlayerWinLvlEvent_Handler;
        InGameEvents.playerDeadEvent += this.PlayerDeadEvent_Handler;

        InGameEvents.CallStartLvlMusicEvent();
    }

    private void PlayerDeadEvent_Handler()
    {
        isCountingEnable = false;
    }

    private void StartCountingTime_Handler()
    {
        isCountingEnable = true;
    }

    public void PlayerWinLvlEvent_Handler()
    {
        gameMenager.CurrentTimeEdited = currentEditedTime;
        gameMenager.AddTimeToLvl(currentEditedTime);
        isCountingEnable = false;
    }

    private void OnDestroy()
    {
        InGameEvents.startCountingTimeEvent -= this.StartCountingTime_Handler;
        InGameEvents.playerWinLvlEvent -= this.PlayerWinLvlEvent_Handler;
        InGameEvents.playerDeadEvent -= this.PlayerDeadEvent_Handler;
    }

    private void FindLvlTimeText()
    {
        var objTmp = GameObject.FindGameObjectWithTag("LvlTime");
        lvlTime = objTmp.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(isCountingEnable)
        {
            currentLvlTime += Time.deltaTime;
            EditAndDisplayTime();
        }
    }

    private void EditAndDisplayTime()
    {
        var fullMin = (int)(currentLvlTime / 60f);
        var fullSec = currentLvlTime - fullMin * 60f;
        currentEditedTime = fullMin * 100f + fullSec;
        lvlTime.text = currentEditedTime.ToString("00:00") + "s";
    }

    private IEnumerator CheckSceneFullyLoaded()
    {
        while (!SceneManager.GetActiveScene().isLoaded)
        {
            yield return null;
        }

        gameMenager.LvlLoadedSuccess();
    }
}
