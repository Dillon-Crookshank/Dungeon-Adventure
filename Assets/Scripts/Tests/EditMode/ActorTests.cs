using NUnit.Framework;

namespace DefaultNamespace
{



    public class ActorTests
    {

        [Test]
        public void ConstructorTests()
        {
            testHero test = new testHero("warrior", 25, 10, 5, 2, 1);
            Assert.AreEqual("warrior", test.Name);
            Assert.AreEqual(25, test.maxHitpoints);
            Assert.AreEqual(test.currentHitpoints, test.maxHitpoints);
            Assert.AreEqual(10, test.attack);
            Assert.AreEqual(5, test.defence);
            Assert.AreEqual(2, test.maxMana);
            Assert.AreEqual(test.currentMana, test.maxMana);
            Assert.AreEqual(1, test.initiative);
            Assert.AreEqual(0, test.combatInitiative);
            Assert.AreEqual(true, test.IsAlive());

        }

        [Test]
        public void SetterTests()
        {
            // If ConstructorTests passes, these values are initialized correctly and
            // getters work as expected.
            testHero test = new testHero("warrior", 25, 10, 5, 2, 1);

            test.Name = "testname";
            Assert.AreEqual("testname", test.Name);

            test.maxHitpoints = 10;
            Assert.AreEqual(35, test.maxHitpoints);
            Assert.AreEqual(35, test.currentHitpoints);

            test.attack = 5;
            Assert.AreEqual(15, test.attack);

            test.defence = 5;
            Assert.AreEqual(10, test.defence);

            test.maxMana = 7;
            Assert.AreEqual(9, test.maxMana);
            Assert.AreEqual(9, test.currentMana);

            test.initiative = 5;
            Assert.AreEqual(6, test.initiative);

            test.combatInitiative = 5;
            Assert.AreEqual(5, test.combatInitiative);

            test.partyPosition = 1;
            Assert.AreEqual(1, test.partyPosition);

        }

        [Test]
        public void EdgeCaseSetterTests()
        {
            // If SetterTests passes, then these setters work as expected under "normal" cases.
            testHero test = new testHero("warrior", 25, 10, 5, 2, 1);

            test.partyPosition = 1;
            Assert.AreEqual(1, test.partyPosition);
            test.partyPosition = AbstractParty.MAX_PARTY_SIZE + 1;
            Assert.AreEqual(1, test.partyPosition);



        }

    }
}
