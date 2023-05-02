
//A Mock of DungeonRoom that returns null values, to test the DungeonMap class
class MockDungeonRoom : DungeonRoom {

    public override Loot GetLoot() {
        return null;
    }

    public override int GetX() { return 0; } 
    public override int GetY() { return 0; }
    public override int GetW() { return 0; }
    public override int GetH() { return 0; }
}