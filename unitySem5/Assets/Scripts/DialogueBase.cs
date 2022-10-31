using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DialogueSystem
{
    public class DialogueBase : MonoBehaviour
    {

        protected IEnumerator OverwriteText(string input, TextMeshProUGUI textHolder, float typingDelay, AudioClip clip)
        {
            textHolder.text = "";
            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                if (char.ToLower(input[i]) == 'a' || char.ToLower(input[i]) == 'e' || char.ToLower(input[i]) == 'i' || char.ToLower(input[i]) == 'o' || char.ToLower(input[i]) == 'u')
                {
                    SoundManager.instance.PlaySound(clip);
                }
                yield return new WaitForSeconds(typingDelay);
            }
        }
    }
}