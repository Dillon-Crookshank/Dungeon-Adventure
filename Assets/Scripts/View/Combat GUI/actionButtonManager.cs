using UnityEngine;

namespace DungeonAdventure{
    public class actionButtonManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] myActionButtons; 
    
        void UnlockButtons(bool theAvailability){
            Debug.Log("Yep!");
            foreach (GameObject button in myActionButtons){
                button.SendMessage("SetClickable", theAvailability);
                if (theAvailability){
                    button.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }

        void EnableButtons(bool theAvailability){
            foreach (GameObject button in myActionButtons){
                button.SetActive(theAvailability);
            }
        }
    
    }   
}

