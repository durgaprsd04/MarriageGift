use MarriageGift;
select * from [EVEnts]

select * from OCCASSION_TYPE

insert into OCCASSION_TYPE (occassion_type)
values('Marriage'),
('BirthDay'),
('HouseWarming');


select occassion_type_id, occassion_type from OCCASSION_TYPE

select * from OCCASSION
select NEWID()

select gift_id, gift_name from Gift

select * from Gift

select * from  GIFT_TYPE

Values(newid(), 4,'Trip to Thailand','30000')
,(newid(), 2,'TV Stand','8000')
,(newid(), 3,'Plates','1000')
,(newid(), 1,'Bluetooth Speaker','3000')

select * from gift_type
insert into gift_type values('Electronics'),
('Furniture'),
('Crockery'),
('Trips')


sp_Tables '%gift%'


sp_help GIFT_EXPECTED


select * from GIFT_EXPECTED

select * from [occassion]

select * from [Events]

select * from gift

select * from customer

insert into customer
values(newid(), 'Jeff','pass@word'),
(newid(), 'Larry','larry@word')