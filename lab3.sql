--������������� 

--������� �������������, ������������ ��� ������ � ����� ������/������ ���������� ��������. 
create view allarticles as 
    select articles.id id, sum(operations.debit) debitsum, sum(operations.credit) creditsum from articles 
    left join operations on articles.id=operations.article_id and operations.balance_id is null group by articles.id;
select * from allarticles;
--������� �������������, ������������ ��� ������� � ����� ��������, �� ��������� ������� ��� ���� ������������. 
create view allbalances as 
    select balance.id id,count(operations.id) countop from balance join 
    operations on balance.id=operations.balance_id group by balance.id;
select * from allbalances;

--�������� ��������� 
--��� ����������: 
--������� �������� ���������, ��������� ��� �������� ���������� ������� � ������� �� ������. 
create or replace procedure lastBalanceOperations is 
begin
    for i in 
    (select id as ID_, (debit-credit) as AMOUNT_ from operations where balance_id=(select max(id) from balance))
    loop
    DBMS_OUTPUT.enable;
    DBMS_OUTPUT.PUT_LINE('ID: '||i.ID_||' | AMOUNT: '||i.AMOUNT_);
    end loop;
end lastBalanceOperations;
exec lastBalanceOperations;

--� �������� �����������: 
--������� �������� ���������, ������� ��� ��������� �������1� � �������2�. 
--��� ������ ���������� �������, �������� �� �������1� � ������� ��������� ������� �������, ��� �� �������2�.
--���� � ������� ����������� �������� �� ����� �� ������ � �� �� ���������������. 
create or replace procedure biggestAmount(arg1 in number, arg2 in number) is
BEGIN
    for j in 
    (select id as ID_ from balance b where
    exists (select id from operations where balance_id = b.id and article_id = arg1)
    and exists (select id from operations where balance_id = b.id and article_id = arg2)
    and (select sum(debit-credit) from operations where
    balance_id = b.id and article_id = arg1)
    >
    (select sum(debit-credit) from operations where
    balance_id = b.id and article_id = arg2))
    loop
    DBMS_OUTPUT.enable;
    DBMS_OUTPUT.PUT_LINE('ID: '||j.ID_);
    end loop;
end biggestAmount;
exec biggestAmount(1,3);
--� ��������� �����������: 
--������� �������� ��������� � ������� ���������� ������� � �������� ���������� � ������, 
--�������� �� ������� ��������� � ����������� ���������. 
create or replace procedure biggestCreditArticle(bal_id in number, art_id out number) is 
begin
    select article_id into art_id from operations op where balance_id=bal_id and 
    ((select sum(credit) from operations where article_id=op.article_id and balance_id=bal_id)
    =
    (select max(sum(credit)) from operations group by article_id,balance_id having balance_id=bal_id));
end biggestCreditArticle;

declare
ar NUMBER;
begin
biggestCreditArticle(21, ar);
end;

--�������� 
--�������� �� �������: 
--������� �������, ������� �� ��������� ������� ������ � ������� �������� � �������� ��� ���������� �����. 
create or replace trigger createBalance
before insert on balance 
for each row
begin
    if :new.debit=0 or :new.credit=0 or :new.create_date is NULL
    then raise_application_error(-20000,'Wrong data');
    end if;
end;

insert into balance(create_date, debit,credit,amount) values(
'08-04-20 10:00:00',
(select sum(debit) from operations where create_date between '01-04-20 10:00:00' and '08-04-20 10:00:00'),
0,
(select sum(debit) from operations where create_date between '01-04-20 10:00:00' and '08-04-20 10:00:00')-
(select sum(credit) from operations where create_date between '01-04-20 10:00:00' and '08-04-20 10:00:00'));


--�������� �� �����������: 
--������� �������, ������� �� ��������� �������� ��������, ������� ������ � �������. 
create or replace trigger updateOperations
before update on operations
for each row
begin
    if :new.balance_id is not NULL
    then raise_application_error(-20000,'You cant update this operation');
    end if;
end;

update operations set credit = credit+5 where id=41;

--�������� �� ��������: 
--+������� �������, ������� ��� �������� ��������, ���� ��� ������ � ������� � ���������� ����������. 
create or replace trigger deleteOp
before delete on operations
for each row
begin
    if :old.balance_id is not null
    then 
    raise_application_error(-20000,'You cant delete this operation');
    end if;
end;
delete operations where id=1;

--������� 
--��. ��. ��� ������� ����������� ����������� ���������� ������� �� �������: 
--�������� ����������� ��������� �������� �������� �������, ��������� ������������� ������ 
--� ��� ������� ��� ������� (������, ������, �������). 
--����������� ������ ��������� ������ ������� �������, ���������� �������������� ������ � ������� ��������, 
--������������� ���� ������ �������� �������������� ���� �������. 
--������������ ��������� �������� ����������. �������� ���������� � ��������� ���������� 
--������������ ����� ���������� ������� ��������� ���� �������� ��������, �������� � �������� ��������. 
--�����, ���������� ������ �� �������, �������������� ������� �������� � ���������. 
--� ���� ������� ��� ������ ������ ������������ ������� �������� �� ������ � �������� ���. 
--Hints: CURSOR,  %NOTFOUND, FETCH 
CREATE OR REPLACE TYPE NUMBER_TABLE_TYPE AS TABLE OF NUMBER;

create or replace procedure pr
(begintime timestamp, endtime timestamp, arts TABLE) is
    
    sumfin NUMBER;--����� ���. �������
    artid operations.article_id%TYPE;
    artper NUMBER;
    
    cursor getpercent is
    
    select article_id, sum(credit) from operations 
    where balance_id in(select id from balance where create_date between begintime and endtime) 
    and article_id in (select * from arts)
    group by article_id;

begin
    --������� ����� ���. �������
    select sum(credit) into sumfin from balance
    where create_date between begintime and endtime;
    
    --������������ ������� ��������    
    open getpercent;
    loop
    fetch getpercent into artid, artper; 
    exit when getpercent%notfound;
    DBMS_OUTPUT.enable;
    --DBMS_OUTPUT.put_line('sumfin: '|| sumfin || '  artper: ' || artper);
    DBMS_OUTPUT.put_line('article id: '|| artid || '  percent: ' || artper*100/sumfin);
    end loop;
    close getpercent;
end pr;
 
 exec pr('07.04.20','10.04.20',(select id from articles));
    