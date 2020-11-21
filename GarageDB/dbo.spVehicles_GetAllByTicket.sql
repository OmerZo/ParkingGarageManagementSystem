CREATE PROCEDURE [dbo].spVehicles_GetAllByTicket
	@First int,
	@Last int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Name, LotNumber 
	FROM Vehicles
	WHERE LotNumber BETWEEN @First AND @Last;
END
RETURN 0
