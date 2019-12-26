USE master
GO 

/*
-- ROLLBACK

USE master
GO 

DROP PROCEDURE dbo.PivotTabelaDeClasseInternacional

-- TESTE

*/

CREATE PROCEDURE PivotTabelaDeClasseInternacional (
	@numerosDeProcessos VARCHAR(MAX)
)
AS
BEGIN

	IF OBJECT_ID('tempDB..#TmpClassesInternacionais', 'U') IS NOT NULL
		DROP TABLE #TmpClassesInternacionais


	SELECT @maxNcls = MAX(x.Qtd) FROM (
		SELECT COUNT(n.ncl) AS Qtd
		FROM Apol.dbo.mi_nclsPorProcesso n
		INNER JOIN dbo.Split(@procs, ',') AS ps ON ps.Campo = n.codigoProcesso
		GROUP BY n.codigoProcesso
	) x



	-- DYNAMIC PIVOT TABLE

	SELECT 'CLASSE_INTERNACIONAL_' + CAST(ROW_NUMBER() OVER(PARTITION BY n.codigoProcesso ORDER BY n.codigoProcesso DESC) AS VARCHAR) AS [Column01]
	, n.codigoProcesso
	, n.ncl
	, n.especificacao
	INTO #TmpClassesInternacionais
	FROM Apol.dbo.mi_nclsPorProcesso AS n
	INNER JOIN Apol.dbo.Split(@numerosDeProcessos, ',') AS ps ON ps.Campo = n.codigoProcesso
	--INNER JOIN Apol.dbo.mi_processos AS p ON p.codigo = n.codigoProcesso
	--WHERE n.codigoProcesso IN (115673,115674,115675,115676,115677,115678,115679,115680,115681,115682,115683,225285,225286)
	GROUP BY n.codigoProcesso, n.ncl, n.especificacao

	DECLARE @SQLStr VARCHAR(MAX)
	--DECLARE @SQLStrColumns VARCHAR(MAX)

	SET @SQLStr = ''
	--SET @SQLStrColumns = ''

	SELECT @SQLStr = @SQLStr + '[' + [a].[Column01] + '], ' -- , @SQLStrColumns = @SQLStrColumns + '[' + [a].[Column01] + '] VARCHAR(50) NULL, '
	FROM ( 
		SELECT DISTINCT [Column01]
		FROM #TmpClassesInternacionais
	) a

	SET @SQLStr = LEFT(@SQLStr, len(@SQLStr) - 1)
	--SET @SQLStrColumns = LEFT(@SQLStrColumns, len(@SQLStrColumns) - 1)

	--PRINT @SQLStr
	--PRINT @SQLStrColumns

	--DECLARE @SQLStrTable NVARCHAR(MAX)

	IF OBJECT_ID('tempDB..##TmpClassesInternacionaisColunas', 'U') IS NOT NULL
		DROP TABLE ##TmpClassesInternacionaisColunas

	--SET @SQLStrTable = 'CREATE TABLE ##TmpClassesInternacionaisColunas (CodigoDoProcesso INT NULL, ' + @SQLStrColumns + ')'

	--PRINT @SQLStrTable

	--EXEC (@SQLStrTable)

	--DROP TABLE ##TmpClassesInternacionaisColunas

	SET @SQLStr = 'SELECT codigoProcesso, '
	 +  @SQLStr
	 --+ ' INTO ##TmpClassesInternacionaisColunas '
	 + ' FROM ('
	 + '  SELECT t.[Column01], t.codigoProcesso, t.ncl, t.especificacao '
	 + '  FROM #TmpClassesInternacionais AS t '
	 + ' ) sq PIVOT (MAX(ncl) FOR [Column01] IN (' + @SQLStr + ')) AS pt '
	 + '  ORDER BY codigoProcesso '

	--PRINT @SQLStr

	EXEC (@SQLStr)

	--EXEC ('SELECT * FROM ##TmpClassesInternacionaisColunas')

END
/*
USE Apol
GO

SELECT * 
INTO #myUpdatedTable 
EXEC master.dbo.PivotTabelaDeClasseInternacional @numerosDeProcessos = '115673,115674,115675,115676,115677,115678,115679,115680,115681,115682,115683,225285,225286'

SELECT #myUpdatedTable
*/