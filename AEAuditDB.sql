USE [master]
GO
/****** Object:  Database [AEAuditDB]    Script Date: 7/26/2021 12:03:29 PM ******/
CREATE DATABASE [AEAuditDB] ON  PRIMARY 
( NAME = N'AEAuditDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\AEAuditDB.mdf' , SIZE = 9216KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AEAuditDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\AEAuditDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [AEAuditDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AEAuditDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AEAuditDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AEAuditDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AEAuditDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AEAuditDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AEAuditDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [AEAuditDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AEAuditDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AEAuditDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AEAuditDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AEAuditDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AEAuditDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AEAuditDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AEAuditDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AEAuditDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AEAuditDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AEAuditDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AEAuditDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AEAuditDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AEAuditDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AEAuditDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AEAuditDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AEAuditDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AEAuditDB] SET RECOVERY FULL 
GO
ALTER DATABASE [AEAuditDB] SET  MULTI_USER 
GO
ALTER DATABASE [AEAuditDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AEAuditDB] SET DB_CHAINING OFF 
GO
EXEC sys.sp_db_vardecimal_storage_format N'AEAuditDB', N'ON'
GO
USE [AEAuditDB]
GO
/****** Object:  User [dba]    Script Date: 7/26/2021 12:03:30 PM ******/
CREATE USER [dba] FOR LOGIN [dba] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [dba]
GO
/****** Object:  UserDefinedFunction [dbo].[funcCurrentDateTime]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[funcCurrentDateTime]()
RETURNS datetime
AS
BEGIN

	RETURN getDate()

END

GO
/****** Object:  Table [dbo].[tblAllocateCourses]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAllocateCourses](
	[tAllocateID] [tinyint] IDENTITY(1,1) NOT NULL,
	[tCluster_Head_Id] [tinyint] NULL,
	[tCampus_Id] [tinyint] NULL,
	[ClusterHeadID] [varchar](20) NULL,
	[ClusterHeadName] [varchar](100) NULL,
	[Year] [varchar](10) NULL,
	[Semster] [varchar](100) NULL,
	[Program] [varchar](10) NULL,
	[Program_ShortDesc] [varchar](20) NULL,
	[Program_LongDesc] [varchar](100) NULL,
	[tFaculty_Id] [tinyint] NULL,
	[Faculty_ShortDesc] [varchar](20) NULL,
	[Faculty_LongDesc] [varchar](100) NULL,
	[ExamType] [tinyint] NULL,
	[ExamTypeName] [varchar](100) NULL,
	[CreationDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_tblClustCourseAssign] PRIMARY KEY CLUSTERED 
(
	[tAllocateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAllocateCoursesDetails]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAllocateCoursesDetails](
	[tAllocateDetailID] [tinyint] IDENTITY(1,1) NOT NULL,
	[tAllocateID] [tinyint] NULL,
	[Course_Id] [numeric](18, 0) NULL,
	[sCourse_Code] [varchar](15) NULL,
	[sCourse_LongDesc] [varchar](max) NULL,
	[Instructor] [varchar](max) NULL,
	[userID] [varchar](20) NULL,
	[Class] [varchar](50) NULL,
	[iSemester_Id] [numeric](18, 0) NULL,
	[iSemesterSection_Id] [tinyint] NULL,
	[tCampus_Id] [tinyint] NULL,
	[tProgram_Id] [tinyint] NULL,
	[CreationDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[isChecked] [bit] NULL,
 CONSTRAINT [PK_tblClustCrsAsignDetails] PRIMARY KEY CLUSTERED 
(
	[tAllocateDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAssessmentResult]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[tblAssessmentResult](
	[tAResultID] [tinyint] IDENTITY(1,1) NOT NULL,
	[tAllocateID] [tinyint] NULL,
	[tAllocateDetailID] [tinyint] NULL,
	[CourseID] [varchar](50) NULL,
	[CreationDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[Remarks] [varchar](max) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_tblAssessmentResult_1] PRIMARY KEY CLUSTERED 
(
	[tAResultID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAssessmentResultDetail]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAssessmentResultDetail](
	[tAResultDID] [tinyint] IDENTITY(1,1) NOT NULL,
	[tAResultID] [tinyint] NULL,
	[StandardID] [tinyint] NULL,
	[QuestionID] [tinyint] NULL,
	[GradePoint] [tinyint] NULL,
 CONSTRAINT [PK_tblAssessmentResultDetail] PRIMARY KEY CLUSTERED 
(
	[tAResultDID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblCampus]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblCampus](
	[tCampus_Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[sCampus_ShortDesc] [varchar](10) NOT NULL,
	[sCampus_LongDesc] [varchar](30) NOT NULL,
	[tCity_Id] [tinyint] NOT NULL,
	[sCreatedBy] [varchar](20) NOT NULL,
	[sModifiedBy] [varchar](20) NOT NULL,
	[xLastModifiedDate] [smalldatetime] NOT NULL,
	[ConrollerLtrGrd] [varchar](30) NULL,
	[AssConrollerLtrGrd] [varchar](30) NULL,
	[dbConnectionString] [varchar](50) NULL,
 CONSTRAINT [PK_tblCampus] PRIMARY KEY CLUSTERED 
(
	[tCampus_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblClusterHead]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblClusterHead](
	[tCluster_Head_Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[tCluster_ID] [tinyint] NULL,
	[tMemberID] [tinyint] NULL,
	[ClusterHeadID] [varchar](20) NULL,
	[ClusterHeadName] [varchar](max) NULL,
	[WEF_From] [datetime] NULL,
	[WEF_To] [datetime] NULL,
	[CreatedBy] [varchar](100) NULL,
	[ModifiedBy] [varchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CampusID] [tinyint] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_tblCluster] PRIMARY KEY CLUSTERED 
(
	[tCluster_Head_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblClusterInfo]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblClusterInfo](
	[tCluster_ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[ClusterTitle] [varchar](100) NULL,
	[ClusterDescription] [varchar](max) NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_tblClusterInfo] PRIMARY KEY CLUSTERED 
(
	[tCluster_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblClusterMember]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblClusterMember](
	[tMemberID] [tinyint] IDENTITY(1,1) NOT NULL,
	[tCluster_ID] [tinyint] NULL,
	[ClusterMemberID] [varchar](20) NULL,
	[ClusterMemberName] [varchar](max) NULL,
	[CreatedBy] [varchar](100) NULL,
	[ModifiedBy] [varchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_tblClusterHeadMember] PRIMARY KEY CLUSTERED 
(
	[tMemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblQuestionGrading]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblQuestionGrading](
	[tGID] [tinyint] IDENTITY(1,1) NOT NULL,
	[GradeDescription] [varchar](max) NULL,
	[complianceLevel] [tinyint] NULL,
	[GradePoint] [tinyint] NULL,
	[CreationDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_tblQuestionGrading] PRIMARY KEY CLUSTERED 
(
	[tGID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblQuestionnaire]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblQuestionnaire](
	[tQID] [tinyint] IDENTITY(1,1) NOT NULL,
	[QSNo] [tinyint] NULL,
	[Question] [varchar](max) NULL,
	[tSID] [tinyint] NULL,
	[CreationDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_tblQuestionnaire] PRIMARY KEY CLUSTERED 
(
	[tQID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblQuestionStandard]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblQuestionStandard](
	[tSID] [tinyint] IDENTITY(1,1) NOT NULL,
	[StandardNo] [tinyint] NULL,
	[StandardDescription] [varchar](max) NULL,
	[StandardPecentage] [decimal](18, 2) NULL,
	[CreationDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_tblQuestionStandard] PRIMARY KEY CLUSTERED 
(
	[tSID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblRole]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblRole](
	[RID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[Role] [varchar](20) NULL,
 CONSTRAINT [PK_tbl_Rights] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUsers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[RoleID] [int] NULL,
	[DepartID] [int] NULL,
	[CampusID] [tinyint] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_tblUsers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tmpImportClusterData]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tmpImportClusterData](
	[Cluster_Title] [varchar](100) NULL,
	[Cluster_Head_Name] [varchar](max) NULL,
	[Cluster_Head_User_Id] [varchar](20) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[DepartmenttoStandard]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [dbo].[DepartmenttoStandard]    
      
  as    
      select  tc.semster, tc.Year as searchyear ,tc.Faculty_ShortDesc as DepartName,(select cast((sum(GradePoint)/Cast(Count(StandardID) * 4 AS DECIMAL(18,2)) * 100 * 0.7) as decimal(10,2))  from tblAssessmentREsultDetail where tAResultID = RD.tAResultID and StandardID = 1) as SCore1
,      
    -- union (select(sum(GradePoint) / Cast(Count(StandardID) * 4 AS DECIMAL(16, 2))) * 100 * 0.3 from tblAssessmentREsultDetail where tAResultID = 1 and StandardID = 2)) as SCore1,      
             (select StandardDescription from tblQuestionStandard where StandardNo = 1 ) as StandardDescription                     
             from tblAllocateCoursesDetails d inner  join                
             tblAssessmentResult tb on d.tAllocateDetailID = TB.tAllocateDetailID      
            INNER JOIN tblAssessmentResultDetail RD on RD.tAResultID = tb.tAResultID     
            inner join tblAllocateCourses tc on tc.tAllocateID = tb.tAllocateID                        
             group by Year,tc.Faculty_ShortDesc, tc.semster,RD.tAResultID    
    union       
    select tc.semster, tc.Year as searchyear ,tc.Faculty_ShortDesc as DepartName,(select cast((sum(GradePoint)/Cast(Count(StandardID) * 4 AS DECIMAL(18,2)) * 100 * 0.3) as decimal(10,2)) from tblAssessmentREsultDetail where tAResultID = RD.tAResultID and  StandardID = 2)  as SCore1,   
   
    -- union (select(sum(GradePoint) / Cast(Count(StandardID) * 4 AS DECIMAL(16, 2))) * 100 * 0.3 from tblAssessmentREsultDetail where tAResultID = 1 and StandardID = 2)) as SCore1,      
           ( select StandardDescription from tblQuestionStandard where StandardNo = 2) as StandardDescription                   
             from tblAllocateCoursesDetails d inner  join                
             tblAssessmentResult tb on d.tAllocateDetailID = TB.tAllocateDetailID      
            INNER JOIN tblAssessmentResultDetail RD on RD.tAResultID = tb.tAResultID      
                  inner join tblAllocateCourses tc on tc.tAllocateID = tb.tAllocateID                        
             group by Year,tc.Faculty_ShortDesc, tc.semster, RD.tAResultID
GO
/****** Object:  View [dbo].[departmentWiseScore]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [dbo].[departmentWiseScore]    
      
  as      
      
  select  tbC.Year as searchyear, Faculty_ShortDesc as DepartName,SUM(Rd.Gradepoint) as total,tbc.semster, Faculty_LongDesc from tblAllocateCourses tbC      
  inner join tblAllocateCoursesDetails CD on tbC.tAllocateID = CD.tAllocateID      
  inner join tblAssessmentResult R on Cd.tAllocateDetailID = r.tAllocateDetailID       
  inner join tblAssessmentResultDetail Rd on R.tAllocateID = RD.tAResultID      
  group by tbc.Faculty_ShortDesc,tbC.Year,tbc.semster,Faculty_LongDesc
GO
/****** Object:  View [dbo].[GetPercetageDistributionofScore]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [dbo].[GetPercetageDistributionofScore]

			 as

			select sCourse_Code,sCourse_LongDesc,Instructor, (select Round(cast((Count(GradePoint)/Cast(11 AS DECIMAL(18,2))) * 100 as decimal(18,2)),1)   from tblAssessmentREsultDetail where tAResultID = 1 and GradePoint = 4)   as SCore1,
             (select StandardDescription from tblQuestionStandard where StandardNo = 1 ) as StandardDescription,
			 '100' +'%' as complianceLevel,
			 4 as Gradepoint            
             from tblAllocateCoursesDetails d inner  join          
             tblAssessmentResult tb on d.tAllocateDetailID = TB.tAllocateDetailID
            INNER JOIN tblAssessmentResultDetail RD on RD.tAResultID = tb.tAResultID         
             group by sCourse_Code,sCourse_LongDesc,Instructor
			 union all
			 select sCourse_Code,sCourse_LongDesc,Instructor, (select Round(cast((Count(GradePoint)/Cast(11 AS DECIMAL(18,2))) * 100 as decimal(10,2)),1)  from tblAssessmentREsultDetail where tAResultID = 1 and GradePoint = 3)   as SCore1,
           ( select StandardDescription from tblQuestionStandard where StandardNo = 2) as StandardDescription,            
              '75' +'%' as complianceLevel,
			  3 as Gradepoint  
			 from tblAllocateCoursesDetails d inner  join          
             tblAssessmentResult tb on d.tAllocateDetailID = TB.tAllocateDetailID
            INNER JOIN tblAssessmentResultDetail RD on RD.tAResultID = tb.tAResultID 
      
             group by sCourse_Code,sCourse_LongDesc,Instructor
			  union all
			 select sCourse_Code,sCourse_LongDesc,Instructor, (select  Round(cast((Count(GradePoint)/Cast(11 AS DECIMAL(18,2))) * 100 as decimal(10,2)),1)   from tblAssessmentREsultDetail where tAResultID = 1 and GradePoint = 2)   as SCore1,
           ( select StandardDescription from tblQuestionStandard where StandardNo = 2) as StandardDescription,
		     '50' +'%' as complianceLevel,
			 2 as Gradepoint             
             from tblAllocateCoursesDetails d inner  join          
             tblAssessmentResult tb on d.tAllocateDetailID = TB.tAllocateDetailID
            INNER JOIN tblAssessmentResultDetail RD on RD.tAResultID = tb.tAResultID 
 
             group by sCourse_Code,sCourse_LongDesc,Instructor
			 	  union all 
			 select sCourse_Code,sCourse_LongDesc,Instructor, (select + Round(cast((Count(GradePoint)/Cast(11 AS DECIMAL(18,2))) * 100 as decimal(10,2)),1)  from tblAssessmentREsultDetail where tAResultID = 1 and GradePoint = 1)   as SCore1,
           ( select StandardDescription from tblQuestionStandard where StandardNo = 2) as StandardDescription,
		    '25' +'%' as complianceLevel,
			 1 as Gradepoint               
             from tblAllocateCoursesDetails d inner  join          
             tblAssessmentResult tb on d.tAllocateDetailID = TB.tAllocateDetailID
            INNER JOIN tblAssessmentResultDetail RD on RD.tAResultID = tb.tAResultID 
             group by sCourse_Code,sCourse_LongDesc,Instructor
			   union all			  
			 select sCourse_Code,sCourse_LongDesc,Instructor, (select Round(cast((Count(GradePoint)/Cast(11 AS DECIMAL(18,2))) * 100 as decimal(10,2) ),1)  from tblAssessmentREsultDetail where tAResultID = 1 and GradePoint = 0)   as SCore1,
           ( select StandardDescription from tblQuestionStandard where StandardNo = 2) as StandardDescription,
		     '0' +'%' as complianceLevel,
			  0 as Gradepoint                 
             from tblAllocateCoursesDetails d inner  join          
             tblAssessmentResult tb on d.tAllocateDetailID = TB.tAllocateDetailID
            INNER JOIN tblAssessmentResultDetail RD on RD.tAResultID = tb.tAResultID 
             group by sCourse_Code,sCourse_LongDesc,Instructor
		



GO
/****** Object:  View [dbo].[GetStandardWisePicChart]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [dbo].[GetStandardWisePicChart]    
    

    as    
    
    select  c.Year as searchyear,c.Semster, sCourse_Code,sCourse_LongDesc,Instructor,Class, (select cast((sum(GradePoint)/Cast(Count(StandardID) * 4 AS DECIMAL(18,2)) * 100 * 0.7) as decimal(10,2))  from tblAssessmentREsultDetail where tAResultID = 1 and StandardID = 1)   as SCore1,    
    -- union (select(sum(GradePoint) / Cast(Count(StandardID) * 4 AS DECIMAL(16, 2))) * 100 * 0.3 from tblAssessmentREsultDetail where tAResultID = 1 and StandardID = 2)) as SCore1,    
             (select StandardDescription from tblQuestionStandard where StandardNo = 1 ) as StandardDescription                    
           from tblAllocateCoursesDetails d inner  join              
             tblAssessmentResult tb on d.tAllocateDetailID = TB.tAllocateDetailID    
            INNER JOIN tblAssessmentResultDetail RD on RD.tAResultID = tb.tAResultID   
            inner join  dbo.tblAllocateCourses c on c.tAllocateID = d.tAllocateID
             group by sCourse_Code,sCourse_LongDesc,Instructor,c.Year,c.Semster,Class   
    union     
    select c.Year as searchyear,c.Semster,  sCourse_Code,sCourse_LongDesc,Instructor, Class,(select cast((sum(GradePoint)/Cast(Count(StandardID) * 4 AS DECIMAL(18,2)) * 100 * 0.3) as decimal(10,2)) from tblAssessmentREsultDetail where tAResultID = 1 and StandardID = 2)  as SCore1,    
    -- union (select(sum(GradePoint) / Cast(Count(StandardID) * 4 AS DECIMAL(16, 2))) * 100 * 0.3 from tblAssessmentREsultDetail where tAResultID = 1 and StandardID = 2)) as SCore1,    
           ( select StandardDescription from tblQuestionStandard where StandardNo = 2) as StandardDescription     
                
             from tblAllocateCoursesDetails d inner  join              
             tblAssessmentResult tb on d.tAllocateDetailID = TB.tAllocateDetailID    
            INNER JOIN tblAssessmentResultDetail RD on RD.tAResultID = tb.tAResultID 
            inner join  dbo.tblAllocateCourses c on c.tAllocateID = d.tAllocateID            
             group by sCourse_Code,sCourse_LongDesc,Instructor,c.Year,c.Semster,Class


			 
GO
/****** Object:  View [dbo].[MidtoFinal]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [dbo].[MidtoFinal]    
    
  as    
    
  select tbc.Year as Searchyear,tbc.semster, tbC.Program,case when tbC.ExamType = 1 then 'Final' when tbC.ExamType = 2 then 'Mid Term' else 'Both' end as ExamType ,SUM(Rd.Gradepoint) as total,tbc.Program_ShortDesc as ProgramName,CD.sCourse_LongDesc as Course from tblAllocateCourses tbC    
  inner join tblAllocateCoursesDetails CD on tbC.tAllocateID = CD.tAllocateID    
  inner join tblAssessmentResult R on Cd.tAllocateDetailID = r.tAllocateDetailID     
  inner join tblAssessmentResultDetail Rd on R.tAllocateID = RD.tAResultID    
  group by tbc.Year,tbc.semster, tbC.Program,tbC.ExamType,tbc.Program_ShortDesc,CD.sCourse_LongDesc
GO
/****** Object:  View [dbo].[v_cHeads_clusterInfo]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_cHeads_clusterInfo]
AS
SELECT        dbo.tblClusterHead.tCluster_Head_Id, dbo.tblClusterHead.tCluster_ID, dbo.tblClusterHead.tMemberID, dbo.tblClusterHead.ClusterHeadID, dbo.tblClusterHead.ClusterHeadName, dbo.tblClusterHead.WEF_From, 
                         dbo.tblClusterHead.WEF_To, dbo.tblClusterHead.CampusID, dbo.tblClusterHead.Status AS HeadStatus, dbo.tblClusterInfo.ClusterTitle, dbo.tblClusterInfo.ClusterDescription, dbo.tblClusterInfo.Status AS ClusterStatus
FROM            dbo.tblClusterHead INNER JOIN
                         dbo.tblClusterInfo ON dbo.tblClusterHead.tCluster_ID = dbo.tblClusterInfo.tCluster_ID

GO
/****** Object:  View [dbo].[v_Cluster_cMember_cHead]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_Cluster_cMember_cHead]
AS
SELECT        dbo.tblClusterInfo.tCluster_ID, dbo.tblClusterInfo.ClusterTitle, dbo.tblClusterInfo.ClusterDescription, dbo.tblClusterInfo.Status, dbo.tblClusterMember.ClusterMemberID, dbo.tblClusterMember.ClusterMemberName, 
                         dbo.tblClusterHead.ClusterHeadName
FROM            dbo.tblClusterInfo INNER JOIN
                         dbo.tblClusterMember ON dbo.tblClusterInfo.tCluster_ID = dbo.tblClusterMember.tCluster_ID INNER JOIN
                         dbo.tblClusterHead ON dbo.tblClusterMember.tCluster_ID = dbo.tblClusterHead.tCluster_ID

GO
/****** Object:  View [dbo].[Vu_CampustoStandard]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[Vu_CampustoStandard]         
  as    
     select  tc.semster, tc.Year as searchyear,(select cast((sum(GradePoint)/Cast(Count(StandardID) * 4 AS DECIMAL(18,2)) * 100 * 0.7) as decimal(10,2))  from tblAssessmentREsultDetail where StandardID = 1) as SCore1,
	 camp.sCampus_ShortDesc as campusname,
	 
    -- union (select(sum(GradePoint) / Cast(Count(StandardID) * 4 AS DECIMAL(16, 2))) * 100 * 0.3 from tblAssessmentREsultDetail where tAResultID = 1 and StandardID = 2)) as SCore1,      
             (select StandardDescription from tblQuestionStandard where StandardNo = 1 ) as StandardDescription                     
             from tblAllocateCoursesDetails d inner  join                
             tblAssessmentResult tb on d.tAllocateDetailID = TB.tAllocateDetailID      
            INNER JOIN tblAssessmentResultDetail RD on RD.tAResultID = tb.tAResultID     
            inner join tblAllocateCourses tc on tc.tAllocateID = tb.tAllocateID                        
             inner join tblCampus camp on camp.tCampus_Id = tc.tCampus_Id 
			 group by Year, tc.semster, tc.tCampus_Id,camp.sCampus_ShortDesc  

    union       
    select  tc.semster, tc.Year as searchyear ,(select cast((sum(GradePoint)/Cast(Count(StandardID) * 4 AS DECIMAL(18,2)) * 100 * 0.3) as decimal(10,2))  from tblAssessmentREsultDetail where StandardID = 2) as SCore1,
	  camp.sCampus_ShortDesc as campusname,
     
    -- union (select(sum(GradePoint) / Cast(Count(StandardID) * 4 AS DECIMAL(16, 2))) * 100 * 0.3 from tblAssessmentREsultDetail where tAResultID = 1 and StandardID = 2)) as SCore1,      
             (select StandardDescription from tblQuestionStandard where StandardNo = 2 ) as StandardDescription                     
             from tblAllocateCoursesDetails d inner  join                
             tblAssessmentResult tb on d.tAllocateDetailID = TB.tAllocateDetailID      
            INNER JOIN tblAssessmentResultDetail RD on RD.tAResultID = tb.tAResultID     
            inner join tblAllocateCourses tc on tc.tAllocateID = tb.tAllocateID 
			  inner join tblCampus camp on camp.tCampus_Id = tc.tCampus_Id 
			 group by Year, tc.semster, tc.tCampus_Id ,camp.sCampus_ShortDesc                        
             
   
GO
ALTER TABLE [dbo].[tblClusterHead]  WITH NOCHECK ADD  CONSTRAINT [FK_tblClusterHead_tblClusterInfo] FOREIGN KEY([tCluster_ID])
REFERENCES [dbo].[tblClusterInfo] ([tCluster_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblClusterHead] NOCHECK CONSTRAINT [FK_tblClusterHead_tblClusterInfo]
GO
ALTER TABLE [dbo].[tblQuestionnaire]  WITH CHECK ADD  CONSTRAINT [FK_tblQuestionnaire_tblQuestionStandard] FOREIGN KEY([tSID])
REFERENCES [dbo].[tblQuestionStandard] ([tSID])
GO
ALTER TABLE [dbo].[tblQuestionnaire] CHECK CONSTRAINT [FK_tblQuestionnaire_tblQuestionStandard]
GO
ALTER TABLE [dbo].[tblUsers]  WITH CHECK ADD  CONSTRAINT [FK_tblUsers_tblRole] FOREIGN KEY([RoleID])
REFERENCES [dbo].[tblRole] ([RoleID])
GO
ALTER TABLE [dbo].[tblUsers] CHECK CONSTRAINT [FK_tblUsers_tblRole]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetCourse_Instructor_ByYearSemsProg]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_GetCourse_Instructor_ByYearSemsProg]
@Year int,
@SemsterID int,
@ProgramID varchar(10)
AS
BEGIN
SELECT * FROM 
(SELECT c.sCourse_Code,c.sCourse_LongDesc,
REPLACE(up.sUserProfile_FirstName+' '+up.sUserProfile_MiddleName+' '+up.sUserProfile_LastName,'  ',' ') Instructor,
	  REPLACE(RTRIM(LTRIM(prog.sProgram_ShortDesc))+' '+RTRIM(LTRIM(sem.tSemester_No))+'-'+ss.cSemesterSection_Name,'  ',' ') Class
	  ,iOfferedCourses_Course_Id as Course_Id,oct.sUser_Id,oct.iSemester_Id,oct.iSemesterSection_Id,sem.tCampus_Id,sem.tProgram_Id
  FROM OfferedCourseTea oct 
  INNER JOIN Course C ON oct.iOfferedCourses_Course_Id = c.iCourse_Id 
  INNER JOIN Semester sem INNER JOIN 
  Program prog on sem.tCampus_Id = prog.tCampus_Id AND sem.tProgram_Id = prog.tProgram_Id AND sem.tDegree_Id = prog.tDegree_Id  ON oct.iSemester_Id = sem.iSemester_Id
  INNER JOIN UserProfile up on oct.sUser_Id = up.sUser_Id
  INNER JOIN SemesterSection SS on oct.iSemester_Id = ss.iSemester_Id and oct.iSemesterSection_Id = ss.iSemesterSection_Id
  where hSemester_Year = @Year and tSemesterType_Id = @SemsterID
  and rtrim(ltrim(cast(sem.tCampus_Id as varchar(3))))+'-'+rtrim(ltrim(cast(sem.tProgram_Id as varchar(3))))+'-'+rtrim(ltrim(cast(sem.tDegree_Id as varchar(3))))=@ProgramID) crsCourseData
  ORDER BY sCourse_LongDesc,Instructor
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetCourseByYearSemsProg]    Script Date: 7/26/2021 12:03:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_GetCourseByYearSemsProg]
@Year int,
@SemsterID int,
@ProgramID varchar(10)
AS
BEGIN
SELECT distinct c.iCourse_Id,c.sCourse_Code,c.sCourse_LongDesc from OfferedCourses ofc 
	inner join Semester sem on ofc.iSemester_Id = sem.iSemester_Id
	inner join Course c on ofc.iCourse_Id = c.iCourse_Id
	where sem.hSemester_Year=@Year and sem.tSemesterType_Id=@SemsterID and cast(tCampus_Id as varchar(4))+'-'+cast(tDegree_Id as varchar(4))+'-'+cast(tProgram_Id as varchar(4))=@ProgramID
  --where sem.hSemester_Year=2015 and sem.tSemesterType_Id = 3 and tCampus_Id=1 and tDegree_Id=1 and tProgram_Id =2
  order by sCourse_LongDesc
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[17] 2[14] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tblClusterHead"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 196
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tblClusterInfo"
            Begin Extent = 
               Top = 6
               Left = 262
               Bottom = 183
               Right = 448
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 21
         Width = 284
         Width = 1530
         Width = 1050
         Width = 1095
         Width = 1665
         Width = 2160
         Width = 1980
         Width = 795
         Width = 1020
         Width = 1095
         Width = 1080
         Width = 2595
         Width = 1020
         Width = 675
         Width = 1080
         Width = 2595
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_cHeads_clusterInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_cHeads_clusterInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[21] 2[14] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tblClusterInfo"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 179
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "tblClusterMember"
            Begin Extent = 
               Top = 6
               Left = 316
               Bottom = 202
               Right = 519
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tblClusterHead"
            Begin Extent = 
               Top = 6
               Left = 557
               Bottom = 186
               Right = 743
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1050
         Width = 1080
         Width = 2520
         Width = 675
         Width = 1590
         Width = 2205
         Width = 2205
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_Cluster_cMember_cHead'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_Cluster_cMember_cHead'
GO
USE [master]
GO
ALTER DATABASE [AEAuditDB] SET  READ_WRITE 
GO
