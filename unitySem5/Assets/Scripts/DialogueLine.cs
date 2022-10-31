using System.Collections;
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

        private void Awake()
        {
            textHolder = GetComponent<TextMeshProUGUI>();

            StartCoroutine(OverwriteText(input, textHolder, typingDelay, clip));
            imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true;
        }

        public void OverwriteDialogue(string newInput, float newTypingDelay, AudioClip newClip, Sprite newCharacterSprite)
        {
            StartCoroutine(OverwriteText(newInput, textHolder, newTypingDelay, newClip));
            imageHolder.sprite = newCharacterSprite;
        }
    }

}