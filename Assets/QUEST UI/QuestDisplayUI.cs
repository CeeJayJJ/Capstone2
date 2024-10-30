using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestDisplayUI : MonoBehaviour
{
    public TextMeshProUGUI questTitleText;
    public TextMeshProUGUI questDescriptionText;
    public Image questIcon;
    public TextMeshProUGUI questStatusText;

    public void SetQuestInfo(QuestData quest)
    {
        questTitleText.text = quest.questTitle;
        questDescriptionText.text = quest.questDescription;
        questIcon.sprite = quest.icon;
        questStatusText.text = quest.status.ToString();
    }
}
