using NUnit.Framework;

namespace DungeonAdventure
{



    public class CharacterTests
    {

        [Test]
        public void ConstructorTest()
        {
            PlayerCharacter test = new PlayerCharacter("warrior", 25, 10, 5, 2, 1);
            Assert.AreEqual("Warrior", test.Name);
            Assert.AreEqual("warrior", test.CharacterClass);
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
        public void CharacterDatabaseTest()
        {
            PlayerCharacter localWarrior = new PlayerCharacter("warrior", 100, 20, 20, 10, 0);
            PlayerCharacter localBarbarian = new PlayerCharacter("barbarian", 150, 25, 0, 10, 5);
            PlayerCharacter localArcher = new PlayerCharacter("archer", 80, 20, 10, 15, 10);
            PlayerCharacter localRogue = new PlayerCharacter("rogue", 60, 30, 0, 10, 15);
            PlayerCharacter localWizard = new PlayerCharacter("wizard", 50, 15, 0, 25, 5);
            PlayerCharacter localCleric = new PlayerCharacter("cleric", 75, 10, 5, 20, 0);


            PlayerCharacter databaseWarrior = AccessDB.PlayerDatabaseConstructor("warrior");
            PlayerCharacter databaseBarbarian = AccessDB.PlayerDatabaseConstructor("barbarian");
            PlayerCharacter databaseArcher = AccessDB.PlayerDatabaseConstructor("archer");
            PlayerCharacter databaseRogue = AccessDB.PlayerDatabaseConstructor("rogue");
            PlayerCharacter databaseWizard = AccessDB.PlayerDatabaseConstructor("wizard");
            PlayerCharacter databaseCleric = AccessDB.PlayerDatabaseConstructor("cleric");

            Assert.AreEqual(localWarrior.ToString(), databaseWarrior.ToString());
            Assert.AreEqual(localBarbarian.ToString(), databaseBarbarian.ToString());
            Assert.AreEqual(localArcher.ToString(), databaseArcher.ToString());
            Assert.AreEqual(localRogue.ToString(), databaseRogue.ToString());
            Assert.AreEqual(localWizard.ToString(), databaseWizard.ToString());
            Assert.AreEqual(localCleric.ToString(), databaseCleric.ToString());
        }

        [Test]
        public void BasicAttackTests()
        {
            PlayerCharacter testWarrior = new PlayerCharacter("warrior", 100, 20, 20, 10, 0);
            EnemyCharacter testEnemy = new EnemyCharacter("goblin", 100, 20, 20, 10, 0);

            testWarrior.BasicAttack(testEnemy);

            Assert.AreEqual(true, ((100 - testEnemy.CurrentHitpoints) <= 10) && (100 - testEnemy.CurrentHitpoints > 0));

            EnemyCharacter testEnemy2 = new EnemyCharacter("goblin", 100, 20, 0, 10, 0);

            testWarrior.BasicAttack(testEnemy2);

            Assert.AreEqual(true, ((100 - testEnemy2.CurrentHitpoints) <= 30) && (100 - testEnemy2.CurrentHitpoints > 20));
        }

        [Test]
        public void BuffDatabaseTests()
        {
            PlayerCharacter testWarrior = new PlayerCharacter("warrior", 100, 20, 20, 10, 0);

            Assert.AreEqual(2, testWarrior.Buff());
            Assert.AreEqual(26, testWarrior.Attack);
        }

        [Test]
        public void SpecialAttackDatabaseTests()
        {
            PlayerCharacter testWarrior = new PlayerCharacter("warrior", 100, 20, 20, 10, 0);

            EnemyCharacter testEnemy = new EnemyCharacter("goblin", 100, 20, 20, 10, 0);

            double damage = testWarrior.SpecialAttack(testEnemy);

            Assert.AreEqual(true, damage <= 20 && damage > 10);
        }

    }
}
