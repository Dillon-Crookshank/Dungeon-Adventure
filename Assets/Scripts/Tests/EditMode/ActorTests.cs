using NUnit.Framework;
using UnityEngine;

namespace DefaultNamespace
{



    public class ActorTests
    {

        [Test]
        public void ConstructorTests()
        {
            testHero test = new testHero("warrior", 25, 10, 5, 2, 1);
            Assert.AreEqual("warrior", test.GetName());
            Assert.AreEqual(25, test.GetMaxHitpoints());
            Assert.AreEqual(test.GetCurrentHitpoints(), test.GetMaxHitpoints());
            Assert.AreEqual(10, test.GetAttack());
            Assert.AreEqual(5, test.GetDefence());
            Assert.AreEqual(2, test.GetMaxMana());
            Assert.AreEqual(test.GetCurrentMana(), test.GetMaxMana());
            Assert.AreEqual(1, test.GetInitiative());
            Assert.AreEqual(true, test.IsAlive());

        }

        [Test]
        public void SetterTests()
        {
            // If ConstructorTests passes, these values are initialized correctly and
            // getters work as expected.
            testHero test = new testHero("warrior", 25, 10, 5, 2, 1);

            test.SetName("testname");
            Assert.AreEqual("testname", test.GetName());

            test.SetMaxHitpoints(10);
            Assert.AreEqual(35, test.GetMaxHitpoints());
            Assert.AreEqual(35, test.GetCurrentHitpoints());

            test.SetAttack(5);
            Assert.AreEqual(15, test.GetAttack());

            test.SetDefence(5);
            Assert.AreEqual(10, test.GetDefence());

            test.SetMaxMana(7);
            Assert.AreEqual(9, test.GetMaxMana());
            Assert.AreEqual(9, test.GetCurrentMana());

            test.SetInitiative(5);
            Assert.AreEqual(6, test.GetInitiative());


            test.SetAlive(false);
            Assert.AreEqual(false, test.IsAlive());

        }

    }
}