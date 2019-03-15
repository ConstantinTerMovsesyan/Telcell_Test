
--1. Запрос для получения выборки вида: Дата (сутки), Колличество новых Paste за сутки
select  CONVERT(date, CreatedDate) as day, count(id) as created_pastes 
from pastes 
group by CONVERT(date, CreatedDate);

--2. Запрос для получения выборки вида: Дата (сутки), Самый последний Paste к которому был доступ в сутках
select  CONVERT(date, p1.AccessDate) as day, p1.*
from pastes p1 
where p1.AccessDate  = 
    (select max(p2.AccessDate) 
     from pastes p2 
     where CONVERT(date, p2.AccessDate) = CONVERT(date, p1.AccessDate) 
	 );

--3. Запрос для получения ближайшей даты (суток) относительно указанного параметра X, в которую не было ни одного доступа к любому Paste
DECLARE @P_Date DATETIME ='2019-01-16';
select 
CASE 
     WHEN 
	 (select max(CONVERT(date, AccessDate)) from pastes ) < @P_Date or  
	 (select min(CONVERT(date, AccessDate)) from pastes ) > @P_Date 
	  THEN @P_Date
    
     ELSE (
		select top 1
		CASE   
			WHEN diff_dfrom > diff_dto THEN dto   
			ELSE dfrom    
		END res
		from(
			select 
			
				DATEADD(day, 1, days1.d) dfrom, 
				DATEADD(day, -1, days2.d) dto, 
				ABS(DATEDIFF(day, DATEADD(day, 1, days1.d), @P_Date)) diff_dfrom , 
				ABS(DATEDIFF(day, DATEADD(day, -1, days2.d), @P_Date)) diff_dto
			from 
			(select ROW_NUMBER() OVER(ORDER BY days.d) n, days.d 
			from(select CONVERT(date, AccessDate) d from pastes group by CONVERT (date, AccessDate)) as days) days1,
			(select ROW_NUMBER() OVER(ORDER BY days.d) n, days.d 
			from(select CONVERT(date, AccessDate) d from pastes group by CONVERT (date, AccessDate)) as days) days2
			where 
			days1.n + 1 = days2.n 
			and DATEDIFF(day, days1.d , days2.d) > 1 
			
		) 
		as missing_ranges 
		order by CASE   
			WHEN diff_dfrom > diff_dto THEN diff_dto   
			ELSE diff_dfrom    
		END

) END

select * from pastes
update pastes set AccessDate = '2019-01-16 19:05:19.543' where id='WDsbUTSU'
update pastes set AccessDate = '2019-01-15 19:05:19.543' where id='1Kp1izF2'
