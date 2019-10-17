USE [dummy]
GO

/****** Object:  Table [dbo].[ex_nfe]    Script Date: 09/09/2019 11:23:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ex_nfe](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nfeid] [int] NOT NULL,
	[extra] [nvarchar](50) NULL,
 CONSTRAINT [PK_ex_nfe] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


