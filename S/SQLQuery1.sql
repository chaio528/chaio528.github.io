--USE [Northwind]

--1 列出各產品的供應商名稱
select s.CompanyName,p.ProductName from Products p inner join Suppliers s on s.SupplierID = p.SupplierID
--2 列出各產品的種類名稱
select p.ProductName,c.CategoryName from Products p inner join Categories c on c.CategoryID = p.CategoryID
--3 列出各訂單的顧客名字
select o.OrderID,c.CompanyName from Orders o inner join Customers c on c.CustomerID = o.CustomerID 
--4 列出各訂單的所負責的物流商名字以及員工名字
select o.ShipName,e.LastName+FirstName as [full Name] from Orders o inner join Employees e on e.EmployeeID = o.EmployeeID
--5 列出1998年的訂單
select * from Orders where datepart(year,OrderDate) = 1998;
--6 各產品，UnitsInStock < UnitsOnOrder 顯示'供不應求'，否則顯示'正常'
select ProductName,
Case 
 when UnitsInStock < UnitsOnOrder Then '供不應求'
 else '正常'
 End as description
from Products
--7 取得訂單日期最新的9筆訂單
select top 9 with ties OrderID,OrderDate from Orders order by OrderDate ASC
--8 產品單價最便宜的第4~8名
select ProductName,UnitPrice from Products order by UnitPrice ASC offset 3 rows fetch next 5 rows only;
--9 列出每種類別中最高單價的商品
select
    c.categoryid,
    c.categoryname,
    p.productid,
    p.productname,
    p.unitprice
from
    categories c
inner join
    products p on p.categoryid = c.categoryid
inner join
    (
        select
            categoryid,
            max(unitprice) as maxprice
        from
            products
        group by
            categoryid
    ) maxprices on p.categoryid = maxprices.categoryid and p.unitprice = maxprices.maxprice;
--10 列出每個訂單的總金額
select
    OrderID,
    sum(UnitPrice * Quantity * (1 - Discount)) as total
from
    [Order Details]
group by
    OrderID;
--11 列出每個物流商送過的訂單筆數

--12 列出被下訂次數小於9次的產品
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
delete from shippers
where lower(companyname) like '青杉人才%'
   or lower(companyname) like '青群科技%';

