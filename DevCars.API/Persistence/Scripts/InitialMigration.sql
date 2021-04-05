IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Cars] (
    [Id] int NOT NULL IDENTITY,
    [Brand] nvarchar(100) NOT NULL,
    [Model] nvarchar(100) NOT NULL,
    [VinCode] nvarchar(100) NOT NULL,
    [Color] nvarchar(50) NOT NULL,
    [Year] int NOT NULL,
    [Price] decimal(20,2) NOT NULL,
    [ProductionDate] datetime2 NOT NULL,
    [Status] int NOT NULL DEFAULT 0,
    [RegisteredAt] datetime2 NOT NULL DEFAULT (getdate()),
    CONSTRAINT [PK_Cars] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Customers] (
    [Id] int NOT NULL IDENTITY,
    [FullName] nvarchar(200) NOT NULL,
    [Document] nvarchar(50) NOT NULL,
    [BirthDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [IdCar] int NOT NULL,
    [IdCustomer] int NOT NULL,
    [TotalCost] decimal(20,2) NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_Cars_IdCar] FOREIGN KEY ([IdCar]) REFERENCES [Cars] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Orders_Customers_IdCustomer] FOREIGN KEY ([IdCustomer]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ExtraOrderItems] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(200) NOT NULL,
    [Price] decimal(20,2) NOT NULL,
    [IdOrder] int NOT NULL,
    CONSTRAINT [PK_ExtraOrderItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ExtraOrderItems_Orders_IdOrder] FOREIGN KEY ([IdOrder]) REFERENCES [Orders] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_ExtraOrderItems_IdOrder] ON [ExtraOrderItems] ([IdOrder]);
GO

CREATE UNIQUE INDEX [IX_Orders_IdCar] ON [Orders] ([IdCar]);
GO

CREATE INDEX [IX_Orders_IdCustomer] ON [Orders] ([IdCustomer]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210405022640_InitialMigration', N'5.0.4');
GO

COMMIT;
GO

