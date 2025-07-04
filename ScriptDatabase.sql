USE [MusicSchoolsDb]
GO
/****** Object:  Table [dbo].[MusicSchools]    Script Date: 13/06/2025 15:13:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MusicSchools](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 13/06/2025 15:13:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[StudentNumber] [nvarchar](50) NOT NULL,
	[MusicSchoolId] [uniqueidentifier] NOT NULL,
	[TeacherId] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[StudentNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentTeacher]    Script Date: 13/06/2025 15:13:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentTeacher](
	[StudentId] [uniqueidentifier] NOT NULL,
	[TeacherId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC,
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 13/06/2025 15:13:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teachers](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[TeacherNumber] [nvarchar](50) NOT NULL,
	[MusicSchoolId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[TeacherNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD FOREIGN KEY([MusicSchoolId])
REFERENCES [dbo].[MusicSchools] ([Id])
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teachers] ([Id])
GO
ALTER TABLE [dbo].[StudentTeacher]  WITH CHECK ADD FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([Id])
GO
ALTER TABLE [dbo].[StudentTeacher]  WITH CHECK ADD FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teachers] ([Id])
GO
ALTER TABLE [dbo].[Teachers]  WITH CHECK ADD FOREIGN KEY([MusicSchoolId])
REFERENCES [dbo].[MusicSchools] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[sp_ManageSchool]    Script Date: 13/06/2025 15:13:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_ManageSchool]
    @Operation NVARCHAR(10),
    @Id UNIQUEIDENTIFIER = NULL,
    @Name NVARCHAR(100) = NULL,
    @Description NVARCHAR(255) = NULL,
    @Code NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Operation = 'CREATE'
    BEGIN
        INSERT INTO MusicSchools (Id, Name, Description, Code)
        VALUES (NEWID(), @Name, @Description, @Code);
    END
    ELSE IF @Operation = 'READ'
    BEGIN
        SELECT * FROM MusicSchools WHERE Id = @Id;
    END
    ELSE IF @Operation = 'UPDATE'
    BEGIN
        UPDATE MusicSchools
        SET Name = @Name, Description = @Description, Code = @Code
        WHERE Id = @Id;
    END
    ELSE IF @Operation = 'DELETE'
    BEGIN
        DELETE FROM MusicSchools WHERE Id = @Id;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ManageStudent]    Script Date: 13/06/2025 15:13:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_ManageStudent]
    @Operation NVARCHAR(10),
    @Id UNIQUEIDENTIFIER = NULL,
    @FirstName NVARCHAR(100) = NULL,
    @LastName NVARCHAR(100) = NULL,
    @BirthDate DATE = NULL,
    @StudentNumber NVARCHAR(50) = NULL,
    @MusicSchoolId UNIQUEIDENTIFIER = NULL,
    @TeacherId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Operation = 'CREATE'
    BEGIN
        INSERT INTO Students (Id, FirstName, LastName, BirthDate, StudentNumber, MusicSchoolId, TeacherId)
        VALUES (NEWID(), @FirstName, @LastName, @BirthDate, @StudentNumber, @MusicSchoolId, @TeacherId);
    END
    ELSE IF @Operation = 'READ'
    BEGIN
        SELECT * FROM Students WHERE Id = @Id;
    END
    ELSE IF @Operation = 'UPDATE'
    BEGIN
        UPDATE Students
        SET FirstName = @FirstName, LastName = @LastName, BirthDate = @BirthDate,
            StudentNumber = @StudentNumber, MusicSchoolId = @MusicSchoolId, TeacherId = @TeacherId
        WHERE Id = @Id;
    END
    ELSE IF @Operation = 'DELETE'
    BEGIN
        DELETE FROM Students WHERE Id = @Id;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ManageTeacher]    Script Date: 13/06/2025 15:13:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_ManageTeacher]
    @Operation NVARCHAR(10),
    @Id UNIQUEIDENTIFIER = NULL,
    @FirstName NVARCHAR(100) = NULL,
    @LastName NVARCHAR(100) = NULL,
    @TeacherNumber NVARCHAR(50) = NULL,
    @MusicSchoolId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Operation = 'CREATE'
    BEGIN
        INSERT INTO Teachers (Id, FirstName, LastName, TeacherNumber, MusicSchoolId)
        VALUES (NEWID(), @FirstName, @LastName, @TeacherNumber, @MusicSchoolId);
    END
    ELSE IF @Operation = 'READ'
    BEGIN
        SELECT * FROM Teachers WHERE Id = @Id;
    END
    ELSE IF @Operation = 'UPDATE'
    BEGIN
        UPDATE Teachers
        SET FirstName = @FirstName, LastName = @LastName, TeacherNumber = @TeacherNumber, MusicSchoolId = @MusicSchoolId
        WHERE Id = @Id;
    END
    ELSE IF @Operation = 'DELETE'
    BEGIN
        DELETE FROM Teachers WHERE Id = @Id;
    END
END
GO
