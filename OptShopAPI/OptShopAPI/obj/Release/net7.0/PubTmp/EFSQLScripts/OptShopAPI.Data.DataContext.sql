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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240130112624_Initial Migration')
BEGIN
    CREATE TABLE [customers] (
        [id] int NOT NULL IDENTITY,
        [username] nvarchar(max) NOT NULL,
        [email] nvarchar(max) NOT NULL,
        [phoneNumber] nvarchar(max) NOT NULL,
        [country] nvarchar(max) NOT NULL,
        [city] nvarchar(max) NOT NULL,
        [streetAddress] nvarchar(max) NOT NULL,
        [postalCode] nvarchar(max) NULL,
        [zip_code] nvarchar(max) NULL,
        [province] nvarchar(max) NULL,
        [plz] nvarchar(max) NULL,
        [orderIds] nvarchar(max) NULL,
        [houseNumber] nvarchar(max) NOT NULL,
        [mailbox] nvarchar(max) NULL,
        [eircode] nvarchar(max) NULL,
        CONSTRAINT [PK_customers] PRIMARY KEY ([id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240130112624_Initial Migration')
BEGIN
    CREATE TABLE [orders] (
        [Id] uniqueidentifier NOT NULL,
        [productId] int NOT NULL,
        [productCount] int NOT NULL,
        [color] nvarchar(max) NULL,
        [description] nvarchar(max) NULL,
        [status] nvarchar(max) NULL,
        [totalPrice] int NULL,
        CONSTRAINT [PK_orders] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240130112624_Initial Migration')
BEGIN
    CREATE TABLE [payments] (
        [Id] uniqueidentifier NOT NULL,
        [orderids] nvarchar(max) NULL,
        [totalPrice] int NOT NULL,
        [alreadyPaid] int NOT NULL,
        [deliveryPrice] int NOT NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_payments] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240130112624_Initial Migration')
BEGIN
    CREATE TABLE [products] (
        [id] int NOT NULL IDENTITY,
        [name] nvarchar(200) NOT NULL,
        [description] nvarchar(max) NOT NULL,
        [price] int NOT NULL,
        [characters] nvarchar(max) NOT NULL,
        [photoName] nvarchar(max) NULL,
        [color] nvarchar(max) NULL,
        [minimalCount] int NOT NULL,
        [photoSrc] nvarchar(max) NULL,
        CONSTRAINT [PK_products] PRIMARY KEY ([id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240130112624_Initial Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240130112624_Initial Migration', N'7.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240130182516_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240130182516_InitialCreate', N'7.0.13');
END;
GO

COMMIT;
GO

