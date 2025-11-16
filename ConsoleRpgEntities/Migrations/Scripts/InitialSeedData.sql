SET IDENTITY_INSERT Players ON;
INSERT INTO Players (Id, Name, Health, Experience, EquippedJson)
VALUES
    (1, 'Sir Lancelot', 100, 0, N'{}');
SET IDENTITY_INSERT Players OFF;

-- Insert Monsters
SET IDENTITY_INSERT Monsters ON;
INSERT INTO Monsters (Id, Name, MonsterType, Health, AggressionLevel, Sneakiness)
VALUES
    (1, 'Bob Goblin', 'Goblin', 20, 10, 3);
SET IDENTITY_INSERT Monsters OFF;

-- Insert Abilities
SET IDENTITY_INSERT Abilities ON;
INSERT INTO Abilities (Id, Name, Description, AbilityType, Damage, Distance)
VALUES
    (1, 'Shove', 'Power Shove', 'ShoveAbility', 10, 5);
SET IDENTITY_INSERT Abilities OFF;

-- Link Player to Ability
INSERT INTO PlayerAbilities (PlayersId, AbilitiesId)
VALUES
    (1, 1);
