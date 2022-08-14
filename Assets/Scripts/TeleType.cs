using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeleType : MonoBehaviour
{
    private TextMeshProUGUI m_textMeshPro;
    IEnumerator Start()
        {

            // Force and update of the mesh to get valid information.
            m_textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
            m_textMeshPro.ForceMeshUpdate();
            Debug.Log(m_textMeshPro.text);

            int totalVisibleCharacters = m_textMeshPro.textInfo.characterCount; // Get # of Visible Character in text object
            int counter = 0;
            int visibleCount = 0;

            while (true)
            {
                visibleCount = counter % (totalVisibleCharacters + 1);

                m_textMeshPro.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

                // Once the last character has been revealed, stop.
                if (visibleCount >= totalVisibleCharacters)
                {
                    yield break;
                }

                counter += 1;

                yield return new WaitForSeconds(0.05f);
            }
        }

}
