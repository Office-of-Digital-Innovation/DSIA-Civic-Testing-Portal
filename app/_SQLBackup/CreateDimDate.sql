/***********************************************************
Use this script to create the date and time dimension
tables that can be used with PowerBI slicers
************************************************************/

BEGIN TRY
	DROP TABLE [dbo].[DimDate]
END TRY

BEGIN CATCH
	/*DO NOTHING*/
END CATCH

/*******************************************************************************************************************************************************/

CREATE TABLE [dbo].[DimDate](
	/*[ID] [int] IDENTITY(1,1) NOT NULL--Use this line if you just want an autoincrementing counter AND COMMENT BELOW LINE*/
	[ID] [int] NOT NULL--TO MAKE THE ID THE YYYYMMDD FORMAT USE THIS LINE AND COMMENT ABOVE LINE.
	, [Date] [DATETIME] NOT NULL
	, [StandardDate] [VARCHAR](10) NULL
	, [Day] [CHAR](2) NOT NULL
	, [DaySuffix] [VARCHAR](4) NOT NULL
	, [DayOfWeek] [VARCHAR](9) NOT NULL
	, [DOWInMonth] [TINYINT] NOT NULL
	, [DayOfYear] [INT] NOT NULL
	, [WeekOfMonth] [TINYINT] NOT NULL
	, [WeekOfYear] [TINYINT] NOT NULL
	, [Month] [CHAR](2) NOT NULL
	, [MonthName] [VARCHAR](9) NOT NULL
	, [Quarter] [VARCHAR] NOT NULL
	, [QuarterName] [VARCHAR](6) NOT NULL
	, [Year] [CHAR](4) NOT NULL
	, [MonthYear] [VARCHAR](10) NULL
	, [MMYYY] [VARCHAR](6) NULL
	, [FirstDayOfMonth] [DATE] NULL
	, [LastDayOfMonth] [DATE] NULL
	, [FirstDayOfQuarter] [DATE] NULL
	, [LastDayOfQuarter] [DATE] NULL
	, [FirstDayOfYear] [DATE] NULL
	, [LastDayOfYear] [DATE] NULL
	, [HolidayText] [VARCHAR](50) NULL
	CONSTRAINT [PKDimDate] PRIMARY KEY CLUSTERED 
	( [ID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
	) ON [PRIMARY]

/*******************************************************************************************************************************************************/
TRUNCATE TABLE [dbo].[DimDate]

/*IF YOU ARE USING THE YYYYMMDD format for the primary key then you need to comment out this line.
DBCC CHECKIDENT ([dbo].[DimDate], RESEED, 60000) --In case you need to add earlier dates later.*/

/*Table for counting DOW occurance in a month*/
DECLARE @tmpDOW TABLE (DOW INT, Cntr INT)
INSERT INTO @tmpDOW(DOW, Cntr) VALUES(1,0)
INSERT INTO @tmpDOW(DOW, Cntr) VALUES(2,0)
INSERT INTO @tmpDOW(DOW, Cntr) VALUES(3,0)
INSERT INTO @tmpDOW(DOW, Cntr) VALUES(4,0)
INSERT INTO @tmpDOW(DOW, Cntr) VALUES(5,0)
INSERT INTO @tmpDOW(DOW, Cntr) VALUES(6,0)
INSERT INTO @tmpDOW(DOW, Cntr) VALUES(7,0)

DECLARE
	@StartDate DATETIME
	, @EndDate DATETIME
	, @Date DATETIME
	, @WDofMonth INT
	, @CurrentMonth INT
 
SELECT
	@StartDate = '1/1/2010'
	/*Non inclusive. Stops on the day before this.*/
	, @EndDate = '1/1/2020'
	, @CurrentMonth = 1

SELECT @Date = @StartDate

/*******************************************************************************************************************************************************/

WHILE @Date < @EndDate
BEGIN

	IF DATEPART(MONTH,@Date) <> @CurrentMonth 
	BEGIN
		SELECT @CurrentMonth = DATEPART(MONTH,@Date)
		UPDATE @tmpDOW SET Cntr = 0
	END

	UPDATE @tmpDOW
	SET
		Cntr = Cntr + 1
	WHERE DOW = DATEPART(DW,@DATE)

	SELECT
		@WDofMonth = Cntr
	FROM @tmpDOW
	WHERE DOW = DATEPART(DW,@DATE) 

	INSERT INTO [dbo].[DimDate]
	([ID],/*TO MAKE THE ID THE YYYYMMDD FORMAT UNCOMMENT THIS LINE... Comment for autoincrementing.*/
	[Date]
	, [StandardDate]
	, [Day]
	, [DaySuffix]
	, [DayOfWeek]
	, [DOWInMonth]
	, [DayOfYear]
	, [WeekOfMonth] 
	, [WeekOfYear]
	, [Month]
	, [MonthName]
	, [Quarter]
	, [QuarterName]
	, [Year])
	SELECT
		CONVERT(VARCHAR,@Date,112) AS [ID], /*TO MAKE THE ID THE YYYYMMDD FORMAT UNCOMMENT THIS LINE COMMENT FOR AUTOINCREMENT*/
		@Date AS [Date]
		, RIGHT('0' + CONVERT(VARCHAR, DATEPART(MONTH,@DATE)), 2) + '/' + RIGHT('0' + CONVERT(VARCHAR, DATEPART(DAY,@DATE)), 2) + '/' + CONVERT(VARCHAR, DATEPART(YEAR,@Date)) AS [StandardDate]
		, RIGHT('0' + CONVERT(VARCHAR, DATEPART(DAY,@DATE)), 2) AS [Day]
		, CASE 
			WHEN DATEPART(DAY,@DATE) IN (11,12,13) THEN CAST(DATEPART(DAY,@DATE) AS VARCHAR) + 'th'
			WHEN RIGHT(DATEPART(DAY,@DATE),1) = 1 THEN CAST(DATEPART(DAY,@DATE) AS VARCHAR) + 'st'
			WHEN RIGHT(DATEPART(DAY,@DATE),1) = 2 THEN CAST(DATEPART(DAY,@DATE) AS VARCHAR) + 'nd'
			WHEN RIGHT(DATEPART(DAY,@DATE),1) = 3 THEN CAST(DATEPART(DAY,@DATE) AS VARCHAR) + 'rd'
			ELSE CAST(DATEPART(DAY,@DATE) AS VARCHAR)	 + 'th' 
			END AS [DaySuffix]
		, CASE
			DATEPART(DW, @DATE)
			WHEN 1 THEN 'Sunday'
			WHEN 2 THEN 'Monday'
			WHEN 3 THEN 'Tuesday'
			WHEN 4 THEN 'Wednesday'
			WHEN 5 THEN 'Thursday'
			WHEN 6 THEN 'Friday'
			WHEN 7 THEN 'Saturday'
			END AS [DayOfWeek]
		, @WDofMonth AS [DOWInMonth] /*Occurance of this day in this month. If Third Monday then 3 and DOW would be Monday.*/
		, DATEPART(dy,@Date) AS [DayOfYear] /*Day of the year. 0 - 365/366*/
		, DATEPART(ww,@Date) + 1 - DATEPART(ww,CAST(DATEPART(mm,@Date) AS VARCHAR) + '/1/' + CAST(DATEPART(yy,@Date) AS VARCHAR)) AS [WeekOfMonth]
		, DATEPART(ww,@Date) AS [WeekOfYear] /*0-52/53*/
		, RIGHT('0' + CONVERT(VARCHAR, DATEPART(MONTH,@DATE)), 2) AS [Month]
		, DATENAME(MONTH,@DATE) AS [MonthName]
		, DATEPART(qq,@DATE) AS [Quarter] /*Calendar quarter*/
		, CASE
			DATEPART(qq,@DATE) 
			WHEN 1 THEN 'First'
			WHEN 2 THEN 'Second'
			WHEN 3 THEN 'Third'
			WHEN 4 THEN 'Fourth'
			END AS [QuarterName]
		, DATEPART(YEAR,@Date) AS [Year]

	SELECT @Date = DATEADD(dd,1,@Date)
END

/*******************************************************************************************************************************************************/

/*Set first and last days of the months*/
UPDATE [dbo].[DimDate]
SET
	FirstDayOfMonth = minmax.StartDate,
	LastDayOfMonth = minmax.EndDate
FROM
[dbo].[DimDate] t,
	(
	SELECT [Month], [Quarter], [Year], MIN([Date]) AS StartDate, MAX([Date]) AS EndDate
	FROM [dbo].[DimDate]
	GROUP BY [Month], [Quarter], [Year]
	) minmax
WHERE
	t.[Month] = minmax.[Month] AND
	t.[Quarter] = minmax.[Quarter] AND
	t.[Year] = minmax.[Year] 

/*Set first and last days of the quarters*/
UPDATE [dbo].[DimDate]
SET
	FirstDayOfQuarter = minmax.StartDate,
	LastDayOfQuarter = minmax.EndDate
FROM
[dbo].[DimDate] t,
	(
	SELECT [Quarter], [Year], min([Date]) as StartDate, max([Date]) as EndDate
	FROM [dbo].[DimDate]
	GROUP BY [Quarter], [Year]
	) minmax
WHERE
	t.[Quarter] = minmax.[Quarter] AND
	t.[Year] = minmax.[Year] 

/*Set first and last days of the year*/
UPDATE [dbo].[DimDate]
SET
	FirstDayOfYear = minmax.StartDate,
	LastDayOfYear = minmax.EndDate
FROM
[dbo].[DimDate] t,
	(
	SELECT [Year], min([Date]) as StartDate, max([Date]) as EndDate
	FROM [dbo].[DimDate]
	GROUP BY [Year]
	) minmax
WHERE
	t.[Year] = minmax.[Year] 

/*Set YearMonth*/
UPDATE [dbo].[DimDate]
SET
	MonthYear = 
		CASE [Month]
		WHEN 1 THEN 'Jan'
		WHEN 2 THEN 'Feb'
		WHEN 3 THEN 'Mar'
		WHEN 4 THEN 'Apr'
		WHEN 5 THEN 'May'
		WHEN 6 THEN 'Jun'
		WHEN 7 THEN 'Jul'
		WHEN 8 THEN 'Aug'
		WHEN 9 THEN 'Sep'
		WHEN 10 THEN 'Oct'
		WHEN 11 THEN 'Nov'
		WHEN 12 THEN 'Dec'
		END + '-' + CONVERT(VARCHAR, [Year])

/*Set MMYYY*/
UPDATE [dbo].[DimDate]
SET
	MMYYY = RIGHT('0' + CONVERT(VARCHAR, [Month]),2) + CONVERT(VARCHAR, [Year])

/*******************************************************************************************************************************************************/

/*Add HOLIDAYS*/
/*THANKSGIVING - Fourth THURSDAY in November*/
UPDATE [dbo].[DimDate]
	SET HolidayText = 'Thanksgiving Day'
WHERE
	[Month] = 11 
	AND [DAYOFWEEK] = 'Thursday' 
	AND [DOWInMonth] = 4
GO

/*CHRISTMAS*/
UPDATE [dbo].[DimDate]
	SET HolidayText = 'Christmas Day'
WHERE [Month] = 12 AND [Day] = 25
GO

/*4th of July*/
UPDATE [dbo].[DimDate]
	SET HolidayText = 'Independance Day'
WHERE [Month] = 7 AND [Day] = 4
GO

/*New Years Day*/
UPDATE [dbo].[DimDate]
	SET HolidayText = 'New Year''s Day'
WHERE [Month] = 1 AND [Day] = 1
GO

/*Memorial Day - Last Monday in May*/
UPDATE [dbo].[DimDate]
	SET HolidayText = 'Memorial Day'
FROM [dbo].[DimDate]
WHERE ID IN 
	(
	SELECT
		MAX([ID])
	FROM [dbo].[DimDate]
	WHERE
		[MonthName] = 'May'
		AND [DayOfWeek] = 'Monday'
	GROUP BY
		[Year],
		[Month]
	)
GO

/*Labor Day - First Monday in September*/
UPDATE [dbo].[DimDate]
	SET HolidayText = 'Labor Day'
FROM [dbo].[DimDate]
WHERE ID IN 
	(
	SELECT
		MIN([ID])
	FROM [dbo].[DimDate]
	WHERE
		[MonthName] = 'September'
		AND [DayOfWeek] = 'Monday'
	GROUP BY
		[Year],
		[Month]
	)
GO

/*Valentine's Day*/
UPDATE [dbo].[DimDate]
	SET HolidayText = 'Valentine''s Day'
WHERE
	[Month] = 2 
	AND [Day] = 14
GO

/*Saint Patrick's Day*/
UPDATE [dbo].[DimDate]
	SET HolidayText = 'Saint Patrick''s Day'
WHERE
	[Month] = 3
	AND [Day] = 17
GO

/*Martin Luthor King Day - Third Monday in January starting in 1983*/
UPDATE [dbo].[DimDate]
	SET HolidayText = 'Martin Luthor King Jr Day'
WHERE
	[Month] = 1
	AND [Dayofweek] = 'Monday'
	AND [Year] >= 1983
	AND [DOWInMonth] = 3
GO

/*President's Day - Third Monday in February*/
UPDATE [dbo].[DimDate]
	SET HolidayText = 'President''s Day'
WHERE
	[Month] = 2
	AND [Dayofweek] = 'Monday'
	AND [DOWInMonth] = 3
GO

/*Mother's Day - Second Sunday of May*/
UPDATE [dbo].[DimDate]
	SET HolidayText = 'Mother''s Day'
WHERE
	[Month] = 5
	AND [Dayofweek] = 'Sunday'
	AND [DOWInMonth] = 2
GO

/*Father's Day - Third Sunday of June*/
UPDATE [dbo].[DimDate]
	SET HolidayText = 'Father''s Day'
WHERE
	[Month] = 6
	AND [Dayofweek] = 'Sunday'
	AND [DOWInMonth] = 3
GO

/*Halloween 10/31*/
UPDATE [dbo].[DimDate]
	SET HolidayText = 'Halloween'
WHERE
	[Month] = 10
	AND [Day] = 31
GO

/*Election Day - The first Tuesday after the first Monday in November*/
BEGIN
	BEGIN TRY
		DROP TABLE #tmpHoliday
	END TRY 
	BEGIN CATCH
		/*Do Nothing*/
	END CATCH

	CREATE TABLE #tmpHoliday(ID INT IDENTITY(1,1), DateID int, Week TINYINT, YEAR CHAR(4), DAY CHAR(2))

	INSERT INTO #tmpHoliday(DateID, [Year],[Day])
	SELECT
		[ID],
		[Year],
		[Day]
	FROM [dbo].[DimDate]
	WHERE
		[Month] = 11
		AND [Dayofweek] = 'Monday'
	ORDER BY
		YEAR,
		DAY

	DECLARE @CNTR INT, @POS INT, @STARTYEAR INT, @ENDYEAR INT, @CURRENTYEAR INT, @MINDAY INT

	SELECT
		@CURRENTYEAR = MIN([Year])
		, @STARTYEAR = MIN([Year])
		, @ENDYEAR = MAX([Year])
	FROM #tmpHoliday

	WHILE @CURRENTYEAR <= @ENDYEAR
	BEGIN
		SELECT @CNTR = COUNT([Year])
		FROM #tmpHoliday
		WHERE [Year] = @CURRENTYEAR

		SET @POS = 1

		WHILE @POS <= @CNTR
		BEGIN
			SELECT @MINDAY = MIN(DAY)
			FROM #tmpHoliday
			WHERE
				[Year] = @CURRENTYEAR
				AND [Week] IS NULL

			UPDATE #tmpHoliday
				SET [Week] = @POS
			WHERE
				[Year] = @CURRENTYEAR
				AND [Day] = @MINDAY

			SELECT @POS = @POS + 1
		END

		SELECT @CURRENTYEAR = @CURRENTYEAR + 1
	END

	UPDATE DT
		SET HolidayText = 'Election Day'
	FROM [dbo].[DimDate] DT
		JOIN #tmpHoliday HL ON (HL.DateID + 1) = DT.ID
	WHERE
		[Week] = 1

	DROP TABLE #tmpHoliday
END
GO

/*******************************************************************************************************************************************************/

CREATE UNIQUE NONCLUSTERED INDEX [IDXDimDateDate] ON [dbo].[DimDate] 
(
[Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IDXDimDateDay] ON [dbo].[DimDate] 
(
[Day] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IDXDimDateDayOfWeek] ON [dbo].[DimDate] 
(
[DayOfWeek] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IDXDimDateDOWInMonth] ON [dbo].[DimDate] 
(
[DOWInMonth] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IDXDimDateDayOfYear] ON [dbo].[DimDate] 
(
[DayOfYear] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IDXDimDateWeekOfYear] ON [dbo].[DimDate] 
(
[WeekOfYear] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IDXDimDateWeekOfMonth] ON [dbo].[DimDate] 
(
[WeekOfMonth] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IDXDimDateMonth] ON [dbo].[DimDate] 
(
[Month] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IDXDimDateMonthName] ON [dbo].[DimDate] 
(
[MonthName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IDXDimDateQuarter] ON [dbo].[DimDate] 
(
[Quarter] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IDXDimDateQuarterName] ON [dbo].[DimDate] 
(
[QuarterName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IDXDimDateYear] ON [dbo].[DimDate] 
(
[Year] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IDX_dim_Time_HolidayText] ON [dbo].[DimDate] 
(
[HolidayText] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]

SELECT
	*
FROM [dbo].[DimDate]
ORDER BY ID
