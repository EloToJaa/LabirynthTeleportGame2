using UnityEngine;

public class Lock : MonoBehaviour
{
    public Door[] doors;
    public KeyColor myColor;
    private bool canOpen = false;
    private bool locked = false;
    private Animator key;

    private void Start()
    {
        key = GetComponent<Animator>();
        Debug.Log("Key: " + key.GetBool("useKey"));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            canOpen = true;
            Debug.Log("Can open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            canOpen = false;
            Debug.Log("Can't open");
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canOpen && !locked)
        {
            key.SetBool("useKey", CheckTheKey());
            if(key.GetBool("useKey"))
            {
                UseKey();
            }
        }
    }

    public bool CheckTheKey()
    {
        if (GameManager.instance.keys[(int)KeyColor.Red] > 0 && myColor == KeyColor.Red)
        {
            GameManager.instance.keys[(int)KeyColor.Red]--;
            locked = true;
            return true;
        }
        else if (GameManager.instance.keys[(int)KeyColor.Green] > 0 && myColor == KeyColor.Green)
        {
            GameManager.instance.keys[(int)KeyColor.Green]--;
            locked = true;
            return true;
        }
        else if (GameManager.instance.keys[(int)KeyColor.Gold] > 0 && myColor == KeyColor.Gold)
        {
            GameManager.instance.keys[(int)KeyColor.Gold]--;
            locked = true;
            return true;
        }
        else
        {
            Debug.Log("No key");
            return false;
        }
    }

    public void UseKey()
    {
        Debug.Log("Using key");
        foreach (var door in doors)
        {
            door.Open();
        }
    }
}
