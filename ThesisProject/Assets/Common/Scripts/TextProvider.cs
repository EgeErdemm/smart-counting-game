using TMPro;
using UnityEngine;

public class TextProvider : ITextProvider
{

    private readonly TMP_Text _text;

    public TextProvider(GameObject obj)
    {
        _text = obj.GetComponentInChildren<TextMeshPro>() ??(TMP_Text)obj.GetComponentInChildren<TextMeshProUGUI>();

    }

    public void SetText(string value)
    {
        if (_text != null) _text.text = value;
    }

    public string GetText()
    {
        return _text != null ? _text.text : null;
    }

}
