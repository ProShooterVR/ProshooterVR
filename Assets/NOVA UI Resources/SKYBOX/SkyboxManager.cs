using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public Material BeachskyboxMaterial;

    public Material BarrenLandskyboxMaterial;

    public Material BlockskyboxMaterial;

    public Material CyberpunkskyboxMaterial;

    public void Start()
    {

    }

    public void OnBeachSkyboxButtonClick()
    {
        RenderSettings.skybox = BeachskyboxMaterial;

    }
    public void OnBarrenLandskyboxButtonClick()
    {
        RenderSettings.skybox = BarrenLandskyboxMaterial;

    }
    public void OnBlockskyboxButtonClick()
    {
        RenderSettings.skybox = BlockskyboxMaterial;
    }
    public void OnCyberpunkskyboxButtonClick()
    {
        RenderSettings.skybox = CyberpunkskyboxMaterial;
    }

}
