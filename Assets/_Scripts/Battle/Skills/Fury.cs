using Hiragana.Battle.Effects;
using Hiragana.Battle.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Hiragana.Battle.Enemy;

namespace Hiragana.Battle.Skills
{
    [Serializable]
    public class Fury : Skill
    {
        public override string Name => "Fury";
        public override int FocusCost => 4;
        public override SkillType Type => SkillType.Offensive;

        protected override IEnumerator Effect()
        {
            AttackMenu attackMenu = GameObject.FindObjectOfType<AttackMenu>(true);
            EnemyScreen enemies = GameObject.FindObjectOfType<EnemyScreen>();

            attackMenu.Show();
            attackMenu.keyListening = false;

            enemies.EnableSelection();
            if (enemies.selected == null) enemies.SelectEnemy(0);
            else enemies.SelectEnemy(enemies.selected); // reselect previous one

            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame(); // build bug workaroud
            yield return new WaitUntil(()
                => Input.GetKeyDown(KeyCode.Return)
                || Input.GetKeyDown(KeyCode.Escape));

            if (Input.GetKeyDown(KeyCode.Return)) // ENTER
            {
                StartCoroutine(AttackLoop(times: 5));
                yield break;
            }
            else // ESCAPE
            {
                enemies.DisableSelection(keepState: false);
                ReturnToSkillMenu();
            }
        }

        private IEnumerator AttackLoop(int times)
        {
            AttackMenu attackMenu = GameObject.FindObjectOfType<AttackMenu>(true);
            EnemyScreen enemies = GameObject.FindObjectOfType<EnemyScreen>();

            int i = 0;
            while(i < times)
            {
                bool hitResult = false;
                attackMenu.romajiText.interactable = false; // bug workaround
                attackMenu.romajiText.interactable = true;
                attackMenu.romajiText.text = "";
                attackMenu.romajiText.Select();
                yield return new WaitUntil(()
                    => Input.GetKeyUp(KeyCode.Return)
                    || Input.GetKeyUp(KeyCode.Escape)
                );
                yield return new WaitUntil(()
                    => Input.GetKeyDown(KeyCode.Return)
                    || Input.GetKeyDown(KeyCode.Escape)
                );

                if (Input.GetKeyDown(KeyCode.Return)) // ENTER
                {
                    var enemy = enemies.selected.GetComponent<Enemy>();
                    if (Enum.TryParse(attackMenu.romajiText.text.ToUpper(), false, out Romaji romaji)) // if parsing ok hit
                    {
                        hitResult = enemy.Health.Any(x => !x.damaged && x.Romaji == romaji);
                        enemy.ApplyEffect(new Damage(romaji, false));
                    }
                    if (hitResult && enemy.Alive)
                    {
                        enemy.Sprite.UpdateGUI();
                        i++;
                    }
                    else break;
                }
                else if (i == 0 && Input.GetKeyDown(KeyCode.Escape)) // can return
                {
                    StartCoroutine(Effect());
                }
            }
            enemies.selected = null;
            enemies.EnableSelection(); // bug workaround
            enemies.DisableSelection(false);
            BattlePlayer.player.Focus -= FocusCost;
            BattlePlayer.player.haveTurn = false;
        }
    }
}