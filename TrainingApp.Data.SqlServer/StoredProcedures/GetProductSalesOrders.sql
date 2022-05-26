USE [AdventureWorks2019]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[GetProductSalesOrders]
(
	@productId INT
)

AS

	SET NOCOUNT ON

	SELECT
			sod.SalesOrderID
		INTO #TempSalesOrders
		FROM Sales.SalesOrderDetail sod
		WHERE
			sod.ProductID = @productId

	SELECT
			soh.SalesOrderID
			,CAST(soh.OrderDate AS DATE) AS [OrderDate]
			,sc.AccountNumber
			,ppr.LastName AS [CustomerLastName]
			,ppr.FirstName AS [CustomerFirstName]
			,pea.EmailAddress AS [CustomerEmailAddress]
			,ppp.PhoneNumber AS [CustomerPhone]
			,pnt.Name AS [PhoneType]
			,sod.ProductID
			,sod.OrderQty
			,pp.Name AS [ProductName]
			,pp.ListPrice
			,sod.LineTotal
		FROM #TempSalesOrders tso
		INNER JOIN
		Sales.SalesOrderHeader soh
			ON tso.SalesOrderID = soh.SalesOrderID
		INNER JOIN
		Sales.SalesOrderDetail sod
			ON soh.SalesOrderID = sod.SalesOrderID
			INNER JOIN
			Production.Product pp
				ON sod.ProductID = pp.ProductID
		INNER JOIN
		Sales.Customer sc
			ON soh.CustomerID = sc.CustomerID
			INNER JOIN
			Person.Person ppr
				ON sc.PersonID = ppr.BusinessEntityID
				INNER JOIN
				Person.EmailAddress pea
					ON ppr.BusinessEntityID = pea.BusinessEntityID
				INNER JOIN
				Person.PersonPhone ppp
					ON ppr.BusinessEntityID = ppp.BusinessEntityID
					INNER JOIN
					Person.PhoneNumberType pnt
						ON ppp.PhoneNumberTypeID = pnt.PhoneNumberTypeID
		ORDER BY
			soh.OrderDate DESC
			,soh.SalesOrderID
			,pp.Name

GO


