using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestObject : MonoBehaviour
{

    private bool inTrigger = false;

    public List<int> availaQuestIDs = new List<int>();
    public List<int> receivableQuestIDs = new List<int>();

    public GameObject QuestMarker;
    public Image theImage;

    public Sprite questAvailableSprite;
    public Sprite questReceivableSprite;
    // Start is called before the first frame update
    void Start()
    {
        SetQuestMaker();
    }

    // Update is called once per frame
    void Update()
    {
        if (QuestUIManager.uiManager.questPanelActive) 
        { 
              if (inTrigger && Input.GetKeyDown(KeyCode.Space))
                {
                   //QuestsManager.questsManager.QuestRequest(this);
                   QuestUIManager.uiManager.CheckQuest(this);
                }
        }
    }

   public void SetQuestMaker()
    {
        if (QuestsManager.questsManager.CheckCompletedQuest(this))
        {
            QuestMarker.SetActive(true);
            theImage.sprite = questReceivableSprite;
            theImage.color = Color.red;
        }
        else if (QuestsManager.questsManager.CheckAvailableQuest(this))
        {
            QuestMarker.SetActive(true);
            theImage.sprite = questAvailableSprite;
            theImage.color = Color.red;
        }
        else if (QuestsManager.questsManager.CheckAcceptedQuest(this))
        {
            QuestMarker.SetActive(true);
            theImage.sprite = questReceivableSprite;
            theImage.color = Color.blue;
        }
        else
        {
            QuestMarker.SetActive(false);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Player")
        {
            inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inTrigger = false;
        }
    }

}
