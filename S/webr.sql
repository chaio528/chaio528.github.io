-- Q：join、inner join、left outer join、full outer join 之間有什麼區別？
--回答: join是加入其他資料表				/ join 和 inner join等效,且不會有null值
--回答: left outer join包含左表的 null值 / full outer join包含2表的null值
-- Q：'供不應求' 和 N'供不應求' 在 T-SQL 中有什麼區別？
--回答: Unicode
-- Q：你能使用 CTE（不是必須的）重寫所有的問題嗎？
--回答: 沒辦法
-- https://medium.com/jimmy-wang/sql-with-as%E9%80%B2%E8%A1%8C%E5%AD%90%E6%9F%A5%E8%A9%A2-cte-sql-004-e045147f0317
-- https://learn.microsoft.com/zh-tw/sql/t-sql/queries/with-common-table-expression-transact-sql?view=sql-server-ver16
-- Q: 你可以使用 SELECT 陳述式中的子查詢將所有問題重新寫一遍嗎？（非必需的）
--回答:不熟
-- Ex. 1
-- SELECT ProductID,
--        ProductName,
--        (SELECT s.CompanyName
--         FROM Suppliers s
--         WHERE p.SupplierID = s.SupplierID
--         ) AS SupplierName
-- FROM Products p

-- 強烈建議 sql 語法用大寫英文字母

--1 列出各產品的供應商名稱
select s.CompanyName,p.ProductName 
from Products p 
inner join Suppliers s on s.SupplierID = p.SupplierID
--2 列出各產品的種類名稱
select p.ProductName,c.CategoryName 
from Products p 
inner join Categories c on c.CategoryID = p.CategoryID
--3 列出各訂單的顧客名字
select o.OrderID,c.CompanyName 
from Orders o 
inner join Customers c on c.CustomerID = o.CustomerID
--4 列出各訂單的所負責的物流商名字以及員工名字
select o.ShipName,e.LastName+FirstName as [full Name] 
from Orders o 
inner join Employees e on e.EmployeeID = o.EmployeeID
--5 列出1998年的訂單
select * 
from Orders 
where datepart(year,OrderDate) = 1998;
--6 各產品，UnitsInStock < UnitsOnOrder 顯示'供不應求'，否則顯示'正常'
-- TODO: 要統一大小寫比較好，建議統一大寫，巢狀結構建議縮排要處理好
-- e.g.
-- SELECT
--     ProductID, ProductName, UnitsInStock, UnitsOnOrder, (
--         CASE
--             WHEN UnitsInStock < UnitsOnOrder THEN N'供不應求'
--             ELSE N'正常'
--         END
--     )AS OrderStatus
--     FROM Products
-- Q: What is the difference between '供不應求' and N'供不應求' in tsql?
--回答: Unicode
select ProductName,
Case
 when UnitsInStock < UnitsOnOrder Then N'供不應求'
 else N'正常'
 End as description
from Products
--7 取得訂單日期最新的9筆訂單
-- Q：WITH TIES 和 not WITH TIES 之間有什麼區別？
--回答:假如有同排名的,使用 WITH TIES 能將他們一起輸出
-- Q：ORDER BY 子句的默認排序是升序還是降序？
--回答:默認是升序ASC ,雖然這題我還是刻意寫OrderDate ASC
select top 9 with ties 
OrderID,OrderDate 
from Orders 
order by OrderDate ASC

--8 產品單價最便宜的第4~8名
-- Q：ORDER BY 子句的默認排序是升序還是降序？
--回答:默認是升序ASC ,雖然這題我還是刻意寫OrderDate ASC
select ProductName,UnitPrice 
from Products 
order by UnitPrice ASC offset 3 rows fetch next 5 rows only;
--9 列出每種類別中最高單價的商品
-- TODO: 
--10 列出每個訂單的總金額
select
    OrderID,
    sum(UnitPrice * Quantity * (1 - Discount)) as total
from
    [Order Details]
group by
    OrderID;
--11 列出每個物流商送過的訂單筆數
-- TODO:
--12 列出被下訂次數小於9次的產品
-- Q：為什麼使用兩個條件 p.ProductID、p.ProductName 進行分組？
-- 為什麼不僅僅使用 p.ProductID 呢？
--回答:因為不寫 p.ProductName 就無法輸出
select
    p.ProductID,
    p.ProductName,
    count(od.OrderID) as [Order Count]
from
    Products p
left join
    [Order Details]od on p.ProductID = od.ProductID
group by
    p.ProductID, p.ProductName
having
    count(od.OrderID) < 9;

-- (13、14、15請一起寫)
--13 新增物流商(Shippers)：
-- 公司名稱: 青杉人才，電話: (02)66057606
-- 公司名稱: 青群科技，電話: (02)14055374
SELECT * FROM Shippers;
insert into shippers (CompanyName, Phone)
values
    ('青杉人才', '(02)66057606'),
    ('青群科技', '(02)14055374');

--14 方才新增的兩筆資料，電話都改成(02)66057606，公司名稱結尾加'有限公司'
update shippers
set Phone = '(02)66057606'
where CompanyName in ('青杉人才', '青群科技');
--15 刪除剛才兩筆資料
-- Q: Why use like? Why not use strictly equal?
-- https://learn.microsoft.com/zh-tw/sql/t-sql/language-elements/like-transact-sql?view=sql-server-ver16
delete from shippers
where lower(companyname) like '青杉人才%'
   or lower(companyname) like '青群科技%';