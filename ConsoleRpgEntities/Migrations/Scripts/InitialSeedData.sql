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

SET IDENTITY_INSERT Items ON;

-- ===========================
-- Equipment (100 items)
-- ===========================
INSERT INTO Items (Id, Name, ItemCategory, EquipmentType, EquipmentSlot, ConsumableType, Weight, Value, BuffDuration)
VALUES
(1, 'Dragonfang Sword', 'Equipment', 'Attack', 'Weapon', NULL, 5.2, 25, NULL),
(2, 'Shadowsteel Dagger', 'Equipment', 'Attack', 'Weapon', NULL, 1.8, 15, NULL),
(3, 'Moonblade', 'Equipment', 'Attack', 'Weapon', NULL, 4.3, 18, NULL),
(4, 'Phoenix Longsword', 'Equipment', 'Attack', 'Weapon', NULL, 6.1, 30, NULL),
(5, 'Obsidian Greatsword', 'Equipment', 'Attack', 'Weapon', NULL, 8.2, 35, NULL),
(6, 'Emerald Helm', 'Equipment', 'Defense', 'Head', NULL, 4.0, 20, NULL),
(7, 'Titanium Chestplate', 'Equipment', 'Defense', 'Chest', NULL, 12.5, 35, NULL),
(8, 'Iron Greaves', 'Equipment', 'Defense', 'Legs', NULL, 8.0, 18, NULL),
(9, 'Shadowcloak Hood', 'Equipment', 'Defense', 'Head', NULL, 2.4, 12, NULL),
(10, 'Silver Pauldrons', 'Equipment', 'Defense', 'Hands', NULL, 3.5, 15, NULL),
(11, 'Dragonhide Boots', 'Equipment', 'Defense', 'Feet', NULL, 3.1, 14, NULL),
(12, 'Vampire Fang Gauntlets', 'Equipment', 'Attack', 'Hands', NULL, 2.0, 10, NULL),
(13, 'Stormbringer Axe', 'Equipment', 'Attack', 'Weapon', NULL, 9.0, 40, NULL),
(14, 'Frostbite Spear', 'Equipment', 'Attack', 'Weapon', NULL, 7.5, 28, NULL),
(15, 'Titanium Legplates', 'Equipment', 'Defense', 'Legs', NULL, 10.0, 22, NULL),
(16, 'Obsidian Shield', 'Equipment', 'Defense', 'Hands', NULL, 6.0, 25, NULL),
(17, 'Celestial Helm', 'Equipment', 'Defense', 'Head', NULL, 5.0, 18, NULL),
(18, 'Golden Chestguard', 'Equipment', 'Defense', 'Chest', NULL, 11.0, 30, NULL),
(19, 'Mystic Greaves', 'Equipment', 'Defense', 'Legs', NULL, 6.5, 17, NULL),
(20, 'Elven Boots', 'Equipment', 'Defense', 'Feet', NULL, 2.7, 12, NULL),
(21, 'Shadowfang Katana', 'Equipment', 'Attack', 'Weapon', NULL, 4.5, 32, NULL),
(22, 'Dragon Scale Armor', 'Equipment', 'Defense', 'Chest', NULL, 15.0, 38, NULL),
(23, 'Wyrm Helm', 'Equipment', 'Defense', 'Head', NULL, 5.0, 22, NULL),
(24, 'Golem Greaves', 'Equipment', 'Defense', 'Legs', NULL, 9.0, 24, NULL),
(25, 'Spectral Gloves', 'Equipment', 'Defense', 'Hands', NULL, 2.5, 15, NULL),
(26, 'Windrunner Boots', 'Equipment', 'Defense', 'Feet', NULL, 2.0, 14, NULL),
(27, 'Inferno Blade', 'Equipment', 'Attack', 'Weapon', NULL, 6.5, 36, NULL),
(28, 'Frostfang Sword', 'Equipment', 'Attack', 'Weapon', NULL, 5.5, 30, NULL),
(29, 'Thunderstrike Mace', 'Equipment', 'Attack', 'Weapon', NULL, 7.0, 33, NULL),
(30, 'Celestial Spear', 'Equipment', 'Attack', 'Weapon', NULL, 6.0, 28, NULL),
(31, 'Obsidian Pauldrons', 'Equipment', 'Defense', 'Hands', NULL, 3.5, 16, NULL),
(32, 'Titan Gauntlets', 'Equipment', 'Defense', 'Hands', NULL, 4.0, 18, NULL),
(33, 'Storm Helm', 'Equipment', 'Defense', 'Head', NULL, 4.5, 20, NULL),
(34, 'Dragonbone Chestplate', 'Equipment', 'Defense', 'Chest', NULL, 13.0, 36, NULL),
(35, 'Iron Boots', 'Equipment', 'Defense', 'Feet', NULL, 3.0, 12, NULL),
(36, 'Nightfall Hood', 'Equipment', 'Defense', 'Head', NULL, 2.5, 14, NULL),
(37, 'Hellfire Axe', 'Equipment', 'Attack', 'Weapon', NULL, 8.0, 42, NULL),
(38, 'Bloodfang Sword', 'Equipment', 'Attack', 'Weapon', NULL, 5.0, 30, NULL),
(39, 'Stormhammer', 'Equipment', 'Attack', 'Weapon', NULL, 9.0, 38, NULL),
(40, 'Soulblade', 'Equipment', 'Attack', 'Weapon', NULL, 6.0, 31, NULL),
(41, 'Golden Helm', 'Equipment', 'Defense', 'Head', NULL, 5.0, 18, NULL),
(42, 'Shadow Chestplate', 'Equipment', 'Defense', 'Chest', NULL, 12.0, 32, NULL),
(43, 'Titan Legplates', 'Equipment', 'Defense', 'Legs', NULL, 10.0, 24, NULL),
(44, 'Iron Gloves', 'Equipment', 'Defense', 'Hands', NULL, 3.0, 14, NULL),
(45, 'Storm Boots', 'Equipment', 'Defense', 'Feet', NULL, 3.0, 15, NULL),
(46, 'Dragonclaw Sword', 'Equipment', 'Attack', 'Weapon', NULL, 5.5, 29, NULL),
(47, 'Frostbrand Axe', 'Equipment', 'Attack', 'Weapon', NULL, 7.5, 35, NULL),
(48, 'Nightfang Dagger', 'Equipment', 'Attack', 'Weapon', NULL, 3.0, 20, NULL),
(49, 'Titanium Spear', 'Equipment', 'Attack', 'Weapon', NULL, 6.5, 32, NULL),
(50, 'Celestial Shield', 'Equipment', 'Defense', 'Hands', NULL, 6.0, 28, NULL),
(51, 'Obsidian Helm', 'Equipment', 'Defense', 'Head', NULL, 4.8, 20, NULL),
(52, 'Sunfire Chestplate', 'Equipment', 'Defense', 'Chest', NULL, 11.5, 34, NULL),
(53, 'Moonlight Greaves', 'Equipment', 'Defense', 'Legs', NULL, 7.0, 18, NULL),
(54, 'Windwalker Boots', 'Equipment', 'Defense', 'Feet', NULL, 2.5, 13, NULL),
(55, 'Specter Gauntlets', 'Equipment', 'Defense', 'Hands', NULL, 3.2, 15, NULL),
(56, 'Hellstorm Sword', 'Equipment', 'Attack', 'Weapon', NULL, 6.8, 33, NULL),
(57, 'Frostmourne', 'Equipment', 'Attack', 'Weapon', NULL, 7.2, 36, NULL),
(58, 'Voidfang Blade', 'Equipment', 'Attack', 'Weapon', NULL, 5.5, 28, NULL),
(59, 'Thunderfury', 'Equipment', 'Attack', 'Weapon', NULL, 8.0, 40, NULL),
(60, 'Celestial Hammer', 'Equipment', 'Attack', 'Weapon', NULL, 7.0, 34, NULL),
(61, 'Shadow Gauntlets', 'Equipment', 'Defense', 'Hands', NULL, 3.5, 16, NULL),
(62, 'Titan Shield', 'Equipment', 'Defense', 'Hands', NULL, 5.0, 28, NULL),
(63, 'Dragonbone Helm', 'Equipment', 'Defense', 'Head', NULL, 4.5, 22, NULL),
(64, 'Oblivion Chestplate', 'Equipment', 'Defense', 'Chest', NULL, 13.5, 37, NULL),
(65, 'Iron Legplates', 'Equipment', 'Defense', 'Legs', NULL, 9.0, 23, NULL),
(66, 'Stormbreaker Boots', 'Equipment', 'Defense', 'Feet', NULL, 3.2, 15, NULL),
(67, 'Bloodfang Gauntlets', 'Equipment', 'Attack', 'Hands', NULL, 2.5, 14, NULL),
(68, 'Flamebrand Sword', 'Equipment', 'Attack', 'Weapon', NULL, 6.0, 30, NULL),
(69, 'Icefang Dagger', 'Equipment', 'Attack', 'Weapon', NULL, 3.2, 22, NULL),
(70, 'Thunderstrike Blade', 'Equipment', 'Attack', 'Weapon', NULL, 7.5, 36, NULL),
(71, 'Obsidian Boots', 'Equipment', 'Defense', 'Feet', NULL, 3.5, 15, NULL),
(72, 'Golden Gauntlets', 'Equipment', 'Defense', 'Hands', NULL, 3.0, 16, NULL),
(73, 'Celestial Legplates', 'Equipment', 'Defense', 'Legs', NULL, 8.0, 20, NULL),
(74, 'Dragon Helm', 'Equipment', 'Defense', 'Head', NULL, 5.0, 22, NULL),
(75, 'Frostfire Chestplate', 'Equipment', 'Defense', 'Chest', NULL, 12.5, 34, NULL),
(76, 'Nightfall Greaves', 'Equipment', 'Defense', 'Legs', NULL, 7.0, 19, NULL),
(77, 'Stormclaw Boots', 'Equipment', 'Defense', 'Feet', NULL, 3.0, 14, NULL),
(78, 'Vortex Sword', 'Equipment', 'Attack', 'Weapon', NULL, 6.0, 31, NULL),
(79, 'Dragonfang Axe', 'Equipment', 'Attack', 'Weapon', NULL, 7.0, 33, NULL),
(80, 'Moonlit Spear', 'Equipment', 'Attack', 'Weapon', NULL, 6.5, 32, NULL),
(81, 'Obsidian Pauldrons', 'Equipment', 'Defense', 'Hands', NULL, 3.8, 17, NULL),
(82, 'Titan Gauntlets II', 'Equipment', 'Defense', 'Hands', NULL, 4.2, 18, NULL),
(83, 'Storm Helm II', 'Equipment', 'Defense', 'Head', NULL, 4.5, 21, NULL),
(84, 'Dragonbone Chestplate II', 'Equipment', 'Defense', 'Chest', NULL, 12.8, 36, NULL),
(85, 'Iron Boots II', 'Equipment', 'Defense', 'Feet', NULL, 3.0, 13, NULL),
(86, 'Nightfall Hood II', 'Equipment', 'Defense', 'Head', NULL, 2.5, 14, NULL),
(87, 'Hellfire Axe II', 'Equipment', 'Attack', 'Weapon', NULL, 8.0, 42, NULL),
(88, 'Bloodfang Sword II', 'Equipment', 'Attack', 'Weapon', NULL, 5.0, 31, NULL),
(89, 'Stormhammer II', 'Equipment', 'Attack', 'Weapon', NULL, 9.0, 39, NULL),
(90, 'Soulblade II', 'Equipment', 'Attack', 'Weapon', NULL, 6.0, 32, NULL),
(91, 'Golden Helm II', 'Equipment', 'Defense', 'Head', NULL, 5.0, 19, NULL),
(92, 'Shadow Chestplate II', 'Equipment', 'Defense', 'Chest', NULL, 12.0, 33, NULL),
(93, 'Titan Legplates II', 'Equipment', 'Defense', 'Legs', NULL, 10.0, 25, NULL),
(94, 'Iron Gloves II', 'Equipment', 'Defense', 'Hands', NULL, 3.0, 15, NULL),
(95, 'Storm Boots II', 'Equipment', 'Defense', 'Feet', NULL, 3.0, 16, NULL),
(96, 'Dragonclaw Sword II', 'Equipment', 'Attack', 'Weapon', NULL, 5.5, 30, NULL),
(97, 'Frostbrand Axe II', 'Equipment', 'Attack', 'Weapon', NULL, 7.5, 36, NULL),
(98, 'Nightfang Dagger II', 'Equipment', 'Attack', 'Weapon', NULL, 3.0, 21, NULL),
(99, 'Titanium Spear II', 'Equipment', 'Attack', 'Weapon', NULL, 6.5, 33, NULL),
(100, 'Void Reaver', 'Equipment', 'Attack', 'Weapon', NULL, 7.0, 40, NULL);

