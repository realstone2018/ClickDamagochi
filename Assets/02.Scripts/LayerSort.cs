using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSort : MonoBehaviour {
    public enum Layers
    {
        UnActive,
        Active
    }

    public Layers mLayerName;
    public int mOrderNumber = 0;

    public MeshRenderer sprite;

    void Start()
    {
        // 책이랑 다른 부분  
        sprite = GetComponent<MeshRenderer>();
        sprite.sortingLayerName = mLayerName.ToString();
        sprite.sortingOrder = mOrderNumber;
        //renderer.sortingLayerName = mLayerName.ToString();
        //renderer.sortingOrder = mOrderNumber;

    }
}
