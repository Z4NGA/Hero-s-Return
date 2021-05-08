using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class text_spelling : MonoBehaviour
{
    public TextMeshProUGUI dialog;
    public string text="";
    int counter = 0;
    int words_per_display = 5;
    int index = 0;
    string temp_text="";
    private string[] text_words;
    [SerializeField]private float last_write = 0;
    [SerializeField]private float cd = 0.2f;
    private void Awake()
    {
        dialog = GetComponent<TextMeshProUGUI>();

    }
    private void Start()
    {
        text_words = text.Split(' ');
        
    }
    // Update is called once per frame
    void Update()
    {
        if (index > text_words.Length) Destroy(gameObject, 2f);
        if (counter < temp_text.Length)
        {
            if(Time.time>last_write+cd){
                last_write = Time.time;
                counter++;
                dialog.SetText(temp_text.Substring(0, counter));
            }
        }
        else
        {
            counter = 0;
            temp_text = "";
            //set the temp text to display
            for (int i = index; i < index + words_per_display; i++)
            {
                temp_text += text_words[i];
                temp_text += " ";
            }
            index = index + words_per_display<=text_words.Length ? index + words_per_display:index + text_words.Length-index;
        }  
    }
}
