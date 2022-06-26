using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoverInfoPopup : MonoBehaviour
{
    [SerializeField] private GameObject popupCanvasObject = null;
    [SerializeField] private RectTransform popupObject = null;
    [SerializeField] private TextMeshProUGUI infoText = null;
    [SerializeField] private Vector3 offset = new Vector3(0f, 50f, 0f);
    [SerializeField] private float padding = 25f;

    private Canvas popupCanvas = null;

    private void Start()
    {
        EventManager.Instance.AddListener<OnMouseStartHoverItem>(OnMouseStartHoverItem);
        EventManager.Instance.AddListener<OnMouseEndHoverItem>(OnMouseEndHoverItem);
        popupCanvas = popupCanvasObject.GetComponent<Canvas>();
    }

    private void OnDestroy()
    {
        if(EventManager.HasInstance())
        {
            EventManager.Instance.RemoveListener<OnMouseStartHoverItem>(OnMouseStartHoverItem);
            EventManager.Instance.RemoveListener<OnMouseEndHoverItem>(OnMouseEndHoverItem);
        }
    }

    private void Update() => FollowCursor();

    public void HideInfo() => popupObject.gameObject.SetActive(false);

    private void FollowCursor()
    {
        if (!popupObject.gameObject.activeSelf) { return; }

        Vector3 newPos = Input.mousePosition + offset;
        newPos.z = 0f;

        float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + popupObject.rect.width * popupCanvas.scaleFactor / 2) - padding;
        if (rightEdgeToScreenEdgeDistance < 0)
        {
            newPos.x += rightEdgeToScreenEdgeDistance;
        }
        float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - popupObject.rect.width * popupCanvas.scaleFactor / 2) + padding;
        if (leftEdgeToScreenEdgeDistance > 0)
        {
            newPos.x += leftEdgeToScreenEdgeDistance;
        }
        float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + popupObject.rect.height * popupCanvas.scaleFactor) - padding;
        if (topEdgeToScreenEdgeDistance < 0)
        {
            newPos.y += topEdgeToScreenEdgeDistance;
        }
        popupObject.transform.position = newPos;
    }

    private void DisplayInfo(HotBarItem infoItem)
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("<size=35>").Append(infoItem.ColoredName).Append("</size>\n");
        builder.Append(infoItem.GetInfoDisplayText());

        infoText.text = builder.ToString();

        popupObject.gameObject.SetActive(true);

        LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
    }

    private void OnMouseStartHoverItem(OnMouseStartHoverItem e)
    {
        DisplayInfo(e.hotBarItem);
    }

    private void OnMouseEndHoverItem(GlobalEvent e)
    {
        HideInfo();
    }
}

