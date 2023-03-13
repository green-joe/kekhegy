DELIMITER //
CREATE TRIGGER `delete_reservations` BEFORE DELETE ON `szobak`
 FOR EACH ROW BEGIN
    DELETE FROM foglalasok WHERE szobaId = OLD.Id;
END;
//