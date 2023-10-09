using UnityEngine;
using TMPro;

public class TwoColorGradientText : MonoBehaviour
{
    public TextMeshPro textMeshPro;

    public Color startColor = Color.red;
    public Color endColor = Color.green;

    private void Start()
    {
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro reference not set!");
            return;
        }

        // Create a new gradient using your custom start and end colors
        VertexGradient gradient = new VertexGradient(startColor, startColor, endColor, endColor);

        // Apply the gradient to the TextMeshPro component
        textMeshPro.colorGradient = gradient;

        // Manually update the vertex colors of the mesh to achieve a two-color gradient
        UpdateVertexColors();
    }

    private void UpdateVertexColors()
    {
        // Get the TextMeshPro mesh and its vertex colors
        TMP_MeshInfo[] meshInfo = textMeshPro.textInfo.CopyMeshInfoVertexData();

        // Calculate the gradient colors for each vertex
        for (int i = 0; i < meshInfo.Length; i++)
        {
            TMP_MeshInfo info = meshInfo[i];
            int vertexCount = info.vertices.Length;

            for (int j = 0; j < vertexCount; j++)
            {
                // Calculate the lerp value based on vertex position
                float lerpValue = (float)j / (vertexCount - 1);

                // Interpolate between startColor and endColor
                Color lerpedColor = Color.Lerp(startColor, endColor, lerpValue);

                // Update the vertex color
                info.colors32[j] = lerpedColor;
            }

            // Update the TextMeshPro mesh with the modified vertex colors
            textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }
    }
}
