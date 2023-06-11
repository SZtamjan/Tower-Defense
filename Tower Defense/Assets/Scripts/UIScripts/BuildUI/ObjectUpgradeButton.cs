using UnityEngine;

public class ObjectUpgradeButton : MonoBehaviour
{
    public Color buttonCol;
    private Color originalCol;
    private Renderer rend;
    
    #region ButtonColor

    private void Start()
    {
        rend = GetComponent<Renderer>();
        originalCol = rend.material.color;
    }
    
    public void OnMouseEnter()
    {
        rend.material.color = buttonCol;
    }

    public void OnMouseExit()
    {
        rend.material.color = originalCol;
    }
    
    #endregion

    public void OnMouseDown()
    {
        GameManager.Instance.GetComponent<BuildTower>().UpgradeTower();
    }
}
