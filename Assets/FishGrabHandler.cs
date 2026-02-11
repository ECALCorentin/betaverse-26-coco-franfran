using UnityEngine;
using Oculus.Interaction; // Requis pour les composants du Building Block

public class FishGrabHandler : MonoBehaviour
{
    [Header("Données")]
    [SerializeField] private FishData fishData; 

    // On cible le composant Grabbable qui est généré par le Building Block
    private Grabbable _grabbable;

    void Start()
    {
        // On cherche le composant Grabbable sur cet objet ou ses enfants
        _grabbable = GetComponentInChildren<Grabbable>();

        if (_grabbable == null)
        {
            Debug.LogError("Grabbable introuvable ! Assure-toi que le Building Block a fini sa configuration.");
            return;
        }

        // On s'abonne à l'événement de saisie
        _grabbable.WhenPointerEventRaised += HandleGrabEvent;
    }

    void OnDestroy()
    {
        if (_grabbable != null)
            _grabbable.WhenPointerEventRaised -= HandleGrabEvent;
    }

    private void HandleGrabEvent(PointerEvent evt)
    {
        // PointerEventType.Select correspond au moment où l'objet est saisi
        if (evt.Type == PointerEventType.Select)
        {
            TriggerFishCheck();
        }
    }

    private void TriggerFishCheck()
    {
        if (fishData != null)
        {
            string hexColor = ColorUtility.ToHtmlStringRGB(fishData.rarityColor);
            Debug.Log($"<b><color=#{hexColor}>[FISH GRABBED]</color></b> Name: {fishData.fishName} | Rarity: {fishData.rarity}");
        }
    }
}