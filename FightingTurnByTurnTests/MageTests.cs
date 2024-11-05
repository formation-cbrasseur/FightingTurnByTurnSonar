using FightingTurnByTurn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightingTurnByTurnTests
{
    [TestClass]
    public class MageTests
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
            playerOne.ChooseCharacter(CharacterTypes.Mage);
            character = playerOne.Characters.First();
        }

        [TestMethod]
        public void MageCharacterHaveTheRightStats()
        {
            Assert.AreEqual(5, character.Speed);
            Assert.AreEqual(6, character.Power);
            Assert.AreEqual(24, character.LifePoint);
        }
    }
}
