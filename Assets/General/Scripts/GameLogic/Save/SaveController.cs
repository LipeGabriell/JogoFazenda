using System;
using CI.QuickSave;
using Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.GameLogic.Menu
{
    public class SaveController : MonoBehaviour
    {
        [SerializeField] private Button button;

        [Space(10)] [SerializeField] private Button submitButton;

        [SerializeField] private TMP_InputField nickInput;

        [Space(10)] [SerializeField] private TMP_Text nick;

        [SerializeField] private TMP_Text coins;

        [SerializeField] private Sprite loadSprite;

        private string _root;
        private string _nickInput;

        public void OnEnable()
        {
            _root = $"Save{name}";

            var hasFile = QuickSaveRaw.Exists($"{_root}.json");

            if (hasFile)
            {
                var savedPlayerData = LoadPlayerData();
                nick.SetText(savedPlayerData.PlayerNick);
                coins.SetText($"{savedPlayerData.Coins} coins");
                button.gameObject.GetComponent<Image>().sprite = loadSprite;

               //button.onClick.AddListener(() => menu.LaunchGame(savedPlayerData));
            }
            else
            {
               // button.onClick.AddListener(() => menu.createChar.SetActive(true));
                button.onClick.AddListener(() =>
                    nickInput.onValueChanged.AddListener(value =>
                    {
                        _nickInput = value;
                        submitButton.gameObject.SetActive(value.Length is > 4 and < 16);
                    }));
                // button.onClick.AddListener(() =>
                //     submitButton.onClick.AddListener(() => menu.LaunchGame(NewPlayerData())));
            }
        }

        private PlayerData NewPlayerData()
        {
            var writer = QuickSaveWriter.Create(_root);
            var playerData = new PlayerData(_nickInput ?? "Guest");
            writer.Write("PlayerData", playerData).TryCommit();
            return playerData;
        }


        private PlayerData LoadPlayerData()
        {
            var reader = QuickSaveReader.Create(_root);
            reader.Reload();
            reader.TryRead("PlayerData", out PlayerData playerData);
            return playerData;
        }
    }
}