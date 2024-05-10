using UnityEngine;

public class Lock : MonoBehaviour
{
    public Door[] doors;
    public KeyColor myColor;
    public Renderer myLock;

    public Material red;
    public Material green;
    public Material gold;

    private bool canOpen = false;
    private bool locked = false;
    private Animator key;

    private void Start()
    {
        SetMyColor();
        key = GetComponent<Animator>();
        Debug.Log("Key: " + key.GetBool("useKey"));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            canOpen = true;
            if(!locked)
            {
                GameManager.instance.SetUseInfo("Press E to open lock");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            canOpen = false;
            GameManager.instance.SetUseInfo("");
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
            GameManager.instance.redKeyText.text = GameManager.instance.keys[(int)KeyColor.Red].ToString();
            locked = true;
            return true;
        }
        else if (GameManager.instance.keys[(int)KeyColor.Green] > 0 && myColor == KeyColor.Green)
        {
            GameManager.instance.keys[(int)KeyColor.Green]--;
            GameManager.instance.greenKeyText.text = GameManager.instance.keys[(int)KeyColor.Green].ToString();
            locked = true;
            return true;
        }
        else if (GameManager.instance.keys[(int)KeyColor.Gold] > 0 && myColor == KeyColor.Gold)
        {
            GameManager.instance.keys[(int)KeyColor.Gold]--;
            GameManager.instance.goldKeyText.text = GameManager.instance.keys[(int)KeyColor.Gold].ToString();
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

    private void SetMyColor()
    {
        switch (myColor)
        {
            case KeyColor.Red:
                myLock.material = red;
                break;
            case KeyColor.Green:
                myLock.material = green;
                break;
            case KeyColor.Gold:
                myLock.material = gold;
                break;
        }
    }
}
