DROP PROCEDURE AddUser;
delimiter //
CREATE PROCEDURE AddUser(_id INT, _firstname VARCHAR(40), _lastname VARCHAR(40), _city VARCHAR(40), _state VARCHAR(40))
BEGIN
    INSERT INTO student(id, firstname, lastname, city, state) 
    VALUES (_id, _firstname, _lastname, _city, _state);
END
//

CALL AddUser(5, 'hoang', 'cong hung', 'bac giang', 'itg');

