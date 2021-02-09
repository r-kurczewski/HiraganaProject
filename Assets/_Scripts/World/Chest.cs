using BayatGames.SaveGameFree;
using Hiragana.Other;
using Hiragana.World.UI;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Hiragana.Battle.BattleItem;

namespace Hiragana.World
{
    public class Chest : SaveObject
    {
        [SerializeField] private Sprite openSprite = default;
        [SerializeField] private Sprite closeSprite = default;

        public List<ItemQuantity<Item>> loot = new List<ItemQuantity<Item>>();

        [SerializeField] private bool trigger;

        [SerializeField] private bool opened = false;

        private string ChestID
        {
            get
            {
                var sceneID = SceneManager.GetActiveScene().buildIndex;
                var uniqueID = $"C_{sceneID}_{(int)transform.position.x}x{(int)transform.position.y}";
                return uniqueID;
            }
        }

        private void Start()
        {
            Load();
            if (opened) GetComponent<SpriteRenderer>().sprite = openSprite;
        }

        private void OnTriggerEnter2D()
        {
            trigger = true;
        }

        private void OnTriggerExit2D()
        {
            trigger = false;
        }

        private void OnDestroy()
        {
            Save();
        }

        public override void Save()
        {
            SaveGame.Save(ChestID, opened);
        }

        public override void Load()
        {
            if (SaveGame.Exists(ChestID)) opened = SaveGame.Load<bool>(ChestID);
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (trigger && Input.GetKeyDown(KeyCode.R))
            {
                opened = false;
                GetComponent<SpriteRenderer>().sprite = closeSprite;
            }
#endif
            if (Input.GetKeyDown(KeyCode.Return) && trigger && !opened)
            {
                opened = true;
                StringBuilder sb = new StringBuilder();
                foreach (var iQuantity in loot)
                {
                    sb.Append($"{iQuantity.quantity}x {iQuantity.item.name}\n");
                    iQuantity.item.AddToInventory(iQuantity.quantity);
                }
                WorldLog.log.ShowMessage(sb.ToString());

                GetComponent<SpriteRenderer>().sprite = openSprite;
            }
        }
    }
}