USE Airport

SELECT f.Id AS [FlightId], SUM(t.Price) AS [Price]
FROM Flights AS f
JOIN Tickets AS t
ON f.Id = t.FlightId
GROUP BY f.Id
ORDER BY SUM(t.Price) DESC, f.Id;