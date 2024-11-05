using FightingTurnByTurn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FightingTurnByTurnTests
{
    [TestClass]
    public class Z_CharacterTests
    {
        private Game game;
        private Player playerOne;
        private Player playerTwo;
        private Character character;

        [TestInitialize]
        [TestCategory("UnitTest")]
        public void InitTests()
        {
            game = new Game();
            game.Start();
            playerOne = game.Players[0];
            playerTwo = game.Players[1];
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void TurnOnePlayerOne_FirstCharacterIsTheFastest()
        {
            var playerOneFastest = playerOne.GetFastestCharacter();
            character = playerOne.Characters.OrderByDescending(player => player.Speed).First();
            Assert.AreEqual(playerOneFastest, character);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void TurnOnePlayerTwo_FirstCharacterIsTheFastest()
        {
            var playerTwoFastest = playerTwo.GetFastestCharacter();
            character = playerTwo.Characters.OrderByDescending(player => player.Speed).First();
            Assert.AreEqual(playerTwoFastest, character);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void CharacterIsDisabledAfterHisTurn()
        {
            character = playerOne.GetFastestCharacter();
            game.Turn();
            Assert.IsTrue(character.HasAlreadyAttack);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void CharacterDoDamageDuringHisTurn()
        {
            character = playerOne.GetFastestCharacter();
            character.IsOnCooldown = true;
            var previousEnemyLifePoint = playerTwo.Characters.First().LifePoint;
            character.Attack(playerTwo.Characters.First(), null);
            var actualEnemyLifePoint = playerTwo.Characters.First().LifePoint;
            Assert.IsTrue(previousEnemyLifePoint > actualEnemyLifePoint);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void CharacterCanOnlyDamageAliveCharacter()
        {
            character = playerOne.GetFastestCharacter();
            playerTwo.Characters[0].LifePoint = 0;
            Assert.ThrowsException<CannotAttackDeadCharacterException>(() => character.Attack(playerTwo.Characters.First(), null));
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void CharacterAttackFirstPlayerCharacter()
        {
            var previousEnemiesLifePoint = playerTwo.Characters.Select(charac => charac.LifePoint).ToArray();
            game.Turn();
            var actualEnemiesLifePoint = playerTwo.Characters.Select(charac => charac.LifePoint).ToArray();
            Assert.AreNotEqual(previousEnemiesLifePoint, actualEnemiesLifePoint);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void CharacterAttackTargetAliveWithLessLifePointPlayerCharacter()
        {
            playerTwo.Characters[0].LifePoint = 0;
            var target = game.GetTargetWithLessLifePoint(playerTwo);
            var previousEnemyLifePoint = target.LifePoint;
            game.Turn();
            var actualEnemyLifePoint = target.LifePoint;
            Assert.IsTrue(previousEnemyLifePoint > actualEnemyLifePoint);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void UsingSpecialAttack_WillSetNumberOfTimePlayedSinceSpecial()
        {
            var usedCharacter = game.Turn();
            Assert.AreEqual(0, usedCharacter.NumberOfTimePlayedSinceSpecial);
        }

        //[TestMethod]
        //[TestCategory("UnitTest")]
        //public void CharacterDoSpecialAttackWhenAvailable()
        //{
        //    character = playerOne.Characters[0];
        //    Assert.IsFalse(character.IsOnCooldown);
        //    character.Attack(playerTwo.Characters[0], null);
        //    Assert.IsTrue(character.IsOnCooldown);
        //}

        [TestMethod]
        [TestCategory("UnitTest")]
        public void IfWarriorCharacterIsTargetAfterUsingSpecial_DamageTakenWillBeDividedByTwo()
        {
            game.Players[0].Characters[0]= new Warrior();
            character = playerOne.Characters[0];
            character.SpecialAttack(game.Players[1].Characters[0], null);
            var previousCharacterLife = character.LifePoint;
            game.Players[1].Characters[0].IsOnCooldown = true;
            game.Players[1].Characters[0].Attack(character, null);
            var actualCharacterLife = character.LifePoint;

            Assert.IsTrue(previousCharacterLife > actualCharacterLife);
            Assert.AreEqual(previousCharacterLife - actualCharacterLife, (int) Decimal.Round((decimal) game.Players[1].Characters[0].Power / 2));
            Assert.IsFalse(character.HasShield);
        }

        //MageCanDoSpecialAttackTeam_DoDoubleDamage()
        [TestMethod]
        [TestCategory("UnitTest")]
        public void MageCanDoSpecialAttackTeam_DoDoubleDamage()
        {
            playerOne.Characters[0] = new Mage();
            playerTwo.Characters[0] = new Healer();
            character = playerOne.Characters[0];
            var previousCharacterLife = playerTwo.Characters[0].LifePoint;
            character.SpecialAttack(playerTwo.Characters[0], null);
            var actualCharacterLife = playerTwo.Characters[0].LifePoint;

            Assert.IsTrue(previousCharacterLife > actualCharacterLife);
            Assert.AreEqual(previousCharacterLife - actualCharacterLife, character.Power * 2);
            Assert.IsFalse(character.HasShield);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        public void HealerCanDoSpecialAttack_WillHealLowerLifePointTargetInOwnTeam()
        {
            character = playerOne.Characters[0] = new Healer();
            var lowestLifePointCharacterInOwnTeam = playerOne.Characters.OrderBy(charac => charac.LifePoint).First();
            var previousLowestLifePoint = lowestLifePointCharacterInOwnTeam.LifePoint;
            character.SpecialAttack(playerTwo.Characters[0], lowestLifePointCharacterInOwnTeam);
            var afterHealLowestLifePoint = lowestLifePointCharacterInOwnTeam.LifePoint;

            Assert.IsTrue(previousLowestLifePoint < afterHealLowestLifePoint);
        }
    }
}