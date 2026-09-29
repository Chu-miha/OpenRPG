using UnityEngine;
using UnityEngine.UIElements;

public abstract class ResourceBarView
{
    protected VisualElement Fill;
    protected Label ValueLabel;

    protected ResourceBarView(
        VisualElement fill,
        Label valueLabel)
    {
        Fill = fill;
        ValueLabel = valueLabel;
    }


    public virtual void UpdateValue(int current, int max)
    {
        ValueLabel.text = $"{current}/{max}";

        float percent = (float)current / max;

        Fill.style.width = Length.Percent(percent * 100);
    }
}
