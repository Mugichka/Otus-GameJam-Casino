using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public Button rollButton;
    public TMP_Text resultText;
    public ArtifactSO[] availableArtifacts;
    public MoneyCounter moneyCounter;
    public TMP_Text moneyText;
    //public GameObject player;

    // Artifact Display UI
    public GameObject artifactPrefab;
    public Transform artifactContentParent;

    private List<ArtifactSO> playerArtifacts = new List<ArtifactSO>();

    private void Start()
    {
        rollButton.onClick.AddListener(RollForArtifact);
        resultText.text = "Press Roll to Get an Artifact!";
        moneyText.text = $"Money: {moneyCounter._totalMoney}";
    }

    private void RollForArtifact()
    {

        if (moneyCounter._totalMoney < 100)
        {
            resultText.text = "Не хватает денег на спин :(";
        }
        else
        {
            moneyCounter._totalMoney -= 100;
            moneyText.text = $"Money: {moneyCounter._totalMoney}";
            // Pick a random artifact
            ArtifactSO chosenArtifact = availableArtifacts[Random.Range(0, availableArtifacts.Length)];

            // Add the artifact buff to the player
            PlayerBuffs.Instance.OnlyAddBuff(chosenArtifact);

            // Add the artifact to the player's collection and update UI
            playerArtifacts.Add(chosenArtifact);
            DisplayArtifacts();

            // Display the result
            resultText.text = $"You got: {chosenArtifact.artifactName}!\n{chosenArtifact.description}";
        }

    }

    private void DisplayArtifacts()
    {
        // Clear previous display
        foreach (Transform child in artifactContentParent)
        {
            Destroy(child.gameObject);
        }

        // Create a UI element for each artifact in the player's collection
        foreach (ArtifactSO artifact in playerArtifacts)
        {
            GameObject artifactDisplay = Instantiate(artifactPrefab, artifactContentParent);
            TMP_Text artifactText = artifactDisplay.GetComponent<TMP_Text>();
            artifactText.text = artifact.artifactName;
        }
    }
}
