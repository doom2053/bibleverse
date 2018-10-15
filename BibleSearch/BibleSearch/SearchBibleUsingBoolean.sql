DELIMITER //
CREATE PROCEDURE SearchBibleUsingBoolean(
   IN searchTerm VARCHAR(255),
   IN startIndex INT
)
BEGIN
   SELECT bi.id, bo.name as book, bi.cap, bi.verse, bi.line
   FROM bible AS bi
   INNER JOIN books AS bo ON bi.book = bo.abbr
   WHERE MATCH (line)
   AGAINST (searchTerm IN BOOLEAN MODE)
   LIMIT startIndex,20;
END //
DELIMITER ;
