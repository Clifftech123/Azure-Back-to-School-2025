var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Azure Back to School API"));
app.UseHttpsRedirection();

// Health check endpoint
app.MapGet("/health", () => new { 
    Status = "Healthy", 
    Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production",
    Timestamp = DateTime.UtcNow
})
.WithName("HealthCheck")
.WithTags("System");

// Get all courses available for Back to School
app.MapGet("/api/courses", () => new[] {
    new { Id = 1, Name = "Azure Fundamentals", Code = "AZ-900", Duration = "40 hours", Level = "Beginner" },
    new { Id = 2, Name = "Azure Developer Associate", Code = "AZ-204", Duration = "60 hours", Level = "Intermediate" },
    new { Id = 3, Name = "Azure Solutions Architect", Code = "AZ-305", Duration = "80 hours", Level = "Advanced" },
    new { Id = 4, Name = ".NET Development with Azure", Code = "DEV-101", Duration = "50 hours", Level = "Intermediate" }
})
.WithName("GetCourses")
.WithTags("Education");

// // Get enrolled students
// app.MapGet("/api/students", () => new[] {
//     new { Id = 1, Name = "Sarah Chen", Email = "sarah.chen@school.edu", Course = "Azure Fundamentals", EnrollmentDate = new DateTime(2025, 8, 15) },
//     new { Id = 2, Name = "Miguel Rodriguez", Email = "miguel.r@school.edu", Course = "Azure Developer Associate", EnrollmentDate = new DateTime(2025, 8, 18) },
//     new { Id = 3, Name = "Emma Thompson", Email = "emma.t@school.edu", Course = ".NET Development with Azure", EnrollmentDate = new DateTime(2025, 8, 20) }
// })
// .WithName("GetStudents")
// .WithTags("Education");

// Get Back to School statistics
// app.MapGet("/api/stats", () => new {
//     TotalStudents = 156,
//     TotalCourses = 12,
//     NewEnrollments = 45,
//     CompletionRate = 87.5,
//     Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production",
//     LastUpdated = DateTime.UtcNow
// })
// .WithName("GetStatistics")
// .WithTags("Analytics");

app.Run();