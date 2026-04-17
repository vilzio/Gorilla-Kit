using UnityEngine;

public class Incrementor : MonoBehaviour
{
    public int i;
    public TextMesh textMesh;
    
    public void Increment(int amount)
    {
        i += amount;
        textMesh.text = i.ToString();
    }
}
