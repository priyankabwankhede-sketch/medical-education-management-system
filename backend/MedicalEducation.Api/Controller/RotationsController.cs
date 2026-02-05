using MedicalEducation.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace MedicalEducation.Api.Controllers
{
[Route("api/[controller]")]
[ApiController]
public class RotationsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public RotationsController(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }


        // ========================
        // GET ALL ROTATIONS
        // ========================
[HttpGet]
public IActionResult GetRotations()
{
    var rotations = new List<object>();

    using (var conn = new SqlConnection(_connectionString))
    {
        conn.Open();
        var cmd = new SqlCommand(@"
            SELECT 
                r.RotationId, 
                t.TraineeId, 
                u.Username, 
                p.ProgramName, 
                r.StartDate, 
                r.EndDate
            FROM Rotations r
            JOIN Trainees t ON r.TraineeId = t.TraineeId
            JOIN Users u ON t.UserId = u.UserId
            JOIN Programs p ON t.ProgramId = p.ProgramId", conn);

        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            rotations.Add(new
            {
                RotationId = reader.GetInt32(0),
                TraineeId = reader.GetInt32(1),
                Username = reader.GetString(2),
                ProgramName = reader.GetString(3),
                StartDate = reader.GetDateTime(4),
                EndDate = reader.GetDateTime(5)
            });
        }
    }

    return Ok(rotations);
}


[HttpPost]
public IActionResult CreateRotation([FromBody] RotationCreateDto dto)
{
    using (SqlConnection conn = new SqlConnection(_connectionString))
    {
        conn.Open();
        SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Rotations (TraineeId, StartDate, EndDate)
            VALUES (@TraineeId, @StartDate, @EndDate)", conn);

        cmd.Parameters.AddWithValue("@TraineeId", dto.TraineeId);
        cmd.Parameters.AddWithValue("@StartDate", dto.StartDate);
        cmd.Parameters.AddWithValue("@EndDate", dto.EndDate);

        cmd.ExecuteNonQuery();
    }

    return Ok(new { message = "Rotation created successfully" });
}


    }
}
