--create database MarriageGift;
CREATE TABLE CUSTOMER
(
 customer_id varchar(200) PRIMARY KEY,
 username varchar(200) UNIQUE,
 [password] varchar(200)
)
----occasion type
CREATE TABLE OCCASSION_TYPE
(
occassion_type_id int IDENTITY PRIMARY KEY,
occassion_type varchar(100)
);
--occasion
CREATE TABLE OCCASSION
(
occasion_id varchar(200) primary key,
person1 varchar(200),
person2 varchar(200),
occassion_type_id INT,
FOREIGN KEY (occassion_type_id) REFERENCES OCCASSION_TYPE(occassion_type_id)
);
--events
CREATE TABLE [EVENTS]
(
occassion_id varchar(200),
 event_id varchar(200) ,
 event_venue varchar(200),
 event_date varchar(200),
 customer_id varchar(200),
 is_canceled bit,
 FOREIGN KEY (customer_id) REFERENCES CUSTOMER(customer_id),
 FOREIGN KEY (occassion_id) REFERENCES OCCASSION(occasion_id)
)
ALTER TABLE [EVENTS] ALTER COLUMN event_id  varchar(200) NOT NULL
ALTER TABLE [EVENTS] ADD   PRIMARY KEY( Event_id)

--giftype
CREATE TABLE GIFT_TYPE
(
gift_type_id int IDENTITY PRIMARY KEY,
gift_type VARCHAR(200)
)
---gift
CREATE TABLE GIFT
(
gift_id varchar(200) PRIMARY KEY,
gift_type_id INT,
gift_name varchar(200) ,
gift_price float,
FOREIGN KEY (gift_type_id) REFERENCES GIFT_TYPE(gift_type_id)
)
---gift
CREATE TABLE PRESENTABLE_GIFT
(
presenter_id varchar(200),
gift_id varchar(200),
FOREIGN KEY (gift_id) REFERENCES GIFT(gift_id),
FOREIGN KEY (presenter_id) REFERENCES CUSTOMER(customer_id)
)

--gift expected;
CREATE TABLE GIFT_EXPECTED
(
event_id varchar(200),
gift_id varchar(200),
FOREIGN KEY (gift_id) REFERENCES GIFT(gift_id),
FOREIGN KEY (event_id) REFERENCES [EVENTS](event_id)
)
--gift recieved
CREATE TABLE GIFT_RECIEVED
(
event_id varchar(200),
gift_id varchar(200),
FOREIGN KEY (gift_id) REFERENCES GIFT(gift_id),
FOREIGN KEY (event_id) REFERENCES [EVENTS](event_id)
)
--invitation
CREATE TABLE INVITATION
(
invitation_id varchar(200) PRIMARY KEY,
event_id varchar(200),
cust_id_of_invitee varchar(200)
FOREIGN KEY (event_id) REFERENCES [EVENTS](event_id),
FOREIGN KEY (cust_id_of_invitee) REFERENCES CUSTOMER(customer_id)
)