-- ===========================
-- Consumables (30 items)
-- ===========================
INSERT INTO Items (Id, Name, ItemCategory, EquipmentType, EquipmentSlot, ConsumableType, Weight, Value, BuffDuration)
VALUES
(101, 'Moonlight Potion', 'Consumable', NULL, NULL, 'Heal', 0.25, 20, NULL),
(102, 'Rage Elixir', 'Consumable', NULL, NULL, 'Attack', 0.30, 5, 3),
(103, 'Stonehide Brew', 'Consumable', NULL, NULL, 'Defense', 0.40, 4, 3),
(104, 'Sunfire Tonic', 'Consumable', NULL, NULL, 'Heal', 0.35, 25, NULL),
(105, 'Berserker Draught', 'Consumable', NULL, NULL, 'Attack', 0.20, 6, 2),
(106, 'Guardian Serum', 'Consumable', NULL, NULL, 'Defense', 0.45, 5, 4),
(107, 'Healing Nectar', 'Consumable', NULL, NULL, 'Heal', 0.30, 18, NULL),
(108, 'Fury Phial', 'Consumable', NULL, NULL, 'Attack', 0.25, 7, 3),
(109, 'Stone Skin Potion', 'Consumable', NULL, NULL, 'Defense', 0.50, 6, 3),
(110, 'Life Elixir', 'Consumable', NULL, NULL, 'Heal', 0.40, 30, NULL),
(111, 'Power Draught', 'Consumable', NULL, NULL, 'Attack', 0.35, 10, 4),
(112, 'Ironhide Brew', 'Consumable', NULL, NULL, 'Defense', 0.50, 8, 4),
(113, 'Healing Infusion', 'Consumable', NULL, NULL, 'Heal', 0.30, 22, NULL),
(114, 'Raging Flask', 'Consumable', NULL, NULL, 'Attack', 0.25, 12, 3),
(115, 'Fortitude Tonic', 'Consumable', NULL, NULL, 'Defense', 0.40, 9, 3),
(116, 'Sunlight Potion', 'Consumable', NULL, NULL, 'Heal', 0.35, 24, NULL),
(117, 'Vigor Elixir', 'Consumable', NULL, NULL, 'Attack', 0.30, 8, 3),
(118, 'Bulwark Brew', 'Consumable', NULL, NULL, 'Defense', 0.45, 7, 3),
(119, 'Rejuvenation Tonic', 'Consumable', NULL, NULL, 'Heal', 0.35, 26, NULL),
(120, 'Savage Flask', 'Consumable', NULL, NULL, 'Attack', 0.25, 9, 3),
(121, 'Protector Serum', 'Consumable', NULL, NULL, 'Defense', 0.50, 10, 4),
(122, 'Healing Draught', 'Consumable', NULL, NULL, 'Heal', 0.50, 10, 4)
