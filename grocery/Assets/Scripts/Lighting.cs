using UnityEngine;

[ExecuteInEditMode]
public class Lighting : MonoBehaviour
{
    public Light directionalLight;
    public Color lightColor = Color.white;
    public float lightIntensity = 1.0f;
    public float shadowStrength = 1.0f;
    public Color ambientLightColor = Color.gray;

    void Start()
    {
        if (directionalLight == null)
        {
            directionalLight = RenderSettings.sun;
        }

        if (directionalLight != null)
        {
            // Configure the directional light
            directionalLight.color = lightColor;
            directionalLight.intensity = lightIntensity;
            directionalLight.shadows = LightShadows.Soft;
            directionalLight.shadowStrength = shadowStrength;
        }

        // Set ambient light
        RenderSettings.ambientLight = ambientLightColor;

        // Optionally, set other lighting settings
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientIntensity = 1.0f;

        // Optionally, set skybox material (if you have a skybox material)
        // RenderSettings.skybox = yourSkyboxMaterial;
    }

    void Update()
    {
        // Update lighting settings in edit mode
        if (directionalLight != null)
        {
            directionalLight.color = lightColor;
            directionalLight.intensity = lightIntensity;
            directionalLight.shadowStrength = shadowStrength;
        }

        RenderSettings.ambientLight = ambientLightColor;
    }
}
