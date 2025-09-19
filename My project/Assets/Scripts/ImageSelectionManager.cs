using UnityEngine;
using System.Collections.Generic;

public class ImageSelectionManager : MonoBehaviour
{
    public static ImageSelectionManager Instance { get; private set; }
    private List<SelectableImage> allImages = new List<SelectableImage>();
    private SelectableImage currentSelected;

    void Awake()
    {
        Instance = this;
    }

    public void RegisterImage(SelectableImage img)
    {
        if (!allImages.Contains(img))
            allImages.Add(img);
    }

    public void SelectImage(SelectableImage selected)
    {
        // ȡ��֮ǰ��
        if (currentSelected != null && currentSelected != selected)
            currentSelected.SetSelected(false);

        currentSelected = selected;
        currentSelected.SetSelected(true);
    }
}
