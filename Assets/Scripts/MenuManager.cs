using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public TMP_InputField userNameText;


    // Start is called before the first frame update
    void Start()
    {
        userNameText = GetComponent<TMP_InputField>();
    }

    public void StarNew()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
       instance = this;
       DontDestroyOnLoad(gameObject);
    }
}
