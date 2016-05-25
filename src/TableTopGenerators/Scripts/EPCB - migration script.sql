USE [EclipsePhaseCharacterBuilder]
GO

/*
EXEC sp_rename 'dbo.AtomBook', 'Book';
EXEC sp_rename 'dbo.Ai', 'Sapient';
EXEC sp_rename 'dbo.AiAptitude', 'SapientAptitude';
EXEC sp_rename 'dbo.AiSkill', 'SapientSkill';
EXEC sp_rename 'dbo.AiStat', 'SapientStat';
EXEC sp_rename 'dbo.GroupName', 'SkillGroup';
*/

/*
ALTER TABLE [dbo].[Book] DROP CONSTRAINT [PK_atombook_name]
GO

ALTER TABLE [dbo].[Book] ADD
	bookId int primary key identity(1,1)
GO

ALTER TABLE [dbo].[Reputation] DROP CONSTRAINT [PK_reputation_name]
GO

ALTER TABLE [dbo].[Reputation] ADD
	reputationId int primary key identity(1,1)
GO

ALTER TABLE [dbo].[Stat] DROP CONSTRAINT [PK_stat_name]
GO

ALTER TABLE [dbo].[Stat] ADD
	statId int primary key identity(1,1)
GO

ALTER TABLE [dbo].[Aptitude] DROP CONSTRAINT [PK_aptitude_name]
GO

ALTER TABLE [dbo].[Aptitude] ADD
	aptitudeId int primary key identity(1,1)
GO

ALTER TABLE [dbo].[Gear] DROP CONSTRAINT [PK_gear_name]
GO

ALTER TABLE [dbo].[Gear] ADD
	gearId int primary key identity(1,1)
GO

ALTER TABLE [dbo].[Trait] DROP CONSTRAINT [PK_traits_name]
GO

ALTER TABLE [dbo].[Trait] ADD
	traitId int primary key identity(1,1)
GO

ALTER TABLE [dbo].[Psysleight] DROP CONSTRAINT [PK_psysleight_name]
GO

ALTER TABLE [dbo].[Psysleight] ADD
	psysleightId int primary key identity(1,1)
GO

ALTER TABLE [dbo].[Sapient] DROP CONSTRAINT [PK_ai_name]
GO

ALTER TABLE [dbo].[Sapient] ADD
	sapientId int primary key identity(1,1)
GO

ALTER TABLE [dbo].[Background] DROP CONSTRAINT [PK_background_name]
GO

ALTER TABLE [dbo].[Background] ADD
	backgroundId int primary key identity(1,1)
GO

ALTER TABLE [dbo].[Morph] DROP CONSTRAINT [PK_morph_name]
GO

ALTER TABLE [dbo].[Morph] ADD
	morphId int primary key identity(1,1)
GO

ALTER TABLE [dbo].[BonusMalus] DROP CONSTRAINT [PK_bonusmalus_name]
GO

ALTER TABLE [dbo].[BonusMalus] ADD
	bonusMalusId int primary key identity(1,1)
GO
*/

