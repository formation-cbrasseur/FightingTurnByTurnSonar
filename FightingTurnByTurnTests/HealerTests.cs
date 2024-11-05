using FightingTurnByTurn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightingTurnByTurnTests
{
    [TestClass]
    public class HealerTests
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
            playerOne.ChooseCharacter(CharacterTypes.Healer);
            character = playerOne.Characters.First();
        }

        [TestMethod]
        public void HealerCharacterHaveTheRightStats()
        {
            Assert.AreEqual(4, character.Speed);
            Assert.AreEqual(3, character.Power);
            Assert.AreEqual(20, character.LifePoint);
        }
    }
}
