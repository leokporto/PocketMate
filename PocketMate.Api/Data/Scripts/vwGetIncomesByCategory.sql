CREATE OR ALTER VIEW [vwGetIncomesByCategory] AS
    SELECT
        [Transaction].[UserId],
        [Category].[Title] AS [Category],
        YEAR([Transaction].[AccountedAt]) AS [Year],
        SUM([Transaction].[Amount]) AS [Incomes]
    FROM
        [Transaction]
            INNER JOIN [Category]
                       ON [Transaction].[CategoryId] = [Category].[Id]
    WHERE
        [Transaction].[AccountedAt]
            >= DATEADD(MONTH, -11, CAST(GETDATE() AS DATE))
      AND [Transaction].[AccountedAt]
        < DATEADD(MONTH, 1, CAST(GETDATE() AS DATE))
      AND [Transaction].[Type] = 1
    GROUP BY
        [Transaction].[UserId],
        [Category].[Title],
        YEAR([Transaction].[AccountedAt])