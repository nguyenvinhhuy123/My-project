using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class MainCharacterMovementController : MonoBehaviour
{
    [SerializeField] private MainCharacterData _data;
    private ReusableProperty reusableProperty = new ReusableProperty();
    // Start is called before the first frame update
    void Awake()
    {
        reusableProperty.Init(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
