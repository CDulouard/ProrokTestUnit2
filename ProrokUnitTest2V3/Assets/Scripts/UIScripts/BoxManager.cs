using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class BoxManager : MonoBehaviour
    {
        public void HideContent(GameObject box)
        {
            box.SetActive(false);

        }
        
        public void ShowContent(GameObject box)
        {
            box.SetActive(true);
    
        }
        
        public static void HideContentGeneric(GameObject box)
        {
            box.SetActive(false);
            
        }
        
    }
}
