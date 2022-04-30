using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public GameObject[] heartContainers;
    public Image[] heartFills;
    public GameObject Player;
    public Transform heartsParent;
    public GameObject heartContainerPrefab;

    private void Start()
    {
        heartContainers = new GameObject[Player.GetComponent<PlayerInfo>().maxLife];
        heartFills = new Image[Player.GetComponent<PlayerInfo>().maxLife];
        PlayerInfo.Instance.onHealthChangedCallback += UpdateHeartsHUD;
        InstantiateHeartContainers();
        UpdateHeartsHUD();
    }

    public void UpdateHeartsHUD()
    {
        SetHeartContainers();
        SetFilledHearts();
    }

    public void SetHeartContainers()
    {
        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i < Player.GetComponent<PlayerInfo>().maxLife)
            {
                heartContainers[i].SetActive(true);
            }
            else
            {
                heartContainers[i].SetActive(false);
            }
        }
    }

    public void SetFilledHearts()
    {
        for (int i = 0; i < heartFills.Length; i++)
        {
            if (i < Player.GetComponent<PlayerInfo>().life)
            {
                heartFills[i].fillAmount = 1;
            }
            else
            {
                heartFills[i].fillAmount = 0;
            }
        }

        if (Player.GetComponent<PlayerInfo>().life % 1 != 0)
        {
            int lastPos = Mathf.FloorToInt(Player.GetComponent<PlayerInfo>().life);
            heartFills[lastPos].fillAmount = Player.GetComponent<PlayerInfo>().life % 1;
        }
    }

    public void InstantiateHeartContainers()
    {
        for (int i = 0; i < Player.GetComponent<PlayerInfo>().maxLife; i++)
        {
            GameObject temp = Instantiate(heartContainerPrefab);
            temp.transform.SetParent(heartsParent, false);
            heartContainers[i] = temp;
            heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
        }
    }
}
