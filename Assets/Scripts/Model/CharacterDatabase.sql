-- A file used for storing templates for SQLite database entries, as well as former entries into the database.

-- CREATE TABLE characterTemplates(
-- class TEXT NOT NULL PRIMARY KEY,
-- hitpoints REAL NOT NULL,
-- attack REAL NOT NULL,
-- defence REAL NOT NULL,
-- mana REAL NOT NULL,
-- initiative INTEGER NOT NULL);

-- INSERT INTO characterTemplates(class, hitpoints, attack, defence, mana, initiative)
-- VALUES ("warrior", 100, 20, 20, 10, 0);
-- VALUES ("barbarian", 150, 25, 0, 10, 5);
-- VALUES ("archer", 80, 20, 10, 15, 10);
-- VALUES ("rogue", 60, 30, 0, 10, 15);
-- VALUES ("wizard", 50, 15, 0, 25, 5);
-- VALUES ("cleric", 75, 10, 5, 20, 0);

-- DELETE FROM actorTemplates WHERE name="Warrior";

-- CREATE TABLE enemyTemplates(
-- class TEXT NOT NULL PRIMARY KEY,
-- hitpoints REAL NOT NULL,
-- attack REAL NOT NULL,
-- defence REAL NOT NULL,
-- mana REAL NOT NULL,
-- initiative INTEGER NOT NULL);

--INSERT INTO enemyTemplates(class, hitpoints, attack, defence, mana, initiative)
-- VALUES ("skeleton", 50, 10, 10, 10, 5);
-- VALUES ("zombie", 100, 5, 5, 10, 0);
-- VALUES ("goblin", 30, 10, 5, 10, 10);
-- VALUES ("orc", 120, 15, 10, 10, 5);
-- VALUES ("skeleton archer", 40, 15, 5, 10, 5);
-- VALUES ("necromancer", 60, 5, 5, 25, 5);
-- VALUES ("goblin archer", 40, 10, 0, 10, 10);

-- CREATE TABLE buffTemplates(
-- class TEXT NOT NULL PRIMARY KEY,
-- buffName TEXT NOT NULL,
-- duration INTEGER NOT NULL,
-- statModified TEXT NOT NULL,
-- manaCost INTEGER NOT NULL,
-- percentage REAL NOT NULL);

-- INSERT INTO buffTemplates(class, buffName, duration, statModified, manaCost, percentage)
-- VALUES ("cleric", "prayer", 2, "defence", 5, 0.30);