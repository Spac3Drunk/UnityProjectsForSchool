using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBase 
    {
        private TextMeshProUGUI textHolder;
        [SerializeField] private string input;
        [SerializeField] private float typingDelay;
        [SerializeField] private AudioClip clip;
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private Image imageHolder;

        private Coroutine speakingCoroutine;

        private class array_entry
            {
                public string currentDialogue = "";
                public float typingDelay = 0.05f;
                public string audioClipPath = "";
                public string spritePath = "";
            }

        private void Awake()
        {
            textHolder = GetComponent<TextMeshProUGUI>();

            //StartCoroutine(OverwriteText(input, textHolder, typingDelay, clip));
            //imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true;

            setDiscussion("Assets/Resources/Dialogues/Discussion1.txt");
        }

        public void OverwriteDialogue(string newInput, float newTypingDelay, AudioClip newClip, Sprite newCharacterSprite)
        {
            speakingCoroutine = StartCoroutine(OverwriteText(newInput, textHolder, newTypingDelay, newClip));
            imageHolder.sprite = newCharacterSprite;
        }

        public void setDiscussion(string path_Of_Txt) // "Assets/Resources/Dialogues/."
        {
            string[] txtNotParseYet = File.ReadAllLines(path_Of_Txt);
            List<array_entry> txtParsed = new List<array_entry>();
            int tmp = 0;
            //just parse the txt into a list
            for (int i = 0; i < txtNotParseYet.Length; i++)
            {
                //Debug.Log(i);
                switch (tmp)
                {
                    case <= 0:
                        txtParsed.Add(new array_entry());
                        break;
                    case <= 1:
                        txtParsed[txtParsed.Count-1].currentDialogue = txtNotParseYet[i];
                        //Debug.Log(txtParsed[txtParsed.Count-1].currentDialogue);
                        break;
                    case <= 2:
                        //Debug.Log(txtNotParseYet[i]);
                        txtParsed[txtParsed.Count-1].typingDelay = float.Parse(txtNotParseYet[i]);
                        break;
                    case <= 3:
                        txtParsed[txtParsed.Count-1].audioClipPath = txtNotParseYet[i];
                        break;
                    case <= 4:
                        txtParsed[txtParsed.Count-1].spritePath = txtNotParseYet[i];
                        tmp = -1;
                        break;
                    default:
                        break;
                }
                tmp++;
            }
            StartCoroutine(readDiscussion(txtParsed));
        }

        private IEnumerator readDiscussion(List<array_entry> txtParsed)
        {
            GameState.dialogue_On();
            for (int i = 0; i < txtParsed.Count; i++)
            {
                var audioClip = Resources.Load<AudioClip>(txtParsed[i].audioClipPath);
                var sprite = Resources.Load<Sprite>(txtParsed[i].spritePath);
                OverwriteDialogue(txtParsed[i].currentDialogue, txtParsed[i].typingDelay, audioClip, sprite);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.RightArrow));
                yield return new WaitForSeconds(0.15f);
                StopCoroutine(speakingCoroutine);
            }
            GameState.dialogue_Off();
        }


        /* Json not working don't touch
        [System.Serializable] private struct JSON_entry
        {
            public class array_entry
            {
                public string jsonDialogue;
                public float typingDelay;
                public string audioClipPath;
                public string spritePath;
            }

            public array_entry[] json_entry;
        }

        public void setDiscussion(string path_Of_Json) // "Assets/Resources/Dialogues/."
        {
            string myJsonString = File.ReadAllText(path_Of_Json);
            Debug.Log(myJsonString);
            JSON_entry theJson = JsonUtility.FromJson<JSON_entry>(myJsonString);
            Debug.Log(theJson);
            for (int i = 0; i < theJson.json_entry.Length; i++)
            {
                Debug.Log(i);
            }
        }
        */
    }

}