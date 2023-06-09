using UnityEngine;
using NUnit.Framework;

/// <summary>
/// A testing class that tests the DungeonRoom class.
/// </summary>
public class DungeonRoomTests {
    [Test]
    /// <summary>
    /// Make sure that the immutable fields of DungeonRoom that are initialized through the constructor are set properly.
    /// </summary>
    public void ConstructorTest() {
        //This should create a 1 x 1 room located at (1.5f, 1.5f)
        DungeonRoom testRoom = new DungeonRoom(1, 1, 1, 1);

        Assert.AreEqual(1.5f, testRoom.GetX());
        Assert.AreEqual(1.5f, testRoom.GetY());
        Assert.AreEqual(1, testRoom.GetW());
        Assert.AreEqual(1, testRoom.GetH());
    }

    [Test]
    /// <summary>
    /// Make sure that the mutable fields of DungeonRoom have functional accessors and mutators.
    /// </summary>
    public void FieldTest() {
        DungeonRoom testRoom = new DungeonRoom(1, 1, 1, 1);

        testRoom.SetEnemyFlag(true);
        testRoom.SetID(0);
        testRoom.SetSeenFlag(true);

        Assert.AreEqual(true, testRoom.GetEnemyFlag());
        Assert.AreEqual(0, testRoom.GetID());
        Assert.AreEqual(true, testRoom.GetSeenFlag());
    }
}