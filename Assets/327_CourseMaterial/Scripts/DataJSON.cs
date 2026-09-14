using UnityEngine;
// IMDM327 Material
// JsonUtility reads one serializable object, so the JSON file has a "bodies" array.
[System.Serializable]
public class SolarData
{
    public SolarBody[] bodies;
}

[System.Serializable]
public class SolarBody
{
    public string name;
    public string type;
    public float mass;
    public float orbitAU;
    public float distance;
    public float initial_velocity;
}

public class DataJSON : MonoBehaviour
{
    // See the loaded values in the Inspector while the game is running.
    public SolarBody[] bodies;

    // This has the same three values (and the same mass scale) as DataCSV.bp.
    public BodyProperty[] bp;

    void Start()
    {
        // Loads Assets/Resources/JSON/solar.json. Do not include the .json extension.
        // The JSON is in its own folder so it does not share a name with solar.csv.
        TextAsset json = Resources.Load<TextAsset>("JSON/solar");

        if (json == null)
        {
            Debug.LogError("Resources/JSON/solar.json not found.");
            bodies = new SolarBody[0];
            bp = new BodyProperty[0];
            return;
        }

        // Turn the JSON text into a SolarData object, then get its bodies array.
        SolarData solarData = JsonUtility.FromJson<SolarData>(json.text);
        bodies = solarData.bodies;

        // Copy the three simulation values into the same format used by DataCSV.
        bp = new BodyProperty[bodies.Length];
        for (int i = 0; i < bodies.Length; i++)
        {
            bp[i].mass = bodies[i].mass * 1e22f;
            bp[i].distance = bodies[i].distance;
            bp[i].initial_velocity = bodies[i].initial_velocity;
        }
    }
}
