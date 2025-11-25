SET IDENTITY_INSERT Monsters ON;

INSERT INTO Monsters (Id, Name, Difficulty, MonsterType, Health, StunStack)
VALUES
(201, 'Goblin Grunt', 1,'Goblin', 100, 0),
(202, 'Goblin Sneak', 1, 'Goblin', 100, 0),
(203, 'Goblin Cutthroat', 2,'Goblin', 200, 0),
(204, 'Goblin Scout', 2, 'Goblin', 200, 0),
(205, 'Goblin Bruiser', 3,'Goblin', 300, 0),
(206, 'Goblin Raider', 3, 'Goblin', 300, 0),
(207, 'Goblin Slinger', 2, 'Goblin', 200, 0),
(208, 'Goblin Firestarter', 4, 'Goblin', 400, 0),
(209, 'Goblin Shadowblade', 4, 'Goblin', 400, 0),
(210, 'Goblin Berserker', 5, 'Goblin', 500, 0),
(211, 'Goblin Bonecrusher', 5, 'Goblin', 500, 0),
(212, 'Goblin Poisonblade', 3, 'Goblin', 300, 0),
(213, 'Goblin Stormcaller', 4, 'Goblin', 400, 0);

SET IDENTITY_INSERT Monsters OFF;



SET IDENTITY_INSERT Abilities ON;

INSERT INTO Abilities (Id, Name, Description, AbilityType, ManaCost, BuffDuration, TurnUsed)
VALUES
(1, 'Arcane Barrage', 'Blasts a foe with high magic damage based on intelligence', 'Arcane', 20, 0, 0),
(2, 'Nature''s Embrace', 'Initial heal with lingering heal over 3 turns', 'Healing', 12, 3, 0),
(3, 'Shadow Veil', 'A swift attack that deals damage and has a chance to stun based on agility', 'Physical', 8, 1, 0),
(4, 'Nullyfying Aegis', 'Protects against next enemy attack and deflects it back', 'Defensive', 10, 1, 0),
(5, 'Siphoning Strike', 'Strike enemy and heal for damage done', 'Hybrid', 8, 0, 0);

SET IDENTITY_INSERT Abilities OFF;



SET IDENTITY_INSERT Rooms ON;

iNSERT INTO Rooms (Id, Name, Description, RoomType, IsLocked)
VALUES
(1, 'Dungeon', 'A dark, damp cell with chains on the walls.', 'Dungeon', 1),
(2, 'Torture Chamber', 'An ominous room filled with instruments of pain.', 'Torture Chamber', 0),
(3, 'Spiral Stairwell', 'A narrow stone stairwell leading upward.', 'Stairwell', 0),
(4, 'Guard Room', 'A well-lit room with tables, chairs, and weapons racks.', 'Guard Area', 0),
(5, 'Barracks', 'Rows of beds and personal lockers for the castle guards.', 'Barracks', 0),
(6, 'Scullery', 'The kitchen’s cleaning and prep area, filled with utensils and sinks.', 'Scullery', 0),
(7, 'Armory', 'A room stacked with weapons, armor, and training gear.', 'Armory', 0),
(8, 'Castle Garden', 'A beautiful, well-kept garden leading to the castle entrance.', 'Garden', 0);

SET IDENTITY_INSERT Rooms OFF;

SET IDENTITY_INSERT Items ON;

