USE [SA_ISYS_USER_PERMISSIONS_MANAGER]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 15/09/2022 13:55:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[LogLevel] [nvarchar](128) NULL,
	[Date] [datetime] NULL,
	[Exception] [nvarchar](max) NULL,
	[UserName] [nvarchar](200) NULL,
	[LogId] [bigint] NULL,
	[ClientIP] [nvarchar](100) NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Log] SET (LOCK_ESCALATION = DISABLE)
GO
