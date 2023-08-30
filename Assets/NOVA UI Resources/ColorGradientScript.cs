using UnityEngine;
using TMPro;

public class ColorGradientScript : MonoBehaviour
{
    public Gradient gradient;
    public GradientMode gradientMode = GradientMode.Horizontal;
    public TextMeshProUGUI textMeshPro;

    private void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        ApplyGradient();
    }

    private void ApplyGradient()
    {
        TMP_TextInfo textInfo = textMeshPro.textInfo;

        int characterCount = textInfo.characterCount;
        if (characterCount == 0) return;

        Color32[] newVertexColors;

        switch (gradientMode)
        {
            case GradientMode.Horizontal:
                ApplyHorizontalGradient(textInfo, characterCount);
                break;

            case GradientMode.Vertical:
                ApplyVerticalGradient(textInfo, characterCount);
                break;
        }

        // Update the textmesh with the new vertex colors
        textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    }

    private void ApplyHorizontalGradient(TMP_TextInfo textInfo, int characterCount)
    {
        int currentLine = 0;
        int currentLineStart = 0;

        for (int i = 0; i < characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

            if (charInfo.isVisible)
            {
                int lineNumber = charInfo.lineNumber;
                if (currentLine != lineNumber)
                {
                    currentLine = lineNumber;
                    currentLineStart = i;
                }

                float gradientPercentage = (float)(i - currentLineStart) / (float)(currentLineStart + textInfo.lineInfo[currentLine].characterCount);

                Color32 gradientColor = gradient.Evaluate(gradientPercentage);
                ModifyCharacterVertexColors(textMeshPro, charInfo.vertexIndex, gradientColor);
            }
        }
    }

    private void ApplyVerticalGradient(TMP_TextInfo textInfo, int characterCount)
    {
        for (int i = 0; i < characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

            if (charInfo.isVisible)
            {
                float gradientPercentage = (float)i / (float)characterCount;

                Color32 gradientColor = gradient.Evaluate(gradientPercentage);
                ModifyCharacterVertexColors(textMeshPro, charInfo.vertexIndex, gradientColor);
            }
        }
    }

    private void ModifyCharacterVertexColors(TextMeshProUGUI textMeshPro, int vertexIndex, Color32 color)
    {
        TMP_TextInfo textInfo = textMeshPro.textInfo;
        int materialIndex = textInfo.characterInfo[vertexIndex].materialReferenceIndex;
        int vertexIndexInMaterial = vertexIndex - textInfo.meshInfo[materialIndex].vertexCount * materialIndex;

        TMP_MeshInfo meshInfo = textMeshPro.textInfo.meshInfo[materialIndex];
        Color32[] vertexColors = meshInfo.colors32;

        vertexColors[vertexIndexInMaterial + 0] = color;
        vertexColors[vertexIndexInMaterial + 1] = color;
        vertexColors[vertexIndexInMaterial + 2] = color;
        vertexColors[vertexIndexInMaterial + 3] = color;
    }
}

public enum GradientMode
{
    Horizontal,
    Vertical
}
