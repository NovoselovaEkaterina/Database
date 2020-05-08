--+1 Посчитать прибыль за заданную дату. 
select sum(debit-credit) from operations where create_date between '09-04-20 00:00:00' and '10-04-20 00:00:00';

--+2 Вывести наименования всех статей, в рамках которых не проводилось операций за заданный период времени. 
select name from articles where id not in(select article_id from operations where 
create_date between '05-04-20 00:00:00' and '21-04-20 00:00:00');

--+3 Вывести операции и наименования статей, включая статьи, в рамках которых не проводились операции. 
select operations.*, articles.name from operations right join articles on operations.article_id=articles.id;

--+4 Вывести число балансов, в которых учтены операции принадлежащие статье с заданным наименованием.
select count(balance.id) from balance join operations on 
balance.id=operations.balance_id join articles on operations.article_id=articles.id and articles.name='Salary';

--!5 Вывести сумму расходов по заданной статье, агрегируя по сформированным балансам за указанный период времени. 
select sum(credit), balance_id from operations where
article_id=3 and create_date between '09-04-20 10:00:00' and '21-04-20 10:00:00' group by balance_id;

--!1 Добавить новую статью. 
declare 
rt_id NUMBER;
begin
insert into articles(name) values('Send mail') returning id into rt_id;

--+2 Добавить операцию в рамках статьи из п.1. 
insert into operations(article_id, debit, credit, create_date)
values(rt_id,1000,0,'07-04-20 10:00:00');
end;

--!1 Сформировать баланс. Если сумма прибыли меньше некоторой суммы – транзакцию откатить. 
set autocommit=0;
declare 
am NUMBER;
ret_id NUMBER;
begin
select sum(debit-credit) into am from operations where create_date between '01-04-20 10:00:00' and '08-04-20 10:00:00';
insert into balance(create_date, debit,credit,amount) values(
'08-04-20 10:00:00',
(select sum(debit) from operations where create_date between '01-04-20 10:00:00' and '08-04-20 10:00:00'),
(select sum(credit) from operations where create_date between '01-04-20 10:00:00' and '08-04-20 10:00:00'),
(select sum(debit-credit) from operations where create_date between '01-04-20 10:00:00' and '08-04-20 10:00:00'))
returning id into ret_id;
update operations set balance_id=ret_id where create_date between '01-04-20 10:00:00' and '08-04-20 10:00:00';
if (am<100) then rollback;
end if;
commit;
end;

--+1 Удалить статью и операции, выполненные в ее рамках. 
delete operations where article_id='1';
delete articles where id='1';
--+1 Удалить в рамках транзакции самый убыточный баланс и операции. 
set autocommit=0;
begin
delete operations where balance_id in (select id from balance where amount = (select min(amount) from balance));
delete balance where amount = (select min(amount) from balance);
commit;
end;
--!2 то же, что и п.1, но, если в удаленном балансе использовались статьи, 
--операции в рамках которых больше нигде не проводились – транзакцию откатить.
set autocommit=0;
declare tmp number;
begin
--select count(id) into tmp from articles where id in
--((select article_id from operations where balance_id in
--(select id from balance where amount = (select min(amount) from balance)))
--minus (select article_id from operations where balance_id not in
--(select id from balance where amount = (select min(amount) from balance))));

select count(article_id) into tmp from operations op 
where  balance_id in (select id from balance where amount = (select min(amount) from balance)) 
and (select count(balance_id) from operations where op.article_id=article_id 
and balance_id not in (select id from balance where amount = (select min(amount) from balance)))<1;

delete operations where balance_id in (select id from balance where amount = (select min(amount) from balance));
delete balance where amount = (select min(amount) from balance);
if (tmp>0)
then rollback;
else commit;
end if;
end;

--+1 Увеличить сумму расхода операций, выполненных в рамках статьи, заданной по наименованию, на заданную величину. 
--Расход и прибыль сформированных на основании модифицируемых операций балансов должны быть пересчитаны.  
update operations set credit = credit+10 where article_id=(select id from articles where name='Salary');
update balance set credit=credit+10, amount=amount-10 where 
id=(select balance_id from operations where article_id=(select id from articles where name='Salary'));

--+1 В рамках транзакции поменять заданную статью во всех операциях на другую и удалить ее.
set autocommit=0;
begin
update operations set article_id=2 where article_id=(select id from articles where name='Salary');
delete articles where name='Salary';
commit;
end;

--+2 то же, что и п.1, но транзакцию откатить. 
set autocommit=0;
begin
update operations set article_id=3 where article_id=(select id from articles where name='Taxes');
delete articles where name='Taxes';
rollback;
end;


