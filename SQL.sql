create database BroadbandDevelopment

delete from bill

create database broadbandproject

use BroadbandDevelopment

delete from feedback

select * from Feedback
select * from TarrifPlan
select * from admin
select * from Account, TarrifPlan where Account.tarrif_plan_id = TarrifPlan.tarrif_plan_id
select * from Customer
delete from admin where email = 'user1@example.com'
delete from admin where email = 'admin@broadband.com'
select * from customer where email = 'aniruddhabhatk@gmail.com'
select * from bill
select * from customer where customer_id = 'b6c7f1f2-b620-4718-8be1-06eb3038571d'

update account set tarrif_plan_id = null where account_id = 'c8581fe0-d745-4f9f-9940-b6a045c3e83f'
delete from customer where customer_id='4b6ea5e9-0c19-441e-a315-9036441f0207'
delete from Feedback;

insert into bill values (
    'b09',
    100,
    '2021-12-31',
    '2021-12-22',
    null,
    null,
    null,
    'c8581fe0-d745-4f9f-9940-b6a045c3e83f'
)

update feedback set rating='4' where feedback_id='3f15f665-4502-4f56-9b58-ed14279e8697'
 
select * from INFORMATION_SCHEMA.VIEWS
select * from feedback
use new
create database new

drop database new

select count(*), t.tarrif_plan_id, t.amount, t.DESCRIPTION, t.admin_id
 from TarrifPlan t, Account a
where t.tarrif_plan_id = a.tarrif_plan_id AND
a.tarrif_plan_id is not NULL
group by t.tarrif_plan_id, t.amount, t.DESCRIPTION, t.admin_id

having count(a.account_id) >= all(
    select count(a1.account_id)
 from TarrifPlan t1, Account a1
where t1.tarrif_plan_id = a1.tarrif_plan_id AND
a1.tarrif_plan_id is not NULL
group by t1.tarrif_plan_id
)


-- create procedure 
-- mostusedplan  as 
-- begin
-- select t.tarrif_plan_id, t.amount, t.DESCRIPTION, t.admin_id
--  from TarrifPlan t, Account a
-- where t.tarrif_plan_id = a.tarrif_plan_id AND
-- a.tarrif_plan_id is not NULL
-- group by t.tarrif_plan_id, t.amount, t.DESCRIPTION, t.admin_id

-- having count(a.account_id) >= all(
--     select count(a1.account_id)
--  from TarrifPlan t1, Account a1
-- where t1.tarrif_plan_id = a1.tarrif_plan_id AND
-- a1.tarrif_plan_id is not NULL
-- group by t1.tarrif_plan_id
-- )
-- end

-- exec mostusedplan

-- drop procedure mostusedplan

update bill set payment_mode=null, payment_date = null,customer_id=null where bill_id='7e3b6eff-bfaa-433b-bc32-a696b87fe636'

create trigger accountStatus on Account instead of update as update Account set Account.status = 'active', Account.tarrif_plan_id=inserted.tarrif_plan_id from inserted where Account.account_id = inserted.account_id and inserted.tarrif_plan_id is not null

drop trigger accountStatus1

select * from plan

update account set tarrif_plan_id = '1ec7c0ec-e731-4238-b868-c17813b4b85a' where account_id='f03188d9-5264-4e1f-95a1-0ea6decfe027' 

update account set status='idle' where account_id='22b7fc79-ff11-4c02-9f23-3f2a60b9ec28'