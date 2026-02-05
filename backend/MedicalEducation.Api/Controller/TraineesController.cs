using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MedicalEducation.Api.Dtos;

namespace MedicalEducation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraineesController : ControllerBase
    {
        private readonly string _connectionString;

        public TraineesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // ========================
        // GET ALL TRAINEES
        // ========================
        [HttpGet]
        public IActionResult GetTrainees()
        {
            var trainees = new List<object>();

            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand(@"
                SELECT t.TraineeId, u.Username, p.ProgramName
                FROM Trainees t
                JOIN Users u ON t.UserId = u.UserId
                JOIN Programs p ON t.ProgramId = p.ProgramId", conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                trainees.Add(new
                {
                    TraineeId = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    ProgramName = reader.GetString(2)
                });
            }

            return Ok(trainees);
        }

        // ========================
        // CREATE TRAINEE
        // ========================
        [HttpPost]
        public IActionResult CreateTrainee([FromBody] CreateTraineeDto dto)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            // Insert User
            var userCmd = new SqlCommand(
                @"INSERT INTO Users (Username, PasswordHash, Role)
                  OUTPUT INSERTED.UserId
                  VALUES (@u, @p, 'Trainee')", conn);

            userCmd.Parameters.AddWithValue("@u", dto.Username);
            userCmd.Parameters.AddWithValue("@p", "TEMP");

            int userId = (int)userCmd.ExecuteScalar();

            // Insert Trainee
            var traineeCmd = new SqlCommand(
                "INSERT INTO Trainees (UserId, ProgramId) VALUES (@uid, @pid)", conn);

            traineeCmd.Parameters.AddWithValue("@uid", userId);
            traineeCmd.Parameters.AddWithValue("@pid", dto.ProgramId);

            traineeCmd.ExecuteNonQuery();

            return Ok(new { message = "Trainee created successfully" });
        }

        // ========================
        // UPDATE TRAINEE
        // ========================
        [HttpPut("{traineeId}")]
        public IActionResult UpdateTrainee(int traineeId, [FromBody] CreateTraineeDto dto)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand(@"
                UPDATE Trainees
                SET ProgramId = @ProgramId
                WHERE TraineeId = @TraineeId", conn);

            cmd.Parameters.AddWithValue("@ProgramId", dto.ProgramId);
            cmd.Parameters.AddWithValue("@TraineeId", traineeId);

            int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
                return Ok(new { message = "Trainee updated successfully" });

            return NotFound(new { message = "Trainee not found" });
        }
// ========================
// DELETE TRAINEE
// ========================
[HttpDelete("{traineeId}")]
public IActionResult DeleteTrainee(int traineeId)
{
    using var conn = new SqlConnection(_connectionString);
    conn.Open();

    // Delete trainee first (FK constraint safe)
    var traineeCmd = new SqlCommand(
        "DELETE FROM Trainees WHERE TraineeId = @id", conn);

    traineeCmd.Parameters.AddWithValue("@id", traineeId);
    int rows = traineeCmd.ExecuteNonQuery();

    if (rows == 0)
        return NotFound(new { message = "Trainee not found" });

    return Ok(new { message = "Trainee deleted successfully" });
}

    }
}
