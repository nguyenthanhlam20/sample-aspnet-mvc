USE [WebClient]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 7/10/2024 12:08:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 7/10/2024 12:08:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fullname] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
	[Avatar] [nvarchar](max) NULL,
	[Cccd] [nvarchar](max) NULL,
	[Gender] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Bio] [nvarchar](max) NULL,
	[DateOfBirth] [datetime2](7) NULL,
	[Campus] [nvarchar](max) NULL,
	[Major] [nvarchar](max) NULL,
	[Class] [nvarchar](max) NULL,
	[StudentCode] [nvarchar](max) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240709170511_Migrations', N'6.0.32')
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([Id], [Fullname], [Email], [Password], [Role], [Avatar], [Cccd], [Gender], [Address], [Phone], [Bio], [DateOfBirth], [Campus], [Major], [Class], [StudentCode]) VALUES (1, N'Admin', N'admin@gmail.com', N'admin', N'Admin', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Account] ([Id], [Fullname], [Email], [Password], [Role], [Avatar], [Cccd], [Gender], [Address], [Phone], [Bio], [DateOfBirth], [Campus], [Major], [Class], [StudentCode]) VALUES (2, N'Nguyễn Thùy Linh', N'linhnthe151434@fpt.edu.vn', N'lamnthe151515', N'User', N'https://marketplace.canva.com/EAFdII60dSs/1/0/1600w/canva-lilac-brown-illustrative-cute-anime-girl-avatar-8GSg5pXqpk8.jpg', N'0303031124312', N'Nữ', N'Hà Nội', N'0385971809', N'Chào bạn.', CAST(N'2004-07-10T00:00:00.0000000' AS DateTime2), N'Hòa Lạc', N'Software Engineering', N'.NET', N'HE151434')
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
