USE [DHHH]
GO

/****** Object:  Table [dbo].[POR_GiaoVien]    Script Date: 11/07/2012 04:51:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POR_GiaoVien](
	[ID_cb] [int] NOT NULL,
	[Mat_khau] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[Trang_thai] [bit] NULL,
 CONSTRAINT [PK_POR_GiaoVien] PRIMARY KEY CLUSTERED 
(
	[ID_cb] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[POR_GiaoVien]  WITH CHECK ADD  CONSTRAINT [FK_POR_GiaoVien_PLAN_GiaoVien] FOREIGN KEY([ID_cb])
REFERENCES [dbo].[PLAN_GiaoVien] ([ID_cb])
GO

ALTER TABLE [dbo].[POR_GiaoVien] CHECK CONSTRAINT [FK_POR_GiaoVien_PLAN_GiaoVien]
GO

