using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Material material1;
    public Material material2;

    public Texture2D map;
    public ColorToPrefab[] colorMappings;
    public float offset = 5f;

    private void GenerateTile(int x, int z)
    {
        Color pixelColor = map.GetPixel(x, z);

        if (pixelColor.a == 0)
        {
            return;
        }

        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector3 position = new Vector3(x * offset, 0, z * offset);
                Instantiate(colorMapping.prefab, position, Quaternion.identity);
            }
        }
    }

    public void GenerateLabirynth()
    {
        for(int x = 0; x < map.width; x++)
        {
            for(int z = 0; z < map.height; z++)
            {
                GenerateTile(x, z);
            }
        }


    }

    public void ColorTheChildren()
    {
        foreach(Transform child in transform)
        {
            if(child.tag == "Wall")
            {
                if(Random.Range(1, 100) % 3 == 0)
                {
                    child.gameObject.GetComponent<Renderer>().material = material1;
                }
                else
                {
                    child.gameObject.GetComponent<Renderer>().material = material2;
                }
            }

            
        }
    }
}