/*
ALTER TABLE [dbo].[MorphTrait] ADD
	morphId int null,
	traitId int null
GO

UPDATE [dbo].[MorphTrait]
SET morphId = (SELECT morphId FROM Morph where [MorphTrait].morph = [Morph].name),
traitId = (SELECT traitId FROM Trait where [MorphTrait].trait = Trait.name)
GO

ALTER TABLE [dbo].[MorphTrait]
ALTER COLUMN	morphId int not null
GO

ALTER TABLE [dbo].[MorphTrait]
ALTER COLUMN	traitId int not null
GO

ALTER TABLE [dbo].[MorphTrait] DROP CONSTRAINT [PK_morphtrait_morph]
GO

ALTER TABLE [dbo].[MorphTrait] ADD  CONSTRAINT [PK_morphtrait_morph] PRIMARY KEY CLUSTERED
(
	morphId ASC,
	traitId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

ALTER TABLE [dbo].[TraitBonusMalus] ADD
	traitId int null,
	bonusMalusId int null
GO
*/
/* noting some bad data in this transaction */
/*
UPDATE [dbo].[TraitBonusMalus]
SET traitId = (SELECT traitId FROM Trait where [TraitBonusMalus].traitName = Trait.name),
bonusMalusId = (SELECT bonusMalusId FROM [BonusMalus] where [TraitBonusMalus].bonusMalusName = [BonusMalus].name)
GO

ALTER TABLE [dbo].[TraitBonusMalus]
ALTER COLUMN	bonusMalusId int not null
GO

ALTER TABLE [dbo].[TraitBonusMalus]
ALTER COLUMN	traitId int not null
GO

ALTER TABLE [dbo].[TraitBonusMalus] DROP CONSTRAINT [PK_traitbonusmalus_traitName]
GO

ALTER TABLE [dbo].[TraitBonusMalus] ADD  CONSTRAINT [PK_traitbonusmalus_traitName] PRIMARY KEY CLUSTERED
(
	bonusMalusId ASC,
	traitId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

ALTER TABLE [dbo].[GearBonusMalus] ADD
	gearId int null,
	bonusMalusId int null
GO

UPDATE [dbo].[GearBonusMalus]
SET gearId = (SELECT gearId FROM Gear where [GearBonusMalus].gear = Gear.name),
bonusMalusId = (SELECT bonusMalusId FROM [BonusMalus] where [GearBonusMalus].bonusMalus = [BonusMalus].name)
GO

ALTER TABLE [dbo].[GearBonusMalus]
ALTER COLUMN	bonusMalusId int not null
GO

ALTER TABLE [dbo].[GearBonusMalus]
ALTER COLUMN	gearId int not null
GO

ALTER TABLE [dbo].[GearBonusMalus] DROP CONSTRAINT [PK_gearbonusmalus_gear]
GO

ALTER TABLE [dbo].[GearBonusMalus] ADD  CONSTRAINT [PK_gearbonusmalus_gear] PRIMARY KEY CLUSTERED
(
	bonusMalusId ASC,
	gearId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

ALTER TABLE [dbo].[MorphBonusMalus] ADD
	morphId int null,
	bonusMalusId int null
GO

UPDATE [dbo].[MorphBonusMalus]
SET morphId = (SELECT morphId FROM Morph where [MorphBonusMalus].morph = Morph.name),
bonusMalusId = (SELECT bonusMalusId FROM [BonusMalus] where [MorphBonusMalus].bonusMalus = [BonusMalus].name)
GO

ALTER TABLE [dbo].[MorphBonusMalus]
ALTER COLUMN	bonusMalusId int not null
GO

ALTER TABLE [dbo].[MorphBonusMalus]
ALTER COLUMN	morphId int not null
GO

ALTER TABLE [dbo].[MorphBonusMalus] DROP CONSTRAINT [PK_morphbonusmalus_morph]
GO

ALTER TABLE [dbo].[MorphBonusMalus] ADD  CONSTRAINT [PK_morphbonusmalus_morph] PRIMARY KEY CLUSTERED
(
	bonusMalusId ASC,
	morphId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

ALTER TABLE [dbo].[MorphGear] ADD
	morphId int null,
	gearId int null
GO

UPDATE [dbo].[MorphGear]
SET morphId = (SELECT morphId FROM Morph where [MorphGear].morph = Morph.name),
gearId = (SELECT gearId FROM [Gear] where [MorphGear].gear = [Gear].name)
GO

ALTER TABLE [dbo].[MorphGear]
ALTER COLUMN	gearId int not null
GO

ALTER TABLE [dbo].[MorphGear]
ALTER COLUMN	morphId int not null
GO

ALTER TABLE [dbo].[MorphGear] DROP CONSTRAINT [PK_morphgears_morph]
GO

ALTER TABLE [dbo].[MorphGear] ADD  CONSTRAINT [PK_morphgears_morph] PRIMARY KEY CLUSTERED
(
	gearId ASC,
	morphId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

ALTER TABLE [dbo].[BackgroundBonusMalus] ADD
	backgroundId int null,
	bonusMalusId int null
GO

UPDATE [dbo].[BackgroundBonusMalus]
SET backgroundId = (SELECT backgroundId FROM Background where [BackgroundBonusMalus].background = Background.name),
bonusMalusId = (SELECT bonusMalusId FROM [BonusMalus] where [BackgroundBonusMalus].bonusMalus = [BonusMalus].name)
GO

ALTER TABLE [dbo].[BackgroundBonusMalus]
ALTER COLUMN	backgroundId int not null
GO

ALTER TABLE [dbo].[BackgroundBonusMalus]
ALTER COLUMN	bonusMalusId int not null
GO

ALTER TABLE [dbo].[BackgroundBonusMalus] DROP CONSTRAINT [PK_backgroundbonusmalus_background]
GO

ALTER TABLE [dbo].[BackgroundBonusMalus] ADD  CONSTRAINT [PK_backgroundbonusmalus_background] PRIMARY KEY CLUSTERED
(
	backgroundId ASC,
	bonusMalusId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

ALTER TABLE [dbo].[BackgroundTrait] ADD
	backgroundId int null,
	traitId int null
GO

UPDATE [dbo].[BackgroundTrait]
SET backgroundId = (SELECT backgroundId FROM Background where [BackgroundTrait].background = Background.name),
traitId = (SELECT traitId FROM [Trait] where [BackgroundTrait].trait = [Trait].name)
GO

ALTER TABLE [dbo].[BackgroundTrait]
ALTER COLUMN	backgroundId int not null
GO

ALTER TABLE [dbo].[BackgroundTrait]
ALTER COLUMN	traitId int not null
GO

ALTER TABLE [dbo].[BackgroundTrait] DROP CONSTRAINT [PK_backgroundtrait_background]
GO

ALTER TABLE [dbo].[BackgroundTrait] ADD  CONSTRAINT [PK_backgroundtrait_background] PRIMARY KEY CLUSTERED
(
	backgroundId ASC,
	traitId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

ALTER TABLE [dbo].[PsysleightBonusMalus] ADD
	psysleightId int null,
	bonusMalusId int null
GO

UPDATE [dbo].[PsysleightBonusMalus]
SET psysleightId = (SELECT psysleightId FROM Psysleight where [PsysleightBonusMalus].psy = Psysleight.name),
bonusMalusId = (SELECT bonusMalusId FROM [BonusMalus] where [PsysleightBonusMalus].bonusmalus = [BonusMalus].name)
GO

ALTER TABLE [dbo].[PsysleightBonusMalus]
ALTER COLUMN	psysleightId int not null
GO

ALTER TABLE [dbo].[PsysleightBonusMalus]
ALTER COLUMN	bonusMalusId int not null
GO

ALTER TABLE [dbo].[PsysleightBonusMalus] DROP CONSTRAINT [PK_psysleightbonusmalus_psy]
GO

ALTER TABLE [dbo].[PsysleightBonusMalus] ADD  CONSTRAINT [PK_psysleightbonusmalus_psy] PRIMARY KEY CLUSTERED
(
	psysleightId ASC,
	bonusMalusId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

ALTER TABLE [dbo].[SapientAptitude] ADD
	sapientId int null,
	aptitudeId int null
GO

UPDATE [dbo].[SapientAptitude]
SET sapientId = (SELECT sapientId FROM Sapient where [SapientAptitude].ai = Sapient.name),
aptitudeId = (SELECT aptitudeId FROM Aptitude where [SapientAptitude].aptitude = Aptitude.name)
GO

ALTER TABLE [dbo].[SapientAptitude]
ALTER COLUMN	sapientId int not null
GO

ALTER TABLE [dbo].[SapientAptitude]
ALTER COLUMN	aptitudeId int not null
GO

ALTER TABLE [dbo].[SapientAptitude] DROP CONSTRAINT [PK_aiaptitude_ai]
GO

ALTER TABLE [dbo].[SapientAptitude] ADD  CONSTRAINT [PK_aiaptitude_ai] PRIMARY KEY CLUSTERED
(
	sapientId ASC,
	aptitudeId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO


ALTER TABLE [dbo].[SapientStat] ADD
	sapientId int null,
	statId int null
GO

UPDATE [dbo].[SapientStat]
SET sapientId = (SELECT sapientId FROM Sapient where [SapientStat].ai = Sapient.name),
statId = (SELECT statId FROM Stat where [SapientStat].stat = Stat.name)
GO

ALTER TABLE [dbo].[SapientStat]
ALTER COLUMN	sapientId int not null
GO

ALTER TABLE [dbo].[SapientStat]
ALTER COLUMN	statId int not null
GO

ALTER TABLE [dbo].[SapientStat] DROP CONSTRAINT [PK_aistat_ai]
GO

ALTER TABLE [dbo].[SapientStat] ADD  CONSTRAINT [PK_aistat_ai] PRIMARY KEY CLUSTERED
(
	sapientId ASC,
	statId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
*/