-- ===========================
-- Equipment (100 items)
-- ===========================
INSERT INTO Items (Id, Name, ItemCategory, EquipmentType, EquipmentSlot, ConsumableType, Weight, Value, BuffDuration, GrantedAbilityID)
VALUES
(201, 'Tome of the Aether', 'Spellbook', NULL, NULL, NULL, 1.2, 0, 0, 1),
(202, 'Flora''s Handbook', 'Spellbook', NULL, NULL, NULL, 1.2, 0, 0, 2),
(203, 'Theif''s Journal', 'Spellbook', NULL, NULL, NULL, 1.2, 0, 0, 3),
(204, 'Warrior''s Codex', 'Spellbook', NULL, NULL, NULL, 1.2, 0, 0, 4),
(205, 'Vampire''s Tome', 'Spellbook', NULL, NULL, NULL, 1.2, 0, 0, 5),
(1, 'Dragonfang Sword', 'Equipment', 'Attack', 'Weapon', NULL, 5.2, 25, NULL, NULL),
(2, 'Shadowsteel Dagger', 'Equipment', 'Attack', 'Weapon', NULL, 1.8, 15, NULL, NULL),
(3, 'Moonblade', 'Equipment', 'Attack', 'Weapon', NULL, 4.3, 18, NULL, NULL),
(4, 'Phoenix Longsword', 'Equipment', 'Attack', 'Weapon', NULL, 6.1, 30, NULL, NULL),
(5, 'Obsidian Greatsword', 'Equipment', 'Attack', 'Weapon', NULL, 8.2, 35, NULL, NULL),
(6, 'Emerald Helm', 'Equipment', 'Defense', 'Head', NULL, 4.0, 20, NULL, NULL),
(7, 'Titanium Chestplate', 'Equipment', 'Defense', 'Chest', NULL, 12.5, 35, NULL, NULL),
(8, 'Iron Greaves', 'Equipment', 'Defense', 'Legs', NULL, 8.0, 18, NULL, NULL),
(9, 'Shadowcloak Hood', 'Equipment', 'Defense', 'Head', NULL, 2.4, 12, NULL, NULL),
(10, 'Silver Pauldrons', 'Equipment', 'Defense', 'Hands', NULL, 3.5, 15, NULL, NULL),
(11, 'Dragonhide Boots', 'Equipment', 'Defense', 'Feet', NULL, 3.1, 14, NULL, NULL),
(12, 'Vampire Fang Gauntlets', 'Equipment', 'Attack', 'Hands', NULL, 2.0, 10, NULL, NULL),
(13, 'Stormbringer Axe', 'Equipment', 'Attack', 'Weapon', NULL, 9.0, 40, NULL, NULL),
(14, 'Frostbite Spear', 'Equipment', 'Attack', 'Weapon', NULL, 7.5, 28, NULL, NULL),
(15, 'Titanium Legplates', 'Equipment', 'Defense', 'Legs', NULL, 10.0, 22, NULL, NULL),
(16, 'Obsidian Shield', 'Equipment', 'Defense', 'Hands', NULL, 6.0, 25, NULL, NULL),
(17, 'Celestial Helm', 'Equipment', 'Defense', 'Head', NULL, 5.0, 18, NULL, NULL),
(18, 'Golden Chestguard', 'Equipment', 'Defense', 'Chest', NULL, 11.0, 30, NULL, NULL),
(19, 'Mystic Greaves', 'Equipment', 'Defense', 'Legs', NULL, 6.5, 17, NULL, NULL),
(20, 'Elven Boots', 'Equipment', 'Defense', 'Feet', NULL, 2.7, 12, NULL, NULL),
(21, 'Shadowfang Katana', 'Equipment', 'Attack', 'Weapon', NULL, 4.5, 32, NULL, NULL),
(22, 'Dragon Scale Armor', 'Equipment', 'Defense', 'Chest', NULL, 15.0, 38, NULL, NULL),
(23, 'Wyrm Helm', 'Equipment', 'Defense', 'Head', NULL, 5.0, 22, NULL, NULL),
(24, 'Golem Greaves', 'Equipment', 'Defense', 'Legs', NULL, 9.0, 24, NULL, NULL),
(25, 'Spectral Gloves', 'Equipment', 'Defense', 'Hands', NULL, 2.5, 15, NULL, NULL),
(26, 'Windrunner Boots', 'Equipment', 'Defense', 'Feet', NULL, 2.0, 14, NULL, NULL),
(27, 'Inferno Blade', 'Equipment', 'Attack', 'Weapon', NULL, 6.5, 36, NULL, NULL),
(28, 'Frostfang Sword', 'Equipment', 'Attack', 'Weapon', NULL, 5.5, 30, NULL, NULL),
(29, 'Thunderstrike Mace', 'Equipment', 'Attack', 'Weapon', NULL, 7.0, 33, NULL, NULL),
(30, 'Celestial Spear', 'Equipment', 'Attack', 'Weapon', NULL, 6.0, 28, NULL, NULL),
(31, 'Obsidian Pauldrons', 'Equipment', 'Defense', 'Hands', NULL, 3.5, 16, NULL, NULL),
(32, 'Titan Gauntlets', 'Equipment', 'Defense', 'Hands', NULL, 4.0, 18, NULL, NULL),
(33, 'Storm Helm', 'Equipment', 'Defense', 'Head', NULL, 4.5, 20, NULL, NULL),
(34, 'Dragonbone Chestplate', 'Equipment', 'Defense', 'Chest', NULL, 13.0, 36, NULL, NULL),
(35, 'Iron Boots', 'Equipment', 'Defense', 'Feet', NULL, 3.0, 12, NULL, NULL),
(36, 'Nightfall Hood', 'Equipment', 'Defense', 'Head', NULL, 2.5, 14, NULL, NULL),
(37, 'Hellfire Axe', 'Equipment', 'Attack', 'Weapon', NULL, 8.0, 42, NULL, NULL),
(38, 'Bloodfang Sword', 'Equipment', 'Attack', 'Weapon', NULL, 5.0, 30, NULL, NULL),
(39, 'Stormhammer', 'Equipment', 'Attack', 'Weapon', NULL, 9.0, 38, NULL, NULL),
(40, 'Soulblade', 'Equipment', 'Attack', 'Weapon', NULL, 6.0, 31, NULL, NULL),
(41, 'Golden Helm', 'Equipment', 'Defense', 'Head', NULL, 5.0, 18, NULL, NULL),
(42, 'Shadow Chestplate', 'Equipment', 'Defense', 'Chest', NULL, 12.0, 32, NULL, NULL),
(43, 'Titan Legplates', 'Equipment', 'Defense', 'Legs', NULL, 10.0, 24, NULL, NULL),
(44, 'Iron Gloves', 'Equipment', 'Defense', 'Hands', NULL, 3.0, 14, NULL, NULL),
(45, 'Storm Boots', 'Equipment', 'Defense', 'Feet', NULL, 3.0, 15, NULL, NULL),
(46, 'Dragonclaw Sword', 'Equipment', 'Attack', 'Weapon', NULL, 5.5, 29, NULL, NULL),
(47, 'Frostbrand Axe', 'Equipment', 'Attack', 'Weapon', NULL, 7.5, 35, NULL, NULL),
(48, 'Nightfang Dagger', 'Equipment', 'Attack', 'Weapon', NULL, 3.0, 20, NULL, NULL),
(49, 'Titanium Spear', 'Equipment', 'Attack', 'Weapon', NULL, 6.5, 32, NULL, NULL),
(50, 'Celestial Shield', 'Equipment', 'Defense', 'Hands', NULL, 6.0, 28, NULL, NULL),
(51, 'Obsidian Helm', 'Equipment', 'Defense', 'Head', NULL, 4.8, 20, NULL, NULL),
(52, 'Sunfire Chestplate', 'Equipment', 'Defense', 'Chest', NULL, 11.5, 34, NULL, NULL),
(53, 'Moonlight Greaves', 'Equipment', 'Defense', 'Legs', NULL, 7.0, 18, NULL, NULL),
(54, 'Windwalker Boots', 'Equipment', 'Defense', 'Feet', NULL, 2.5, 13, NULL, NULL),
(55, 'Specter Gauntlets', 'Equipment', 'Defense', 'Hands', NULL, 3.2, 15, NULL, NULL),
(56, 'Hellstorm Sword', 'Equipment', 'Attack', 'Weapon', NULL, 6.8, 33, NULL, NULL),
(57, 'Frostmourne', 'Equipment', 'Attack', 'Weapon', NULL, 7.2, 36, NULL, NULL),
(58, 'Voidfang Blade', 'Equipment', 'Attack', 'Weapon', NULL, 5.5, 28, NULL, NULL),
(59, 'Thunderfury', 'Equipment', 'Attack', 'Weapon', NULL, 8.0, 40, NULL, NULL),
(60, 'Celestial Hammer', 'Equipment', 'Attack', 'Weapon', NULL, 7.0, 34, NULL, NULL),
(61, 'Shadow Gauntlets', 'Equipment', 'Defense', 'Hands', NULL, 3.5, 16, NULL, NULL),
(62, 'Titan Shield', 'Equipment', 'Defense', 'Hands', NULL, 5.0, 28, NULL, NULL),
(63, 'Dragonbone Helm', 'Equipment', 'Defense', 'Head', NULL, 4.5, 22, NULL, NULL),
(64, 'Oblivion Chestplate', 'Equipment', 'Defense', 'Chest', NULL, 13.5, 37, NULL, NULL),
(65, 'Iron Legplates', 'Equipment', 'Defense', 'Legs', NULL, 9.0, 23, NULL, NULL),
(66, 'Stormbreaker Boots', 'Equipment', 'Defense', 'Feet', NULL, 3.2, 15, NULL, NULL),
(67, 'Bloodfang Gauntlets', 'Equipment', 'Attack', 'Hands', NULL, 2.5, 14, NULL, NULL),
(68, 'Flamebrand Sword', 'Equipment', 'Attack', 'Weapon', NULL, 6.0, 30, NULL, NULL),
(69, 'Icefang Dagger', 'Equipment', 'Attack', 'Weapon', NULL, 3.2, 22, NULL, NULL),
(70, 'Thunderstrike Blade', 'Equipment', 'Attack', 'Weapon', NULL, 7.5, 36, NULL, NULL),
(71, 'Obsidian Boots', 'Equipment', 'Defense', 'Feet', NULL, 3.5, 15, NULL, NULL),
(72, 'Golden Gauntlets', 'Equipment', 'Defense', 'Hands', NULL, 3.0, 16, NULL, NULL),
(73, 'Celestial Legplates', 'Equipment', 'Defense', 'Legs', NULL, 8.0, 20, NULL, NULL),
(74, 'Dragon Helm', 'Equipment', 'Defense', 'Head', NULL, 5.0, 22, NULL, NULL),
(75, 'Frostfire Chestplate', 'Equipment', 'Defense', 'Chest', NULL, 12.5, 34, NULL, NULL),
(76, 'Nightfall Greaves', 'Equipment', 'Defense', 'Legs', NULL, 7.0, 19, NULL, NULL),
(77, 'Stormclaw Boots', 'Equipment', 'Defense', 'Feet', NULL, 3.0, 14, NULL, NULL),
(78, 'Vortex Sword', 'Equipment', 'Attack', 'Weapon', NULL, 6.0, 31, NULL, NULL),
(79, 'Dragonfang Axe', 'Equipment', 'Attack', 'Weapon', NULL, 7.0, 33, NULL, NULL),
(80, 'Moonlit Spear', 'Equipment', 'Attack', 'Weapon', NULL, 6.5, 32, NULL, NULL),
(81, 'Obsidian Pauldrons', 'Equipment', 'Defense', 'Hands', NULL, 3.8, 17, NULL, NULL),
(82, 'Titan Gauntlets II', 'Equipment', 'Defense', 'Hands', NULL, 4.2, 18, NULL, NULL),
(83, 'Storm Helm II', 'Equipment', 'Defense', 'Head', NULL, 4.5, 21, NULL, NULL),
(84, 'Dragonbone Chestplate II', 'Equipment', 'Defense', 'Chest', NULL, 12.8, 36, NULL, NULL),
(85, 'Iron Boots II', 'Equipment', 'Defense', 'Feet', NULL, 3.0, 13, NULL, NULL),
(86, 'Nightfall Hood II', 'Equipment', 'Defense', 'Head', NULL, 2.5, 14, NULL, NULL),
(87, 'Hellfire Axe II', 'Equipment', 'Attack', 'Weapon', NULL, 8.0, 42, NULL, NULL),
(88, 'Bloodfang Sword II', 'Equipment', 'Attack', 'Weapon', NULL, 5.0, 31, NULL, NULL),
(89, 'Stormhammer II', 'Equipment', 'Attack', 'Weapon', NULL, 9.0, 39, NULL, NULL),
(90, 'Soulblade II', 'Equipment', 'Attack', 'Weapon', NULL, 6.0, 32, NULL, NULL),
(91, 'Golden Helm II', 'Equipment', 'Defense', 'Head', NULL, 5.0, 19, NULL, NULL),
(92, 'Shadow Chestplate II', 'Equipment', 'Defense', 'Chest', NULL, 12.0, 33, NULL, NULL),
(93, 'Titan Legplates II', 'Equipment', 'Defense', 'Legs', NULL, 10.0, 25, NULL, NULL),
(94, 'Iron Gloves II', 'Equipment', 'Defense', 'Hands', NULL, 3.0, 15, NULL, NULL),
(95, 'Storm Boots II', 'Equipment', 'Defense', 'Feet', NULL, 3.0, 16, NULL, NULL),
(96, 'Dragonclaw Sword II', 'Equipment', 'Attack', 'Weapon', NULL, 5.5, 30, NULL, NULL),
(97, 'Frostbrand Axe II', 'Equipment', 'Attack', 'Weapon', NULL, 7.5, 36, NULL, NULL),
(98, 'Nightfang Dagger II', 'Equipment', 'Attack', 'Weapon', NULL, 3.0, 21, NULL, NULL),
(99, 'Titanium Spear II', 'Equipment', 'Attack', 'Weapon', NULL, 6.5, 33, NULL, NULL),
(100, 'Void Reaver', 'Equipment', 'Attack', 'Weapon', NULL, 7.0, 40, NULL, NULL),
(101, 'Moonlight Potion', 'Consumable', NULL, NULL, 'Heal', 0.25, 20, NULL, NULL),
(102, 'Rage Elixir', 'Consumable', NULL, NULL, 'Attack', 0.30, 5, 3, NULL),
(103, 'Stonehide Brew', 'Consumable', NULL, NULL, 'Defense', 0.40, 4, 3, NULL),
(104, 'Sunfire Tonic', 'Consumable', NULL, NULL, 'Heal', 0.35, 25, NULL, NULL),
(105, 'Berserker Draught', 'Consumable', NULL, NULL, 'Attack', 0.20, 6, 2, NULL),
(106, 'Guardian Serum', 'Consumable', NULL, NULL, 'Defense', 0.45, 5, 4, NULL),
(107, 'Healing Nectar', 'Consumable', NULL, NULL, 'Heal', 0.30, 18, NULL, NULL),
(108, 'Fury Phial', 'Consumable', NULL, NULL, 'Attack', 0.25, 7, 3, NULL),
(109, 'Stone Skin Potion', 'Consumable', NULL, NULL, 'Defense', 0.50, 6, 3, NULL),
(110, 'Life Elixir', 'Consumable', NULL, NULL, 'Heal', 0.40, 30, NULL, NULL),
(111, 'Power Draught', 'Consumable', NULL, NULL, 'Attack', 0.35, 10, 4, NULL),
(112, 'Ironhide Brew', 'Consumable', NULL, NULL, 'Defense', 0.50, 8, 4, NULL),
(113, 'Healing Infusion', 'Consumable', NULL, NULL, 'Heal', 0.30, 22, NULL, NULL),
(114, 'Raging Flask', 'Consumable', NULL, NULL, 'Attack', 0.25, 12, 3, NULL),
(115, 'Fortitude Tonic', 'Consumable', NULL, NULL, 'Defense', 0.40, 9, 3, NULL),
(116, 'Sunlight Potion', 'Consumable', NULL, NULL, 'Heal', 0.35, 24, NULL, NULL),
(117, 'Vigor Elixir', 'Consumable', NULL, NULL, 'Attack', 0.30, 8, 3, NULL),
(118, 'Bulwark Brew', 'Consumable', NULL, NULL, 'Defense', 0.45, 7, 3, NULL),
(119, 'Rejuvenation Tonic', 'Consumable', NULL, NULL, 'Heal', 0.35, 26, NULL, NULL),
(120, 'Savage Flask', 'Consumable', NULL, NULL, 'Attack', 0.25, 9, 3, NULL),
(121, 'Protector Serum', 'Consumable', NULL, NULL, 'Defense', 0.50, 10, 4, NULL),
(122, 'Healing Draught', 'Consumable', NULL, NULL, 'Heal', 0.50, 10, NULL, NULL);

SET IDENTITY_INSERT Items OFF;