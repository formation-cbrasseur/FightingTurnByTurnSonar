using FightingTurnByTurn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FightingTurnByTurnTests
{
    [TestClass]
    public class WarriorTests
    {
        private Game game;
        private Player playerOne;
        private Character character;

        [TestInitialize]
        public void InitTests()
        {
            game = new Game();
            game.AddPlayer("David");
            game.AddPlayer("Divad");
            playerOne = game.Players.First();
            playerOne.ChooseCharacter(CharacterTypes.Warrior);
            character = playerOne.Characters.First();
        }

        [TestMethod]
        public void WarriorCharacterHaveTheRightStats()
        {
            Assert.AreEqual(6, character.Speed);
            Assert.AreEqual(5, character.Power);
            Assert.AreEqual(30, character.LifePoint);
        }
    }
}
