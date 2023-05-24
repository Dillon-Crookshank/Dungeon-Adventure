using UnityEngine;
using NUnit.Framework;

namespace DefaultNamespace
{



    public class CharacterTests
    {

        [Test]
        public void ConstructorTests()
        {
            PlayerCharacter test = new PlayerCharacter("warrior", 25, 10, 5, 2, 1);
            Assert.AreEqual("warrior", test.Name);
            Assert.AreEqual(25, test.MaxHitpoints);
            Assert.AreEqual(test.CurrentHitpoints, test.MaxHitpoints);
            Assert.AreEqual(10, test.Attack);
            Assert.AreEqual(5, test.Defence);
            Assert.AreEqual(2, test.MaxMana);
            Assert.AreEqual(test.CurrentMana, test.MaxMana);
            Assert.AreEqual(1, test.Initiative);
            Assert.AreEqual(0, test.CombatInitiative);
            Assert.AreEqual(true, test.IsAlive());

        }

        [Test]
        public void SetterTests()
        {
            // If ConstructorTests passes, these values are initialized correctly and
            // getters work as expected.
            PlayerCharacter test = new PlayerCharacter("warrior", 25, 10, 5, 2, 1);

            test.Name = "testname";
            Assert.AreEqual("testname", test.Name);

            test.MaxHitpoints = 10;
            Assert.AreEqual(35, test.MaxHitpoints);
            Assert.AreEqual(35, test.CurrentHitpoints);

            test.Attack = 5;
            Assert.AreEqual(15, test.Attack);

            test.Defence = 5;
            Assert.AreEqual(10, test.Defence);

            test.MaxMana = 7;
            Assert.AreEqual(9, test.MaxMana);
            Assert.AreEqual(9, test.CurrentMana);

            test.Initiative = 5;
            Assert.AreEqual(6, test.Initiative);

            test.CombatInitiative = 5;
            Assert.AreEqual(5, test.CombatInitiative);

            test.PartyPosition = 1;
            Assert.AreEqual(1, test.PartyPosition);

        }

        [Test]
        public void EdgeCaseSetterTests()
        {
            // If SetterTests passes, then these setters work as expected under "normal" cases.
            PlayerCharacter test = new PlayerCharacter("warrior", 25, 10, 5, 2, 1);

            test.PartyPosition = 1;
            Assert.AreEqual(1, test.PartyPosition);
            test.PartyPosition = AbstractParty.MAX_PARTY_SIZE + 1;
            Assert.AreEqual(1, test.PartyPosition);



        }

        [Test]
        public void DatabaseTest()
        {
            PlayerCharacter databaseTest1 = accessDB.accessCharacterDatabase("warrior");
            Debug.Log(databaseTest1);
            PlayerCharacter databaseTest2 = accessDB.accessCharacterDatabase("barbarian");
            Debug.Log(databaseTest2);
            PlayerCharacter databaseTest3 = accessDB.accessCharacterDatabase("archer");
            Debug.Log(databaseTest3);
            PlayerCharacter databaseTest4 = accessDB.accessCharacterDatabase("rogue");
            Debug.Log(databaseTest4);
            PlayerCharacter databaseTest5 = accessDB.accessCharacterDatabase("wizard");
            Debug.Log(databaseTest5);
            PlayerCharacter databaseTest6 = accessDB.accessCharacterDatabase("cleric");
            Debug.Log(databaseTest6);

        }

    }
}
